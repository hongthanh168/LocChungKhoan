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
    public partial class frmDieuKienTuan : Form
    {
        public Boolean IsKhoiLuong = true;
        public Boolean IsGiaDongMo = false;
        public Boolean IsGiaCaoThap = false;
        public Boolean IsDieuKienTuan1 = false;
        public Boolean IsLoc2Tuan = false;
        public frmDieuKienTuan()
        {
            InitializeComponent();
        }
        public frmDieuKienTuan(bool _isKhoiLuong, bool _isGiaDongMo, bool _isGiaCaoThap, bool _isDieuKienTuan1, bool _isLoc2Tuan = false)
        {
            InitializeComponent();
            IsKhoiLuong = _isKhoiLuong;
            IsGiaDongMo = _isGiaDongMo;
            IsGiaCaoThap = _isGiaCaoThap;
            IsDieuKienTuan1 = _isDieuKienTuan1;
            IsLoc2Tuan = _isLoc2Tuan;
        }
        private void frmDieuKienTuan_Load(object sender, EventArgs e)
        {
            chkGiaDongMo.Checked = IsGiaDongMo;
            chkGiaCaoThap.Checked = IsGiaCaoThap;
            ckKhoiLuong.Checked = IsKhoiLuong;
            chkDieuKienTuan1.Checked = IsDieuKienTuan1;
            if (IsLoc2Tuan)
            {
                chkDieuKienTuan1.Text = "Áp dụng Giá đóng cửa tuần 2 < Giá mở cửa tuần 2?";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IsKhoiLuong = ckKhoiLuong .Checked;
            IsGiaDongMo = chkGiaDongMo .Checked;
            IsGiaCaoThap = chkGiaCaoThap .Checked;
            IsDieuKienTuan1 = chkDieuKienTuan1.Checked;
            //return value to another form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
