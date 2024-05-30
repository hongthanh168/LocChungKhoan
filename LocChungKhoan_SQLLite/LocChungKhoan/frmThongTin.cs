using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocChungKhoan
{
    public partial class frmThongTin : Form
    {
        public frmThongTin()
        {
            InitializeComponent();
        }

        private void btnThuNghiem_Click(object sender, EventArgs e)
        {
            frmPhanTichKyThuat frm = new frmPhanTichKyThuat();
            frm.Show();
        }
    }
}
