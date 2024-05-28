namespace LocChungKhoan
{
    partial class frmChiTietMaCK
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
            this.txtMaCK = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDenNgay = new System.Windows.Forms.TextBox();
            this.txtTuNgay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMaCK
            // 
            this.txtMaCK.Location = new System.Drawing.Point(127, 22);
            this.txtMaCK.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaCK.Name = "txtMaCK";
            this.txtMaCK.Size = new System.Drawing.Size(143, 22);
            this.txtMaCK.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(46, 26);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Tìm mã CK:";
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.Location = new System.Drawing.Point(359, 51);
            this.txtDenNgay.Margin = new System.Windows.Forms.Padding(4);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(143, 22);
            this.txtDenNgay.TabIndex = 2;
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.Location = new System.Drawing.Point(127, 52);
            this.txtTuNgay.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(143, 22);
            this.txtTuNgay.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "đến ngày:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Từ ngày:";
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(133, 96);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(105, 27);
            this.btnXem.TabIndex = 3;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(251, 96);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(105, 27);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmChiTietMaCK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 169);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.txtDenNgay);
            this.Controls.Add(this.txtTuNgay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaCK);
            this.Controls.Add(this.label9);
            this.Name = "frmChiTietMaCK";
            this.Text = "Xem dữ liệu chi tiết của 1 mã chứng khoán";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaCK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDenNgay;
        private System.Windows.Forms.TextBox txtTuNgay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnHuy;
    }
}