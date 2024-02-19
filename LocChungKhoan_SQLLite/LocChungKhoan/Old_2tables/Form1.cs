using Microsoft.Office.Interop.Excel;
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
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;

namespace LocChungKhoan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GlobalVar.ConnectString = AppContext.BaseDirectory + "ChungKhoan.db";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dữ liệu bắt đầu từ dòng thứ 2 (Dòng đầu là tiêu đề cột). Dữ liệu có cấu trúc cột theo thứ tự: Mã chứng khoán|Ngày (dd/MM/yyyy)|Giá đóng cửa (Số). Bạn có chắc chắn file sẽ chọn đúng cấu trúc?", "Xác nhận file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Excel.Application xlApp = null;
                Excel.Workbook wb = null;
                //Excel.Worksheet osheet = null;
                int lastUsedRow = 0;

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                //mở file excel
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtDuongDan.Text = openFileDialog1.FileName;
                    xlApp = new Excel.Application();
                    xlApp.Visible = false;
                    wb = ((Microsoft.Office.Interop.Excel.Application)xlApp).Workbooks.Open(openFileDialog1.FileName);
                    //lấy lần lượt từng sheet ra
                    foreach (Worksheet osheet in wb.Worksheets)
                    {
                        //osheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.get_Item(1);//lấy sheet đầu tiên
                        Microsoft.Office.Interop.Excel.Range range = osheet.UsedRange;

                        // Find the last real row
                        lastUsedRow = osheet.Cells.Find("*", System.Reflection.Missing.Value,
                                       System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                       Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious,
                                       false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;
                        int soDong = lastUsedRow - 2 + 1;
                        //hiển thị progress bar
                        progressBar1.Visible = true;
                        progressBar1.Maximum = soDong;
                        progressBar1.Step = 1;
                        for (int i = 2; i <= lastUsedRow; i++) //NHỚ SỬA Ở ĐÂY
                        {
                            progressBar1.Value = i - 2 + 1;
                            System.Windows.Forms.Application.DoEvents();                            
                            //kiểm tra mã chứng khoán có chưa
                            string name = ((Excel.Range)osheet.Cells[i, 1]).Text.ToString();
                            DanhMucChungKhoan item = DanhMucChungKhoanController.FindByName(name);
                            if (item == null)
                            {
                                item = new DanhMucChungKhoan();
                                item.MaChungKhoan = name;
                                DanhMucChungKhoanController.Insert(item);
                            }
                            BieuDoGia obj = new BieuDoGia();
                            obj.ChungKhoanID = item.ChungKhoanID;
                            //ngày
                            string myString = ((Excel.Range)osheet.Cells[i, 2]).Text.ToString();
                            obj.Ngay = DateTime.ParseExact(myString, "dd/MM/yyyy", CultureInfo.InvariantCulture);                            
                            //giá
                            myString = ((Excel.Range)osheet.Cells[i, 3]).Value2.ToString();
                            obj.GiaDongCua = Convert.ToDecimal(myString);                           
                            BieuDoGiaController .Insert(obj);
                        }
                    }

                    MessageBox.Show("chuyển số liệu xong");
                    xlApp.Workbooks.Close();
                    xlApp.Quit();
                    progressBar1.Value = progressBar1.Minimum;
                    //Marshal.ReleaseComObject(osheet);
                    Marshal.ReleaseComObject(wb);
                    Marshal.ReleaseComObject(xlApp);
                }
            }

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (txtNgay3.Text != "")
            {
                ThongKe2();
            }
            else
            {
                ThongKe1();
            }
        }
        private void ThongKe1()
        {
            try
            {
                DateTime ngay1 = DateTime.ParseExact(txtNgay1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ngay2 = DateTime.ParseExact(txtNgay2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ngay1 = new DateTime(ngay1.Year, ngay1.Month, ngay1.Day, 0, 0, 0);
                ngay2 = new DateTime(ngay2.Year, ngay2.Month, ngay2.Day, 0, 0, 0);
                decimal nguong = Convert.ToDecimal(txtNguong.Text);
                var list = BieuDoGiaController.ThongKe1(ngay1, ngay2).OrderBy(x => x.MaChungKhoan);

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("MaCK", typeof(string));
                dt.Columns.Add("Gia1", typeof(decimal));
                dt.Columns.Add("Gia2", typeof(decimal));
                dt.Columns.Add("Lech", typeof(string));

                int i = 1;
                foreach (var item in list)
                {
                    decimal dolech = item.DoLech * 100 / item.Gia1;
                    if (dolech >= nguong)
                    {
                        DataRow dr = dt.NewRow();
                        dr["STT"] = i;
                        dr["MaCK"] = item.MaChungKhoan;
                        dr["Gia1"] = Math.Round(item.Gia1, 2, MidpointRounding.AwayFromZero);
                        dr["Gia2"] = Math.Round(item.Gia2, 2, MidpointRounding.AwayFromZero);
                        //format %  
                        dr["Lech"] = dolech.ToString("F2") + "%";
                        dt.Rows.Add(dr);
                        i++;
                    }

                }
                gridKQLoc.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nhận dạng dữ liệu ngày. Kiểm tra lại ngày nhập!");
            }
        }        
        private void ThongKe2()
        {
            try
            {
                DateTime ngay1 = DateTime.ParseExact(txtNgay1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ngay2 = DateTime.ParseExact(txtNgay2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ngay3 = DateTime.ParseExact(txtNgay3.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ngay1 = new DateTime(ngay1.Year, ngay1.Month, ngay1.Day, 0, 0, 0);
                ngay2 = new DateTime(ngay2.Year, ngay2.Month, ngay2.Day, 0, 0, 0);
                ngay3 = new DateTime(ngay3.Year, ngay3.Month, ngay3.Day, 0, 0, 0);
                decimal nguong = Convert.ToDecimal(txtNguong.Text);
                var list = BieuDoGiaController.ThongKe2(ngay1, ngay2, ngay3).OrderBy(x => x.MaChungKhoan);

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("MaCK", typeof(string));
                dt.Columns.Add("Gia1", typeof(decimal));
                dt.Columns.Add("Gia2", typeof(decimal));
                dt.Columns.Add("Gia3", typeof(decimal));
                dt.Columns.Add("Lech12", typeof(string));
                dt.Columns.Add("Lech23", typeof(string));
                dt.Columns.Add("Lech13", typeof(string));
                dt.Columns.Add("LechMax", typeof(string));
                int i = 1;
                foreach (var item in list)
                {
                    decimal maxDoLech = Decimal.MinValue;
                    decimal dolech12 = item.DoLech12 * 100 / item.Gia1;
                    if (dolech12 > maxDoLech)
                    {
                        maxDoLech = dolech12;
                    }
                    decimal dolech23 = item.DoLech23* 100 / item.Gia2;
                    if (dolech23 > maxDoLech)
                    {
                        maxDoLech = dolech23;
                    }
                    decimal dolech13 = item.DoLech13 * 100 / item.Gia1;
                    if (dolech13 > maxDoLech)
                    {
                        maxDoLech = dolech13;
                    }
                    if (maxDoLech >= nguong)
                    {
                        DataRow dr = dt.NewRow();
                        dr["STT"] = i;
                        dr["MaCK"] = item.MaChungKhoan;
                        dr["Gia1"] = Math.Round (item.Gia1,2,MidpointRounding.AwayFromZero );
                        dr["Gia2"] = Math.Round(item.Gia2, 2, MidpointRounding.AwayFromZero);
                        dr["Gia3"] = Math.Round(item.Gia3, 2, MidpointRounding.AwayFromZero);
                        dr["Lech12"] = dolech12.ToString("F2") + "%";
                        dr["Lech23"] = dolech23.ToString("F2") + "%";
                        dr["Lech13"] = dolech13.ToString("F2") + "%";
                        //format %  
                        dr["LechMax"] = maxDoLech.ToString("F2") + "%";
                        dt.Rows.Add(dr);
                        i++;
                    }

                }
                gridKQLoc.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nhận dạng dữ liệu ngày. Kiểm tra lại ngày nhập!");
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
    }
}
