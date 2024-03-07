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
    public partial class frmLocTheoNgay : Form
    {
        public frmLocTheoNgay()
        {
            InitializeComponent();
            GlobalVar.ConnectString = AppContext.BaseDirectory + "ChungKhoan.db";
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void btnTab1_Click(object sender, EventArgs e)
        {
            ImportFromExcel(1);
        }
        private void ImportFromExcel(int phanLoai)
        {
            if (MessageBox.Show("Dữ liệu mã chứng khoán ở cột 1. Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //mở file excel
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //delete all data from TABChungKhoan by PhanLoai 
                    TABCKController.DeleteAll(phanLoai);
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
                            TABChungKhoan obj = new TABChungKhoan();
                            obj.MaChungKhoan = name.Trim();
                            obj.PhanLoai = phanLoai;
                            TABCKController.Insert(obj);
                        }
                    }
                    var list = TABCKController.GetAll(phanLoai);
                    if (phanLoai == 1)
                    {
                        gridTAB1.DataSource = list;
                        FormatGrid(gridTAB1);
                    }
                    else if (phanLoai == 2)
                    {
                        gridTAB2.DataSource = list;
                        FormatGrid(gridTAB2);
                    }
                    else if (phanLoai == 3)
                    {
                        gridTAB3.DataSource = list;
                        FormatGrid(gridTAB3);
                    }
                    MessageBox.Show("chuyển số liệu xong");
                }
            }
        }

        private void btnTab2_Click(object sender, EventArgs e)
        {
            ImportFromExcel(2);
        }

        private void btnTab3_Click(object sender, EventArgs e)
        {
            ImportFromExcel(3);
        }

        private void btnLocTheoNgay_Click(object sender, EventArgs e)
        {
            var list = TABCKController.ThongKe();
            //display data
            gridKQLoc.DataSource = list;
            FormatGrid(gridKQLoc);
        }
        private void FormatGrid(DataGridView grid)
        {
            grid.Columns[0].Visible = false;
            grid.Columns[1].HeaderText = "Mã CK";
            grid.Columns[2].Visible = false;            
        }

        private void frmLocTheoNgay_Load(object sender, EventArgs e)
        {
            //hiển thị dữ liệu đã có
            var list = TABCKController.GetAll(1);
            gridTAB1.DataSource = list;
            var list2 = TABCKController.GetAll(2);
            gridTAB2.DataSource = list2;
            var list3 = TABCKController.GetAll(3);
            gridTAB3.DataSource = list3;
            FormatGrid(gridTAB1);
            FormatGrid(gridTAB2);
            FormatGrid(gridTAB3);
        }
    }
}
