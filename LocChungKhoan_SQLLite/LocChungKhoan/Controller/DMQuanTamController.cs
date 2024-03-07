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
    public static class DMQuanTamController
    {        
        public static List<DMQuanTam> GetAll()
        {
            using (var db = new ChungKhoanEntities())
            {
                var list = db.DMQuanTams.ToList ();
                return list;
            }
        }        
        public static int Insert(DMQuanTam obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                //kiểm tra xem có giá trị nào chưa
                //nếu chưa có mới thêm
                DMQuanTam item = db.DMQuanTams.Where (t => t.MaChungKhoan == obj.MaChungKhoan ).FirstOrDefault();
                if (item ==null)
                {
                    obj = db.DMQuanTams.Add(obj);
                    db.SaveChanges();
                }                             
            }
            return obj.ID;
        }      
            
        public static void DeleteAll()
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    List<DMQuanTam> objs = (from b in db.DMQuanTams
                                            select b).ToList();
                    db.DMQuanTams.RemoveRange(objs);
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
