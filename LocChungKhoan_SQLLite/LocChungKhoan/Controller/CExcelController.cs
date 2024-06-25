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
                        Cell cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(dgvCell.Value?.ToString())
                        };
                        newRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }
        }
    }
}
