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
    public partial class frmChiTietMaCK : Form
    {
        public string MaCK = "";
        public DateTime NgayBD = new DateTime(2024,1,1);
        public DateTime NgayKT = DateTime.Now;
        public frmChiTietMaCK()
        {
            InitializeComponent();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (txtMaCK .Text == "")
            {
                MessageBox.Show("Mã cổ phiếu không được để trống");
                txtMaCK.Focus();
                return;
            }   
            MaCK = txtMaCK.Text;
            try
            {
                NgayBD = DateTime.ParseExact(txtTuNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                if (txtTuNgay.Text != "")
                {
                    MessageBox.Show("Ngày bắt đầu không hợp lệ");
                    txtTuNgay.Focus();
                    return;
                }
            }
            //ngayKT
            try
            {
                NgayKT = DateTime.ParseExact(txtDenNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                if (txtDenNgay.Text != "")
                {
                    MessageBox.Show("Ngày kết thúc không hợp lệ");
                    txtDenNgay.Focus();
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChiTietMaCK_Load(object sender, EventArgs e)
        {
            txtDenNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTuNgay.Text = "01/01/2024";
        }
    }
}
