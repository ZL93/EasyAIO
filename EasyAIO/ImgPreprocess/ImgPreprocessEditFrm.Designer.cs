namespace EasyAIO
{
    partial class ImgPreprocessFrm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_None = new System.Windows.Forms.TabPage();
            this.tabPage_Threshold = new System.Windows.Forms.TabPage();
            this.rdbtn_Relative = new System.Windows.Forms.RadioButton();
            this.rdbtn_Absolute = new System.Windows.Forms.RadioButton();
            this.rdbtn_Auto = new System.Windows.Forms.RadioButton();
            this.valueControl1 = new EasyAIO.ValueControl();
            this.tabPage_Morphology = new System.Windows.Forms.TabPage();
            this.vlctrl_height = new EasyAIO.ValueControl();
            this.vlctrl_width = new EasyAIO.ValueControl();
            this.rdbtn_circle = new System.Windows.Forms.RadioButton();
            this.rdbtn_rect = new System.Windows.Forms.RadioButton();
            this.rdbtn_square = new System.Windows.Forms.RadioButton();
            this.tabPage_GainOffset = new System.Windows.Forms.TabPage();
            this.floatValueControl1 = new EasyAIO.FloatValueControl();
            this.valueControl4 = new EasyAIO.ValueControl();
            this.tabPage_Roatae = new System.Windows.Forms.TabPage();
            this.valueControl3 = new EasyAIO.ValueControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.阈值处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.膨胀ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.腐蚀ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开运算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.闭运算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度调节ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旋转ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中值运算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobel运算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcrl_ToolEdit.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_Threshold.SuspendLayout();
            this.tabPage_Morphology.SuspendLayout();
            this.tabPage_GainOffset.SuspendLayout();
            this.tabPage_Roatae.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            // 
            // picbox_ToolEdit
            // 
            this.picbox_ToolEdit.Location = new System.Drawing.Point(0, 22);
            this.picbox_ToolEdit.Size = new System.Drawing.Size(750, 590);
            // 
            // cmbox_ToolEdit
            // 
            this.cmbox_ToolEdit.Size = new System.Drawing.Size(750, 22);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Location = new System.Drawing.Point(5, 380);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 199);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数列表";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage_None);
            this.tabControl1.Controls.Add(this.tabPage_Threshold);
            this.tabControl1.Controls.Add(this.tabPage_Morphology);
            this.tabControl1.Controls.Add(this.tabPage_GainOffset);
            this.tabControl1.Controls.Add(this.tabPage_Roatae);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(80, 25);
            this.tabControl1.Location = new System.Drawing.Point(3, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(401, 177);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 5;
            this.tabControl1.Tag = "";
            // 
            // tabPage_None
            // 
            this.tabPage_None.Location = new System.Drawing.Point(4, 29);
            this.tabPage_None.Name = "tabPage_None";
            this.tabPage_None.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_None.Size = new System.Drawing.Size(393, 144);
            this.tabPage_None.TabIndex = 7;
            this.tabPage_None.Text = "空";
            this.tabPage_None.UseVisualStyleBackColor = true;
            // 
            // tabPage_Threshold
            // 
            this.tabPage_Threshold.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Threshold.Controls.Add(this.rdbtn_Relative);
            this.tabPage_Threshold.Controls.Add(this.rdbtn_Absolute);
            this.tabPage_Threshold.Controls.Add(this.rdbtn_Auto);
            this.tabPage_Threshold.Controls.Add(this.valueControl1);
            this.tabPage_Threshold.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Threshold.Name = "tabPage_Threshold";
            this.tabPage_Threshold.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Threshold.Size = new System.Drawing.Size(393, 144);
            this.tabPage_Threshold.TabIndex = 0;
            this.tabPage_Threshold.Text = "二值化";
            // 
            // rdbtn_Relative
            // 
            this.rdbtn_Relative.AutoSize = true;
            this.rdbtn_Relative.Location = new System.Drawing.Point(277, 31);
            this.rdbtn_Relative.Name = "rdbtn_Relative";
            this.rdbtn_Relative.Size = new System.Drawing.Size(81, 18);
            this.rdbtn_Relative.TabIndex = 3;
            this.rdbtn_Relative.TabStop = true;
            this.rdbtn_Relative.Text = "相对阈值";
            this.rdbtn_Relative.UseVisualStyleBackColor = true;
            this.rdbtn_Relative.CheckedChanged += new System.EventHandler(this.rdbtn_Relative_CheckedChanged);
            // 
            // rdbtn_Absolute
            // 
            this.rdbtn_Absolute.AutoSize = true;
            this.rdbtn_Absolute.Location = new System.Drawing.Point(140, 31);
            this.rdbtn_Absolute.Name = "rdbtn_Absolute";
            this.rdbtn_Absolute.Size = new System.Drawing.Size(81, 18);
            this.rdbtn_Absolute.TabIndex = 2;
            this.rdbtn_Absolute.TabStop = true;
            this.rdbtn_Absolute.Text = "绝对阈值";
            this.rdbtn_Absolute.UseVisualStyleBackColor = true;
            this.rdbtn_Absolute.CheckedChanged += new System.EventHandler(this.rdbtn_Absolute_CheckedChanged);
            // 
            // rdbtn_Auto
            // 
            this.rdbtn_Auto.AutoSize = true;
            this.rdbtn_Auto.Location = new System.Drawing.Point(16, 31);
            this.rdbtn_Auto.Name = "rdbtn_Auto";
            this.rdbtn_Auto.Size = new System.Drawing.Size(81, 18);
            this.rdbtn_Auto.TabIndex = 1;
            this.rdbtn_Auto.TabStop = true;
            this.rdbtn_Auto.Text = "自动阈值";
            this.rdbtn_Auto.UseVisualStyleBackColor = true;
            this.rdbtn_Auto.CheckedChanged += new System.EventHandler(this.rdbtn_Auto_CheckedChanged);
            // 
            // valueControl1
            // 
            this.valueControl1.Location = new System.Drawing.Point(2, 66);
            this.valueControl1.Max = 255;
            this.valueControl1.Min = 0;
            this.valueControl1.Name = "valueControl1";
            this.valueControl1.Size = new System.Drawing.Size(387, 54);
            this.valueControl1.TabIndex = 0;
            this.valueControl1.Value = 0;
            this.valueControl1.ValueName = "阈值";
            this.valueControl1.ValueChanged += new EasyAIO.ValueControl.ValueChangedEventHandler(this.valueControl1_ValueChanged);
            // 
            // tabPage_Morphology
            // 
            this.tabPage_Morphology.Controls.Add(this.vlctrl_height);
            this.tabPage_Morphology.Controls.Add(this.vlctrl_width);
            this.tabPage_Morphology.Controls.Add(this.rdbtn_circle);
            this.tabPage_Morphology.Controls.Add(this.rdbtn_rect);
            this.tabPage_Morphology.Controls.Add(this.rdbtn_square);
            this.tabPage_Morphology.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Morphology.Name = "tabPage_Morphology";
            this.tabPage_Morphology.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Morphology.Size = new System.Drawing.Size(393, 144);
            this.tabPage_Morphology.TabIndex = 1;
            this.tabPage_Morphology.Text = "形态学";
            this.tabPage_Morphology.UseVisualStyleBackColor = true;
            // 
            // vlctrl_height
            // 
            this.vlctrl_height.Location = new System.Drawing.Point(6, 80);
            this.vlctrl_height.Max = 100;
            this.vlctrl_height.Min = 0;
            this.vlctrl_height.Name = "vlctrl_height";
            this.vlctrl_height.Size = new System.Drawing.Size(369, 54);
            this.vlctrl_height.TabIndex = 4;
            this.vlctrl_height.Value = 0;
            this.vlctrl_height.ValueName = "高度";
            this.vlctrl_height.ValueChanged += new EasyAIO.ValueControl.ValueChangedEventHandler(this.valueControl2_ValueChanged);
            // 
            // vlctrl_width
            // 
            this.vlctrl_width.Location = new System.Drawing.Point(6, 30);
            this.vlctrl_width.Max = 100;
            this.vlctrl_width.Min = 0;
            this.vlctrl_width.Name = "vlctrl_width";
            this.vlctrl_width.Size = new System.Drawing.Size(369, 44);
            this.vlctrl_width.TabIndex = 3;
            this.vlctrl_width.Value = 0;
            this.vlctrl_width.ValueName = "宽度";
            this.vlctrl_width.ValueChanged += new EasyAIO.ValueControl.ValueChangedEventHandler(this.valueControl2_ValueChanged);
            // 
            // rdbtn_circle
            // 
            this.rdbtn_circle.AutoSize = true;
            this.rdbtn_circle.Location = new System.Drawing.Point(245, 6);
            this.rdbtn_circle.Name = "rdbtn_circle";
            this.rdbtn_circle.Size = new System.Drawing.Size(53, 18);
            this.rdbtn_circle.TabIndex = 2;
            this.rdbtn_circle.Text = "圆形";
            this.rdbtn_circle.UseVisualStyleBackColor = true;
            this.rdbtn_circle.CheckedChanged += new System.EventHandler(this.rdbtn_CheckedChanged);
            // 
            // rdbtn_rect
            // 
            this.rdbtn_rect.AutoSize = true;
            this.rdbtn_rect.Location = new System.Drawing.Point(129, 6);
            this.rdbtn_rect.Name = "rdbtn_rect";
            this.rdbtn_rect.Size = new System.Drawing.Size(53, 18);
            this.rdbtn_rect.TabIndex = 1;
            this.rdbtn_rect.Text = "矩形";
            this.rdbtn_rect.UseVisualStyleBackColor = true;
            this.rdbtn_rect.CheckedChanged += new System.EventHandler(this.rdbtn_CheckedChanged);
            // 
            // rdbtn_square
            // 
            this.rdbtn_square.AutoSize = true;
            this.rdbtn_square.Checked = true;
            this.rdbtn_square.Location = new System.Drawing.Point(18, 6);
            this.rdbtn_square.Name = "rdbtn_square";
            this.rdbtn_square.Size = new System.Drawing.Size(67, 18);
            this.rdbtn_square.TabIndex = 0;
            this.rdbtn_square.TabStop = true;
            this.rdbtn_square.Text = "正方形";
            this.rdbtn_square.UseVisualStyleBackColor = true;
            this.rdbtn_square.CheckedChanged += new System.EventHandler(this.rdbtn_CheckedChanged);
            // 
            // tabPage_GainOffset
            // 
            this.tabPage_GainOffset.Controls.Add(this.floatValueControl1);
            this.tabPage_GainOffset.Controls.Add(this.valueControl4);
            this.tabPage_GainOffset.Location = new System.Drawing.Point(4, 29);
            this.tabPage_GainOffset.Name = "tabPage_GainOffset";
            this.tabPage_GainOffset.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_GainOffset.Size = new System.Drawing.Size(393, 144);
            this.tabPage_GainOffset.TabIndex = 5;
            this.tabPage_GainOffset.Text = "亮度调节";
            this.tabPage_GainOffset.UseVisualStyleBackColor = true;
            // 
            // floatValueControl1
            // 
            this.floatValueControl1.Location = new System.Drawing.Point(3, 10);
            this.floatValueControl1.Max = 3F;
            this.floatValueControl1.Min = 0F;
            this.floatValueControl1.Name = "floatValueControl1";
            this.floatValueControl1.Size = new System.Drawing.Size(387, 56);
            this.floatValueControl1.TabIndex = 4;
            this.floatValueControl1.Value = 0F;
            this.floatValueControl1.ValueName = "Gain";
            this.floatValueControl1.ValueChanged += new EasyAIO.FloatValueControl.ValueChangedEventHandler(this.floatValueControl1_ValueChanged);
            // 
            // valueControl4
            // 
            this.valueControl4.Location = new System.Drawing.Point(6, 72);
            this.valueControl4.Max = 255;
            this.valueControl4.Min = -255;
            this.valueControl4.Name = "valueControl4";
            this.valueControl4.Size = new System.Drawing.Size(381, 59);
            this.valueControl4.TabIndex = 3;
            this.valueControl4.Value = 0;
            this.valueControl4.ValueName = "OffSet";
            this.valueControl4.ValueChanged += new EasyAIO.ValueControl.ValueChangedEventHandler(this.valueControl4_ValueChanged);
            // 
            // tabPage_Roatae
            // 
            this.tabPage_Roatae.Controls.Add(this.valueControl3);
            this.tabPage_Roatae.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Roatae.Name = "tabPage_Roatae";
            this.tabPage_Roatae.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Roatae.Size = new System.Drawing.Size(393, 144);
            this.tabPage_Roatae.TabIndex = 6;
            this.tabPage_Roatae.Text = "旋转";
            this.tabPage_Roatae.UseVisualStyleBackColor = true;
            // 
            // valueControl3
            // 
            this.valueControl3.Location = new System.Drawing.Point(4, 39);
            this.valueControl3.Max = 360;
            this.valueControl3.Min = 0;
            this.valueControl3.Name = "valueControl3";
            this.valueControl3.Size = new System.Drawing.Size(383, 54);
            this.valueControl3.TabIndex = 0;
            this.valueControl3.Value = 0;
            this.valueControl3.ValueName = "角度";
            this.valueControl3.ValueChanged += new EasyAIO.ValueControl.ValueChangedEventHandler(this.valueControl3_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 370);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "处理列表";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(308, 22);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 33);
            this.button4.TabIndex = 4;
            this.button4.Text = "下移";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(210, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 33);
            this.button3.TabIndex = 3;
            this.button3.Text = "上移";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(111, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 33);
            this.button2.TabIndex = 2;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 62);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(383, 298);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 46;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型";
            this.columnHeader2.Width = 121;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "参数";
            this.columnHeader3.Width = 137;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.阈值处理ToolStripMenuItem,
            this.膨胀ToolStripMenuItem,
            this.腐蚀ToolStripMenuItem,
            this.开运算ToolStripMenuItem,
            this.闭运算ToolStripMenuItem,
            this.亮度调节ToolStripMenuItem,
            this.旋转ToolStripMenuItem,
            this.中值运算ToolStripMenuItem,
            this.sobel运算ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 202);
            // 
            // 阈值处理ToolStripMenuItem
            // 
            this.阈值处理ToolStripMenuItem.Name = "阈值处理ToolStripMenuItem";
            this.阈值处理ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.阈值处理ToolStripMenuItem.Text = "二值化";
            this.阈值处理ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 膨胀ToolStripMenuItem
            // 
            this.膨胀ToolStripMenuItem.Name = "膨胀ToolStripMenuItem";
            this.膨胀ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.膨胀ToolStripMenuItem.Text = "膨胀";
            this.膨胀ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 腐蚀ToolStripMenuItem
            // 
            this.腐蚀ToolStripMenuItem.Name = "腐蚀ToolStripMenuItem";
            this.腐蚀ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.腐蚀ToolStripMenuItem.Text = "腐蚀";
            this.腐蚀ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 开运算ToolStripMenuItem
            // 
            this.开运算ToolStripMenuItem.Name = "开运算ToolStripMenuItem";
            this.开运算ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.开运算ToolStripMenuItem.Text = "开运算";
            this.开运算ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 闭运算ToolStripMenuItem
            // 
            this.闭运算ToolStripMenuItem.Name = "闭运算ToolStripMenuItem";
            this.闭运算ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.闭运算ToolStripMenuItem.Text = "闭运算";
            this.闭运算ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 亮度调节ToolStripMenuItem
            // 
            this.亮度调节ToolStripMenuItem.Name = "亮度调节ToolStripMenuItem";
            this.亮度调节ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.亮度调节ToolStripMenuItem.Text = "亮度调节";
            this.亮度调节ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 旋转ToolStripMenuItem
            // 
            this.旋转ToolStripMenuItem.Name = "旋转ToolStripMenuItem";
            this.旋转ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.旋转ToolStripMenuItem.Text = "旋转";
            this.旋转ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 中值运算ToolStripMenuItem
            // 
            this.中值运算ToolStripMenuItem.Name = "中值运算ToolStripMenuItem";
            this.中值运算ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.中值运算ToolStripMenuItem.Text = "中值运算";
            this.中值运算ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // sobel运算ToolStripMenuItem
            // 
            this.sobel运算ToolStripMenuItem.Name = "sobel运算ToolStripMenuItem";
            this.sobel运算ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.sobel运算ToolStripMenuItem.Text = "Sobel运算";
            this.sobel运算ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ImgPreprocessFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 662);
            this.Name = "ImgPreprocessFrm";
            this.Text = "图像预处理";
            this.Load += new System.EventHandler(this.ImgPreprocessEditFrm_Load);
            this.tbcrl_ToolEdit.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Threshold.ResumeLayout(false);
            this.tabPage_Threshold.PerformLayout();
            this.tabPage_Morphology.ResumeLayout(false);
            this.tabPage_Morphology.PerformLayout();
            this.tabPage_GainOffset.ResumeLayout(false);
            this.tabPage_Roatae.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Threshold;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 膨胀ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 腐蚀ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开运算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 闭运算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 阈值处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亮度调节ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旋转ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中值运算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobel运算ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage_Morphology;
        private System.Windows.Forms.TabPage tabPage_GainOffset;
        private System.Windows.Forms.TabPage tabPage_Roatae;
        private ValueControl valueControl1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private ValueControl valueControl4;
        private FloatValueControl floatValueControl1;
        private ValueControl valueControl3;
        private System.Windows.Forms.TabPage tabPage_None;
        private ValueControl vlctrl_height;
        private ValueControl vlctrl_width;
        private System.Windows.Forms.RadioButton rdbtn_circle;
        private System.Windows.Forms.RadioButton rdbtn_rect;
        private System.Windows.Forms.RadioButton rdbtn_square;
        private System.Windows.Forms.RadioButton rdbtn_Relative;
        private System.Windows.Forms.RadioButton rdbtn_Absolute;
        private System.Windows.Forms.RadioButton rdbtn_Auto;
    }
}