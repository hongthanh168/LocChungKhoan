using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
{
    public partial class sp_ThongKe3Ngay_Result
    {
        [Key]
        public string MaChungKhoan { get; set; }
        public decimal Gia1 { get; set; }
        public decimal Gia2 { get; set; }
        public decimal Gia3 { get; set; }
        public decimal DoLech12 { get; set; }
        public decimal DoLech13 { get; set; }
        public decimal DoLech23 { get; set; }
    }
    public partial class sp_ThongKe4Ngay_Result
    {
        [Key]
        public string MaChungKhoan { get; set; }
        public decimal Gia1 { get; set; }
        public decimal Gia2 { get; set; }
        public decimal Gia3 { get; set; }
        public decimal Gia4 { get; set; }
    }
    public partial class ThongKeKhoiLuong
    {
        [Key]
        public string MaChungKhoan { get; set; }
        public decimal GiaDongCua1 { get; set; }
        public decimal GiaDongCua2 { get; set; }
        public decimal GiaDongCua3 { get; set; }
        public decimal GiaMoCua1 { get; set; }
        public decimal GiaMoCua2 { get; set; }
        public decimal GiaMoCua3 { get; set; }
        public decimal KhoiLuong1 { get; set; }
        public decimal KhoiLuong2 { get; set; }
        public decimal KhoiLuong3 { get; set; }
    }
    public partial class ThongKeKhoiLuong4Tuan
    {
        [Key]
        public string MaChungKhoan { get; set; }
        public decimal GiaDongCua1 { get; set; }
        public decimal GiaDongCua2 { get; set; }
        public decimal GiaDongCua3 { get; set; }
        public decimal GiaDongCua4 { get; set; }
        public decimal GiaMoCua1 { get; set; }
        public decimal GiaMoCua2 { get; set; }
        public decimal GiaMoCua3 { get; set; }
        public decimal GiaMoCua4 { get; set; }
        public decimal KhoiLuong1 { get; set; }
        public decimal KhoiLuong2 { get; set; }
        public decimal KhoiLuong3 { get; set; }
        public decimal KhoiLuong4 { get; set; }
    }
}
