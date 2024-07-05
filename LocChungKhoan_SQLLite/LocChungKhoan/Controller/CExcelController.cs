using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System.Windows.Forms;

namespace LocChungKhoan
{
    public static class CExcelController
    {
        public static void ExportDataGridViewToExcel(DataGridView dgv, string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"
                };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Tạo hàng tiêu đề
                Row headerRow = new Row();
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    Cell cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.HeaderText)
                    };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                // Tạo các hàng dữ liệu
                foreach (DataGridViewRow dgvRow in dgv.Rows)
                {
                    Row newRow = new Row();
                    foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                    {
                        Cell cell;
                        if (dgvCell.Value != null)
                        {
                            if (dgvCell.Value is decimal)
                            {
                                cell = CreateCellNumber((decimal)dgvCell.Value);
                            }
                            else
                            {
                                cell = CreateCellText(dgvCell.Value.ToString());
                            }
                        }
                        else
                        {
                            cell = CreateCellText(string.Empty);
                        }
                        newRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }
        private static Cell CreateCellText(string value)
        {
            return new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(value)
            };
        }
        private static Cell CreateCellNumber(decimal value)
        {
            return new Cell
            {
                DataType = CellValues.Number,
                CellValue = new CellValue(value)
            };
        }
        public static void ExportListThongKeKhoiLuongToExcel(List<ThongKeKhoiLuong> list, string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"
                };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Tạo hàng tiêu đề
                // Tạo hàng tiêu đề
                Row headerRow = new Row();
                string[] headers = {
                    "MaChungKhoan", "GiaDongCua1", "GiaDongCua2", "GiaDongCua3",
                    "GiaMoCua1", "GiaMoCua2", "GiaMoCua3", "KhoiLuong1", "KhoiLuong2", "KhoiLuong3",
                    "GiaCaoNhat1", "GiaCaoNhat2", "GiaCaoNhat3", "GiaThapNhat1", "GiaThapNhat2", "GiaThapNhat3"
                };

                foreach (string header in headers)
                {
                    Cell cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(header)
                    };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                // Tạo các hàng dữ liệu
                // Tạo các hàng dữ liệu
                foreach (ThongKeKhoiLuong item in list)
                {
                    Row newRow = new Row();

                    newRow.AppendChild(CreateCellText(item.MaChungKhoan));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua1));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua2));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua3));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua1));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua2));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua3));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong1));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong2));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong3));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat1));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat2));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat3));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat1));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat2));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat3));

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }
        public static void Export4NgayToExcel(List<ThongKe4Ngay> list, string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"
                };
                sheets.Append(sheet);

                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Tạo hàng tiêu đề
                // Tạo hàng tiêu đề
                Row headerRow = new Row();
                string[] headers = {
                    "MaChungKhoan", "GiaDongCua1", "GiaDongCua2", "GiaDongCua3", "GiaDongCua4",
                    "GiaMoCua1", "GiaMoCua2", "GiaMoCua3", "GiaMoCua4",
                    "KhoiLuong1", "KhoiLuong2", "KhoiLuong3", "KhoiLuong4",
                    "GiaCaoNhat1", "GiaCaoNhat2", "GiaCaoNhat3", "GiaCaoNhat4",
                    "GiaThapNhat1", "GiaThapNhat2", "GiaThapNhat3", "GiaThapNhat4"
                };

                foreach (string header in headers)
                {
                    Cell cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(header)
                    };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                // Tạo các hàng dữ liệu
                // Tạo các hàng dữ liệu
                foreach (ThongKe4Ngay item in list)
                {
                    Row newRow = new Row();

                    newRow.AppendChild(CreateCellText(item.MaChungKhoan));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua1));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua2));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua3));
                    newRow.AppendChild(CreateCellNumber(item.GiaDongCua4));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua1));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua2));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua3));
                    newRow.AppendChild(CreateCellNumber(item.GiaMoCua4));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong1));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong2));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong3));
                    newRow.AppendChild(CreateCellNumber(item.KhoiLuong4));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat1));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat2));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat3));
                    newRow.AppendChild(CreateCellNumber(item.GiaCaoNhat4));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat1));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat2));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat3));
                    newRow.AppendChild(CreateCellNumber(item.GiaThapNhat4));

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }
    }
}
