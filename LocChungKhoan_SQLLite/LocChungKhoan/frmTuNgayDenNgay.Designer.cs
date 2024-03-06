namespace LocChungKhoan
{
    partial class frmTuNgayDenNgay
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
            this.txtDenNgay = new System.Windows.Forms.TextBox();
            this.txtTuNgay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDenNgay
            // 
            this.txtDenNgay.Location = new System.Drawing.Point(361, 33);
            this.txtDenNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDenNgay.Name = "txtDenNgay";
            this.txtDenNgay.Size = new System.Drawing.Size(143, 22);
            this.txtDenNgay.TabIndex = 8;
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.Location = new System.Drawing.Point(129, 34);
            this.txtTuNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(143, 22);
            this.txtTuNgay.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "đến ngày:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Từ ngày:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 77);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "Thực hiện";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTuNgayDenNgay
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 128);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDenNgay);
            this.Controls.Add(this.txtTuNgay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmTuNgayDenNgay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thiết lập ngày";
            this.Load += new System.EventHandler(this.frmNgay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDenNgay;
        private System.Windows.Forms.TextBox txtTuNgay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}