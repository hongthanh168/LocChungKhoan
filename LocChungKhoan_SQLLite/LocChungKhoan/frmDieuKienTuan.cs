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
        public frmDieuKienTuan()
        {
            InitializeComponent();
        }
        public frmDieuKienTuan(bool _isKhoiLuong, bool _isGiaDongMo, bool _isGiaCaoThap, bool isDieuKienTuan1)
        {
            InitializeComponent();
            IsKhoiLuong = _isKhoiLuong;
            IsGiaDongMo = _isGiaDongMo;
            IsGiaCaoThap = _isGiaCaoThap;
            IsDieuKienTuan1 = isDieuKienTuan1;
        }
        private void frmDieuKienTuan_Load(object sender, EventArgs e)
        {
            chkGiaDongMo.Checked = IsGiaDongMo;
            chkGiaCaoThap.Checked = IsGiaCaoThap;
            ckKhoiLuong.Checked = IsKhoiLuong;
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
