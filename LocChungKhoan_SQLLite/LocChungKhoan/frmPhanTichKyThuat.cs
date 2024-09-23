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
using static System.Net.WebRequestMethods;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Security.Policy;
using DocumentFormat.OpenXml;

namespace LocChungKhoan
{
    public partial class frmPhanTichKyThuat : Form
    {
        public frmPhanTichKyThuat()
        {
            InitializeComponent();
            GlobalVar.ConnectString = AppContext.BaseDirectory + "ChungKhoan.db";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dữ liệu theo format của Vietstock theo link: https://finance.vietstock.vn/ket-qua-giao-dich. Tuy nhiên, phải chuyển sang định dạng file .xlsx. Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                //open file dialog with .xlsx filter


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
                        int lastRowIndex = GetLastRowIndexVietStockFile(sheetData );
                        int soDong = lastRowIndex - 17+1;
                        //hiển thị progress bar
                        progressBar1.Visible = true;
                        progressBar1.Maximum = soDong;
                        progressBar1.Step = 1;

                        // Lặp qua từng hàng trong SheetData
                        for (int i = 17; i <= lastRowIndex; i++)
                        {
                            try
                            {
                                Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == i);  // -1 vì index bắt đầu từ 0
                                progressBar1.Value = i - 17 + 1;
                                System.Windows.Forms.Application.DoEvents();
                                //kiểm tra mã chứng khoán có chưa
                                //mã chứng khoán ở cột D
                                Cell cellD = row.Elements<Cell>().ElementAtOrDefault(2);
                                string name = GetCellValue(cellD, workbookPart);
                                BieuDoKhoiLuong obj = new BieuDoKhoiLuong();
                                obj.MaChungKhoan = name.Trim();
                                //ngày
                                Cell cellC = row.Elements<Cell>().ElementAtOrDefault(1);
                                //get date from cell C
                                // Date value is stored as a number in Excel
                                double excelDate = double.Parse(cellC.InnerText);
                                obj.Ngay = DateTime.FromOADate(excelDate);

                                //string myString = GetCellValue(cellC, workbookPart);
                                //obj.Ngay = DateTime.ParseExact(myString, "M/d/yyyy", CultureInfo.InvariantCulture);
                                //giá mở cửa
                                Cell cellF = row.Elements<Cell>().ElementAtOrDefault(4);
                                string myString = GetCellValue(cellF, workbookPart);
                                obj.GiaMoCua = Convert.ToDecimal(myString);
                                //giá đong cửa
                                Cell cellG = row.Elements<Cell>().ElementAtOrDefault(5);
                                myString = GetCellValue(cellG, workbookPart);
                                obj.GiaDongCua = Convert.ToDecimal(myString);
                                //----------các giá trị tùy chọn
                                //giá cao nhất
                                Cell cellH = row.Elements<Cell>().ElementAtOrDefault(6);
                                myString = GetCellValue(cellH, workbookPart);
                                if (myString != "")
                                {
                                    obj.GiaCaoNhat = Convert.ToDecimal(myString);
                                }
                                else
                                {
                                    obj.GiaCaoNhat = 0;
                                }
                                //giá thấp nhất
                                Cell cellI = row.Elements<Cell>().ElementAtOrDefault(7);
                                myString = GetCellValue(cellI, workbookPart);
                                if (myString != "")
                                {
                                    obj.GiaThapNhat = Convert.ToDecimal(myString);
                                }
                                else
                                {
                                    obj.GiaThapNhat = 0;
                                }
                                //khối lượng
                                Cell cellQ = row.Elements<Cell>().ElementAtOrDefault(15);
                                myString = GetCellValue(cellQ, workbookPart);
                                if (myString != "")
                                {
                                    obj.KhoiLuong = Convert.ToDecimal(myString);
                                }
                                else
                                {
                                    obj.KhoiLuong = 0;
                                }
                                //-------------hết đoạn tùy chọn
                                BieuDoKhoiLuongController.Insert(obj);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi ở dòng " + i + " " + ex.Message);
                            }                            
                        }
                    }
                    MessageBox.Show("Chuyển số liệu xong");                    
                }
            }

        }
        public static int GetLastRowIndexVietStockFile(SheetData sheetData)
        {
            int rowIndex = 17; // Start from row 17
            int lastRowIndex = 0;

            // Iterate through rows starting from row 17
            foreach (Row row in sheetData.Elements<Row>())
            {
                if (row.RowIndex < rowIndex)
                    continue; // Skip rows before row 17

                Cell cellB = row.Elements<Cell>().FirstOrDefault(c => c.CellReference.Value.StartsWith("B"));
                if (cellB != null && !string.IsNullOrEmpty(cellB.InnerText))
                {
                    lastRowIndex = (int)row.RowIndex.Value;
                }
                else
                {
                    // Break loop when column B is empty
                    break;
                }
            }

            return lastRowIndex;
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
            
        }      

        private void frmMainKhoiLuong_Load(object sender, EventArgs e)
        {
            txtTuNgay.Text = DateTime.Now.ToString("d/M/yyyy");
            //hướng dẫn cho các textbox
            string toolTiplblDsMaCK = "Danh sách mã chứng khoán cách nhau bởi dấu ;";
            System.Windows.Forms.ToolTip toolTipDsMaCK = new System.Windows.Forms.ToolTip();
            toolTipDsMaCK.SetToolTip(lblMaCKQuanTam, toolTiplblDsMaCK);

            string stoolTipNguongPivot = "Phần % tăng giá của cây nến đạt chuẩn Pivot, chính là % chênh lệch giữa giá đóng cửa và giá mở cửa. Với nến Pivot, % này càng cao càng tốt, nhưng thường là 2%";
            System.Windows.Forms.ToolTip toolTipNguongPivot = new System.Windows.Forms.ToolTip();
            toolTipNguongPivot.SetToolTip(lblTangGiaPivot, stoolTipNguongPivot);

            string stoolTipNgayPivot = "Số ngày xét để tìm phiên Pivot, tính lùi từ ngày tối đa đã thiết lập.";
            System.Windows.Forms.ToolTip toolTipNgayPivot = new System.Windows.Forms.ToolTip();
            toolTipNgayPivot.SetToolTip(lblPhienPivot, stoolTipNgayPivot);


            string stoolTipNguongSupply = "Phần % chênh lệch giữa giá đóng cửa và mở cửa. Với nến No supply, % này càng nhỏ càng tốt.";
            System.Windows.Forms.ToolTip toolTipNguongSupply = new System.Windows.Forms.ToolTip();
            toolTipNguongSupply.SetToolTip(lblThanNenSupply, stoolTipNguongSupply);

            string stoolTipNgaySupply = "Số ngày xét để tìm phiên No supply, tính lùi từ ngày tối đa đã thiết lập.";
            System.Windows.Forms.ToolTip toolTipNgaySupply = new System.Windows.Forms.ToolTip();
            toolTipNgaySupply.SetToolTip(lblPhienSupply , stoolTipNgaySupply);
        }
     

        private float CurrentDpiScalingFactorX()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return graphics.DpiX / 96f;
            }
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

      

        private void btnMoThuMuc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chức năng này sẽ import dữ liệu từ tất cả các file excel .xlsx trong thư mục được chọn. Dữ liệu theo format của Vietstock theo link: https://finance.vietstock.vn/ket-qua-giao-dich. Tuy nhiên, phải chuyển sang định dạng file .xlsx.. Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                            int lastRowIndex = GetLastRowIndexVietStockFile(sheetData);
                            int soDong = lastRowIndex - 17 + 1;
                            //hiển thị progress bar
                            progressBar1.Visible = true;
                            progressBar1.Maximum = soDong;
                            progressBar1.Step = 1;

                            // Lặp qua từng hàng trong SheetData
                            for (int i = 17; i <= lastRowIndex; i++)
                            {
                                try
                                {
                                    Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == i);  // -1 vì index bắt đầu từ 0
                                    progressBar1.Value = i - 17 + 1;
                                    System.Windows.Forms.Application.DoEvents();
                                    //kiểm tra mã chứng khoán có chưa
                                    //mã chứng khoán ở cột D
                                    Cell cellD = row.Elements<Cell>().ElementAtOrDefault(2);
                                    string name = GetCellValue(cellD, workbookPart);
                                    BieuDoKhoiLuong obj = new BieuDoKhoiLuong();
                                    obj.MaChungKhoan = name.Trim();
                                    //ngày
                                    Cell cellC = row.Elements<Cell>().ElementAtOrDefault(1);
                                    //get date from cell C
                                    // Date value is stored as a number in Excel
                                    double excelDate = double.Parse(cellC.InnerText);
                                    obj.Ngay = DateTime.FromOADate(excelDate);

                                    //string myString = GetCellValue(cellC, workbookPart);
                                    //obj.Ngay = DateTime.ParseExact(myString, "M/d/yyyy", CultureInfo.InvariantCulture);
                                    //giá mở cửa
                                    Cell cellF = row.Elements<Cell>().ElementAtOrDefault(4);
                                    string myString = GetCellValue(cellF, workbookPart);
                                    obj.GiaMoCua = Convert.ToDecimal(myString);
                                    //giá đong cửa
                                    Cell cellG = row.Elements<Cell>().ElementAtOrDefault(5);
                                    myString = GetCellValue(cellG, workbookPart);
                                    obj.GiaDongCua = Convert.ToDecimal(myString);
                                    //----------các giá trị tùy chọn
                                    //giá cao nhất
                                    Cell cellH = row.Elements<Cell>().ElementAtOrDefault(6);
                                    myString = GetCellValue(cellH, workbookPart);
                                    if (myString != "")
                                    {
                                        obj.GiaCaoNhat = Convert.ToDecimal(myString);
                                    }
                                    else
                                    {
                                        obj.GiaCaoNhat = 0;
                                    }
                                    //giá thấp nhất
                                    Cell cellI = row.Elements<Cell>().ElementAtOrDefault(7);
                                    myString = GetCellValue(cellI, workbookPart);
                                    if (myString != "")
                                    {
                                        obj.GiaThapNhat = Convert.ToDecimal(myString);
                                    }
                                    else
                                    {
                                        obj.GiaThapNhat = 0;
                                    }
                                    //khối lượng
                                    Cell cellQ = row.Elements<Cell>().ElementAtOrDefault(15);
                                    myString = GetCellValue(cellQ, workbookPart);
                                    if (myString != "")
                                    {
                                        obj.KhoiLuong = Convert.ToDecimal(myString);
                                    }
                                    else
                                    {
                                        obj.KhoiLuong = 0;
                                    }
                                    //-------------hết đoạn tùy chọn
                                    BieuDoKhoiLuongController.Insert(obj);

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi ở dòng " + i + ", file  " + file + ex.Message);
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Chuyển số liệu xong");
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
            System.Data.DataTable sortedDT = dv.ToTable();
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
        private void danhMụcChứngKhoánQuanTâmToolStripMenuItem_Click(object sender, EventArgs e)
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
            System.Data.DataTable sortedDT = dv.ToTable();
            // Finally, set the DataSource of the DataGridView to the sorted DataTable
            gridKQLoc.DataSource = sortedDT;
            //get number of rows
            groupBox2.Text = "Số mã chứng khoán quan tâm: " + (i - 1).ToString();
        }

        private void kiểmTraDữLiệuHiệnCóToolStripMenuItem_Click(object sender, EventArgs e)
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
                dr["Ngay"] = item.ToString("d/M/yyyy");
                dt.Rows.Add(dr);
                i++;
            }
            gridKQLoc.DataSource = dt;
            //get number of rows
            groupBox2.Text = "Số ngày có dữ liệu: " + (i - 1).ToString();
        }

        
        private void xemDữLiệuCủa1MãCụThểToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //hiển thị form frmChiTietMaCK
            frmChiTietMaCK frm = new frmChiTietMaCK();
            frm.ShowDialog();
            //lấy thông tin từ form frmChiTietMaCK
            //hiển thị thông tin lên gridKQLoc
            if (frm.DialogResult == DialogResult.OK)
            {
                gridKQLoc.DataSource = null;
                //get data from frmChiTietMaCK
                DateTime ngayBD = frm.NgayBD;
                DateTime ngayKT = frm.NgayKT;
                string maCK = frm.MaCK.ToUpper().Trim ();
                //display to grid
                //create datatable
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Ngay", typeof(string));
                dt.Columns.Add("GiaMC", typeof(decimal));
                dt.Columns.Add("GiaDC", typeof(decimal));
                dt.Columns.Add("GiaMax", typeof(decimal));
                dt.Columns.Add("GiaMin", typeof(decimal));
                dt.Columns.Add("KhoiLuong", typeof(decimal));
                gridKQLoc.DataSource = null;
                List<BieuDoKhoiLuong> list = BieuDoKhoiLuongController.GetAllByMaCK (ngayBD, ngayKT, maCK);
                int i = 1;
                foreach (var item in list)
                {
                    DataRow dr = dt.NewRow();
                    dr["Ngay"] = item.Ngay.ToString("d/M/yyyy");
                    dr["GiaMC"] = item.GiaMoCua;
                    dr["GiaDC"] = item.GiaDongCua;
                    dr["GiaMax"] = item.GiaCaoNhat;
                    dr["GiaMin"] = item.GiaThapNhat;
                    dr["KhoiLuong"] = item.KhoiLuong;
                    dt.Rows.Add(dr);
                    i++;
                }                
                //order by Ngay
                DataView dv = dt.DefaultView;
                dv.Sort = "Ngay";
                System.Data.DataTable sortedDT = dv.ToTable();
                // Finally, set the DataSource of the DataGridView to the sorted DataTable
                gridKQLoc.DataSource = sortedDT;
                //format column KhoiLuong with 0 decimal
                gridKQLoc.Columns["KhoiLuong"].DefaultCellStyle.Format = "N0";
                //get number of rows
                groupBox2.Text = "Thông tin chi tiết mã: " + maCK;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            gridKQLoc.DataSource = null;
            //lấy ra ngày bắt đầu
            DateTime ngayBD = DateTime.ParseExact(txtTuNgay.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
            int soNgay = Convert.ToInt32(txtSoNgay.Text);
            if (soNgay < 17 && chkSO.Checked)
            {
                MessageBox.Show("Số ngày phải lớn hơn 17 mới dùng được chức năng lọc theo SO");
                return;
            }
            if (soNgay < 14 && chkRSI.Checked)
            {
                MessageBox.Show("Số ngày phải lớn hơn 14 mới dùng được chức năng lọc theo RSI");
                return;
            }
            if (chkMACD.Checked && soNgay<28)
            {
                MessageBox.Show("Nếu bạn muốn kiểm tra xu hướng dựa trên MACD, bạn cần ít nhất 2 dữ liệu nữa để so sánh giữa hai ngày cuối cùng (để xác định sự cắt nhau giữa đường MACD và đường tín hiệu). Như vậy, tổng cộng, bạn sẽ cần ít nhất 28 (26 là chu kỳ dài + 2 ngày) dữ liệu giá đóng cửa để thực hiện phân tích xu hướng dựa trên MACD.");
                return;
            }
            decimal nguongKhoiLuong = Convert.ToDecimal(txtNguongKhoiLuong.Text)/100.0m;
            int soNgayXetKhoiLuong = Convert.ToInt32(txtSoNgayXetTimKL.Text);
            //lấy ra danh sách các cổ phiếu
            List<BieuDoKhoiLuong> duLieu = BieuDoKhoiLuongController.GetAllByDays(soNgay, ngayBD);
            List<string> coPhieuDaLoc = new List<string>(); 
            if (txtMaQuanTam.Text != "")
            {
                //lấy danh sách từ textbox
                string[] maCKs = txtMaQuanTam.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in maCKs)
                {
                    coPhieuDaLoc.Add(item.Trim().ToUpper());
                }
            }
            else
            {
                if (chkDanhMucChuMinh.Checked)
                {
                    coPhieuDaLoc = BieuDoKhoiLuongController.GetListMaCK();
                }
                else
                {
                    decimal volMin = Convert.ToDecimal(txtKLGDMin.Text);
                    coPhieuDaLoc = BieuDoKhoiLuongController.GetListMaCKThanh(10.0m, volMin);
                }
            }            
            duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
            //lọc dữ liệu
            StockAnalyzer stockAnalyzer = new StockAnalyzer();
            //bắt đầu lọc theo các yêu cầu
            if (chkKietCung.Checked)
            {
                decimal nguongKietCung = Convert.ToDecimal(txtNguongKietCung.Text);
                int soNgayXet = Convert.ToInt32(txtNgaySupply.Text);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoNoSupplyBar (duLieu,nguongKietCung,soNgayXet );
            }
            if (coPhieuDaLoc.Count>0 && chkBienDongKhoiLuong.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuBienDongKhoiLuong(duLieu, ngayBD, nguongKhoiLuong,soNgayXetKhoiLuong, false);
            }
            if (coPhieuDaLoc.Count > 0 && chkTangQuyetLiet.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuBienDongKhoiLuong(duLieu, ngayBD, nguongKhoiLuong, soNgayXetKhoiLuong, true);
            }
            if (coPhieuDaLoc.Count > 0 && chkMACD.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTangTheoMACD(duLieu);
            }
            if (coPhieuDaLoc.Count > 0 && chkRSI.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                int rsiMin = Convert.ToInt32(txtRSI_from.Text);
                int rsiMax = Convert.ToInt32(txtRSI_To.Text);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoRSI (duLieu, rsiMin, rsiMax, soNgay);
            }
            if (coPhieuDaLoc.Count > 0 && chkSO .Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTangTheoStochasticOscillator(duLieu);
            }
            if (coPhieuDaLoc.Count > 0 && chkNen_BullishEngulfing.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoNenNhat(duLieu, StockAnalyzer.MauHinhNen.BullishEngulfing);
            }
            if (coPhieuDaLoc.Count > 0 && chkNen_Hammer.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoNenNhat(duLieu, StockAnalyzer.MauHinhNen.Hammer);
            }
            if (coPhieuDaLoc.Count > 0 && chkNen_PiercingPartern.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoNenNhat(duLieu, StockAnalyzer.MauHinhNen.PiercingLine);
            }
            if (coPhieuDaLoc.Count > 0 && chkMorningStar.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoNenNhat(duLieu, StockAnalyzer.MauHinhNen.MorningStar);
            }
            if (coPhieuDaLoc.Count > 0 && chkPivot.Checked)
            {
                duLieu = BieuDoKhoiLuongController.GetFilter(duLieu, coPhieuDaLoc);
                decimal nguongPivot = Convert.ToDecimal(txtNguongPivot.Text);
                int soNgayXet = Convert.ToInt32(txtNgayPivot.Text);
                coPhieuDaLoc = stockAnalyzer.LayCoPhieuTheoPivotPocket(duLieu, nguongPivot,soNgayXet);
            }
            //hiển thị danh sách coPhieuDaLoc lên gridKQLoc
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("MaCK", typeof(string));
            foreach (var item in coPhieuDaLoc)
            {
                DataRow dr = dt.NewRow();
                dr["MaCK"] = item;
                dt.Rows.Add(dr);
            }
            //order by MaCK
            DataView dv = dt.DefaultView;
            dv.Sort = "MaCK";
            System.Data.DataTable sortedDT = dv.ToTable();
            // Finally, set the DataSource of the DataGridView to the sorted DataTable
            gridKQLoc.DataSource = sortedDT;
            //hiển thi số lượng mã chứng khoán đã lọc
            groupBox2.Text = "Số mã chứng khoán đã lọc: " + coPhieuDaLoc.Count.ToString();
        }

        private void btnXuatRaExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sfd.FileName;
                    CExcelController.ExportDataGridViewToExcel(gridKQLoc, filePath);
                    MessageBox.Show("Xuất file excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }        
    }
}
