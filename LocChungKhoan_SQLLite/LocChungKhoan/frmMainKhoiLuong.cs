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
using LocChungKhoan.Controller;

namespace LocChungKhoan
{
    public partial class frmMainKhoiLuong : Form
    {
        public frmMainKhoiLuong()
        {
            InitializeComponent();
            GlobalVar.ConnectString = AppContext.BaseDirectory + "ChungKhoan.db";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dữ liệu bắt đầu từ dòng thứ 2 (Dòng đầu là tiêu đề cột). Dữ liệu có cấu trúc cột theo thứ tự: Ngày|Mã CK|Giá mở cửa|Giá đóng cửa|Khối lượng. Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                            Cell cellB = row.Elements<Cell>().ElementAtOrDefault(0);
                            string name = GetCellValue(cellB, workbookPart);
                            BieuDoKhoiLuong  obj = new BieuDoKhoiLuong ();
                            obj.MaChungKhoan = name.Trim();
                            //ngày
                            Cell cellA = row.Elements<Cell>().ElementAtOrDefault(1);
                            string myString = GetCellValue(cellB, workbookPart);
                            obj.Ngay = DateTime.ParseExact(myString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //giá mở cửa
                            Cell cellC = row.Elements<Cell>().ElementAtOrDefault(2);
                            myString = GetCellValue(cellC, workbookPart);
                            obj.GiaMoCua  = Convert.ToDecimal(myString);
                            //giá đong cửa
                            Cell cellD = row.Elements<Cell>().ElementAtOrDefault(3);
                            myString = GetCellValue(cellD, workbookPart);
                            obj.GiaDongCua  = Convert.ToDecimal(myString);
                            //khối lượng
                            Cell cellE = row.Elements<Cell>().ElementAtOrDefault(4);
                            myString = GetCellValue(cellE, workbookPart);
                            obj.KhoiLuong = Convert.ToDecimal(myString);
                            BieuDoKhoiLuongController .Insert(obj);
                        }
                    }
                    MessageBox.Show("Chuyển số liệu xong");                    
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

        private void btnXoaToanBo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show ("Bạn có chắc chắn muốn xóa toàn bộ dữ liệu?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BieuDoKhoiLuongController.DeleteAll();
                MessageBox.Show("Đã xóa toàn bộ dữ liệu");
            }
        }

        private void btnXoaTheoThoiGian_Click(object sender, EventArgs e)
        {
            frmXoaTheoNgay frm = new frmXoaTheoNgay(true, 1);
            frm.ShowDialog();            
        }

        private void btnXoaTheoNgay_Click(object sender, EventArgs e)
        {
            frmXoaTheoNgay frm = new frmXoaTheoNgay(false, 1);
            frm.ShowDialog();            
        }        

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<DateTime> list = BieuDoKhoiLuongController.GetAllNgay();
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

        private void frmMainKhoiLuong_Load(object sender, EventArgs e)
        {
            string boloc1 = "Gia1 >= Giá 3\r\nGia2>= Giá 3\r\nĐiều kiện hiển thị sẽ sắp xếp theo tự tăng dần của chỉ số hiển thị , chỉ số hiển thị được tính như sau: \r\n               Chỉ số hiển thị =   (Khối lượng tuần 3 / max(Khối lượng tuần 1, Khối lượng tuần 2))*100 ";
            string boloc2 = "Giá1 <= Giá 3 <= Giá 2\r\nĐiều kiện hiển thị : Sắp xếp theo thứ  theo thứ tự tăng dần của     (Giá 2 - giá 3 ) / giá 2 * 100 ";
            string boloc3 = "Giá mở cửa đầu tuần (Tuần 3) = Giá đóng cửa cuối tuần (Tuần 3)\r\nGiá đóng cửa cuối tuần (Tuần 2)  <=  Giá đóng cửa cuối tuần (Tuần 3) \r\n Chỉ số hiển thị  \r\n                                = 0 nếu Giá đóng cửa cuối tuần (Tuần 2)  =  Giá đóng cửa cuối tuần (Tuần 3) \r\n                                 =1  nếu Giá đóng cửa cuối tuần (Tuần 2)  <  Giá đóng cửa cuối tuần (Tuần 3)  \r\n Sắp xếp theo chiều tăng của chỉ số hiển thị .";
            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.SetToolTip(btnBoLoc1 , boloc1 );
            System.Windows.Forms.ToolTip toolTip2 = new System.Windows.Forms.ToolTip();
            toolTip2.SetToolTip(btnBoLoc2 , boloc2);
            System.Windows.Forms.ToolTip toolTip3 = new System.Windows.Forms.ToolTip();
            toolTip3.SetToolTip(btnBoLoc3 , boloc3);
        }

        private void btnChuyenDuLieu_Click(object sender, EventArgs e)
        {
            BieuDoKhoiLuongController.ChuyenDuLieuSangGia();
        }

        private void btnMoFormLocTheoGia_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.ShowDialog();
            this.Close();
        }
    }
}
