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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnMoFile = new System.Windows.Forms.Button();
            this.txtDuongDan = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNguong = new System.Windows.Forms.TextBox();
            this.txtNgay3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNgay2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNgay1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridKQLoc = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMaCK = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMoFile
            // 
            this.btnMoFile.Location = new System.Drawing.Point(571, 12);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(75, 23);
            this.btnMoFile.TabIndex = 0;
            this.btnMoFile.Text = "File Excel...";
            this.btnMoFile.UseVisualStyleBackColor = true;
            this.btnMoFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.Location = new System.Drawing.Point(127, 15);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.Size = new System.Drawing.Size(438, 20);
            this.txtDuongDan.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(127, 41);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(519, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 47);
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
            this.groupBox1.Controls.Add(this.txtNguong);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNgay3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNgay2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNgay1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(26, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 210);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tham số:";
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(250, 143);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(129, 23);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(267, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(341, 65);
            this.label6.TabIndex = 2;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // txtNguong
            // 
            this.txtNguong.Location = new System.Drawing.Point(67, 106);
            this.txtNguong.Name = "txtNguong";
            this.txtNguong.Size = new System.Drawing.Size(187, 20);
            this.txtNguong.TabIndex = 3;
            // 
            // txtNgay3
            // 
            this.txtNgay3.Location = new System.Drawing.Point(67, 78);
            this.txtNgay3.Name = "txtNgay3";
            this.txtNgay3.Size = new System.Drawing.Size(187, 20);
            this.txtNgay3.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ngưỡng:";
            // 
            // txtNgay2
            // 
            this.txtNgay2.Location = new System.Drawing.Point(67, 48);
            this.txtNgay2.Name = "txtNgay2";
            this.txtNgay2.Size = new System.Drawing.Size(187, 20);
            this.txtNgay2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ngày 3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày 2:";
            // 
            // txtNgay1
            // 
            this.txtNgay1.Location = new System.Drawing.Point(67, 19);
            this.txtNgay1.Name = "txtNgay1";
            this.txtNgay1.Size = new System.Drawing.Size(187, 20);
            this.txtNgay1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngày 1:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Đường dẫn:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridKQLoc);
            this.groupBox2.Location = new System.Drawing.Point(26, 298);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(620, 401);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả lọc";
            // 
            // gridKQLoc
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridKQLoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridKQLoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridKQLoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKQLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKQLoc.Location = new System.Drawing.Point(3, 16);
            this.gridKQLoc.Name = "gridKQLoc";
            this.gridKQLoc.Size = new System.Drawing.Size(614, 382);
            this.gridKQLoc.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Lọc mã CK:";
            // 
            // txtMaCK
            // 
            this.txtMaCK.Location = new System.Drawing.Point(85, 174);
            this.txtMaCK.Name = "txtMaCK";
            this.txtMaCK.Size = new System.Drawing.Size(169, 20);
            this.txtMaCK.TabIndex = 3;
            this.txtMaCK.TextChanged += new System.EventHandler(this.txtMaCK_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 711);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtDuongDan);
            this.Controls.Add(this.btnMoFile);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).EndInit();
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
        private System.Windows.Forms.TextBox txtNguong;
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
    }
}

