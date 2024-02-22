using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        public static List<DateTime> GetAllNgay()
        {
            //select all distinct date from BieuDoGia
            using (var dbContext = new ChungKhoanEntities())
            {
                var list = dbContext.BieuDoGias
                    .Select(data => data.Ngay)
                    .Distinct()
                    .ToList();
                return list;
            }           
            
        }
        public static int Insert(BieuDoGia obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                //kiểm tra xem có giá trị nào chưa
                //nếu chưa có mới thêm
                BieuDoGia item = db.BieuDoGias.Where (t => t.MaChungKhoan == obj.MaChungKhoan && t.Ngay == obj.Ngay).FirstOrDefault();
                if (item ==null)
                {
                    obj = db.BieuDoGias.Add(obj);
                    db.SaveChanges();
                }
                else
                {
                    item.GiaDongCua = obj.GiaDongCua;
                    db.SaveChanges();
                }               
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
        public static List<sp_ThongKe2Ngay_Result> ThongKe2(DateTime ngay1, DateTime ngay2)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();
            var result = db.BieuDoGias
                        .Where(g => g.Ngay == ngay1)
                        .Select(g => new sp_ThongKe2Ngay_Result
                        {
                            MaChungKhoan = g.MaChungKhoan,
                            Gia1 = g.GiaDongCua,
                            Gia2 = 0,
                            DoLech = g.GiaDongCua
                        })
                        .Union(db.BieuDoGias
                            .Where(g => g.Ngay == ngay2)
                            .Select(g => new sp_ThongKe2Ngay_Result
                            {
                                MaChungKhoan = g.MaChungKhoan,
                                Gia1 = 0,
                                Gia2 = g.GiaDongCua,
                                DoLech = -g.GiaDongCua
                            }))
                        .GroupBy(temp => temp.MaChungKhoan)
                        .Select(group => new sp_ThongKe2Ngay_Result
                        {
                            MaChungKhoan = group.Key,
                            Gia1 = group.Sum(g => g.Gia1),
                            Gia2 = group.Sum(g => g.Gia2),
                            DoLech = group.Sum(g => g.DoLech)
                        })
                        .Where(x => x.Gia1 != 0 && x.Gia2 != 0)
                        .OrderBy(x=> x.MaChungKhoan)
                        .ToList();
            return result;
        }
        //public static List<sp_ThongKe3Ngay_Result> ThongKe2(DateTime ngay1, DateTime ngay2, DateTime ngay3)
        //{
        //    ChungKhoanEntities db = new ChungKhoanEntities();
        //    var result = db.BieuDoGias
        //                    .Where(g => g.Ngay == ngay1)
        //                    .Select(g => new sp_ThongKe3Ngay_Result
        //                    {
        //                        MaChungKhoan = g.MaChungKhoan,
        //                        Gia1 = g.GiaDongCua,
        //                        Gia2 = 0,
        //                        Gia3 = 0,
        //                        DoLech12 = g.GiaDongCua,
        //                        DoLech13 = g.GiaDongCua,
        //                        DoLech23 = 0
        //                    })
        //                    .Union(db.BieuDoGias
        //                        .Where(g => g.Ngay == ngay2)
        //                        .Select(g => new sp_ThongKe3Ngay_Result
        //                        {
        //                            MaChungKhoan = g.MaChungKhoan,
        //                            Gia1 = 0,
        //                            Gia2 = g.GiaDongCua,
        //                            Gia3 = 0,
        //                            DoLech12 = -g.GiaDongCua,
        //                            DoLech13 = 0,
        //                            DoLech23 = g.GiaDongCua,
        //                        }))
        //                    .Union(db.BieuDoGias
        //                        .Where(g => g.Ngay == ngay3)
        //                        .Select(g => new sp_ThongKe3Ngay_Result
        //                        {
        //                            MaChungKhoan = g.MaChungKhoan,
        //                            Gia1 = 0,
        //                            Gia2 = 0,
        //                            Gia3 = g.GiaDongCua,
        //                            DoLech12 = 0,
        //                            DoLech13 = -g.GiaDongCua,
        //                            DoLech23 = -g.GiaDongCua,
        //                        }))
        //                    .GroupBy(temp => temp.MaChungKhoan)
        //                    .Select(group => new sp_ThongKe3Ngay_Result
        //                    {
        //                        MaChungKhoan = group.Key,
        //                        Gia1 = group.Sum(g => g.Gia1),
        //                        Gia2 = group.Sum(g => g.Gia2),
        //                        Gia3 = group.Sum(g => g.Gia3),
        //                        DoLech12 = group.Sum(g => g.DoLech12),
        //                        DoLech13 = group.Sum(g => g.DoLech13),
        //                        DoLech23 = group.Sum(g => g.DoLech23)
        //                    })
        //                    .Where(x => x.Gia1 != 0 && x.Gia2 != 0 && x.Gia3 != 0)
        //                    .OrderBy(x => x.MaChungKhoan)
        //                    .ToList();
        //    return result;
        //}
        public static List<sp_ThongKe3Ngay_Result> ThongKe3(DateTime ngay1, DateTime ngay2, DateTime ngay3)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                var statisticalTable = dbContext.BieuDoGias
                    .Where(data => data.Ngay == ngay1 || data.Ngay == ngay2 || data.Ngay == ngay3)
                    .GroupBy(data => data.MaChungKhoan)
                    .Select(group => new sp_ThongKe3Ngay_Result
                    {
                        MaChungKhoan = group.Key,
                        //get Gia1 if data.Ngay == ngay1 else 0
                        Gia1 = group.Sum(data => data.Ngay == ngay1 ? data.GiaDongCua : 0),
                        Gia2 = group.Sum(data => data.Ngay == ngay2 ? data.GiaDongCua : 0),
                        Gia3 = group.Sum(data => data.Ngay == ngay3 ? data.GiaDongCua : 0),
                        DoLech12 = group.Sum(data => data.Ngay == ngay1 ? data.GiaDongCua : 0) - group.Sum(data => data.Ngay == ngay2 ? data.GiaDongCua : 0),
                        DoLech13 = group.Sum(data => data.Ngay == ngay1 ? data.GiaDongCua : 0) - group.Sum(data => data.Ngay == ngay3 ? data.GiaDongCua : 0),
                        DoLech23 = group.Sum(data => data.Ngay == ngay2 ? data.GiaDongCua : 0) - group.Sum(data => data.Ngay == ngay3 ? data.GiaDongCua : 0)
                    })
                    .Where(x => x.Gia1 != 0 && x.Gia2 != 0 && x.Gia3 != 0)
                    .ToList();

                return statisticalTable;
            }
        }
        //create the same method for 4 days
        public static List<sp_ThongKe4Ngay_Result> ThongKe4(DateTime ngay1, DateTime ngay2, DateTime ngay3, DateTime ngay4)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                var statisticalTable = dbContext.BieuDoGias
                    .Where(data => data.Ngay == ngay1 || data.Ngay == ngay2 || data.Ngay == ngay3 || data.Ngay == ngay4)
                    .GroupBy(data => data.MaChungKhoan)
                    .Select(group => new sp_ThongKe4Ngay_Result
                    {
                        MaChungKhoan = group.Key,
                        //get Gia1 if data.Ngay == ngay1 else 0
                        Gia1 = group.Sum(data => data.Ngay == ngay1 ? data.GiaDongCua : 0),
                        Gia2 = group.Sum(data => data.Ngay == ngay2 ? data.GiaDongCua : 0),
                        Gia3 = group.Sum(data => data.Ngay == ngay3 ? data.GiaDongCua : 0),
                        Gia4 = group.Sum(data => data.Ngay == ngay4 ? data.GiaDongCua : 0)                        
                    })
                    .Where(x => x.Gia1 != 0 && x.Gia2 != 0 && x.Gia3 != 0 && x.Gia4 != 0)
                    .ToList();
                return statisticalTable;
            }
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
                                    where b.Ngay == ngayXoa
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
        public static void DeleteFromTo(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    List<BieuDoGia> objs = (from b in db.BieuDoGias
                                            where b.Ngay>= tuNgay && b.Ngay <= denNgay
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
        public static void DeleteAll()
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    List<BieuDoGia> objs = (from b in db.BieuDoGias                                            
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
