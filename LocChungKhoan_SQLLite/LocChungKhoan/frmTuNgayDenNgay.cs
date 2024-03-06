using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocChungKhoan
{
    public partial class frmTuNgayDenNgay : Form
    {
        // Khai báo biến        
        public DateTime TuNgay = DateTime.Now ;
        public DateTime DenNgay = DateTime.Now ;
        public frmTuNgayDenNgay()
        {
            InitializeComponent();
        }       
        private void button1_Click(object sender, EventArgs e)
        {            
            try
            {
                TuNgay = DateTime.ParseExact(txtTuNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DenNgay = DateTime.ParseExact(txtDenNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }   
            catch
            {
                MessageBox.Show("Ngày không hợp lệ");
            }  
            
        }

        private void frmNgay_Load(object sender, EventArgs e)
        {
            
        }
    }
}
