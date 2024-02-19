namespace LocChungKhoan
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnMoFile = new System.Windows.Forms.Button();
            this.txtDuongDan = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaCK = new System.Windows.Forms.TextBox();
            this.txtNguongDuoi = new System.Windows.Forms.TextBox();
            this.txtNguongTren = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNgay4 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNgay3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNgay2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNgay1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridKQLoc = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLýDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaToanBo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaTheoNgay = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaTheoThoiGian = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMoFile
            // 
            this.btnMoFile.Location = new System.Drawing.Point(581, 13);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(75, 23);
            this.btnMoFile.TabIndex = 1;
            this.btnMoFile.Text = "File Excel...";
            this.btnMoFile.UseVisualStyleBackColor = true;
            this.btnMoFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.Location = new System.Drawing.Point(112, 16);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.Size = new System.Drawing.Size(463, 20);
            this.txtDuongDan.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(112, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(544, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tiến trình load file:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoc);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMaCK);
            this.groupBox1.Controls.Add(this.txtNguongDuoi);
            this.groupBox1.Controls.Add(this.txtNguongTren);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNgay4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtNgay3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNgay2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNgay1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(645, 222);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tham số:";
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(250, 153);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(86, 23);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(253, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(341, 52);
            this.label6.TabIndex = 2;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // txtMaCK
            // 
            this.txtMaCK.Location = new System.Drawing.Point(85, 180);
            this.txtMaCK.Name = "txtMaCK";
            this.txtMaCK.Size = new System.Drawing.Size(159, 20);
            this.txtMaCK.TabIndex = 7;
            this.txtMaCK.TextChanged += new System.EventHandler(this.txtMaCK_TextChanged);
            // 
            // txtNguongDuoi
            // 
            this.txtNguongDuoi.Location = new System.Drawing.Point(85, 132);
            this.txtNguongDuoi.Name = "txtNguongDuoi";
            this.txtNguongDuoi.Size = new System.Drawing.Size(56, 20);
            this.txtNguongDuoi.TabIndex = 4;
            // 
            // txtNguongTren
            // 
            this.txtNguongTren.Location = new System.Drawing.Point(188, 132);
            this.txtNguongTren.Name = "txtNguongTren";
            this.txtNguongTren.Size = new System.Drawing.Size(56, 20);
            this.txtNguongTren.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Lọc mã CK:";
            // 
            // txtNgay4
            // 
            this.txtNgay4.Location = new System.Drawing.Point(85, 104);
            this.txtNgay4.Name = "txtNgay4";
            this.txtNgay4.Size = new System.Drawing.Size(159, 20);
            this.txtNgay4.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "đến:";
            // 
            // txtNgay3
            // 
            this.txtNgay3.Location = new System.Drawing.Point(85, 77);
            this.txtNgay3.Name = "txtNgay3";
            this.txtNgay3.Size = new System.Drawing.Size(159, 20);
            this.txtNgay3.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ngưỡng từ:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Ngày 4:";
            // 
            // txtNgay2
            // 
            this.txtNgay2.Location = new System.Drawing.Point(85, 48);
            this.txtNgay2.Name = "txtNgay2";
            this.txtNgay2.Size = new System.Drawing.Size(159, 20);
            this.txtNgay2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ngày 3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày 2:";
            // 
            // txtNgay1
            // 
            this.txtNgay1.Location = new System.Drawing.Point(85, 19);
            this.txtNgay1.Name = "txtNgay1";
            this.txtNgay1.Size = new System.Drawing.Size(159, 20);
            this.txtNgay1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngày 1:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Đường dẫn:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridKQLoc);
            this.groupBox2.Location = new System.Drawing.Point(11, 299);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(645, 401);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả lọc";
            // 
            // gridKQLoc
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridKQLoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gridKQLoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridKQLoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKQLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKQLoc.Location = new System.Drawing.Point(3, 16);
            this.gridKQLoc.Name = "gridKQLoc";
            this.gridKQLoc.Size = new System.Drawing.Size(639, 382);
            this.gridKQLoc.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnMoFile);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtDuongDan);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 743);
            this.panel1.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýDữLiệuToolStripMenuItem,
            this.trợGiúpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(696, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "Xóa dữ liệu";
            // 
            // quảnLýDữLiệuToolStripMenuItem
            // 
            this.quảnLýDữLiệuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnXoaToanBo,
            this.btnXoaTheoNgay,
            this.btnXoaTheoThoiGian});
            this.quảnLýDữLiệuToolStripMenuItem.Name = "quảnLýDữLiệuToolStripMenuItem";
            this.quảnLýDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.quảnLýDữLiệuToolStripMenuItem.Text = "Quản lý dữ liệu";
            // 
            // btnXoaToanBo
            // 
            this.btnXoaToanBo.Name = "btnXoaToanBo";
            this.btnXoaToanBo.Size = new System.Drawing.Size(258, 22);
            this.btnXoaToanBo.Text = "Xóa toàn bộ dữ liệu";
            this.btnXoaToanBo.Click += new System.EventHandler(this.btnXoaToanBo_Click);
            // 
            // btnXoaTheoNgay
            // 
            this.btnXoaTheoNgay.Name = "btnXoaTheoNgay";
            this.btnXoaTheoNgay.Size = new System.Drawing.Size(258, 22);
            this.btnXoaTheoNgay.Text = "Xóa dữ liệu theo ngày";
            this.btnXoaTheoNgay.Click += new System.EventHandler(this.btnXoaTheoNgay_Click);
            // 
            // btnXoaTheoThoiGian
            // 
            this.btnXoaTheoThoiGian.Name = "btnXoaTheoThoiGian";
            this.btnXoaTheoThoiGian.Size = new System.Drawing.Size(258, 22);
            this.btnXoaTheoThoiGian.Text = "Xóa dữ liệu trong khoảng thời gian";
            this.btnXoaTheoThoiGian.Click += new System.EventHandler(this.btnXoaTheoThoiGian_Click);
            // 
            // trợGiúpToolStripMenuItem
            // 
            this.trợGiúpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinToolStripMenuItem});
            this.trợGiúpToolStripMenuItem.Name = "trợGiúpToolStripMenuItem";
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.trợGiúpToolStripMenuItem.Text = "Trợ giúp";
            // 
            // thôngTinToolStripMenuItem
            // 
            this.thôngTinToolStripMenuItem.Name = "thôngTinToolStripMenuItem";
            this.thôngTinToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.thôngTinToolStripMenuItem.Text = "Thông tin";
            this.thôngTinToolStripMenuItem.Click += new System.EventHandler(this.thôngTinToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 767);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMoFile;
        private System.Windows.Forms.TextBox txtDuongDan;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNgay1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNgay3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNgay2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridKQLoc;
        private System.Windows.Forms.TextBox txtMaCK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNguongTren;
        private System.Windows.Forms.TextBox txtNgay4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNguongDuoi;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnLýDữLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnXoaToanBo;
        private System.Windows.Forms.ToolStripMenuItem btnXoaTheoNgay;
        private System.Windows.Forms.ToolStripMenuItem btnXoaTheoThoiGian;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinToolStripMenuItem;
    }
}

