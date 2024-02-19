using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
{ 
    
    public static class DanhMucChungKhoanController
    {
        public static List<DanhMucChungKhoan> GetAll()
        {
            ChungKhoanEntities db = new ChungKhoanEntities();            
            var list = (from t in db.DanhMucChungKhoans
                        select t).ToList();
            return list;
        }
        public static DanhMucChungKhoan  FindByName(string name)
        {
            ChungKhoanEntities db = new ChungKhoanEntities();
            var item = (from t in db.DanhMucChungKhoans
                        where t.MaChungKhoan == name
                        select t).FirstOrDefault();
            return item;
        }
        public static int Insert(DanhMucChungKhoan obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                obj = db.DanhMucChungKhoans.Add(obj);
                db.SaveChanges();
            }
            return obj.ChungKhoanID;
        }
        public static int Update(DanhMucChungKhoan  obj)
        {
            using (var db = new ChungKhoanEntities())
            {
                db.DanhMucChungKhoans .Attach(obj);
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return obj.ChungKhoanID;
        }

        public static void Delete(int id)
        {
            using (var db = new ChungKhoanEntities())
            {
                try
                {
                    DanhMucChungKhoan obj = (from b in db.DanhMucChungKhoans
                                   where b.ChungKhoanID == id
                                   select b).FirstOrDefault();
                    db.DanhMucChungKhoans.Remove(obj);
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
