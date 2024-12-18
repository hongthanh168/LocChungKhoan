﻿using System;
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
        //lấy ra danh sách các mã chứng khoán quan tâm
        public static List<string> GetListMaCK()
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                var list = dbContext.DMQuanTams
                    .Select(x => x.MaChungKhoan)
                    .ToList();
                return list;
            }
        }
        //lấy mã chứng khoán theo tiêu chí riêng của Thanh
        public static List<string> GetListMaCKThanh(decimal priceMin = 10.0m, decimal volMin = 500000m)
        {            
            //lấy những cố phiếu có giá trung bình trong 5 ngày gần nhất 
            var ds =GetAllByDays (1, DateTime.Now);
            //get BieuDoKhoiLuong where MaChungKhoan in DMQuanTam
            var list = ds.Where(x => x.GiaDongCua >= priceMin && x.KhoiLuong>=volMin)
                .Select(x => x.MaChungKhoan)
                .Distinct()
                .ToList();
            return list;
        }
        public static decimal LayKhoiLuongTrungBinh(string maCK, DateTime ngayTinh, int days=100)
        {
            decimal avg = 0;
            //select all distinct date from BieuDoKhoiLuong
            using (var dbContext = new ChungKhoanEntities())
            {
                //lấy ra danh sách ngày
                var listNgay = GetAllNgay().Where(x => x <= ngayTinh).OrderByDescending(x => x).ToList().Take(days).ToList();
                DateTime ngayNhoNhat = listNgay.Min();

                avg = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= ngayNhoNhat && x.Ngay <= ngayTinh && x.MaChungKhoan==maCK)
                    .Average(x => x.KhoiLuong);
            }
            return avg;
        }       
        //lấy khối lượng trung bình của tất cả các cổ phiếu trong khoảng days ngày trước ngày ngàyTinh
        public static List<BieuDoKhoiLuongTB  > LayKhoiLuongTrungBinh(DateTime ngayTinh, int days = 100)
        {
            List<BieuDoKhoiLuongTB> list = new List<BieuDoKhoiLuongTB>();
            //select all distinct date from BieuDoKhoiLuong
            using (var dbContext = new ChungKhoanEntities())
            {
                //lấy ra danh sách ngày
                var listNgay = GetAllNgay().Where(x => x <= ngayTinh).OrderByDescending(x => x).ToList().Take(days).ToList();
                DateTime ngayNhoNhat = listNgay.Min();

                //lấy khối lượng trung bình của tất cả các cổ phiếu trong khoảng days ngày trước ngày ngàyTinh
                list = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= ngayNhoNhat && x.Ngay <= ngayTinh)
                    .GroupBy(x => x.MaChungKhoan)
                    .Select(x => new BieuDoKhoiLuongTB
                    {
                        MaChungKhoan = x.Key,
                        KhoiLuongTB = x.Average(y => y.KhoiLuong)
                    })
                    .ToList();
            }
            return list;
        }

        //lấy toàn bộ dữ liệu từ BieuDoKhoiLuong khoảng bao nhiêu ngày trước
        public static List<BieuDoKhoiLuong> GetAllByDays(int days, DateTime ngayTinh)
        {
            //select all distinct date from BieuDoKhoiLuong
            using (var dbContext = new ChungKhoanEntities())
            {
                //lấy ra danh sách ngày
                var listNgay = GetAllNgay().Where (x => x<=ngayTinh).OrderByDescending(x => x).ToList().Take (days).ToList();
                DateTime ngayNhoNhat = listNgay.Min();

                var list = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= ngayNhoNhat && x.Ngay <= ngayTinh)
                    .ToList();
                return list;
            }
        }
        //lấy toàn bộ dữ liệu từ BieuDoKhoiLuong khoảng bao nhiêu ngày trước
        public static List<BieuDoKhoiLuong> GetAllByDaysAndMaCK(int days, DateTime ngayTinh, string maChungKhoan)
        {
            //select all distinct date from BieuDoKhoiLuong
            using (var dbContext = new ChungKhoanEntities())
            {
                //lấy ra danh sách ngày
                var listNgay = GetAllNgay().Where(x => x <= ngayTinh).OrderByDescending(x => x).ToList().Take(days).ToList();
                DateTime ngayNhoNhat = listNgay.Min();

                var list = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.Ngay >= ngayNhoNhat && x.Ngay <= ngayTinh && x.MaChungKhoan == maChungKhoan)
                    .ToList();
                return list;
            }
        }
        public static List<BieuDoKhoiLuong> GetFilter(List<BieuDoKhoiLuong> listIn, List<string> listMa)
        {
            //return list BieuDoKhoiLuong where MaChungKhoan in listMa
            var list = listIn.Where(x => listMa.Contains(x.MaChungKhoan)).ToList();
            return list;
        }
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
        public static List<BieuDoKhoiLuong> GetAllByMaCK(DateTime tuNgay, DateTime denNgay, string maCK)
        {
            List<BieuDoKhoiLuong> list = new List<BieuDoKhoiLuong>();
            using (var dbContext = new ChungKhoanEntities())
            {
                list = dbContext.BieuDoKhoiLuongs
                    .Where(x => x.MaChungKhoan == maCK && x.Ngay >= tuNgay && x.Ngay <= denNgay)
                    .ToList();
            }
            return list;
        }
        public static List<ThongKeKhoiLuong> ThongKe3Tuan(DateTime tuan1_start, DateTime tuan1_end, DateTime tuan2_start, DateTime tuan2_end, DateTime tuan3_start, DateTime tuan3_end)
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
        public static List<ThongKe4Ngay> ThongKe4Tuan(DateTime tuan1_start, DateTime tuan1_end, DateTime tuan2_start, DateTime tuan2_end, DateTime tuan3_start, DateTime tuan3_end, DateTime tuan4_start, DateTime tuan4_end)
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
                                   //where MaChungKhoan in DMQuanTam
                               where t1.Ngay >= tuan1_start && t1.Ngay <= tuan1_end
                               select new ThongKe4Ngay 
                               {
                                   MaChungKhoan = t1.MaChungKhoan,
                                   GiaDongCua1 = (t1.Ngay == tuan12) ? t1.GiaDongCua : 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaDongCua4= 0,
                                   GiaMoCua1 = (t1.Ngay == tuan11) ? t1.GiaMoCua : 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   GiaMoCua4= 0,
                                   KhoiLuong1 = t1.KhoiLuong,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0,
                                   KhoiLuong4= 0,
                                   GiaCaoNhat1 = t1.GiaCaoNhat,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaCaoNhat4= decimal.MinValue,
                                   GiaThapNhat1 = t1.GiaThapNhat,
                                   GiaThapNhat2 = decimal.MaxValue,
                                   GiaThapNhat3 = decimal.MaxValue,
                                   GiaThapNhat4= decimal.MaxValue
                               })
                               .Union(
                               from t2 in dbContext.BieuDoKhoiLuongs
                               where t2.Ngay >= tuan2_start && t2.Ngay <= tuan2_end
                               select new ThongKe4Ngay
                               {
                                   MaChungKhoan = t2.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = (t2.Ngay == tuan22) ? t2.GiaDongCua : 0,
                                   GiaDongCua3 = 0,
                                   GiaDongCua4= 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = (t2.Ngay == tuan21) ? t2.GiaMoCua : 0,
                                   GiaMoCua3 = 0,
                                   GiaMoCua4 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = t2.KhoiLuong,
                                   KhoiLuong3 = 0,
                                   KhoiLuong4 = 0,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = t2.GiaCaoNhat,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaCaoNhat4= decimal.MinValue,
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = t2.GiaThapNhat,
                                   GiaThapNhat3 = decimal.MaxValue,
                                   GiaThapNhat4= decimal.MaxValue
                               })
                               .Union(
                               from t3 in dbContext.BieuDoKhoiLuongs
                               where t3.Ngay >= tuan3_start && t3.Ngay <= tuan3_end
                               select new ThongKe4Ngay
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
                                   KhoiLuong4 = 0,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = t3.GiaCaoNhat,
                                   GiaCaoNhat4 = decimal.MinValue,
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = decimal.MaxValue,
                                   GiaThapNhat3 = t3.GiaThapNhat,
                                   GiaThapNhat4 = decimal.MaxValue
                               })
                               .Union(
                               from t4 in dbContext.BieuDoKhoiLuongs
                               where t4.Ngay >= tuan4_start && t4.Ngay <= tuan4_end
                               select new ThongKe4Ngay
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
                                   KhoiLuong4 = t4.KhoiLuong,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaCaoNhat4 = t4.GiaCaoNhat,
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = decimal.MaxValue,
                                   GiaThapNhat3 = decimal.MaxValue,
                                   GiaThapNhat4 = t4.GiaThapNhat
                               })
                               .Join(dbContext.DMQuanTams, t => t.MaChungKhoan, d => d.MaChungKhoan, (t, d) => t)
                               .GroupBy(x => x.MaChungKhoan)
                               .Select(g => new ThongKe4Ngay
                               {
                                   MaChungKhoan = g.Key,
                                   GiaDongCua1 = g.Sum(x => x.GiaDongCua1),
                                   GiaDongCua2 = g.Sum(x => x.GiaDongCua2),
                                   GiaDongCua3 = g.Sum(x => x.GiaDongCua3),
                                   GiaDongCua4 = g.Sum(x =>x.GiaDongCua4 ),
                                   GiaMoCua1 = g.Sum(x => x.GiaMoCua1),
                                   GiaMoCua2 = g.Sum(x => x.GiaMoCua2),
                                   GiaMoCua3 = g.Sum(x => x.GiaMoCua3),
                                   GiaMoCua4 = g.Sum(x => x.GiaMoCua4),
                                   KhoiLuong1 = g.Sum(x => x.KhoiLuong1),
                                   KhoiLuong2 = g.Sum(x => x.KhoiLuong2),
                                   KhoiLuong3 = g.Sum(x => x.KhoiLuong3),
                                   KhoiLuong4 = g.Sum(x => x.KhoiLuong4 ),
                                   GiaCaoNhat1 = g.Max(x => x.GiaCaoNhat1),
                                   GiaCaoNhat2 = g.Max(x => x.GiaCaoNhat2),
                                   GiaCaoNhat3 = g.Max(x => x.GiaCaoNhat3),
                                   GiaCaoNhat4 = g.Max(x => x.GiaCaoNhat4),
                                   GiaThapNhat1 = g.Min(x => x.GiaThapNhat1),
                                   GiaThapNhat2 = g.Min(x => x.GiaThapNhat2),
                                   GiaThapNhat3 = g.Min(x => x.GiaThapNhat3),
                                   GiaThapNhat4 = g.Min(x => x.GiaThapNhat4)
                               })
                               .Where(x => x.GiaDongCua1 != 0 && x.GiaDongCua2 != 0 && x.GiaDongCua3 != 0 && x.GiaDongCua4 !=0)
                               .ToList();

                return results;
            }
        }
        public static List<ThongKeKhoiLuong> ThongKeNgayVaTuan(DateTime tuan3_start, DateTime tuan3_end, DateTime ngayXet)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                DateTime  tuan31, tuan32;
                //tìm ra ngày bắt đầu và kết thúc của tuần 3
                //ngày bắt đầu của tuần là ngày nhỏ nhất trong khoảng thời gian từ tuần 1_start đến tuần 1_end
                //select min ngay from BieuDoKhoiLuong where ngay >= tuan1_start and ngay <= tuan1_end                
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
                               where t1.Ngay == ngayXet
                               select new ThongKeKhoiLuong
                               {
                                   MaChungKhoan = t1.MaChungKhoan,
                                   GiaDongCua1 = t1.GiaDongCua,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaMoCua1 = t1.GiaMoCua,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   KhoiLuong1 = t1.KhoiLuong,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0,
                                   GiaCaoNhat1 = t1.GiaCaoNhat,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaThapNhat1 = t1.GiaThapNhat,
                                   GiaThapNhat2 = decimal.MaxValue,
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
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = decimal.MaxValue,
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
                                   GiaCaoNhat1 = g.Max(x => x.GiaCaoNhat1),
                                   GiaCaoNhat2 = g.Max(x => x.GiaCaoNhat2),
                                   GiaCaoNhat3 = g.Max(x => x.GiaCaoNhat3),
                                   GiaThapNhat1 = g.Min(x => x.GiaThapNhat1),
                                   GiaThapNhat2 = g.Min(x => x.GiaThapNhat2),
                                   GiaThapNhat3 = g.Min(x => x.GiaThapNhat3)
                               })
                               .Where(x => x.GiaDongCua1 != 0 && x.GiaDongCua3 != 0)
                               .ToList();

                return results;
            }
        }
        public static List<ThongKeKhoiLuong> ThongKe2Tuan(DateTime tuan2_start, DateTime tuan2_end, DateTime tuan3_start, DateTime tuan3_end)
        {
            using (var dbContext = new ChungKhoanEntities())
            {
                DateTime tuan21, tuan22, tuan31, tuan32;
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

                var results = (
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
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = decimal.MaxValue,
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
                                   GiaCaoNhat1 = g.Max(x => x.GiaCaoNhat1),
                                   GiaCaoNhat2 = g.Max(x => x.GiaCaoNhat2),
                                   GiaCaoNhat3 = g.Max(x => x.GiaCaoNhat3),
                                   GiaThapNhat1 = g.Min(x => x.GiaThapNhat1),
                                   GiaThapNhat2 = g.Min(x => x.GiaThapNhat2),
                                   GiaThapNhat3 = g.Min(x => x.GiaThapNhat3)
                               })
                               .Where(x => x.GiaDongCua2 != 0 && x.GiaDongCua3 != 0)
                               .ToList();

                return results;
            }
        }
        //thống kê theo 4 ngày
        public static List<ThongKe4Ngay> ThongKe4Ngay(DateTime ngayXet)
        {            
            using (var dbContext = new ChungKhoanEntities())
            {
                List<ThongKe4Ngay> results = new List<ThongKe4Ngay>();
                //tìm ra 4 ngày liên tiếp nhỏ hơn ngày xét
                //lấy ra danh sách ngày
                var listNgay = GetAllNgay().Where(x => x <= ngayXet).OrderByDescending(x => x).ToList().Take(4).ToList();
                if (listNgay.Count == 4)
                {
                    listNgay = listNgay.OrderBy(x => x).ToList();
                    DateTime ngay1 = listNgay[0];
                    DateTime ngay2 = listNgay[1];
                    DateTime ngay3 = listNgay[2];
                    DateTime ngay4 = listNgay[3];
                    results = (from t1 in dbContext.BieuDoKhoiLuongs
                                   where t1.Ngay == ngay1
                                   select new ThongKe4Ngay
                                   {
                                       MaChungKhoan = t1.MaChungKhoan,
                                       GiaDongCua1 = t1.GiaDongCua,
                                       GiaDongCua2 = 0,
                                       GiaDongCua3 = 0,
                                       GiaDongCua4 = 0,
                                       GiaMoCua1 = t1.GiaMoCua,
                                       GiaMoCua2 = 0,
                                       GiaMoCua3 = 0,
                                       GiaMoCua4 = 0,
                                       KhoiLuong1 = t1.KhoiLuong,
                                       KhoiLuong2 = 0,
                                       KhoiLuong3 = 0,
                                       KhoiLuong4 = 0,
                                       GiaCaoNhat1 = t1.GiaCaoNhat,
                                       GiaCaoNhat2 = decimal.MinValue,
                                       GiaCaoNhat3 = decimal.MinValue,
                                       GiaCaoNhat4 = decimal.MinValue,
                                       GiaThapNhat1 = t1.GiaThapNhat,
                                       GiaThapNhat2 = decimal.MaxValue,
                                       GiaThapNhat3 = decimal.MaxValue,
                                       GiaThapNhat4 = decimal.MaxValue
                                   })
                               .Union(
                                    from t2 in dbContext.BieuDoKhoiLuongs
                                      where t2.Ngay == ngay2
                                      select new ThongKe4Ngay
                                      {
                                          MaChungKhoan = t2.MaChungKhoan,
                                          GiaDongCua1 = 0,
                                          GiaDongCua2 = t2.GiaDongCua,
                                          GiaDongCua3 = 0,
                                          GiaDongCua4 = 0,
                                          GiaMoCua1 = 0,
                                          GiaMoCua2 = t2.GiaMoCua,
                                          GiaMoCua3 = 0,
                                          GiaMoCua4 = 0,
                                          KhoiLuong1 = 0,
                                          KhoiLuong2 = t2.KhoiLuong,
                                          KhoiLuong3 = 0,
                                          KhoiLuong4 = 0,
                                          GiaCaoNhat1 = decimal.MinValue,
                                          GiaCaoNhat2 = t2.GiaCaoNhat,
                                          GiaCaoNhat3 = decimal.MinValue,
                                          GiaCaoNhat4 = decimal.MinValue,
                                          GiaThapNhat1 = decimal.MaxValue,
                                          GiaThapNhat2 = t2.GiaThapNhat,
                                          GiaThapNhat3 = decimal.MaxValue,
                                          GiaThapNhat4 = decimal.MaxValue
                                      })
                               .Union(
                               from t3 in dbContext.BieuDoKhoiLuongs
                               where t3.Ngay == ngay3
                               select new ThongKe4Ngay
                               {
                                   MaChungKhoan = t3.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = t3.GiaDongCua,
                                   GiaDongCua4 = 0,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = t3.GiaMoCua,
                                   GiaMoCua4 = 0,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = t3.KhoiLuong,
                                   KhoiLuong4 = 0,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = t3.GiaCaoNhat,
                                   GiaCaoNhat4 = decimal.MinValue,
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = decimal.MaxValue ,
                                   GiaThapNhat3 = t3.GiaThapNhat,
                                   GiaThapNhat4 = decimal.MaxValue
                               })
                               .Union(
                               from t4 in dbContext.BieuDoKhoiLuongs
                               where t4.Ngay == ngay4
                               select new ThongKe4Ngay
                               {
                                   MaChungKhoan = t4.MaChungKhoan,
                                   GiaDongCua1 = 0,
                                   GiaDongCua2 = 0,
                                   GiaDongCua3 = 0,
                                   GiaDongCua4 = t4.GiaDongCua,
                                   GiaMoCua1 = 0,
                                   GiaMoCua2 = 0,
                                   GiaMoCua3 = 0,
                                   GiaMoCua4 = t4.GiaMoCua,
                                   KhoiLuong1 = 0,
                                   KhoiLuong2 = 0,
                                   KhoiLuong3 = 0,
                                   KhoiLuong4 = t4.KhoiLuong,
                                   GiaCaoNhat1 = decimal.MinValue,
                                   GiaCaoNhat2 = decimal.MinValue,
                                   GiaCaoNhat3 = decimal.MinValue,
                                   GiaCaoNhat4 = t4.GiaCaoNhat,
                                   GiaThapNhat1 = decimal.MaxValue,
                                   GiaThapNhat2 = decimal.MaxValue,
                                   GiaThapNhat3 = decimal.MaxValue,
                                   GiaThapNhat4 = t4.GiaThapNhat
                               })
                                .Join(dbContext.DMQuanTams, t => t.MaChungKhoan, d => d.MaChungKhoan, (t, d) => t)
                               .GroupBy(x => x.MaChungKhoan)
                               .Select(g => new ThongKe4Ngay
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
                                   KhoiLuong4 = g.Sum(x => x.KhoiLuong4),
                                   GiaCaoNhat1 = g.Max(x => x.GiaCaoNhat1),
                                   GiaCaoNhat2 = g.Max(x => x.GiaCaoNhat2),
                                   GiaCaoNhat3 = g.Max(x => x.GiaCaoNhat3),
                                   GiaCaoNhat4 = g.Max(x => x.GiaCaoNhat4),
                                   GiaThapNhat1 = g.Min(x => x.GiaThapNhat1),
                                   GiaThapNhat2 = g.Min(x => x.GiaThapNhat2),
                                   GiaThapNhat3 = g.Min(x => x.GiaThapNhat3),
                                   GiaThapNhat4 = g.Min(x => x.GiaThapNhat4)
                               })
                               .Where(x => x.GiaDongCua1 != 0 && x.GiaDongCua2 != 0 && x.GiaDongCua3 != 0 && x.GiaDongCua4 != 0 && x.KhoiLuong1 != 0 && x.KhoiLuong2 != 0 && x.KhoiLuong3 != 0 && x.KhoiLuong4 != 0)
                               .ToList();
                }

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
