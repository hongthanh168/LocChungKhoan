namespace LocChungKhoan
{
    partial class frmLocTheoNgay
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnTab1 = new System.Windows.Forms.Button();
            this.txtDuongDan = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridKQLoc = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnLocTheoNgay = new System.Windows.Forms.Button();
            this.btnTab3 = new System.Windows.Forms.Button();
            this.btnTab2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gridTAB1 = new System.Windows.Forms.DataGridView();
            this.gridTAB2 = new System.Windows.Forms.DataGridView();
            this.gridTAB3 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTAB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTAB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTAB3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTab1
            // 
            this.btnTab1.Location = new System.Drawing.Point(716, 18);
            this.btnTab1.Margin = new System.Windows.Forms.Padding(4);
            this.btnTab1.Name = "btnTab1";
            this.btnTab1.Size = new System.Drawing.Size(56, 28);
            this.btnTab1.TabIndex = 2;
            this.btnTab1.Text = "TAB1";
            this.btnTab1.UseVisualStyleBackColor = true;
            this.btnTab1.Click += new System.EventHandler(this.btnTab1_Click);
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.Location = new System.Drawing.Point(152, 25);
            this.txtDuongDan.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.Size = new System.Drawing.Size(556, 22);
            this.txtDuongDan.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(152, 57);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(748, 28);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tiến trình load file:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 28);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Đường dẫn:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridKQLoc);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(652, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(260, 543);
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
            this.gridKQLoc.Location = new System.Drawing.Point(4, 19);
            this.gridKQLoc.Margin = new System.Windows.Forms.Padding(4);
            this.gridKQLoc.Name = "gridKQLoc";
            this.gridKQLoc.RowHeadersWidth = 51;
            this.gridKQLoc.Size = new System.Drawing.Size(252, 520);
            this.gridKQLoc.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 711);
            this.panel1.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtDuongDan);
            this.splitContainer1.Panel1.Controls.Add(this.btnLocTheoNgay);
            this.splitContainer1.Panel1.Controls.Add(this.btnTab3);
            this.splitContainer1.Panel1.Controls.Add(this.btnTab2);
            this.splitContainer1.Panel1.Controls.Add(this.btnTab1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(922, 711);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // btnLocTheoNgay
            // 
            this.btnLocTheoNgay.Location = new System.Drawing.Point(406, 97);
            this.btnLocTheoNgay.Margin = new System.Windows.Forms.Padding(4);
            this.btnLocTheoNgay.Name = "btnLocTheoNgay";
            this.btnLocTheoNgay.Size = new System.Drawing.Size(164, 28);
            this.btnLocTheoNgay.TabIndex = 2;
            this.btnLocTheoNgay.Text = "Lọc theo ngày";
            this.btnLocTheoNgay.UseVisualStyleBackColor = true;
            this.btnLocTheoNgay.Click += new System.EventHandler(this.btnLocTheoNgay_Click);
            // 
            // btnTab3
            // 
            this.btnTab3.Location = new System.Drawing.Point(844, 18);
            this.btnTab3.Margin = new System.Windows.Forms.Padding(4);
            this.btnTab3.Name = "btnTab3";
            this.btnTab3.Size = new System.Drawing.Size(56, 28);
            this.btnTab3.TabIndex = 2;
            this.btnTab3.Text = "TAB3";
            this.btnTab3.UseVisualStyleBackColor = true;
            this.btnTab3.Click += new System.EventHandler(this.btnTab3_Click);
            // 
            // btnTab2
            // 
            this.btnTab2.Location = new System.Drawing.Point(780, 18);
            this.btnTab2.Margin = new System.Windows.Forms.Padding(4);
            this.btnTab2.Name = "btnTab2";
            this.btnTab2.Size = new System.Drawing.Size(56, 28);
            this.btnTab2.TabIndex = 2;
            this.btnTab2.Text = "TAB2";
            this.btnTab2.UseVisualStyleBackColor = true;
            this.btnTab2.Click += new System.EventHandler(this.btnTab2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.80851F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.19149F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 555);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridTAB1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 545);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TAB1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridTAB2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(211, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 545);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TAB2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gridTAB3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(445, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 545);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TAB3";
            // 
            // gridTAB1
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridTAB1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridTAB1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridTAB1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTAB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTAB1.Location = new System.Drawing.Point(3, 18);
            this.gridTAB1.Margin = new System.Windows.Forms.Padding(4);
            this.gridTAB1.Name = "gridTAB1";
            this.gridTAB1.RowHeadersWidth = 51;
            this.gridTAB1.Size = new System.Drawing.Size(192, 524);
            this.gridTAB1.TabIndex = 1;
            // 
            // gridTAB2
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridTAB2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gridTAB2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridTAB2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTAB2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTAB2.Location = new System.Drawing.Point(3, 18);
            this.gridTAB2.Margin = new System.Windows.Forms.Padding(4);
            this.gridTAB2.Name = "gridTAB2";
            this.gridTAB2.RowHeadersWidth = 51;
            this.gridTAB2.Size = new System.Drawing.Size(220, 524);
            this.gridTAB2.TabIndex = 1;
            // 
            // gridTAB3
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridTAB3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gridTAB3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridTAB3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTAB3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTAB3.Location = new System.Drawing.Point(3, 18);
            this.gridTAB3.Margin = new System.Windows.Forms.Padding(4);
            this.gridTAB3.Name = "gridTAB3";
            this.gridTAB3.RowHeadersWidth = 51;
            this.gridTAB3.Size = new System.Drawing.Size(192, 524);
            this.gridTAB3.TabIndex = 1;
            // 
            // frmLocTheoNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 711);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmLocTheoNgay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmLocTheoNgay_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTAB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTAB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTAB3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTab1;
        private System.Windows.Forms.TextBox txtDuongDan;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridKQLoc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnLocTheoNgay;
        private System.Windows.Forms.Button btnTab3;
        private System.Windows.Forms.Button btnTab2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridTAB1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gridTAB2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView gridTAB3;
    }
}

