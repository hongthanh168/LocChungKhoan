namespace LocChungKhoan
{
    partial class frmPhanTichKyThuat
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
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridKQLoc = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.btnMoThuMuc = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRSI = new System.Windows.Forms.CheckBox();
            this.chkBienDongKhoiLuong = new System.Windows.Forms.CheckBox();
            this.chkKietCung = new System.Windows.Forms.CheckBox();
            this.chkMorningStar = new System.Windows.Forms.CheckBox();
            this.chkNen_PiercingPartern = new System.Windows.Forms.CheckBox();
            this.chkNen_Hammer = new System.Windows.Forms.CheckBox();
            this.chkNen_BullishEngulfing = new System.Windows.Forms.CheckBox();
            this.chkSO = new System.Windows.Forms.CheckBox();
            this.chkMACD = new System.Windows.Forms.CheckBox();
            this.txtMaCK = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoc = new System.Windows.Forms.Button();
            this.txtNguongKhoiLuong = new System.Windows.Forms.TextBox();
            this.txtSoNgay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTuNgay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLýDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaToanBo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaTheoNgay = new System.Windows.Forms.ToolStripMenuItem();
            this.btnXoaTheoThoiGian = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.danhMụcChứngKhoánQuanTâmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemDữLiệuCủa1MãCụThểToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox2.Size = new System.Drawing.Size(1127, 547);
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
            this.gridKQLoc.Size = new System.Drawing.Size(1119, 524);
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
            this.splitContainer1.SplitterDistance = 262;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRSI);
            this.groupBox1.Controls.Add(this.chkBienDongKhoiLuong);
            this.groupBox1.Controls.Add(this.chkKietCung);
            this.groupBox1.Controls.Add(this.chkMorningStar);
            this.groupBox1.Controls.Add(this.chkNen_PiercingPartern);
            this.groupBox1.Controls.Add(this.chkNen_Hammer);
            this.groupBox1.Controls.Add(this.chkNen_BullishEngulfing);
            this.groupBox1.Controls.Add(this.chkSO);
            this.groupBox1.Controls.Add(this.chkMACD);
            this.groupBox1.Controls.Add(this.txtMaCK);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnLoc);
            this.groupBox1.Controls.Add(this.txtNguongKhoiLuong);
            this.groupBox1.Controls.Add(this.txtSoNgay);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtTuNgay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 92);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1085, 153);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tham số:";
            // 
            // chkRSI
            // 
            this.chkRSI.AutoSize = true;
            this.chkRSI.Location = new System.Drawing.Point(634, 67);
            this.chkRSI.Name = "chkRSI";
            this.chkRSI.Size = new System.Drawing.Size(51, 20);
            this.chkRSI.TabIndex = 16;
            this.chkRSI.Text = "RSI";
            this.chkRSI.UseVisualStyleBackColor = true;
            // 
            // chkBienDongKhoiLuong
            // 
            this.chkBienDongKhoiLuong.AutoSize = true;
            this.chkBienDongKhoiLuong.Location = new System.Drawing.Point(634, 41);
            this.chkBienDongKhoiLuong.Name = "chkBienDongKhoiLuong";
            this.chkBienDongKhoiLuong.Size = new System.Drawing.Size(175, 20);
            this.chkBienDongKhoiLuong.TabIndex = 16;
            this.chkBienDongKhoiLuong.Text = "Biến động khối lượng lớn";
            this.chkBienDongKhoiLuong.UseVisualStyleBackColor = true;
            // 
            // chkKietCung
            // 
            this.chkKietCung.AutoSize = true;
            this.chkKietCung.Location = new System.Drawing.Point(634, 15);
            this.chkKietCung.Name = "chkKietCung";
            this.chkKietCung.Size = new System.Drawing.Size(141, 20);
            this.chkKietCung.TabIndex = 16;
            this.chkKietCung.Text = "Tìm điểm kiệt cung";
            this.chkKietCung.UseVisualStyleBackColor = true;
            // 
            // chkMorningStar
            // 
            this.chkMorningStar.AutoSize = true;
            this.chkMorningStar.Location = new System.Drawing.Point(835, 67);
            this.chkMorningStar.Name = "chkMorningStar";
            this.chkMorningStar.Size = new System.Drawing.Size(185, 20);
            this.chkMorningStar.TabIndex = 15;
            this.chkMorningStar.Text = "Nến Sao mai Morning Star";
            this.chkMorningStar.UseVisualStyleBackColor = true;
            // 
            // chkNen_PiercingPartern
            // 
            this.chkNen_PiercingPartern.AutoSize = true;
            this.chkNen_PiercingPartern.Location = new System.Drawing.Point(835, 95);
            this.chkNen_PiercingPartern.Name = "chkNen_PiercingPartern";
            this.chkNen_PiercingPartern.Size = new System.Drawing.Size(151, 20);
            this.chkNen_PiercingPartern.TabIndex = 15;
            this.chkNen_PiercingPartern.Text = "Nến Piercing Pattern";
            this.chkNen_PiercingPartern.UseVisualStyleBackColor = true;
            // 
            // chkNen_Hammer
            // 
            this.chkNen_Hammer.AutoSize = true;
            this.chkNen_Hammer.Location = new System.Drawing.Point(835, 41);
            this.chkNen_Hammer.Name = "chkNen_Hammer";
            this.chkNen_Hammer.Size = new System.Drawing.Size(109, 20);
            this.chkNen_Hammer.TabIndex = 15;
            this.chkNen_Hammer.Text = "Nến Hammer";
            this.chkNen_Hammer.UseVisualStyleBackColor = true;
            // 
            // chkNen_BullishEngulfing
            // 
            this.chkNen_BullishEngulfing.AutoSize = true;
            this.chkNen_BullishEngulfing.Location = new System.Drawing.Point(835, 15);
            this.chkNen_BullishEngulfing.Name = "chkNen_BullishEngulfing";
            this.chkNen_BullishEngulfing.Size = new System.Drawing.Size(157, 20);
            this.chkNen_BullishEngulfing.TabIndex = 15;
            this.chkNen_BullishEngulfing.Text = "Nến Bullish Engulfing ";
            this.chkNen_BullishEngulfing.UseVisualStyleBackColor = true;
            // 
            // chkSO
            // 
            this.chkSO.AutoSize = true;
            this.chkSO.Location = new System.Drawing.Point(634, 119);
            this.chkSO.Name = "chkSO";
            this.chkSO.Size = new System.Drawing.Size(147, 20);
            this.chkSO.TabIndex = 15;
            this.chkSO.Text = "StochasticOscillator";
            this.chkSO.UseVisualStyleBackColor = true;
            // 
            // chkMACD
            // 
            this.chkMACD.AutoSize = true;
            this.chkMACD.Location = new System.Drawing.Point(634, 93);
            this.chkMACD.Name = "chkMACD";
            this.chkMACD.Size = new System.Drawing.Size(68, 20);
            this.chkMACD.TabIndex = 15;
            this.chkMACD.Text = "MACD";
            this.chkMACD.UseVisualStyleBackColor = true;
            // 
            // txtMaCK
            // 
            this.txtMaCK.Location = new System.Drawing.Point(386, 55);
            this.txtMaCK.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaCK.Name = "txtMaCK";
            this.txtMaCK.Size = new System.Drawing.Size(140, 22);
            this.txtMaCK.TabIndex = 12;
            this.txtMaCK.TextChanged += new System.EventHandler(this.txtMaCK_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(305, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Tìm mã CK:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 54);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ngưỡng khối lượng tăng đột biến (%)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(260, 111);
            this.btnLoc.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(140, 28);
            this.btnLoc.TabIndex = 10;
            this.btnLoc.Text = "Lọc cổ phiếu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // txtNguongKhoiLuong
            // 
            this.txtNguongKhoiLuong.Location = new System.Drawing.Point(135, 56);
            this.txtNguongKhoiLuong.Margin = new System.Windows.Forms.Padding(4);
            this.txtNguongKhoiLuong.Name = "txtNguongKhoiLuong";
            this.txtNguongKhoiLuong.Size = new System.Drawing.Size(140, 22);
            this.txtNguongKhoiLuong.TabIndex = 3;
            this.txtNguongKhoiLuong.Text = "200";
            // 
            // txtSoNgay
            // 
            this.txtSoNgay.Location = new System.Drawing.Point(386, 23);
            this.txtSoNgay.Margin = new System.Windows.Forms.Padding(4);
            this.txtSoNgay.Name = "txtSoNgay";
            this.txtSoNgay.Size = new System.Drawing.Size(140, 22);
            this.txtSoNgay.TabIndex = 0;
            this.txtSoNgay.Text = "28";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Số ngày xét";
            // 
            // txtTuNgay
            // 
            this.txtTuNgay.Location = new System.Drawing.Point(135, 26);
            this.txtTuNgay.Margin = new System.Windows.Forms.Padding(4);
            this.txtTuNgay.Name = "txtTuNgay";
            this.txtTuNgay.Size = new System.Drawing.Size(140, 22);
            this.txtTuNgay.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngày tối đa";
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
            this.kiểmTraDữLiệuHiệnCóToolStripMenuItem,
            this.xemDữLiệuCủa1MãCụThểToolStripMenuItem});
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
            // xemDữLiệuCủa1MãCụThểToolStripMenuItem
            // 
            this.xemDữLiệuCủa1MãCụThểToolStripMenuItem.Name = "xemDữLiệuCủa1MãCụThểToolStripMenuItem";
            this.xemDữLiệuCủa1MãCụThểToolStripMenuItem.Size = new System.Drawing.Size(399, 26);
            this.xemDữLiệuCủa1MãCụThểToolStripMenuItem.Text = "Xem dữ liệu của 1 mã cụ thể";
            this.xemDữLiệuCủa1MãCụThểToolStripMenuItem.Click += new System.EventHandler(this.xemDữLiệuCủa1MãCụThểToolStripMenuItem_Click);
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
            // frmPhanTichKyThuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 846);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPhanTichKyThuat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lọc chứng khoán theo khối lượng";
            this.Load += new System.EventHandler(this.frmMainKhoiLuong_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKQLoc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button btnMoThuMuc;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem danhMụcChứngKhoánQuanTâmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kiểmTraDữLiệuHiệnCóToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemDữLiệuCủa1MãCụThểToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRSI;
        private System.Windows.Forms.CheckBox chkBienDongKhoiLuong;
        private System.Windows.Forms.CheckBox chkKietCung;
        private System.Windows.Forms.CheckBox chkNen_BullishEngulfing;
        private System.Windows.Forms.CheckBox chkSO;
        private System.Windows.Forms.CheckBox chkMACD;
        private System.Windows.Forms.TextBox txtMaCK;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.TextBox txtNguongKhoiLuong;
        private System.Windows.Forms.TextBox txtTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSoNgay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkMorningStar;
        private System.Windows.Forms.CheckBox chkNen_PiercingPartern;
        private System.Windows.Forms.CheckBox chkNen_Hammer;
    }
}

