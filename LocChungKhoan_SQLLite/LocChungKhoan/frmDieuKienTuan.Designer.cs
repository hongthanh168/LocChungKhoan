namespace LocChungKhoan
{
    partial class frmDieuKienTuan
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
            this.ckKhoiLuong = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkGiaDongMo = new System.Windows.Forms.CheckBox();
            this.chkGiaCaoThap = new System.Windows.Forms.CheckBox();
            this.chkDieuKienTuan1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ckKhoiLuong
            // 
            this.ckKhoiLuong.AutoSize = true;
            this.ckKhoiLuong.Checked = true;
            this.ckKhoiLuong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckKhoiLuong.Location = new System.Drawing.Point(80, 27);
            this.ckKhoiLuong.Name = "ckKhoiLuong";
            this.ckKhoiLuong.Size = new System.Drawing.Size(225, 20);
            this.ckKhoiLuong.TabIndex = 1;
            this.ckKhoiLuong.Text = "Áp dụng điều kiện về khối lượng?";
            this.ckKhoiLuong.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(124, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 32);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(222, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkGiaDongMo
            // 
            this.chkGiaDongMo.AutoSize = true;
            this.chkGiaDongMo.Checked = true;
            this.chkGiaDongMo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGiaDongMo.Location = new System.Drawing.Point(80, 67);
            this.chkGiaDongMo.Name = "chkGiaDongMo";
            this.chkGiaDongMo.Size = new System.Drawing.Size(261, 20);
            this.chkGiaDongMo.TabIndex = 1;
            this.chkGiaDongMo.Text = "Biên độ giá theo giá đóng cửa - mở cửa";
            this.chkGiaDongMo.UseVisualStyleBackColor = true;
            // 
            // chkGiaCaoThap
            // 
            this.chkGiaCaoThap.AutoSize = true;
            this.chkGiaCaoThap.Checked = true;
            this.chkGiaCaoThap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGiaCaoThap.Location = new System.Drawing.Point(80, 110);
            this.chkGiaCaoThap.Name = "chkGiaCaoThap";
            this.chkGiaCaoThap.Size = new System.Drawing.Size(266, 20);
            this.chkGiaCaoThap.TabIndex = 1;
            this.chkGiaCaoThap.Text = "Biên độ giá theo giá cao nhất - thấp nhất";
            this.chkGiaCaoThap.UseVisualStyleBackColor = true;
            // 
            // chkDieuKienTuan1
            // 
            this.chkDieuKienTuan1.AutoSize = true;
            this.chkDieuKienTuan1.Checked = true;
            this.chkDieuKienTuan1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDieuKienTuan1.Location = new System.Drawing.Point(80, 147);
            this.chkDieuKienTuan1.Name = "chkDieuKienTuan1";
            this.chkDieuKienTuan1.Size = new System.Drawing.Size(319, 20);
            this.chkDieuKienTuan1.TabIndex = 1;
            this.chkDieuKienTuan1.Text = "Áp dụng Giá đóng cửa tuần 1 < Giá mở cửa tuần 1";
            this.chkDieuKienTuan1.UseVisualStyleBackColor = true;
            // 
            // frmDieuKienTuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 229);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkDieuKienTuan1);
            this.Controls.Add(this.chkGiaCaoThap);
            this.Controls.Add(this.chkGiaDongMo);
            this.Controls.Add(this.ckKhoiLuong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDieuKienTuan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn điều kiện lọc chứng khoán theo tuần";
            this.Load += new System.EventHandler(this.frmDieuKienTuan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox ckKhoiLuong;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkGiaDongMo;
        private System.Windows.Forms.CheckBox chkGiaCaoThap;
        private System.Windows.Forms.CheckBox chkDieuKienTuan1;
    }
}