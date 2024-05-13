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
            if (MessageBox.Show("Dữ liệu bắt đầu từ dòng thứ 2 (Dòng đầu là tiêu đề cột). Dữ liệu có cấu trúc cột theo thứ tự: Ngày|Mã CK|Giá mở cửa|Giá đóng cửa|Giá cao nhất|Giá thấp nhất|Khối lượng. Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //mở file excel
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtDuongDan.Text = openFileDialog1.FileName;
                    using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(openFileDialog1.FileName, false))
                    {
                        WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                        // Find the sheet with the name "Sheet1"
                        WorksheetPart worksheetPart = null;
                        foreach (Sheet sheet in workbookPart.Workbook.Sheets)
                        {
                            if (sheet.Name == "Sheet1")
                            {
                                worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                                break;
                            }
                        }
                        if (worksheetPart == null)
                        {
                            MessageBox.Show("Không tìm thấy Sheet1");
                            return;
                        }
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
                            Cell cellB = row.Elements<Cell>().ElementAtOrDefault(1);
                            string name = GetCellValue(cellB, workbookPart);
                            BieuDoKhoiLuong  obj = new BieuDoKhoiLuong ();
                            obj.MaChungKhoan = name.Trim();
                            //ngày
                            Cell cellA = row.Elements<Cell>().ElementAtOrDefault(0);
                            string myString = GetCellValue(cellA, workbookPart);
                            obj.Ngay = DateTime.ParseExact(myString, "d/M/yyyy", CultureInfo.InvariantCulture);
                            //giá mở cửa
                            Cell cellC = row.Elements<Cell>().ElementAtOrDefault(2);
                            myString = GetCellValue(cellC, workbookPart);
                            obj.GiaMoCua  = Convert.ToDecimal(myString);
                            //giá đong cửa
                            Cell cellD = row.Elements<Cell>().ElementAtOrDefault(3);
                            myString = GetCellValue(cellD, workbookPart);
                            obj.GiaDongCua  = Convert.ToDecimal(myString);
                            //giá cao nhất
                            Cell cellE = row.Elements<Cell>().ElementAtOrDefault(4);
                            myString = GetCellValue(cellE, workbookPart);
                            if (myString != "")
                            {
                                obj.GiaCaoNhat = Convert.ToDecimal(myString);
                            }
                            else
                            {
                                obj.GiaCaoNhat = 0;
                            }
                            //giá thấp nhất
                            Cell cellF = row.Elements<Cell>().ElementAtOrDefault(5);
                            myString = GetCellValue(cellF, workbookPart);
                            if (myString != "")
                            {
                                obj.GiaThapNhat = Convert.ToDecimal(myString);
                            }
                            else
                            {
                                obj.GiaThapNhat = 0;
                            }
                            //khối lượng
                            Cell cellG = row.Elements<Cell>().ElementAtOrDefault(6);
                            myString = GetCellValue(cellG, workbookPart);
                            if (myString != "")
                            {
                                obj.KhoiLuong = Convert.ToDecimal(myString);
                            }
                            else
                            {
                                obj.KhoiLuong = 0;
                            }
                            
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
            gridKQLoc.DataSource = null;
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
            //get number of rows
            groupBox2.Text = "Số ngày có dữ liệu: " + (i - 1).ToString();
        }      

        private void frmMainKhoiLuong_Load(object sender, EventArgs e)
        {
            string thongkeTuan = "Lọc theo cặp. Có 3 cặp , mỗi cặp chênh nhau <= 1% , GiaDongCua3 >= GiaDongCua2 hoặc GiaDongCua3 >= GiaMoCua3.";
            string thongkeNgay = "- Min = min (gía đóng cửa 1, gía đóng cửa 2, gía đóng cửa 3)\r\n-Điều kiện: GiaDongCua1  > min && GiaDongCua3 >= item.GiaDongCua2 && item.GiaDongCua3 >= item.GiaMoCua3";
            string boloc1 = "- Giá đóng cửa ngày 1 > Giá đóng cửa ngày 2 \r\n- Giá đóng cửa ngày 2 < Giá mở cửa ngày 2 \r\n- Giá đóng cửa ngày 3= giá mở cửa ngày 3 = giá đóng cửa ngày 2 ";
            string boloc2 = "- Giá cao nhất ngày 1 < giá cao nhất ngày 2 \r\n- Giá đóng cửa ngày 1 < giá đóng cửa ngày 2 \r\n- Giá đóng cửa ngày 2 > giá mở cửa ngày 2 \r\n- Giá đóng cửa ngày 3 < Giá mở cửa ngày 3 \r\n- Giá đóng cửa ngày 3 > max ( giá đóng cửa ngày 1 , giá mở cửa ngày 2 ) ";
            string boloc3 = "- (|giá đóng cửa ngày 3 - giá mở cửa ngày 3 | / max ( giá mở cửa ngày 3, giá đóng cửa ngày 3 ) ) *100 <= 1 \r\n- Giá đóng cửa ngày 2 > Giá mở cửa ngày 2 \r\n- Giá đóng cửa ngày 2 < min ( giá mở cửa ngày 3 , giá đóng cửa ngày 3 )";
            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.SetToolTip(btnLocNhanh1, boloc1);
            System.Windows.Forms.ToolTip toolTip2 = new System.Windows.Forms.ToolTip();
            toolTip2.SetToolTip(btnLocNhanh2, boloc2);
            System.Windows.Forms.ToolTip toolTip3 = new System.Windows.Forms.ToolTip();
            toolTip3.SetToolTip(btnLocNhanh3, boloc3);
            System.Windows.Forms.ToolTip toolTipNgay = new System.Windows.Forms.ToolTip();
            toolTipNgay.SetToolTip(btnThongKeNgay , thongkeNgay );
            System.Windows.Forms.ToolTip toolTipTuan = new System.Windows.Forms.ToolTip();
            toolTipTuan.SetToolTip(btnThongKeTuan, thongkeTuan);
        }

        private void btnChuyenDuLieu_Click(object sender, EventArgs e)
        {
            frmTuNgayDenNgay frm = new frmTuNgayDenNgay();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                DateTime tuNgay = frm.TuNgay;
                DateTime denNgay = frm.DenNgay;
                BieuDoKhoiLuongController.ChuyenDuLieuSangGia(tuNgay, denNgay);
                MessageBox.Show("Chuyển dữ liệu xong");
            }            
        }

        private void btnMoFormLocTheoGia_Click(object sender, EventArgs e)
        {
            //open frmMain and close this form
            this.Hide();
            var form2 = new frmVersion1();
            form2.Closed += (s, args) => this.Close();
            form2.Show();

        }

        

        private void btnMoThuMuc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chức năng này sẽ import dữ liệu từ tất cả các file excel .xlsx trong thư mục được chọn. Dữ liệu bắt đầu từ dòng thứ 2 (Dòng đầu là tiêu đề cột). Dữ liệu có cấu trúc cột theo thứ tự: Ngày|Mã CK|Giá mở cửa|Giá đóng cửa|Giá cao nhất|Giá thấp nhất|Khối lượng. Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //open dialog to choose folder
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtDuongDan.Text = folderBrowserDialog1.SelectedPath;
                    //open foreach file in folder
                    string[] filePaths = System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.xlsx");
                    foreach (string file in filePaths)
                    {
                        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(file, false))
                        {
                            WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                            // Find the sheet with the name "Sheet1"
                            WorksheetPart worksheetPart = null;
                            foreach (Sheet sheet in workbookPart.Workbook.Sheets)
                            {
                                if (sheet.Name == "Sheet1")
                                {
                                    worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                                    break;
                                }
                            }
                            if (worksheetPart == null)
                            {
                                MessageBox.Show("Không tìm thấy Sheet1");
                                return;
                            }
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
                                Cell cellB = row.Elements<Cell>().ElementAtOrDefault(1);
                                string name = GetCellValue(cellB, workbookPart);
                                BieuDoKhoiLuong obj = new BieuDoKhoiLuong();
                                obj.MaChungKhoan = name.Trim();
                                //ngày
                                Cell cellA = row.Elements<Cell>().ElementAtOrDefault(0);
                                string myString = GetCellValue(cellA, workbookPart);
                                obj.Ngay = DateTime.ParseExact(myString, "d/M/yyyy", CultureInfo.InvariantCulture);
                                //giá mở cửa
                                Cell cellC = row.Elements<Cell>().ElementAtOrDefault(2);
                                myString = GetCellValue(cellC, workbookPart);
                                obj.GiaMoCua = Convert.ToDecimal(myString);
                                //giá đong cửa
                                Cell cellD = row.Elements<Cell>().ElementAtOrDefault(3);
                                myString = GetCellValue(cellD, workbookPart);
                                obj.GiaDongCua = Convert.ToDecimal(myString);
                                //giá cao nhất
                                Cell cellE = row.Elements<Cell>().ElementAtOrDefault(4);
                                myString = GetCellValue(cellE, workbookPart);
                                if (myString != "")
                                {
                                    obj.GiaCaoNhat = Convert.ToDecimal(myString);
                                }
                                else
                                {
                                    obj.GiaCaoNhat = 0;
                                }
                                //giá thấp nhất
                                Cell cellF = row.Elements<Cell>().ElementAtOrDefault(5);
                                myString = GetCellValue(cellF, workbookPart);
                                if (myString != "")
                                {
                                    obj.GiaThapNhat = Convert.ToDecimal(myString);
                                }
                                else
                                {
                                    obj.GiaThapNhat = 0;
                                }
                                //khối lượng
                                Cell cellG = row.Elements<Cell>().ElementAtOrDefault(6);
                                myString = GetCellValue(cellG, workbookPart);
                                if (myString != "")
                                {
                                    obj.KhoiLuong = Convert.ToDecimal(myString);
                                }
                                else
                                {
                                    obj.KhoiLuong = 0;
                                }

                                BieuDoKhoiLuongController.Insert(obj);
                            }
                        }
                    }
                }
                MessageBox.Show("Chuyển số liệu xong");
            }        

        }

        private void btnThongKe4Tuan_Click(object sender, EventArgs e)
        {
            ////check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            //if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "" || txtTuan4DauTuan .Text =="" || txtTuan1CuoiTuan.Text == "" || txtTuan2CuoiTuan.Text == "" || txtTuan3CuoiTuan.Text == "" || txtTuan4CuoiTuan.Text =="")
            //{
            //    MessageBox.Show("Vui lòng nhập đủ thông tin");
            //    return;
            //}
            //DateTime tuan1DauTuan = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan2DauTuan = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan3DauTuan = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan4DauTuan = DateTime.ParseExact(txtTuan4DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan1CuoiTuan = DateTime.ParseExact(txtTuan1CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan2CuoiTuan = DateTime.ParseExact(txtTuan2CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan3CuoiTuan = DateTime.ParseExact(txtTuan3CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime tuan4CuoiTuan = DateTime.ParseExact(txtTuan4CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //List<ThongKeKhoiLuong4Tuan> list = BieuDoKhoiLuongController.ThongKe4(tuan1DauTuan, tuan1CuoiTuan, tuan2DauTuan, tuan2CuoiTuan, tuan3DauTuan, tuan3CuoiTuan, tuan4DauTuan, tuan4CuoiTuan );
            ////display to grid
            ////create datatable
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt.Columns.Add("MaCK", typeof(string));
            //dt.Columns.Add("GiaMC1", typeof(decimal));
            //dt.Columns.Add("GiaDC1", typeof(decimal));
            //dt.Columns.Add("GiaMC2", typeof(decimal));
            //dt.Columns.Add("GiaDC2", typeof(decimal));
            //dt.Columns.Add("GiaMC3", typeof(decimal));
            //dt.Columns.Add("GiaDC3", typeof(decimal));
            //dt.Columns.Add("GiaMC4", typeof(decimal));
            //dt.Columns.Add("GiaDC4", typeof(decimal));
            //dt.Columns.Add("KL1", typeof(decimal));
            //dt.Columns.Add("KL2", typeof(decimal));
            //dt.Columns.Add("KL3", typeof(decimal));
            //dt.Columns.Add("KL4", typeof(decimal));
            //dt.Columns.Add("ChiSo", typeof(decimal));
            //gridKQLoc.DataSource = null;
            //int i = 1;
            //foreach (var item in list)
            //{
            //    decimal chiSo = (item.GiaDongCua4 - item.GiaDongCua3 )/item.GiaDongCua4 *100;                
            //    if (item.KhoiLuong1 > item.KhoiLuong2 && item.KhoiLuong2 > item.KhoiLuong3 
            //        && item.GiaDongCua4 >= item.GiaDongCua3
            //        && item.GiaMoCua3 > item.GiaDongCua3
            //        && chiSo <=1)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["MaCK"] = item.MaChungKhoan;
            //        dr["GiaMC1"] = item.GiaMoCua1;
            //        dr["GiaDC1"] = item.GiaDongCua1;
            //        dr["GiaMC2"] = item.GiaMoCua2;
            //        dr["GiaDC2"] = item.GiaDongCua2;
            //        dr["GiaMC3"] = item.GiaMoCua3;
            //        dr["GiaDC3"] = item.GiaDongCua3;
            //        dr["GiaMC4"] = item.GiaMoCua4;
            //        dr["GiaDC4"] = item.GiaDongCua4;
            //        dr["KL1"] = item.KhoiLuong1;
            //        dr["KL2"] = item.KhoiLuong2;
            //        dr["KL3"] = item.KhoiLuong3;
            //        dr["KL4"] = item.KhoiLuong4;
            //        dr["ChiSo"] = Math.Round (chiSo, 2, MidpointRounding.AwayFromZero);
            //        dt.Rows.Add(dr);
            //        i++;
            //    }
            //}
            //groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();
            ////order by ChiSo
            //DataView dv = dt.DefaultView;
            //dv.Sort = "ChiSo";
            //DataTable sortedDT = dv.ToTable();
            //// Finally, set the DataSource of the DataGridView to the sorted DataTable
            //gridKQLoc.DataSource = sortedDT;
            ////fix first column when scroll
            //gridKQLoc.Columns[0].Frozen = true;            
        }

        private void txtTuan1DauTuan_Leave(object sender, EventArgs e)
        {
            //check if text is Date
            DateTime dt;
            if (DateTime.TryParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                //update txtTuan1CuoiTuan
                txtTuan1CuoiTuan.Text = dt.AddDays(4).ToString("dd/MM/yyyy");
                txtTuan2DauTuan .Text = dt.AddDays(7).ToString("dd/MM/yyyy");
                txtTuan2CuoiTuan.Text = dt.AddDays(11).ToString("dd/MM/yyyy");
                txtTuan3DauTuan.Text = dt.AddDays(14).ToString("dd/MM/yyyy");
                txtTuan3CuoiTuan.Text = dt.AddDays(18).ToString("dd/MM/yyyy");
                //txtTuan4DauTuan.Text = dt.AddDays(21).ToString("dd/MM/yyyy");
                //txtTuan4CuoiTuan.Text = dt.AddDays(25).ToString("dd/MM/yyyy");
            }

        }
        private void txtMaCK_TextChanged(object sender, EventArgs e)
        {
            // Get the search term from the TextBox
            string searchTerm = txtMaCK.Text;

            // Filter the DataGridView based on the search term
            FilterDataGridView(searchTerm);
        }
        private void FilterDataGridView(string searchTerm)
        {
            // Access the default view of the DataGridView's DataSource
            System.Data.DataTable dt = (System.Data.DataTable)gridKQLoc.DataSource;
            DataView dv = dt.DefaultView;

            // Apply the filter to the default view
            dv.RowFilter = $"MaCK LIKE '{searchTerm}%'"; // Replace ColumnName with the actual column name

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "" || txtTuan1CuoiTuan.Text == "" || txtTuan2CuoiTuan.Text == "" || txtTuan3CuoiTuan.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            DateTime tuan1DauTuan = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tuan2DauTuan = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tuan3DauTuan = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);            
            DateTime tuan1CuoiTuan = DateTime.ParseExact(txtTuan1CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tuan2CuoiTuan = DateTime.ParseExact(txtTuan2CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tuan3CuoiTuan = DateTime.ParseExact(txtTuan3CuoiTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<ThongKeKhoiLuong> list = BieuDoKhoiLuongController.ThongKeTuan(tuan1DauTuan, tuan1CuoiTuan, tuan2DauTuan, tuan2CuoiTuan, tuan3DauTuan, tuan3CuoiTuan);
            //display to grid
            //create datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            dt.Columns.Add("GiaDC1", typeof(decimal));
            dt.Columns.Add("GiaDC2", typeof(decimal));
            dt.Columns.Add("GiaDC3", typeof(decimal));            
            dt.Columns.Add("ChiSo12", typeof(decimal));
            dt.Columns.Add("ChiSo23", typeof(decimal));
            dt.Columns.Add("ChiSo13", typeof(decimal));
            dt.Columns.Add("SoCapLech", typeof(decimal));
            gridKQLoc.DataSource = null;
            int i = 1;
            foreach (var item in list)
            {
                decimal chiSo12 = Math.Abs (item.GiaDongCua1 - item.GiaDongCua2) / Math.Max (item.GiaDongCua1, item.GiaDongCua2 ) * 100;
                decimal chiSo23 = Math.Abs(item.GiaDongCua2 - item.GiaDongCua3) / Math.Max(item.GiaDongCua2, item.GiaDongCua3) * 100;
                decimal chiSo13 = Math.Abs(item.GiaDongCua1 - item.GiaDongCua3) / Math.Max(item.GiaDongCua1, item.GiaDongCua3) * 100;
                int chiSo = 0;
                if (chiSo12 <= 1)
                {
                    chiSo += 1;
                }
                if (chiSo23 <= 1)
                {
                    chiSo += 1;
                }
                if (chiSo13 <= 1)
                {
                    chiSo += 1;
                }
                if (chiSo >0 && (item.GiaDongCua3 >= item.GiaDongCua2 || item.GiaDongCua3 >= item.GiaMoCua3 ) )
                {
                    DataRow dr = dt.NewRow();
                    dr["MaCK"] = item.MaChungKhoan;
                    dr["GiaDC1"] = item.GiaDongCua1;
                    dr["GiaDC2"] = item.GiaDongCua2;
                    dr["GiaDC3"] = item.GiaDongCua3;                    
                    dr["ChiSo12"] = Math.Round(chiSo12 , 2, MidpointRounding.AwayFromZero);
                    dr["ChiSo23"] = Math.Round(chiSo23, 2, MidpointRounding.AwayFromZero);
                    dr["ChiSo13"] = Math.Round(chiSo13, 2, MidpointRounding.AwayFromZero);
                    dr["SoCapLech"] = chiSo;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();  
            //order by ChiSo desc
            DataView dv = dt.DefaultView;
            dv.Sort = "SoCapLech desc";
            DataTable sortedDT = dv.ToTable();
            // Finally, set the DataSource of the DataGridView to the sorted DataTable
            gridKQLoc.DataSource = sortedDT;
            //invisible column ChiSo
            //gridKQLoc.Columns["ChiSo"].Visible = false;
            //fix first column when scroll
            gridKQLoc.Columns[0].Frozen = true;
        }

        private void btnDMChungKhoanQuanTam_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            List<DMQuanTam> list = DMQuanTamController.GetAll();
            //display to grid
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("MaCK", typeof(string));
            int i = 1;
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                dr["STT"] = i;
                dr["MaCK"] = item.MaChungKhoan;
                dt.Rows.Add(dr);
                i++;
            }
            //order by MaCK
            DataView dv = dt.DefaultView;
            dv.Sort = "MaCK";
            DataTable sortedDT = dv.ToTable();
            // Finally, set the DataSource of the DataGridView to the sorted DataTable
            gridKQLoc.DataSource = sortedDT;
            //get number of rows
            groupBox2.Text = "Số mã chứng khoán quan tâm: " + (i - 1).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //xóa hết danh mục cũ
            DMQuanTamController.DeleteAll();
            if (MessageBox.Show("Dữ liệu chỉ có 1 cột là Mã chứng khoán và bắt đầu từ dòng thứ 1 (Không có tiêu đề). Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        int soDong = lastRowIndex;
                        //hiển thị progress bar
                        progressBar1.Visible = true;
                        progressBar1.Maximum = soDong;
                        progressBar1.Step = 1;

                        // Lặp qua từng hàng trong SheetData
                        for (int i = 1; i <= lastRowIndex; i++)
                        {
                            Row row = sheetData.Elements<Row>().ElementAtOrDefault(i - 1); // -1 vì index bắt đầu từ 0
                            progressBar1.Value = i;
                            System.Windows.Forms.Application.DoEvents();    
                            DMQuanTam obj = new DMQuanTam();
                            //mã chứng khoán
                            Cell cellA = row.Elements<Cell>().ElementAtOrDefault(0);
                            string myString = GetCellValue(cellA, workbookPart);
                            obj.MaChungKhoan = myString.Trim().ToUpper();
                            DMQuanTamController .Insert(obj);
                        }
                    }
                    MessageBox.Show("Chuyển số liệu xong");
                }
            }
        }

        private void btnThongKeNgay_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            DateTime ngay1 = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<ThongKeKhoiLuong> list = BieuDoKhoiLuongController.ThongKe3Ngay(ngay1, ngay2, ngay3);
            //display to grid
            //create datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            dt.Columns.Add("GiaMC1", typeof(decimal));
            dt.Columns.Add("GiaMC2", typeof(decimal));
            dt.Columns.Add("GiaMC3", typeof(decimal));
            dt.Columns.Add("GiaDC1", typeof(decimal));
            dt.Columns.Add("GiaDC2", typeof(decimal));
            dt.Columns.Add("GiaDC3", typeof(decimal));
            dt.Columns.Add("Nguong", typeof(decimal));
            gridKQLoc.DataSource = null;
            int i = 1;
            foreach (var item in list)
            {
                decimal min = Math.Min(item.GiaDongCua1, Math.Min(item.GiaDongCua2, item.GiaDongCua3));
                decimal nguong = Math.Abs(item.GiaDongCua3 - item.GiaDongCua2) / Math.Max(item.GiaDongCua3, item.GiaDongCua2) * 100;
                if (item.GiaDongCua1  > min && item.GiaDongCua3 >= item.GiaDongCua2 && item.GiaDongCua3 >= item.GiaMoCua3 )
                {
                    DataRow dr = dt.NewRow();
                    dr["MaCK"] = item.MaChungKhoan;
                    dr["GiaDC1"] = item.GiaDongCua1;
                    dr["GiaDC2"] = item.GiaDongCua2;
                    dr["GiaDC3"] = item.GiaDongCua3;
                    dr["GiaMC1"] = item.GiaMoCua1;
                    dr["GiaMC2"] = item.GiaMoCua2;
                    dr["GiaMC3"] = item.GiaMoCua3;
                    dr["Nguong"] = Math.Round(nguong, 2, MidpointRounding.AwayFromZero);
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();            
            gridKQLoc.DataSource = dt;
            //invisible column ChiSo
            //gridKQLoc.Columns["ChiSo"].Visible = false;
            //fix first column when scroll
            gridKQLoc.Columns[0].Frozen = true;
        }

        private void btnThongKeNgay2_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            DateTime ngay1 = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<ThongKeKhoiLuong> list = BieuDoKhoiLuongController.ThongKe3Ngay(ngay1, ngay2, ngay3);
            //display to grid
            //create datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            dt.Columns.Add("GiaDC1", typeof(decimal));
            dt.Columns.Add("GiaDC2", typeof(decimal));
            dt.Columns.Add("GiaMC3", typeof(decimal));
            dt.Columns.Add("GiaDC3", typeof(decimal));
            dt.Columns.Add("GiaDay", typeof(decimal));
            dt.Columns.Add("NgayDay", typeof(string));
            gridKQLoc.DataSource = null;
            int i = 1;
            foreach (var item in list)
            {
                string sNgayDay = "";
                decimal giaMin = 0;
                if (item.GiaDongCua2 <= Math.Min(item.GiaDongCua1 , item.GiaDongCua3 ))
                {
                    giaMin = item.GiaDongCua2;
                    sNgayDay = "Ngày 2";
                }
                else if (item.GiaDongCua3 <= Math.Min(item.GiaDongCua1, item.GiaDongCua2))
                {
                    giaMin = item.GiaDongCua3 ;
                    sNgayDay = "Ngày 3";
                }
                if (giaMin!=0 && item.GiaDongCua3>= item.GiaMoCua3)
                {
                    DataRow dr = dt.NewRow();
                    dr["MaCK"] = item.MaChungKhoan;
                    dr["GiaDC1"] = item.GiaDongCua1;
                    dr["GiaDC2"] = item.GiaDongCua2;
                    dr["GiaMC3"] = item.GiaMoCua3;
                    dr["GiaDC3"] = item.GiaDongCua3;
                    dr["GiaDay"] = giaMin;
                    dr["NgayDay"] = sNgayDay;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();
            gridKQLoc.DataSource = dt;
            //invisible column ChiSo
            //gridKQLoc.Columns["ChiSo"].Visible = false;
            //fix first column when scroll
            gridKQLoc.Columns[0].Frozen = true;
        }

        private void btnLocNhanh1_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            DateTime ngay1 = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<ThongKeKhoiLuong> list = BieuDoKhoiLuongController.ThongKe3Ngay(ngay1, ngay2, ngay3);
            //display to grid
            //create datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            dt.Columns.Add("GiaMC1", typeof(decimal));
            dt.Columns.Add("GiaDC1", typeof(decimal));
            dt.Columns.Add("GiaMC2", typeof(decimal));
            dt.Columns.Add("GiaDC2", typeof(decimal));
            dt.Columns.Add("GiaMC3", typeof(decimal));
            dt.Columns.Add("GiaDC3", typeof(decimal));
            gridKQLoc.DataSource = null;
            int i = 1;
            foreach (var item in list)
            {
                //- Giá đóng cửa ngày 1 > Giá đóng cửa ngày 2 
                //-Giá đóng cửa ngày 2 < Giá mở cửa ngày 2
               // - Giá đóng cửa ngày 3 = giá mở cửa ngày 3 = giá đóng cửa ngày 2
                if (item.GiaDongCua1 > item.GiaDongCua2 &&
                    item.GiaDongCua2 < item.GiaMoCua2 &&
                    item.GiaDongCua3 == item.GiaMoCua3 &&
                    item.GiaDongCua3 == item.GiaDongCua2 )
                {
                    DataRow dr = dt.NewRow();
                    dr["MaCK"] = item.MaChungKhoan;
                    dr["GiaDC1"] = item.GiaDongCua1;
                    dr["GiaDC2"] = item.GiaDongCua2;                    
                    dr["GiaDC3"] = item.GiaDongCua3;
                    dr["GiaMC1"] = item.GiaMoCua1;
                    dr["GiaMC2"] = item.GiaMoCua2;
                    dr["GiaMC3"] = item.GiaMoCua3;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();
            gridKQLoc.DataSource = dt;
            //invisible column ChiSo
            //gridKQLoc.Columns["ChiSo"].Visible = false;
            //fix first column when scroll
            gridKQLoc.Columns[0].Frozen = true;
        }

        private void btnLocNhanh2_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            DateTime ngay1 = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<ThongKeKhoiLuong> list = BieuDoKhoiLuongController.ThongKe3Ngay(ngay1, ngay2, ngay3);
            //display to grid
            //create datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            dt.Columns.Add("GiaMC1", typeof(decimal));
            dt.Columns.Add("GiaDC1", typeof(decimal));
            dt.Columns.Add("GiaMax1", typeof(decimal));
            dt.Columns.Add("GiaMin1", typeof(decimal));
            dt.Columns.Add("GiaMC2", typeof(decimal));
            dt.Columns.Add("GiaDC2", typeof(decimal));
            dt.Columns.Add("GiaMax2", typeof(decimal));
            dt.Columns.Add("GiaMin2", typeof(decimal));
            dt.Columns.Add("GiaMC3", typeof(decimal));
            dt.Columns.Add("GiaDC3", typeof(decimal));
            dt.Columns.Add("GiaMax3", typeof(decimal));
            dt.Columns.Add("GiaMin3", typeof(decimal));
            gridKQLoc.DataSource = null;
            int i = 1;
            foreach (var item in list)
            {
                //- Giá cao nhất ngày 1 < giá cao nhất ngày 2 
                //-Giá đóng cửa ngày 1 < giá đóng cửa ngày 2
                //- Giá đóng cửa ngày 2 > giá mở cửa ngày 2
                //- Giá đóng cửa ngày 3 < Giá mở cửa ngày 3
                //- Giá đóng cửa ngày 3 > max(giá đóng cửa ngày 1, giá mở cửa ngày 2)
                if (item.GiaCaoNhat1 < item.GiaCaoNhat2 &&
                    item.GiaDongCua1 < item.GiaDongCua2 &&
                    item.GiaDongCua2 > item.GiaMoCua2 &&
                    item.GiaDongCua3 < item.GiaDongCua3 &&
                    item.GiaDongCua3 > Math.Max (item.GiaDongCua1, item.GiaMoCua2 ))
                {
                    DataRow dr = dt.NewRow();
                    dr["MaCK"] = item.MaChungKhoan;
                    dr["GiaDC1"] = item.GiaDongCua1;
                    dr["GiaDC2"] = item.GiaDongCua2;
                    dr["GiaDC3"] = item.GiaDongCua3;
                    dr["GiaMC1"] = item.GiaMoCua1;
                    dr["GiaMC2"] = item.GiaMoCua2;
                    dr["GiaMC3"] = item.GiaMoCua3;
                    dr["GiaMax1"] = item.GiaCaoNhat1;
                    dr["GiaMin1"] = item.GiaThapNhat1;
                    dr["GiaMax2"] = item.GiaCaoNhat2;
                    dr["GiaMin2"] = item.GiaThapNhat2;
                    dr["GiaMax3"] = item.GiaCaoNhat3;
                    dr["GiaMin3"] = item.GiaThapNhat3;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();
            gridKQLoc.DataSource = dt;
            //invisible column ChiSo
            //gridKQLoc.Columns["ChiSo"].Visible = false;
            //fix first column when scroll
            gridKQLoc.Columns[0].Frozen = true;
        }

        private void btnLocNhanh3_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //check if txtTuan1DauTuan, txtTuan2DauTuan, txtTuan3DauTuan, txtTuan1CuoiTuan, txtTuan2CuoiTuan, txtTuan3CuoiTuan is not empty and is date
            if (txtTuan1DauTuan.Text == "" || txtTuan2DauTuan.Text == "" || txtTuan3DauTuan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
                return;
            }
            DateTime ngay1 = DateTime.ParseExact(txtTuan1DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay2 = DateTime.ParseExact(txtTuan2DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ngay3 = DateTime.ParseExact(txtTuan3DauTuan.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<ThongKeKhoiLuong> list = BieuDoKhoiLuongController.ThongKe3Ngay(ngay1, ngay2, ngay3);
            //display to grid
            //create datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            dt.Columns.Add("GiaMC1", typeof(decimal));
            dt.Columns.Add("GiaDC1", typeof(decimal));
            dt.Columns.Add("GiaMC2", typeof(decimal));
            dt.Columns.Add("GiaDC2", typeof(decimal));
            dt.Columns.Add("GiaMC3", typeof(decimal));
            dt.Columns.Add("GiaDC3", typeof(decimal));
            dt.Columns.Add("LechNgay3", typeof(decimal));
            gridKQLoc.DataSource = null;
            int i = 1;
            foreach (var item in list)
            {
                //-(| giá đóng cửa ngày 3 - giá mở cửa ngày 3 | / max(giá mở cửa ngày 3, giá đóng cửa ngày 3) ) *100 <= 1
                //- Giá đóng cửa ngày 2 > Giá mở cửa ngày 2
                //- Giá đóng cửa ngày 2 < min(giá mở cửa ngày 3, giá đóng cửa ngày 3)
                decimal lech = Math.Abs(item.GiaDongCua3 - item.GiaMoCua3) / Math.Max(item.GiaDongCua3, item.GiaMoCua3) * 100;
                if (item.GiaDongCua2 > item.GiaMoCua2 &&
                    item.GiaDongCua2 < Math.Min(item.GiaMoCua3, item.GiaDongCua3) &&
                    lech <=1)
                {
                    DataRow dr = dt.NewRow();
                    dr["MaCK"] = item.MaChungKhoan;
                    dr["GiaDC1"] = item.GiaDongCua1;
                    dr["GiaDC2"] = item.GiaDongCua2;
                    dr["GiaDC3"] = item.GiaDongCua3;
                    dr["GiaMC1"] = item.GiaMoCua1;
                    dr["GiaMC2"] = item.GiaMoCua2;
                    dr["GiaMC3"] = item.GiaMoCua3;
                    dr["LechNgay3"] = Math.Round(lech, 2, MidpointRounding.AwayFromZero);
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            groupBox2.Text = "Số cổ phiếu thỏa mãn: " + (i - 1).ToString();
            gridKQLoc.DataSource = dt;
            //invisible column ChiSo
            //gridKQLoc.Columns["ChiSo"].Visible = false;
            //fix first column when scroll
            gridKQLoc.Columns[0].Frozen = true;
        }
    }
}
