using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan.Controller
{
    public  class CToanTuSoSanh
    {        
        public  int vitri1 { get; set; }
        public  int vitri2 { get; set; }
        public  string dau { get; set; }
        public CToanTuSoSanh()
        {  
            vitri1 = 0;
            vitri2 = 0;
            dau = "...";
        }
        public Boolean SoSanh(decimal[] dayso)
        {
            decimal giatri1 = dayso[vitri1 -1];
            decimal giatri2 = dayso[vitri2 - 1];            
            switch (dau.Trim ())
            {
                case ">":
                    if (giatri1 > giatri2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case ">=":
                    if(giatri1 >= giatri2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "<":
                    if(giatri1 < giatri2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "<=":
                    if(giatri1 <= giatri2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "=":
                    if(giatri1 == giatri2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
            
        }
        public string HienThi()
        {
            return tieudegia (vitri1 ) + dau + tieudegia (vitri2 );
        }
        private string tieudegia(int vitri)
        {
            if (vitri != 0)
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
