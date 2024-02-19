using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Configuration;
using System.Runtime.InteropServices.ComTypes;

namespace LocChungKhoan
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            GlobalVar.ConnectString = AppContext.BaseDirectory + "ChungKhoan.db";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dữ liệu bắt đầu từ dòng thứ 2 (Dòng đầu là tiêu đề cột). Dữ liệu có cấu trúc cột theo thứ tự: Mã chứng khoán|Ngày (dd/MM/yyyy)|Giá đóng cửa (Số). Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //mở file excel
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtDuongDan.Text = openFileDialog1.FileName;
                    using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(openFileDialog1.FileName, false))
                    {
                        WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                        SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                        int lastRowIndex = GetRowCountWithData(sheetData);
                        int soDong = lastRowIndex - 1;
                        //hiển thị progress bar
                        progressBar1.Visible = true;
                        progressBar1.Maximum = soDong;
                        progressBar1.Step = 1;

                        // Lặp qua từng hàng trong SheetData
                        for (int i = 2; i <= lastRowIndex; i++)
                        {
                            Row row = sheetData.Elements<Row>().ElementAtOrDefault(i - 1); // -1 vì index bắt đầu từ 0
                            progressBar1.Value = i - 2 + 1;
                            System.Windows.Forms.Application.DoEvents();
                            //kiểm tra mã chứng khoán có chưa
                            Cell cellA = row.Elements<Cell>().ElementAtOrDefault(0);
                            string name = GetCellValue(cellA, workbookPart);
                            BieuDoGia obj = new BieuDoGia();
                            obj.MaChungKhoan = name.Trim();
                            //ngày
                            Cell cellB = row.Elements<Cell>().ElementAtOrDefault(1);
                            string myString = GetCellValue(cellB, workbookPart);
                            obj.Ngay = DateTime.ParseExact(myString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //giá
                            Cell cellC = row.Elements<Cell>().ElementAtOrDefault(2);
                            myString = GetCellValue(cellC, workbookPart);
                            obj.GiaDongCua = Convert.ToDecimal(myString);
                            BieuDoGiaController.Insert(obj);
                        }
                    }
                    MessageBox.Show("chuyển số liệu xong");                    
                }
            }

        }
        // Hàm lấy số dòng có chứa dữ liệu trong SheetData
        static int GetRowCountWithData(SheetData sheetData)
        {
            int count = 0;

            foreach (Row row in sheetData.Elements<Row>())
            {
                // Kiểm tra xem dòng có chứa dữ liệu không
                bool rowHasData = false;

                foreach (Cell cell in row.Elements<Cell>())
                {
                    string cellValue = GetCellValue(cell);

                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        rowHasData = true;
                        break;
                    }
                }

                if (rowHasData)
                {
                    count++;
                }
            }

            return count;
        }
        // Hàm lấy giá trị của một ô
        static string GetCellValue(Cell cell)
        {
            if (cell == null)
            {
                return null;
            }

            return cell.InnerText;
        }
        static string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null)
            {
                return null;
            }

            SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return sharedStringPart.SharedStringTable.ChildElements[int.Parse(cell.CellValue.Text)].InnerText;
            }
            else
            {
                return cell.CellValue.Text;
            }
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            ThongKe1();
        }
               
        private void ThongKe1()
        {
            if (txtNgay1.Text == "" || txtNgay2.Text == "" || txtNgay3.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày");
                return;
            }            
            DateTime ngay1 = DateTime.ParseExact(txtNgay1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtNgay2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtNgay3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ngay1 = new DateTime(ngay1.Year, ngay1.Month, ngay1.Day, 0, 0, 0);
            ngay2 = new DateTime(ngay2.Year, ngay2.Month, ngay2.Day, 0, 0, 0);
            ngay3 = new DateTime(ngay3.Year, ngay3.Month, ngay3.Day, 0, 0, 0);
            decimal nguongTren = decimal.MaxValue ;
            try
            {
                nguongTren = Convert.ToDecimal(txtNguongTren.Text);
            }
            catch
            {                
            }
            decimal nguongDuoi = decimal.MinValue ;
            try
            {
                nguongDuoi  = Convert.ToDecimal(txtNguongDuoi.Text);
            }
            catch
            {
            }            
            try
            {
                var list = BieuDoGiaController.ThongKe2(ngay1, ngay2, ngay3).OrderBy(x => x.MaChungKhoan);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("MaCK", typeof(string));
                dt.Columns.Add("Gia1", typeof(decimal));
                dt.Columns.Add("Gia2", typeof(decimal));
                dt.Columns.Add("Gia3", typeof(decimal));
                dt.Columns.Add("Lech23", typeof(decimal));
                int i = 1;
                foreach (var item in list)
                {  
                    decimal dolech23 = Math.Abs (item.DoLech23) * 100 / Math.Max(item.Gia2, item.Gia3);
                    if (item.Gia1 < item.Gia2 && item.Gia1 < item.Gia3
                        && dolech23 >=nguongDuoi && dolech23 <=nguongTren)
                    {
                        DataRow dr = dt.NewRow();
                        dr["STT"] = i;
                        dr["MaCK"] = item.MaChungKhoan;
                        dr["Gia1"] = Math.Round(item.Gia1, 2, MidpointRounding.AwayFromZero);
                        dr["Gia2"] = Math.Round(item.Gia2, 2, MidpointRounding.AwayFromZero);
                        dr["Gia3"] = Math.Round(item.Gia3, 2, MidpointRounding.AwayFromZero);
                        dr["Lech23"] = Math.Round(dolech23, 2, MidpointRounding.AwayFromZero);
                        dt.Rows.Add(dr);
                        i++;
                    }
                }                
                gridKQLoc.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Không tìm được dữ liệu!");
                return;
            }          
   
        }       
    
        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTin frm = new frmThongTin();
            frm.ShowDialog();
        }

        private void btnXoaToanBo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show ("Bạn có chắc chắn muốn xóa toàn bộ dữ liệu?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BieuDoGiaController.DeleteAll();
                MessageBox.Show("Đã xóa toàn bộ dữ liệu");
            }
        }

        private void btnXoaTheoThoiGian_Click(object sender, EventArgs e)
        {
            frmXoaTheoNgay frm = new frmXoaTheoNgay(true);
            frm.ShowDialog();            
        }

        private void btnXoaTheoNgay_Click(object sender, EventArgs e)
        {
            frmXoaTheoNgay frm = new frmXoaTheoNgay(false);
            frm.ShowDialog();            
        }

        private void btnLocDonNguong_Click(object sender, EventArgs e)
        {
            ThongKe2();
        }
        private void ThongKe2()
        {
            if (txtNgay1.Text == "" || txtNgay2.Text == "" || txtNgay3.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày");
                return;
            }
            DateTime ngay1 = DateTime.ParseExact(txtNgay1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtNgay2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtNgay3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ngay1 = new DateTime(ngay1.Year, ngay1.Month, ngay1.Day, 0, 0, 0);
            ngay2 = new DateTime(ngay2.Year, ngay2.Month, ngay2.Day, 0, 0, 0);
            ngay3 = new DateTime(ngay3.Year, ngay3.Month, ngay3.Day, 0, 0, 0);
            decimal nguongTren = decimal.MaxValue;
            try
            {
                nguongTren = Convert.ToDecimal(txtNguongTren.Text);
            }
            catch
            {
            }
            decimal nguongDuoi = decimal.MinValue;
            try
            {
                nguongDuoi = Convert.ToDecimal(txtNguongDuoi.Text);
            }
            catch
            {
            }
            try
            {
                var list = BieuDoGiaController.ThongKe2(ngay1, ngay2, ngay3).OrderBy(x => x.MaChungKhoan);

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("MaCK", typeof(string));
                dt.Columns.Add("Gia1", typeof(decimal));
                dt.Columns.Add("Gia2", typeof(decimal));
                dt.Columns.Add("Gia3", typeof(decimal));
                dt.Columns.Add("Lech23", typeof(decimal));
                int i = 1;
                foreach (var item in list)
                {
                    decimal dolech23 = Math.Abs(item.DoLech23) * 100 / Math.Max(item.Gia2, item.Gia3);
                    if (item.Gia1 > item.Gia2 && item.Gia1 > item.Gia3
                        && dolech23 >=nguongDuoi && dolech23<= nguongTren)
                    {
                        DataRow dr = dt.NewRow();
                        dr["STT"] = i;
                        dr["MaCK"] = item.MaChungKhoan;
                        dr["Gia1"] = Math.Round(item.Gia1, 2, MidpointRounding.AwayFromZero);
                        dr["Gia2"] = Math.Round(item.Gia2, 2, MidpointRounding.AwayFromZero);
                        dr["Gia3"] = Math.Round(item.Gia3, 2, MidpointRounding.AwayFromZero);
                        dr["Lech23"] = Math.Round(dolech23, 2, MidpointRounding.AwayFromZero);
                        dt.Rows.Add(dr);
                        i++;
                    }
                }
                gridKQLoc.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Không tìm được dữ liệu!");
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<DateTime> list = BieuDoGiaController.GetAllNgay();
            //display to grid
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Ngay", typeof(string));
            int i = 1;
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                dr["STT"] = i;
                dr["Ngay"] = item.ToString("dd/MM/yyyy");
                dt.Rows.Add(dr);
                i++;
            }
            gridKQLoc.DataSource = dt;
        }

        private void btnTinhNhanh_Click(object sender, EventArgs e)
        {
            if (txtNgay1.Text == "" || txtNgay2.Text == "" || txtNgay3.Text == "" || txtNgay4.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngày");
                return;
            }
            if (txtNguongTren.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập ngưỡng");
                txtNguongTren.Focus();
                return;
            }
            try { Convert.ToDecimal(txtNguongTren.Text); }
            catch
            {
                MessageBox.Show("Ngưỡng phải là số");
                txtNguongTren.Focus();
                return;
            }              
            DateTime ngay1 = DateTime.ParseExact(txtNgay1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtNgay2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtNgay3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay4 = DateTime.ParseExact(txtNgay4.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ngay1 = new DateTime(ngay1.Year, ngay1.Month, ngay1.Day, 0, 0, 0);
            ngay2 = new DateTime(ngay2.Year, ngay2.Month, ngay2.Day, 0, 0, 0);
            ngay3 = new DateTime(ngay3.Year, ngay3.Month, ngay3.Day, 0, 0, 0);
            ngay4 = new DateTime(ngay4.Year, ngay4.Month, ngay4.Day, 0, 0, 0);
            decimal nguongTren = Convert.ToDecimal(txtNguongTren.Text);
            decimal nguongDuoi = decimal.MinValue ;
            if (txtNguongDuoi.Text != "")
            {
                try
                {
                    nguongDuoi = Convert.ToDecimal(txtNguongDuoi.Text);
                }
                catch { } 
            }
            try
            {
                var list = BieuDoGiaController.ThongKe4(ngay1, ngay2, ngay3, ngay4).OrderBy(x => x.MaChungKhoan);

                System.Data.DataTable dt = new System.Data.DataTable();
                //dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("MaCK", typeof(string));
                dt.Columns.Add("Gia1", typeof(decimal));
                dt.Columns.Add("Gia2", typeof(decimal));
                dt.Columns.Add("Gia3", typeof(decimal));
                dt.Columns.Add("Gia4", typeof(decimal));
                dt.Columns.Add("Lech23", typeof(decimal));
                int i = 1;
                foreach (var item in list)
                {
                    decimal dolech23 = Math.Abs(item.Gia2 - item.Gia3) * 100 / Math.Max(item.Gia2, item.Gia3);
                    if (dolech23 <= nguongTren && dolech23 >= nguongDuoi && item.Gia1 > item.Gia2 && item.Gia2 >= item.Gia3 &&  item.Gia4 >= item.Gia3)
                    {
                        DataRow dr = dt.NewRow();
                        //dr["STT"] = i;
                        dr["MaCK"] = item.MaChungKhoan;
                        dr["Gia1"] = Math.Round(item.Gia1, 2, MidpointRounding.AwayFromZero);
                        dr["Gia2"] = Math.Round(item.Gia2, 2, MidpointRounding.AwayFromZero);
                        dr["Gia3"] = Math.Round(item.Gia3, 2, MidpointRounding.AwayFromZero);
                        dr["Gia4"] = Math.Round(item.Gia4, 2, MidpointRounding.AwayFromZero);
                        dr["Lech23"] = Math.Round(dolech23, 2, MidpointRounding.AwayFromZero);
                        dt.Rows.Add(dr);
                        i++;
                    }
                }                
                gridKQLoc.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Không tìm được dữ liệu!");
                return;
            }
        }

        private void lọcChứngKhoánTheoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocTheoNgay frm = new frmLocTheoNgay();
            frm.ShowDialog();
        }
    }
}
