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
    public partial class frmXoaTheoNgay : Form
    {
        // Khai báo biến        
        public bool isTuNgayDenNgay = false;
        public frmXoaTheoNgay()
        {
            InitializeComponent();
        }
        public frmXoaTheoNgay(Boolean _isTuNgayDenNgay)
        {
            InitializeComponent();
            isTuNgayDenNgay = _isTuNgayDenNgay;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isTuNgayDenNgay)
            {
                try
                {
                    DateTime ngayBatDau = DateTime.ParseExact(txtTuNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ngayKetThuc = DateTime.ParseExact(txtDenNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //delete all data in BieuDoGias
                    ngayBatDau = new DateTime(ngayBatDau.Year, ngayBatDau.Month, ngayBatDau.Day, 0, 0, 0);
                    ngayKetThuc = new DateTime(ngayKetThuc.Year, ngayKetThuc.Month, ngayKetThuc.Day, 23, 59, 59);
                    BieuDoGiaController.DeleteFromTo(ngayBatDau, ngayKetThuc);
                    MessageBox.Show("Đã xóa dữ liệu từ ngày " + ngayBatDau.ToString("dd/MM/yyyy") + " đến ngày " + ngayKetThuc.ToString("dd/MM/yyyy"));
                    this.Close();
                }   
                catch
                {
                    MessageBox.Show("Ngày không hợp lệ");
                }                                  
            }
            else
            {
                try
                {
                    DateTime ngay = DateTime.ParseExact(txtNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ngay = new DateTime(ngay.Year, ngay.Month, ngay.Day, 0, 0, 0);
                    BieuDoGiaController.DeleteTheoNgay (ngay);
                    MessageBox.Show("Đã xóa dữ liệu ngày " + ngay.ToString("dd/MM/yyyy"));
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Ngày không hợp lệ");
                }
                
            }
        }

        private void frmNgay_Load(object sender, EventArgs e)
        {
            if (isTuNgayDenNgay)
            {
                txtTuNgay.Enabled = true;
                txtDenNgay.Enabled = true;
                txtNgay.Enabled = false;
            }
            else
            {
                txtTuNgay.Enabled = false;
                txtDenNgay.Enabled = false;
                txtNgay.Enabled = true;
            }
        }
    }
}
