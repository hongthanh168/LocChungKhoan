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
        public static List<BieuDoKhoiLuong> GetAll(DateTime tuNgay, DateTime denNgay)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();
            var list = (from t in db.BieuDoKhoiLuongs
                        where t.Ngay >= tuNgay && t.Ngay <= denNgay
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
                    item.GiaMoCua = obj.GiaMoCua;
                    item.KhoiLuong = obj.KhoiLuong;
                    item.GiaCaoNhat = obj.GiaCaoNhat;
                    item.GiaThapNhat = obj.GiaThapNhat;
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
        public static void ChuyenDuLieuSangGia(DateTime tuNgay, DateTime denNgay)
        {            
            //lấy toàn bộ dữ liệu của BieuDoKhoiLuong
            List<BieuDoKhoiLuong> list = BieuDoKhoiLuongController.GetAll(tuNgay, denNgay);
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
        public static List<ThongKeKhoiLuong> ThongKeTuan(DateTime tuan1_start, DateTime tuan1_end, DateTime tuan2_start, DateTime tuan2_end, DateTime tuan3_start, DateTime tuan3_end)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                DateTime tuan11, tuan12, tuan21, tuan22, tuan31, tuan32;
                //tìm ra ngày bắt đầu và kết thúc của tuần 1, tuần 2, tuần 3, tuần 4
                //ngày bắt đầu của tuần là ngày nhỏ nhất trong khoảng thời gian từ tuần 1_start đến tuần 1_end
                //select min ngay from BieuDoKhoiLuong where ngay >= tuan1_start and ngay <= tuan1_end
                tuan11 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan1_start && x.Ngay <= tuan1_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan1_start)
                    .Min();
                tuan12 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan1_start && x.Ngay <= tuan1_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan1_end)
                    .Max();
                tuan21 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan2_start && x.Ngay <= tuan2_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan2_start)
                    .Min();
                tuan22 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan2_start && x.Ngay <= tuan2_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan2_end)
                    .Max();
                tuan31 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan3_start && x.Ngay <= tuan3_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan3_start)
                    .Min();
                tuan32 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan3_start && x.Ngay <= tuan3_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan3_end)
                    .Max();  
                
                var results = (from t1 in dbContext.BieuDoKhoiLuongs
                               //where MaChungKhoan in DMQuanTam
                               where t1.Ngay >= tuan1_start && t1.Ngay <= tuan1_end
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t1.MaChungKhoan,
                                   GiaDongCua1 = (t1.Ngay == tuan12) ? t1.GiaDongCua : 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = (t1.Ngay == tuan11) ? t1.GiaMoCua : 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = t1.KhoiLuong,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0,
                                   GiaCaoNhat1 = t1.GiaCaoNhat,
                                   GiaCaoNhat2 = decimal.MinValue ,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaThapNhat1 = t1.GiaThapNhat,
                                   GiaThapNhat2 = decimal.MaxValue ,
                                   GiaThapNhat3 = decimal.MaxValue
                               })
                               .Union(
                               from t2 in dbContext.BieuDoKhoiLuongs
                               where t2.Ngay >= tuan2_start && t2.Ngay <= tuan2_end
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t2.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = (t2.Ngay == tuan22) ? t2.GiaDongCua : 0,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = (t2.Ngay == tuan21) ? t2.GiaMoCua : 0,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = t2.KhoiLuong,
                                   KhoiLuong3 = 0,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = t2.GiaCaoNhat,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaThapNhat1 = decimal.MaxValue,    
                                   GiaThapNhat2 = t2.GiaThapNhat,
                                   GiaThapNhat3 = decimal.MaxValue 
                               })
                               .Union(
                               from t3 in dbContext.BieuDoKhoiLuongs
                               where t3.Ngay >= tuan3_start && t3.Ngay <= tuan3_end
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t3.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = (t3.Ngay == tuan32) ? t3.GiaDongCua : 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = (t3.Ngay == tuan31) ? t3.GiaMoCua : 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = t3.KhoiLuong,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = t3.GiaCaoNhat,
                                   GiaThapNhat1 = decimal.MaxValue ,
                                   GiaThapNhat2 = decimal.MaxValue,
                                   GiaThapNhat3 = t3.GiaThapNhat
                               })  
                               .Join (dbContext.DMQuanTams, t => t.MaChungKhoan, d => d.MaChungKhoan, (t, d) => t)
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
                                   KhoiLuong3 = g.Sum(x => x.KhoiLuong3),
                                   GiaCaoNhat1 = g.Max(x => x.GiaCaoNhat1),
                                   GiaCaoNhat2 = g.Max(x => x.GiaCaoNhat2),
                                   GiaCaoNhat3 = g.Max(x => x.GiaCaoNhat3),
                                   GiaThapNhat1 = g.Min(x => x.GiaThapNhat1),
                                   GiaThapNhat2 = g.Min(x => x.GiaThapNhat2),
                                   GiaThapNhat3 = g.Min(x => x.GiaThapNhat3)
                               })
                               .Where(x => x.GiaDongCua1 !=0 && x.GiaDongCua2 !=0 && x.GiaDongCua3 !=0 )
                               .ToList();

                return results;
            }
        }
        //thống kê theo 3 ngày đầu tuần
        public static List<ThongKeKhoiLuong> ThongKe3Ngay(DateTime ngay1, DateTime ngay2, DateTime ngay3)
        {
            using (var dbContext = new ChungKhoanEntities())
            {                
                var results = (from t1 in dbContext.BieuDoKhoiLuongs
                                   //where MaChungKhoan in DMQuanTam
                               where t1.Ngay == ngay1
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t1.MaChungKhoan,
                                   GiaDongCua1 =  t1.GiaDongCua,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = t1.GiaMoCua,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = t1.KhoiLuong,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0,
                                   GiaCaoNhat1 = t1.GiaCaoNhat,
                                   GiaCaoNhat2 = 0,
                                   GiaCaoNhat3 = 0,
                                   GiaThapNhat1 = t1.GiaThapNhat,
                                   GiaThapNhat2 = 0,
                                   GiaThapNhat3 = 0
                               })
                               .Union(
                               from t2 in dbContext.BieuDoKhoiLuongs
                               where t2.Ngay == ngay2
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t2.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = t2.GiaDongCua,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = t2.GiaMoCua,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = t2.KhoiLuong,
                                   KhoiLuong3 = 0,
                                   GiaCaoNhat1 = 0,
                                   GiaCaoNhat2 = t2.GiaCaoNhat,
                                   GiaCaoNhat3 = 0,
                                   GiaThapNhat1 = 0,
                                   GiaThapNhat2 = t2.GiaThapNhat,
                                   GiaThapNhat3 = 0
                               })
                               .Union(
                               from t3 in dbContext.BieuDoKhoiLuongs
                               where t3.Ngay == ngay3
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t3.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = t3.GiaDongCua,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = t3.GiaMoCua,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = t3.KhoiLuong,
                                   GiaCaoNhat1 = 0,
                                   GiaCaoNhat2 = 0,
                                   GiaCaoNhat3 = t3.GiaCaoNhat,
                                   GiaThapNhat1 = 0,
                                   GiaThapNhat2 = 0,
                                   GiaThapNhat3 = t3.GiaThapNhat
                               })
                               .Join(dbContext.DMQuanTams, t => t.MaChungKhoan, d => d.MaChungKhoan, (t, d) => t)
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
                                   KhoiLuong3 = g.Sum(x => x.KhoiLuong3),
                                   GiaCaoNhat1 = g.Sum(x => x.GiaCaoNhat1),
                                   GiaCaoNhat2 = g.Sum(x => x.GiaCaoNhat2),
                                   GiaCaoNhat3 = g.Sum(x => x.GiaCaoNhat3),
                                   GiaThapNhat1 = g.Sum(x => x.GiaThapNhat1),
                                   GiaThapNhat2 = g.Sum(x => x.GiaThapNhat2),
                                   GiaThapNhat3 = g.Sum(x => x.GiaThapNhat3)
                               })
                               .Where(x => x.GiaDongCua1 != 0 && x.GiaDongCua2 != 0 && x.GiaDongCua3 != 0)
                               .ToList();

                return results;
            }
        }

        //thống kê theo 4 tuần
        public static List<ThongKeKhoiLuong4Tuan> ThongKe4Tuan(DateTime tuan1_start, DateTime tuan1_end, DateTime tuan2_start, DateTime tuan2_end, DateTime tuan3_start, DateTime tuan3_end, DateTime tuan4_start, DateTime tuan4_end)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                DateTime tuan11, tuan12, tuan21, tuan22, tuan31, tuan32, tuan41, tuan42;
                //tìm ra ngày bắt đầu và kết thúc của tuần 1, tuần 2, tuần 3, tuần 4
                //ngày bắt đầu của tuần là ngày nhỏ nhất trong khoảng thời gian từ tuần 1_start đến tuần 1_end
                //select min ngay from BieuDoKhoiLuong where ngay >= tuan1_start and ngay <= tuan1_end
                tuan11 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan1_start && x.Ngay <= tuan1_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan1_start)
                    .Min();
                tuan12 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan1_start && x.Ngay <= tuan1_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan1_end)
                    .Max();
                tuan21 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan2_start && x.Ngay <= tuan2_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan2_start)
                    .Min();
                tuan22 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan2_start && x.Ngay <= tuan2_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan2_end)
                    .Max();
                tuan31 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan3_start && x.Ngay <= tuan3_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan3_start)
                    .Min();
                tuan32 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan3_start && x.Ngay <= tuan3_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan3_end)
                    .Max();
                tuan41 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan4_start && x.Ngay <= tuan4_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan4_start)
                    .Min();
                tuan42 = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= tuan4_start && x.Ngay <= tuan4_end)
                    .Select(x => x.Ngay)
                    .DefaultIfEmpty(tuan4_end)
                    .Max();

                var results = (from t1 in dbContext.BieuDoKhoiLuongs
                               where t1.Ngay >= tuan1_start && t1.Ngay <= tuan1_end
                               select new ThongKeKhoiLuong4Tuan
                               {
                                   MaChungKhoan = t1.MaChungKhoan,
                                   GiaDongCua1 = (t1.Ngay == tuan12) ? t1.GiaDongCua : 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaDongCua4 = 0,
                                   GiaMoCua1 = (t1.Ngay == tuan11) ? t1.GiaMoCua : 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   GiaMoCua4 = 0,
                                   KhoiLuong1 = t1.KhoiLuong,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0,
                                   KhoiLuong4 = 0
                               })
                               .Union(
                               from t2 in dbContext.BieuDoKhoiLuongs
                               where t2.Ngay >= tuan2_start && t2.Ngay <= tuan2_end
                               select new ThongKeKhoiLuong4Tuan
                               {
                                   MaChungKhoan = t2.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = (t2.Ngay == tuan22) ? t2.GiaDongCua : 0,
                                   GiaDongCua3 = 0,
                                   GiaDongCua4 = 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = (t2.Ngay == tuan21) ? t2.GiaMoCua : 0,
                                   GiaMoCua3 = 0,
                                   GiaMoCua4 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = t2.KhoiLuong,
                                   KhoiLuong3 = 0,
                                   KhoiLuong4 = 0
                               })
                               .Union(
                               from t3 in dbContext.BieuDoKhoiLuongs
                               where t3.Ngay >= tuan3_start && t3.Ngay <= tuan3_end
                               select new ThongKeKhoiLuong4Tuan
                               {
                                   MaChungKhoan = t3.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = (t3.Ngay == tuan32) ? t3.GiaDongCua : 0,
                                   GiaDongCua4 = 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = (t3.Ngay == tuan31) ? t3.GiaMoCua : 0,
                                    GiaMoCua4 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = t3.KhoiLuong,
                                   KhoiLuong4 = 0
                               })
                               .Union(
                               from t4 in dbContext.BieuDoKhoiLuongs
                                where t4.Ngay >= tuan4_start && t4.Ngay <= tuan4_end
                                select new ThongKeKhoiLuong4Tuan
                                {
                                    MaChungKhoan = t4.MaChungKhoan,
                                    GiaDongCua1 = 0,
                                    GiaDongCua2 = 0,
                                    GiaDongCua3 = 0,
                                    GiaDongCua4 = (t4.Ngay == tuan42) ? t4.GiaDongCua : 0,
                                    GiaMoCua1 = 0,
                                    GiaMoCua2 = 0,
                                    GiaMoCua3 = 0,
                                    GiaMoCua4 = (t4.Ngay == tuan41) ? t4.GiaMoCua : 0,
                                    KhoiLuong1 = 0,
                                    KhoiLuong2 = 0,
                                    KhoiLuong3 = 0,
                                    KhoiLuong4 = t4.KhoiLuong
                                })
                               .GroupBy(x => x.MaChungKhoan)
                               .Select(g => new ThongKeKhoiLuong4Tuan
                               {
                                   MaChungKhoan = g.Key,
                                   GiaDongCua1 = g.Sum(x => x.GiaDongCua1),
                                   GiaDongCua2 = g.Sum(x => x.GiaDongCua2),
                                   GiaDongCua3 = g.Sum(x => x.GiaDongCua3),
                                   GiaDongCua4 = g.Sum(x => x.GiaDongCua4),
                                   GiaMoCua1 = g.Sum(x => x.GiaMoCua1),
                                   GiaMoCua2 = g.Sum(x => x.GiaMoCua2),
                                   GiaMoCua3 = g.Sum(x => x.GiaMoCua3),
                                   GiaMoCua4 = g.Sum(x => x.GiaMoCua4),
                                   KhoiLuong1 = g.Sum(x => x.KhoiLuong1),
                                   KhoiLuong2 = g.Sum(x => x.KhoiLuong2),
                                   KhoiLuong3 = g.Sum(x => x.KhoiLuong3),
                                   KhoiLuong4 = g.Sum(x => x.KhoiLuong4)
                               })
                               .Where(x => x.GiaDongCua1 != 0 && x.GiaDongCua2 != 0 && x.GiaDongCua3 != 0 && x.GiaDongCua4!=0 && x.KhoiLuong1 != 0 && x.KhoiLuong2 != 0 && x.KhoiLuong3 != 0 && x.KhoiLuong4 !=0)
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
