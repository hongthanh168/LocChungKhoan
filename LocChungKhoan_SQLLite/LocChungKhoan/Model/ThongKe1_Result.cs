using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
{
    public partial class sp_ThongKe2Ngay_Result
    {
        [Key]
        public string MaChungKhoan { get; set; }
        public decimal Gia1 { get; set; }
        public decimal Gia2 { get; set; }
        public decimal DoLech { get; set; }
    }
}
