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
        public static List<sp_ThongKe3Ngay_Result> ThongKe3(DateTime ngay1, DateTime ngay2, DateTime ngay3)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                var statisticalTable = dbContext.BieuDoKhoiLuongs
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
