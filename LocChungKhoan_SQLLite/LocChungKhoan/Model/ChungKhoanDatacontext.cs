using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocChungKhoan
{
    public class ChungKhoanEntities : DbContext
    {
        public ChungKhoanEntities() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = GlobalVar.ConnectString, ForeignKeys = true }.ConnectionString
            }, true)
        {
        }        
        public  DbSet<BieuDoGia> BieuDoGias { get; set; }
        public DbSet<BieuDoKhoiLuong> BieuDoKhoiLuongs { get; set; }
        public DbSet<DMQuanTam> DMQuanTams { get; set; }
        public DbSet<TABChungKhoan> TABChungKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
