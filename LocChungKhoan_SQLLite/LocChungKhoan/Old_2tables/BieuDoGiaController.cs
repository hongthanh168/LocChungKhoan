using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
{    
    public static class BieuDoGiaController
    {        
        public static List<BieuDoGia> GetAll(DateTime ngay)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();            
            var list = (from t in db.BieuDoGias
                        where t.Ngay == ngay
                        select t).ToList();
            return list;
        }           
        public static int Insert(BieuDoGia obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                obj = db.BieuDoGias.Add(obj);
                db.SaveChanges();
            }
            return obj.BieuDoGiaID;
        }
        public static int Update(BieuDoGia  obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                db.BieuDoGias .Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return obj.BieuDoGiaID;
        }
        public static List<sp_ThongKe2Ngay_Result> ThongKe1(DateTime ngay1, DateTime ngay2)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();
            var list = (from dm in db.DanhMucChungKhoans
                         join g1 in db.BieuDoGias.Where(g => g.Ngay == ngay1) on dm.ChungKhoanID equals g1.ChungKhoanID into gj1
                         from g1 in gj1.DefaultIfEmpty()
                         join g2 in db.BieuDoGias.Where(g => g.Ngay == ngay2) on dm.ChungKhoanID equals g2.ChungKhoanID into gj2
                         from g2 in gj2.DefaultIfEmpty()
                         select new
                         {
                             MaChungKhoan = dm.MaChungKhoan,
                             Gia1 = g1 != null ? g1.GiaDongCua : 0,
                             Gia2 = g2 != null ? g2.GiaDongCua : 0,
                             DoLech = (g1 != null ? g1.GiaDongCua : 0) - (g2 != null ? g2.GiaDongCua : 0)
                         })
                        .GroupBy(x => x.MaChungKhoan)
                        .Select(g => new sp_ThongKe2Ngay_Result 
                        {
                            MaChungKhoan = g.Key,
                            Gia1 = g.Sum(x => x.Gia1),
                            Gia2 = g.Sum(x => x.Gia2),
                            DoLech = Math.Abs(g.Sum(x => x.DoLech))
                        })
                        .ToList();
            return list;
        }
        public static List<sp_ThongKe3Ngay_Result> ThongKe2(DateTime ngay1, DateTime ngay2, DateTime ngay3)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();
            var query = (from dm in db.DanhMucChungKhoans
                         join g1 in db.BieuDoGias.Where(g => g.Ngay == ngay1) on dm.ChungKhoanID equals g1.ChungKhoanID into gj1
                         from g1 in gj1.DefaultIfEmpty()
                         join g2 in db.BieuDoGias.Where(g => g.Ngay == ngay2) on dm.ChungKhoanID equals g2.ChungKhoanID into gj2
                         from g2 in gj2.DefaultIfEmpty()
                         join g3 in db.BieuDoGias.Where(g => g.Ngay == ngay3) on dm.ChungKhoanID equals g3.ChungKhoanID into gj3
                         from g3 in gj3.DefaultIfEmpty()
                         select new
                         {
                             MaChungKhoan = dm.MaChungKhoan,
                             Gia1 = g1 != null ? g1.GiaDongCua : 0,
                             Gia2 = g2 != null ? g2.GiaDongCua : 0,
                             Gia3 = g3 != null ? g3.GiaDongCua : 0,
                             DoLech12 = (g1 != null ? g1.GiaDongCua : 0) - (g2 != null ? g2.GiaDongCua : 0),
                             DoLech13 = (g1 != null ? g1.GiaDongCua : 0) - (g3 != null ? g3.GiaDongCua : 0),
                             DoLech23 = (g2 != null ? g2.GiaDongCua : 0) - (g3 != null ? g3.GiaDongCua : 0)
                         })
                        .GroupBy(x => x.MaChungKhoan)
                        .Select(g => new sp_ThongKe3Ngay_Result
                        {
                            MaChungKhoan = g.Key,
                            Gia1 = g.Sum(x => x.Gia1),
                            Gia2 = g.Sum(x => x.Gia2),
                            Gia3 = g.Sum(x => x.Gia3),
                            DoLech12 = Math.Abs(g.Sum(x => x.DoLech12)),
                            DoLech13 = Math.Abs(g.Sum(x => x.DoLech13)),
                            DoLech23 = Math.Abs(g.Sum(x => x.DoLech23))
                        })
                        .ToList();
            return query;
        }
        public static void Delete(int id)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    BieuDoGia obj = (from b in db.BieuDoGias
                                   where b.BieuDoGiaID == id
                                   select b).FirstOrDefault();
                    db.BieuDoGias.Remove(obj);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception("Record does not exist in the database. " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static void DeleteTheoNgay(DateTime ngayXoa)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    List<BieuDoGia > objs = (from b in db.BieuDoGias
                                    where b.Ngay.Day == ngayXoa.Day &&
                                    b.Ngay.Month == ngayXoa.Month &&
                                    b.Ngay.Year == ngayXoa.Year
                                    select b).ToList ();
                    db.BieuDoGias.RemoveRange(objs);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception("Record does not exist in the database. " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static void DeleteTheoThang(int thang, int nam)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    List<BieuDoGia> objs = (from b in db.BieuDoGias
                                           where b.Ngay.Month == thang 
                                           && b.Ngay.Year == nam
                                           select b).ToList();
                    db.BieuDoGias.RemoveRange(objs);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception("Record does not exist in the database. " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
