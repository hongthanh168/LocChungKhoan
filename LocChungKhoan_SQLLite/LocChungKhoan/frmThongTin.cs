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
            //gọi form frmPhanTichKyThuat và đóng form hiện tại
            this.Hide();
            frmPhanTichKyThuat form2 = new frmPhanTichKyThuat();
            form2.Closed += (s, args) => this.Close();
            form2.ShowDialog ();
        }

        private void frmThongTin_KeyUp(object sender, KeyEventArgs e)
        {
            //nếu nhấn phím Ctrl + T thì gọi form frmPhanTichKyThuat và đóng form hiện tại
            if (e.Control && e.KeyCode == Keys.T)
            {
                this.Hide();
                frmPhanTichKyThuat form2 = new frmPhanTichKyThuat();
                form2.Closed += (s, args) => this.Close();
                form2.ShowDialog();
            }
        }
    }
}
