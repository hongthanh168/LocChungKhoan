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
    public static class TABCKController
    {        
        public static List<TABChungKhoan> GetAll(int phanLoai)
        {
            using (var db = new ChungKhoanEntities())
            {
                var list = db.TABChungKhoans.Where(x => x.PhanLoai==phanLoai).ToList ();
                return list;
            }
        }        
        public static int Insert(TABChungKhoan obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                //kiểm tra xem có giá trị nào chưa
                //nếu chưa có mới thêm
                TABChungKhoan item = db.TABChungKhoans.Where (t => t.MaChungKhoan == obj.MaChungKhoan && t.PhanLoai == obj.PhanLoai).FirstOrDefault();
                if (item ==null)
                {
                    obj = db.TABChungKhoans.Add(obj);
                    db.SaveChanges();
                }                             
            }
            return obj.ID;
        }        
        public static List<TABChungKhoan> ThongKe()
        {
           using (var db = new ChungKhoanEntities())
            {
                //get all TABChungKhoan if it has the same MaChungKhoan and it has PhanLoai =1 and not has PhanLoai =2 and not has PhanLoai =3
                var list = db.TABChungKhoans
                    .Where (t => t.PhanLoai == 1 && !db.TABChungKhoans.Any (t2 => t2.MaChungKhoan == t.MaChungKhoan && t2.PhanLoai == 2) 
                    && !db.TABChungKhoans.Any (t3 => t3.MaChungKhoan == t.MaChungKhoan && t3.PhanLoai == 3))
                    .ToList();
                return list;
            }
        }      
        public static void DeleteAll(int PhanLoai)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    List<TABChungKhoan> objs = (from b in db.TABChungKhoans
                                                where b.PhanLoai == PhanLoai
                                            select b).ToList();
                    db.TABChungKhoans.RemoveRange(objs);
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
