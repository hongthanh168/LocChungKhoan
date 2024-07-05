namespace LocChungKhoan
{
    partial class frmThongTin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnThuNghiem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(71, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phần mềm cập nhật ngày 31/01/2024";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(438, 80);
            this.label2.TabIndex = 1;
            this.label2.Text = "*Ngày 12/1/2024:\r\n- Thêm chức năng xóa toàn bộ, xóa theo ngày, xóa trong khoảng t" +
    "hời gian\r\n- Chức năng Lọc dữ liệu thêm điều kiện: Giá 4 > Giá 1\r\n*Ngày 31/01/202" +
    "4:\r\n- Thêm chức năng lọc theo ngày";
            // 
            // btnThuNghiem
            // 
            this.btnThuNghiem.Location = new System.Drawing.Point(372, 118);
            this.btnThuNghiem.Name = "btnThuNghiem";
            this.btnThuNghiem.Size = new System.Drawing.Size(137, 32);
            this.btnThuNghiem.TabIndex = 2;
            this.btnThuNghiem.Text = "Thử nghiệm";
            this.btnThuNghiem.UseVisualStyleBackColor = true;
            this.btnThuNghiem.Visible = false;
            this.btnThuNghiem.Click += new System.EventHandler(this.btnThuNghiem_Click);
            // 
            // frmThongTin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 162);
            this.Controls.Add(this.btnThuNghiem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmThongTin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin phiên bản";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmThongTin_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnThuNghiem;
    }
}