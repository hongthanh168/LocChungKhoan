namespace LocChungKhoan
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnMoFile = new System.Windows.Forms.Button();
            this.txtDuongDan = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNguong2 = new System.Windows.Forms.TextBox();
            this.txtNguong1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaCK = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnThongKeNen = new System.Windows.Forms.Button();
            this.btnThongKeNgay = new System.Windows.Forms.Button();
            this.btnThongKeTuan = new System.Windows.Forms.Button();
            this.txtTuan3CuoiTuan = new System.Windows.Forms.TextBox();
            this.txtTuan2CuoiTuan = new System.Windows.Forms.TextBox();
            this.txtTuan1CuoiTuan = new System.Windows.Forms.TextBox();
            this.txtTuan3DauTuan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTuan2DauTuan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTuan1DauTuan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridKQLoc = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.btnMoThuMuc = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLýDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaToanBo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaTheoNgay = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaTheoThoiGian = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnThongKeTuan2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMoFile
            // 
            this.btnMoFile.Location = new System.Drawing.Point(756, 21);
            this.btnMoFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoFile.Name = "btnMoFile";
            this.btnMoFile.Size = new System.Drawing.Size(109, 28);
            this.btnMoFile.TabIndex = 1;
            this.btnMoFile.Text = "File dữ liệu...";
            this.btnMoFile.UseVisualStyleBackColor = true;
            this.btnMoFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDuongDan
            // 
            this.txtDuongDan.Location = new System.Drawing.Point(152, 25);
            this.txtDuongDan.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuongDan.Name = "txtDuongDan";
            this.txtDuongDan.Size = new System.Drawing.Size(596, 22);
            this.txtDuongDan.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(152, 57);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(949, 28);
            this.progressBar1.TabIndex = 4;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtNguong2);
            this.groupBox1.Controls.Add(this.txtNguong1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtMaCK);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnThongKeNen);
            this.groupBox1.Controls.Add(this.btnThongKeNgay);
            this.groupBox1.Controls.Add(this.btnThongKeTuan2);
            this.groupBox1.Controls.Add(this.btnThongKeTuan);
            this.groupBox1.Controls.Add(this.txtTuan3CuoiTuan);
            this.groupBox1.Controls.Add(this.txtTuan2CuoiTuan);
            this.groupBox1.Controls.Add(this.txtTuan1CuoiTuan);
            this.groupBox1.Controls.Add(this.txtTuan3DauTuan);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtTuan2DauTuan);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTuan1DauTuan);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 92);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1085, 208);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tham số:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Location = new System.Drawing.Point(752, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(242, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "Lệch 23 <=ngưỡng 1; lệch 33<=ngưỡng 2";
            // 
            // txtNguong2
            // 
            this.txtNguong2.Location = new System.Drawing.Point(653, 120);
            this.txtNguong2.Margin = new System.Windows.Forms.Padding(4);
            this.txtNguong2.Name = "txtNguong2";
            this.txtNguong2.Size = new System.Drawing.Size(91, 22);
            this.txtNguong2.TabIndex = 7;
            this.txtNguong2.Text = "1.5";
            // 
            // txtNguong1
            // 
            this.txtNguong1.Location = new System.Drawing.Point(383, 120);
            this.txtNguong1.Margin = new System.Windows.Forms.Padding(4);
            this.txtNguong1.Name = "txtNguong1";
            this.txtNguong1.Size = new System.Drawing.Size(81, 22);
            this.txtNguong1.TabIndex = 6;
            this.txtNguong1.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(566, 123);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "Ngưỡng 2:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(298, 124);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 16);
            this.label11.TabIndex = 15;
            this.label11.Text = "Ngưỡng 1:";
            // 
            // txtMaCK
            // 
            this.txtMaCK.Location = new System.Drawing.Point(910, 163);
            this.txtMaCK.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaCK.Name = "txtMaCK";
            this.txtMaCK.Size = new System.Drawing.Size(109, 22);
            this.txtMaCK.TabIndex = 12;
            this.txtMaCK.TextChanged += new System.EventHandler(this.txtMaCK_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(829, 167);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Tìm mã CK:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(535, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Ngày cuối tuần: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(535, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ngày cuối tuần: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(535, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Ngày cuối tuần: ";
            // 
            // btnThongKeNen
            // 
            this.btnThongKeNen.Location = new System.Drawing.Point(673, 161);
            this.btnThongKeNen.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeNen.Name = "btnThongKeNen";
            this.btnThongKeNen.Size = new System.Drawing.Size(135, 28);
            this.btnThongKeNen.TabIndex = 11;
            this.btnThongKeNen.Text = "Lọc nến ngày";
            this.btnThongKeNen.UseVisualStyleBackColor = true;
            this.btnThongKeNen.Click += new System.EventHandler(this.btnThongKeNen_Click);
            // 
            // btnThongKeNgay
            // 
            this.btnThongKeNgay.Location = new System.Drawing.Point(532, 161);
            this.btnThongKeNgay.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeNgay.Name = "btnThongKeNgay";
            this.btnThongKeNgay.Size = new System.Drawing.Size(135, 28);
            this.btnThongKeNgay.TabIndex = 11;
            this.btnThongKeNgay.Text = "Thống kê ngày";
            this.btnThongKeNgay.UseVisualStyleBackColor = true;
            this.btnThongKeNgay.Click += new System.EventHandler(this.btnThongKeNgay_Click);
            // 
            // btnThongKeTuan
            // 
            this.btnThongKeTuan.Location = new System.Drawing.Point(248, 160);
            this.btnThongKeTuan.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeTuan.Name = "btnThongKeTuan";
            this.btnThongKeTuan.Size = new System.Drawing.Size(135, 28);
            this.btnThongKeTuan.TabIndex = 10;
            this.btnThongKeTuan.Text = "Thống kê tuần 1";
            this.btnThongKeTuan.UseVisualStyleBackColor = true;
            this.btnThongKeTuan.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtTuan3CuoiTuan
            // 
            this.txtTuan3CuoiTuan.Location = new System.Drawing.Point(653, 87);
            this.txtTuan3CuoiTuan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuan3CuoiTuan.Name = "txtTuan3CuoiTuan";
            this.txtTuan3CuoiTuan.Size = new System.Drawing.Size(140, 22);
            this.txtTuan3CuoiTuan.TabIndex = 5;
            // 
            // txtTuan2CuoiTuan
            // 
            this.txtTuan2CuoiTuan.Location = new System.Drawing.Point(653, 57);
            this.txtTuan2CuoiTuan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuan2CuoiTuan.Name = "txtTuan2CuoiTuan";
            this.txtTuan2CuoiTuan.Size = new System.Drawing.Size(140, 22);
            this.txtTuan2CuoiTuan.TabIndex = 3;
            // 
            // txtTuan1CuoiTuan
            // 
            this.txtTuan1CuoiTuan.Location = new System.Drawing.Point(653, 27);
            this.txtTuan1CuoiTuan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuan1CuoiTuan.Name = "txtTuan1CuoiTuan";
            this.txtTuan1CuoiTuan.Size = new System.Drawing.Size(140, 22);
            this.txtTuan1CuoiTuan.TabIndex = 1;
            // 
            // txtTuan3DauTuan
            // 
            this.txtTuan3DauTuan.Location = new System.Drawing.Point(383, 87);
            this.txtTuan3DauTuan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuan3DauTuan.Name = "txtTuan3DauTuan";
            this.txtTuan3DauTuan.Size = new System.Drawing.Size(140, 22);
            this.txtTuan3DauTuan.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 90);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tuần 3, Ngày đầu tuần:";
            // 
            // txtTuan2DauTuan
            // 
            this.txtTuan2DauTuan.Location = new System.Drawing.Point(383, 57);
            this.txtTuan2DauTuan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuan2DauTuan.Name = "txtTuan2DauTuan";
            this.txtTuan2DauTuan.Size = new System.Drawing.Size(140, 22);
            this.txtTuan2DauTuan.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tuần 2, Ngày đầu tuần:";
            // 
            // txtTuan1DauTuan
            // 
            this.txtTuan1DauTuan.Location = new System.Drawing.Point(383, 27);
            this.txtTuan1DauTuan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuan1DauTuan.Name = "txtTuan1DauTuan";
            this.txtTuan1DauTuan.Size = new System.Drawing.Size(140, 22);
            this.txtTuan1DauTuan.TabIndex = 0;
            this.txtTuan1DauTuan.Leave += new System.EventHandler(this.txtTuan1DauTuan_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tuần 1, Ngày đầu tuần:";
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
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1127, 493);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả lọc";
            // 
            // gridKQLoc
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridKQLoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridKQLoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridKQLoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKQLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKQLoc.Location = new System.Drawing.Point(4, 19);
            this.gridKQLoc.Margin = new System.Windows.Forms.Padding(4);
            this.gridKQLoc.Name = "gridKQLoc";
            this.gridKQLoc.RowHeadersWidth = 51;
            this.gridKQLoc.Size = new System.Drawing.Size(1119, 470);
            this.gridKQLoc.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1131, 818);
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
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.btnMoThuMuc);
            this.splitContainer1.Panel1.Controls.Add(this.btnMoFile);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1131, 818);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(979, 21);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "DM quan tâm...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnMoThuMuc
            // 
            this.btnMoThuMuc.Location = new System.Drawing.Point(875, 22);
            this.btnMoThuMuc.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoThuMuc.Name = "btnMoThuMuc";
            this.btnMoThuMuc.Size = new System.Drawing.Size(96, 28);
            this.btnMoThuMuc.TabIndex = 2;
            this.btnMoThuMuc.Text = "Thư mục...";
            this.btnMoThuMuc.UseVisualStyleBackColor = true;
            this.btnMoThuMuc.Click += new System.EventHandler(this.btnMoThuMuc_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýDữLiệuToolStripMenuItem,
            this.trợGiúpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1131, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "Xóa dữ liệu";
            // 
            // quảnLýDữLiệuToolStripMenuItem
            // 
            this.quảnLýDữLiệuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnXoaToanBo,
            this.btnXoaTheoNgay,
            this.btnXoaTheoThoiGian,
            this.toolStripSeparator1,
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem,
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem});
            this.quảnLýDữLiệuToolStripMenuItem.Name = "quảnLýDữLiệuToolStripMenuItem";
            this.quảnLýDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.quảnLýDữLiệuToolStripMenuItem.Text = "Quản lý dữ liệu";
            // 
            // btnXoaToanBo
            // 
            this.btnXoaToanBo.Name = "btnXoaToanBo";
            this.btnXoaToanBo.Size = new System.Drawing.Size(399, 26);
            this.btnXoaToanBo.Text = "Xóa toàn bộ dữ liệu khối lượng";
            this.btnXoaToanBo.Click += new System.EventHandler(this.btnXoaToanBo_Click);
            // 
            // btnXoaTheoNgay
            // 
            this.btnXoaTheoNgay.Name = "btnXoaTheoNgay";
            this.btnXoaTheoNgay.Size = new System.Drawing.Size(399, 26);
            this.btnXoaTheoNgay.Text = "Xóa dữ liệu khối lượng theo ngày";
            this.btnXoaTheoNgay.Click += new System.EventHandler(this.btnXoaTheoNgay_Click);
            // 
            // btnXoaTheoThoiGian
            // 
            this.btnXoaTheoThoiGian.Name = "btnXoaTheoThoiGian";
            this.btnXoaTheoThoiGian.Size = new System.Drawing.Size(399, 26);
            this.btnXoaTheoThoiGian.Text = "Xóa dữ liệu khối lượng trong khoảng thời gian";
            this.btnXoaTheoThoiGian.Click += new System.EventHandler(this.btnXoaTheoThoiGian_Click);
            // 
            // trợGiúpToolStripMenuItem
            // 
            this.trợGiúpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thôngTinToolStripMenuItem});
            this.trợGiúpToolStripMenuItem.Name = "trợGiúpToolStripMenuItem";
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.trợGiúpToolStripMenuItem.Text = "Trợ giúp";
            // 
            // thôngTinToolStripMenuItem
            // 
            this.thôngTinToolStripMenuItem.Name = "thôngTinToolStripMenuItem";
            this.thôngTinToolStripMenuItem.Size = new System.Drawing.Size(155, 26);
            this.thôngTinToolStripMenuItem.Text = "Thông tin";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(396, 6);
            // 
            // danhMụcChứngKhoánQuanTâmToolStripMenuItem
            // 
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem.Name = "danhMụcChứngKhoánQuanTâmToolStripMenuItem";
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem.Size = new System.Drawing.Size(399, 26);
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem.Text = "Danh mục chứng khoán quan tâm";
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem.Click += new System.EventHandler(this.danhMụcChứngKhoánQuanTâmToolStripMenuItem_Click);
            // 
            // kiểmTraDữLiệuHiệnCóToolStripMenuItem
            // 
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem.Name = "kiểmTraDữLiệuHiệnCóToolStripMenuItem";
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem.Size = new System.Drawing.Size(399, 26);
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem.Text = "Kiểm tra dữ liệu hiện có";
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem.Click += new System.EventHandler(this.kiểmTraDữLiệuHiệnCóToolStripMenuItem_Click);
            // 
            // btnThongKeTuan2
            // 
            this.btnThongKeTuan2.Location = new System.Drawing.Point(391, 161);
            this.btnThongKeTuan2.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeTuan2.Name = "btnThongKeTuan2";
            this.btnThongKeTuan2.Size = new System.Drawing.Size(135, 28);
            this.btnThongKeTuan2.TabIndex = 10;
            this.btnThongKeTuan2.Text = "Thống kê tuần 2";
            this.btnThongKeTuan2.UseVisualStyleBackColor = true;
            this.btnThongKeTuan2.Click += new System.EventHandler(this.btnThongKeTuan2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 846);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lọc chứng khoán theo khối lượng";
            this.Load += new System.EventHandler(this.frmMainKhoiLuong_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtTuan1DauTuan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTuan1CuoiTuan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridKQLoc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnLýDữLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnXoaToanBo;
        private System.Windows.Forms.ToolStripMenuItem btnXoaTheoNgay;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem btnXoaTheoThoiGian;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTuan3CuoiTuan;
        private System.Windows.Forms.TextBox txtTuan2CuoiTuan;
        private System.Windows.Forms.TextBox txtTuan3DauTuan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTuan2DauTuan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMoThuMuc;
        private System.Windows.Forms.Button btnThongKeTuan;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtMaCK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnThongKeNgay;
        private System.Windows.Forms.TextBox txtNguong1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNguong2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnThongKeNen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem danhMụcChứngKhoánQuanTâmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kiểmTraDữLiệuHiệnCóToolStripMenuItem;
        private System.Windows.Forms.Button btnThongKeTuan2;
    }
}

