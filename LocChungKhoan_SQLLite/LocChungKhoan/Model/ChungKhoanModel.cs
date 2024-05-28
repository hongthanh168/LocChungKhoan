using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
{
    public partial class BieuDoGia
    {
        [Key]
        public int BieuDoGiaID { get; set; }
        public System.DateTime Ngay { get; set; }
        public string MaChungKhoan { get; set; }
        public decimal GiaDongCua { get; set; }
    }  
    public partial class DMQuanTam
    {
        [Key]
        public int ID { get; set;}
        public string MaChungKhoan { get; set; }
    }
    public partial class TABChungKhoan
    {
        [Key]
        public int ID { get; set; }
        public string MaChungKhoan { get; set; }
        public int PhanLoai { get; set; }
    }
    public partial class BieuDoKhoiLuong
    {
        [Key]
        public int BieuDoKhoiLuongID { get; set; } //ID của mã chứng khoán
        public System.DateTime Ngay { get; set; } //Ngày giao dịch
        public string MaChungKhoan { get; set; } //Mã chứng khoán
        public decimal GiaMoCua { get; set; } //Giá mở cửa
        public decimal GiaDongCua { get; set; }        //Giá đóng cửa
        public decimal KhoiLuong { get; set; } //Khối lượng
        public decimal GiaCaoNhat { get; set; } //Giá cao nhất
        public decimal GiaThapNhat { get; set; } //
        public BieuDoKhoiLuong()
        {
            GiaCaoNhat = 0;
            GiaThapNhat = 0;
            Ngay = DateTime.Now;
            MaChungKhoan = "";
            GiaMoCua = 0;
            GiaDongCua = 0;
            KhoiLuong = 0;
            BieuDoKhoiLuongID = 0;
        }
    }

}
