using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan.Controller
{
    public  class CSoSanhNguong
    {        
        public  int vitri1 { get; set; }
        public  int vitri2 { get; set; }
        public  decimal nguongtren { get; set; }
        public  decimal nguongduoi { get; set; }
        public CSoSanhNguong()
        {            
            vitri1 = 0;
            vitri2 = 0;
            nguongtren  = 100;
            nguongduoi = 0;
        }
        public  Boolean SoSanh(decimal[] dayso)
        {
            decimal giatri1 = dayso[vitri1 - 1];
            decimal giatri2 = dayso[vitri2 - 1];
            decimal giaTriSoSanh = Math.Abs(giatri1 - giatri2) * 100 / Math.Max(giatri1, giatri2); 
            if (giaTriSoSanh<=nguongtren && giaTriSoSanh>=nguongduoi)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public  string HienThi()
        {
            return "|" + tieudegia (vitri1) + " - " + tieudegia (vitri2) + "|*100/max(" + tieudegia (vitri1 ) + "," + tieudegia (vitri2 ) + ") >=" + nguongduoi.ToString () + " và <=" + nguongtren.ToString ();
        }
        private string tieudegia(int vitri)
        {
            if (vitri!=0)
            {
                return "giá " + vitri.ToString();
            }
            else
            {
                return "...";
            }
        }
    }
}
