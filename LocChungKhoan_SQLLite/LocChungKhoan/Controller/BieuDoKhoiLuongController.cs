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
    public static class BieuDoKhoiLuongController
    {        
        public static List<BieuDoKhoiLuong> GetAll(DateTime ngay)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();            
            var list = (from t in db.BieuDoKhoiLuongs
                        where t.Ngay == ngay
                        select t).ToList();
            return list;
        }
        public static List<BieuDoKhoiLuong> GetAll()
        {
            ChungKhoanEntities db = new ChungKhoanEntities();
            var list = (from t in db.BieuDoKhoiLuongs
                        select t).ToList();
            return list;
        }
        public static List<DateTime> GetAllNgay()
        {
            //select all distinct date from BieuDoKhoiLuong
            using (var dbContext = new ChungKhoanEntities())
            {
                var list = dbContext.BieuDoKhoiLuongs
                    .Select(data => data.Ngay)
                    .Distinct()
                    //order by date
                    .OrderBy(data => data)
                    .ToList();
                return list;
            }           
            
        }
        public static int Insert(BieuDoKhoiLuong obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                //kiểm tra xem có giá trị nào chưa
                //nếu chưa có mới thêm
                BieuDoKhoiLuong item = db.BieuDoKhoiLuongs.Where (t => t.MaChungKhoan == obj.MaChungKhoan && t.Ngay == obj.Ngay).FirstOrDefault();
                if (item ==null)
                {
                    obj = db.BieuDoKhoiLuongs.Add(obj);
                    db.SaveChanges();
                }
                else
                {
                    item.GiaDongCua = obj.GiaDongCua;
                    db.SaveChanges();
                }               
            }
            return obj.BieuDoKhoiLuongID;
        }
        public static int Update(BieuDoKhoiLuong  obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                db.BieuDoKhoiLuongs .Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return obj.BieuDoKhoiLuongID;
        }   
        public static void ChuyenDuLieuSangGia()
        {            
            //lấy toàn bộ dữ liệu của BieuDoKhoiLuong
            List<BieuDoKhoiLuong> list = BieuDoKhoiLuongController.GetAll();
            //chuyển dữ liệu từ BieuDoKhoiLuong sang BieuDoGia
            foreach (var item in list)
            {
                //check if BieuDoGia has this data
                BieuDoGia objTemp = BieuDoGiaController.GetItem(item.MaChungKhoan, item.Ngay);
                if (objTemp == null)
                {
                    BieuDoGia obj = new BieuDoGia();
                    obj.MaChungKhoan = item.MaChungKhoan;
                    obj.Ngay = item.Ngay;
                    obj.GiaDongCua = item.GiaDongCua;
                    BieuDoGiaController.Insert(obj);
                }
                
            }
        }
        public static List<ThongKeKhoiLuong> ThongKe(DateTime tuan1_start, DateTime tuan1_end, DateTime tuan2_start, DateTime tuan2_end, DateTime tuan3_start, DateTime tuan3_end)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                var results = (from t1 in dbContext .BieuDoKhoiLuongs
                               where t1.Ngay >= tuan1_start && t1.Ngay <= tuan1_end
                               select new ThongKeKhoiLuong 
                               {
                                   MaChungKhoan = t1.MaChungKhoan,
                                   GiaDongCua1 = (t1.Ngay == tuan1_end) ? t1.GiaDongCua : 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = (t1.Ngay == tuan1_end) ? t1.GiaMoCua  : 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = t1.KhoiLuong,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0
                               })
                               .Union(
                               from t2 in dbContext.BieuDoKhoiLuongs 
                               where t2.Ngay >= tuan2_start && t2.Ngay <= tuan2_end
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t2.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = (t2.Ngay == tuan2_end) ? t2.GiaDongCua : 0,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = (t2.Ngay == tuan2_end) ? t2.GiaMoCua : 0,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = t2.KhoiLuong,
                                   KhoiLuong3 = 0
                               })
                               .Union(
                               from t3 in dbContext.BieuDoKhoiLuongs
                               where t3.Ngay >= tuan3_start && t3.Ngay <= tuan3_end
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t3.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = (t3.Ngay == tuan3_end) ? t3.GiaDongCua : 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = (t3.Ngay == tuan3_end) ? t3.GiaMoCua : 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = t3.KhoiLuong
                               })
                               .GroupBy(x => x.MaChungKhoan)
                               .Select(g => new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = g.Key,
                                   GiaDongCua1 = g.Sum(x => x.GiaDongCua1),
                                   GiaDongCua2 = g.Sum(x => x.GiaDongCua2),
                                   GiaDongCua3 = g.Sum(x => x.GiaDongCua3),
                                   GiaMoCua1 = g.Sum(x => x.GiaMoCua1),
                                   GiaMoCua2 = g.Sum(x => x.GiaMoCua2),
                                   GiaMoCua3 = g.Sum(x => x.GiaMoCua3),
                                   KhoiLuong1 = g.Sum(x => x.KhoiLuong1),
                                   KhoiLuong2 = g.Sum(x => x.KhoiLuong2),
                                   KhoiLuong3 = g.Sum(x => x.KhoiLuong3)
                               })
                               .Where(x => x.GiaDongCua1 != 0 && x.GiaDongCua2 != 0 && x.GiaDongCua3 != 0)
                               .ToList();

                return results;
            }
        }        
        public static void Delete(int id)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    BieuDoKhoiLuong obj = (from b in db.BieuDoKhoiLuongs
                                   where b.BieuDoKhoiLuongID == id
                                   select b).FirstOrDefault();
                    db.BieuDoKhoiLuongs.Remove(obj);
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
                    List<BieuDoKhoiLuong > objs = (from b in db.BieuDoKhoiLuongs
                                    where b.Ngay == ngayXoa
                                    select b).ToList ();
                    db.BieuDoKhoiLuongs.RemoveRange(objs);
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
                    List<BieuDoKhoiLuong> objs = (from b in db.BieuDoKhoiLuongs
                                           where b.Ngay.Month == thang 
                                           && b.Ngay.Year == nam
                                           select b).ToList();
                    db.BieuDoKhoiLuongs.RemoveRange(objs);
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
                    List<BieuDoKhoiLuong> objs = (from b in db.BieuDoKhoiLuongs
                                            where b.Ngay>= tuNgay && b.Ngay <= denNgay
                                            select b).ToList();
                    db.BieuDoKhoiLuongs.RemoveRange(objs);
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
                    List<BieuDoKhoiLuong> objs = (from b in db.BieuDoKhoiLuongs                                            
                                            select b).ToList();
                    db.BieuDoKhoiLuongs.RemoveRange(objs);
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
