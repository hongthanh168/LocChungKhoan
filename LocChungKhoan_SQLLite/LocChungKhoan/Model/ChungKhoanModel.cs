﻿using System;
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
    public partial class TABChungKhoan
    {
        [Key]
        public int ID { get; set;}
        public string MaCK { get; set; }
        public int PhanLoai { get; set; }
    }
    
}