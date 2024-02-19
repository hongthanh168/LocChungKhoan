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
        public int ChungKhoanID { get; set; }
        public decimal GiaDongCua { get; set; }

        public virtual DanhMucChungKhoan DanhMucChungKhoan { get; set; }
    }
    public partial class DanhMucChungKhoan
    {        
        public DanhMucChungKhoan()
        {
            this.BieuDoGias = new HashSet<BieuDoGia>();
        }
        [Key]
        public int ChungKhoanID { get; set; }
        public string MaChungKhoan { get; set; }

        public virtual ICollection<BieuDoGia> BieuDoGias { get; set; }
    }
}
