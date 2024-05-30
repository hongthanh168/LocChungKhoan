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

namespace LocChungKhoan
{
    public partial class frmPhanTichKyThuat : Form
    {
        public bool isLocKhoiLuong = true;
        public bool isLocGiaCaoNhatThapNhat = true;
        public bool isLocGiaDongCuaMoCua = false;
        public bool isLocDieuKienGiaTuan1 = false;
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
           
        }
        private void AdjustSplitterDistance()
        {
            float scaleFactorX = CurrentDpiScalingFactorX();

            // Adjust SplitContainer's SplitterDistance based on DPI scaling factor
            splitContainer1.SplitterDistance = (int)Math.Round(splitContainer1.Panel1.Height*scaleFactorX);
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
                dr["Ngay"] = item.ToString("dd/MM/yyyy");
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
                    dr["Ngay"] = item.Ngay.ToString("dd/MM/yyyy");
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
    }
}
