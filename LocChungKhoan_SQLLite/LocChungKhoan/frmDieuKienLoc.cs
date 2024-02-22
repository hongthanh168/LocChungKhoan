using DocumentFormat.OpenXml.Office2010.ExcelAc;
using LocChungKhoan.Controller;
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
    public partial class frmDieuKienLoc : Form
    {
        public List<CToanTuSoSanh> listToanTu = new List<CToanTuSoSanh>();
        public List<CSoSanhNguong> listNguong = new List<CSoSanhNguong>();
        CToanTuSoSanh toanTu = new CToanTuSoSanh();
        CSoSanhNguong nguong = new CSoSanhNguong();
        public frmDieuKienLoc()
        {
            InitializeComponent();
        }

        private void frmDieuKienLoc_Load(object sender, EventArgs e)
        {
            lblNguongHienTai.Text = "";
            lblSoSanhHienTai.Text = "";
        }

        private void btnSoSanhMoi_Click(object sender, EventArgs e)
        {            
           toanTu = new CToanTuSoSanh();
            lblSoSanhHienTai.Text = "";
        }

        private void btnNguongMoi_Click(object sender, EventArgs e)
        {
            nguong = new CSoSanhNguong();
            lblNguongHienTai.Text = "";
        }

        private void btnThemSoSanh_Click(object sender, EventArgs e)
        {
            //kiểm tra xem toán tử có đúng ko
            if (toanTu.vitri1 == 0 || toanTu.vitri2 == 0 || toanTu.vitri1 == toanTu.vitri2 )
            {
                MessageBox.Show("Chưa chọn vị trí giá trị hoặc chọn trùng giá trị");
                return;
            }
            listToanTu.Add(toanTu);
            //display in listbox
            string str = toanTu.HienThi();
            HienThiListViewSoSanh();
        }

        private void btnGia4_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (toanTu.vitri1 == 0)
            {
                toanTu.vitri1 = int.Parse (btn.Text.Replace ("Giá ", ""));
            }
            else
            {
                toanTu.vitri2 = int.Parse(btn.Text.Replace("Giá ", ""));
            }
            lblSoSanhHienTai.Text = toanTu.HienThi();
        }

        private void btnBang_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            toanTu.dau = btn.Text;
            lblSoSanhHienTai.Text = toanTu.HienThi();
        }

        private void btnThemNguong_Click(object sender, EventArgs e)
        {
            if(nguong.vitri1 == 0 || nguong.vitri2 == 0 || nguong.vitri1 == nguong.vitri2)
            {
                MessageBox.Show("Chưa chọn vị trí giá trị hoặc chọn trùng giá trị");
                return;
            }
            if (txtNguongDuoi.Text =="" && txtNguongTren.Text =="")
            {
                MessageBox.Show("Chưa nhập ngưỡng");
                return;
            }
            if (txtNguongDuoi.Text != "")
            {
                nguong.nguongduoi  = decimal.Parse(txtNguongDuoi.Text);
            }
            if (txtNguongTren.Text != "")
            {
                nguong.nguongtren = decimal.Parse(txtNguongTren.Text);
            }
            listNguong.Add(nguong);
            //display in listbox
            HienThiListViewNguong();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nguong.vitri1 == 0)
            {
                nguong.vitri1 = int.Parse(btn.Text.Replace("Giá ", ""));
            }
            else
            {
                nguong.vitri2 = int.Parse(btn.Text.Replace("Giá ", ""));
            }
            lblNguongHienTai.Text = nguong.HienThi();
        }

        private void txtNguongDuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nguong.nguongduoi = decimal.Parse(txtNguongDuoi.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập sai dữ liệu");
            }
            lblNguongHienTai.Text = nguong.HienThi();
        }

        private void txtNguongTren_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nguong.nguongtren  = decimal.Parse(txtNguongTren.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập sai dữ liệu");
            }
            lblNguongHienTai.Text = nguong.HienThi();
        }
        private void HienThiListViewSoSanh()
        {
            lvSoSanh.Items.Clear();
            foreach (CToanTuSoSanh item in listToanTu)
            {
                lvSoSanh.Items.Add(item.HienThi());
            }
        }
        private void HienThiListViewNguong() 
        {             
            lvDoLech.Items.Clear();
            foreach (CSoSanhNguong item in listNguong)
            {
                lvDoLech.Items.Add(item.HienThi());
            }
        }

        private void btnXoaDieuKien_Click(object sender, EventArgs e)
        {
            //delete selected item in lvSoSanh
            if (lvSoSanh.SelectedItems.Count > 0)
            {
                //get index of selected item
                int index = lvSoSanh.SelectedIndices[0];
                listToanTu.RemoveAt(index);
                HienThiListViewSoSanh();
            }
        }

        private void btnXoaNguong_Click(object sender, EventArgs e)
        {
            //delete selected item in lvSoSanh
            if (lvDoLech.SelectedItems.Count > 0)
            {
                //get index of selected item
                int index = lvDoLech.SelectedIndices[0];
                listNguong.RemoveAt(index);
                HienThiListViewNguong ();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //return listToanTu and listNguong
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
