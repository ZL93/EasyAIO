namespace EasyAIO
{
    partial class FindPointFrm
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
            this.cmbox_shape = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numTextBox1 = new EasyAIO.NumTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txt_max1 = new EasyAIO.NumTextBox();
            this.txt_min1 = new EasyAIO.NumTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_num = new System.Windows.Forms.TextBox();
            this.txt_centerY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_centerX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbcrl_ToolEdit.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcrl_ToolEdit
            // 
            this.tbcrl_ToolEdit.Controls.Add(this.tabPage3);
            this.tbcrl_ToolEdit.Controls.SetChildIndex(this.tabPage3, 0);
            this.tbcrl_ToolEdit.Controls.SetChildIndex(this.tabPage2, 0);
            this.tbcrl_ToolEdit.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbox_shape);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.SetChildIndex(this.label14, 0);
            this.tabPage1.Controls.SetChildIndex(this.cmbox_shape, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            // 
            // picbox_ToolEdit
            // 
            this.picbox_ToolEdit.Location = new System.Drawing.Point(0, 22);
            this.picbox_ToolEdit.ShowPropertyGrid = true;
            this.picbox_ToolEdit.ShowShape = true;
            this.picbox_ToolEdit.Size = new System.Drawing.Size(750, 590);
            // 
            // cmbox_ToolEdit
            // 
            this.cmbox_ToolEdit.Size = new System.Drawing.Size(750, 22);
            // 
            // cmbox_shape
            // 
            this.cmbox_shape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_shape.FormattingEnabled = true;
            this.cmbox_shape.Items.AddRange(new object[] {
            "默认坐标系"});
            this.cmbox_shape.Location = new System.Drawing.Point(157, 147);
            this.cmbox_shape.Name = "cmbox_shape";
            this.cmbox_shape.Size = new System.Drawing.Size(232, 22);
            this.cmbox_shape.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(60, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 14);
            this.label14.TabIndex = 30;
            this.label14.Text = "选择坐标系";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numTextBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 248);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检测参数";
            // 
            // numTextBox1
            // 
            this.numTextBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.numTextBox1.Location = new System.Drawing.Point(162, 123);
            this.numTextBox1.MaxValue = 255;
            this.numTextBox1.MinValue = 0;
            this.numTextBox1.Name = "numTextBox1";
            this.numTextBox1.Size = new System.Drawing.Size(121, 23);
            this.numTextBox1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "采样阈值:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "起始",
            "最后",
            "最强"});
            this.comboBox2.Location = new System.Drawing.Point(162, 79);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 22);
            this.comboBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "边缘位置:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "黑->白",
            "白->黑",
            "任意",
            "黑->白->黑",
            "白->黑->白"});
            this.comboBox1.Location = new System.Drawing.Point(162, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 22);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "边缘极性:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(441, 609);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "结果与分析";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.txt_max1);
            this.groupBox3.Controls.Add(this.txt_min1);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(11, 280);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 319);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据判断";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 49);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 18);
            this.checkBox1.TabIndex = 42;
            this.checkBox1.Tag = "0";
            this.checkBox1.Text = "点个数";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txt_max1
            // 
            this.txt_max1.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_max1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_max1.Location = new System.Drawing.Point(289, 46);
            this.txt_max1.MaxValue = 999999;
            this.txt_max1.MinValue = 0;
            this.txt_max1.Name = "txt_max1";
            this.txt_max1.Size = new System.Drawing.Size(83, 23);
            this.txt_max1.TabIndex = 30;
            this.txt_max1.Tag = "0";
            this.txt_max1.Text = "999999";
            this.txt_max1.TextChanged += new System.EventHandler(this.txt_max_TextChanged);
            // 
            // txt_min1
            // 
            this.txt_min1.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_min1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_min1.Location = new System.Drawing.Point(132, 46);
            this.txt_min1.MaxValue = 999999;
            this.txt_min1.MinValue = 0;
            this.txt_min1.Name = "txt_min1";
            this.txt_min1.Size = new System.Drawing.Size(83, 23);
            this.txt_min1.TabIndex = 29;
            this.txt_min1.Tag = "0";
            this.txt_min1.Text = "0";
            this.txt_min1.TextChanged += new System.EventHandler(this.txt_min_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(315, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 14);
            this.label11.TabIndex = 28;
            this.label11.Text = "上限";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(150, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 14);
            this.label12.TabIndex = 27;
            this.label12.Text = "下限";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txt_num);
            this.groupBox2.Controls.Add(this.txt_centerY);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_centerX);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(9, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 241);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "点数据";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(75, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 14);
            this.label13.TabIndex = 10;
            this.label13.Text = "点个数";
            // 
            // txt_num
            // 
            this.txt_num.Location = new System.Drawing.Point(155, 46);
            this.txt_num.Name = "txt_num";
            this.txt_num.ReadOnly = true;
            this.txt_num.Size = new System.Drawing.Size(122, 23);
            this.txt_num.TabIndex = 7;
            // 
            // txt_centerY
            // 
            this.txt_centerY.Location = new System.Drawing.Point(155, 150);
            this.txt_centerY.Name = "txt_centerY";
            this.txt_centerY.ReadOnly = true;
            this.txt_centerY.Size = new System.Drawing.Size(122, 23);
            this.txt_centerY.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y坐标";
            // 
            // txt_centerX
            // 
            this.txt_centerX.Location = new System.Drawing.Point(155, 98);
            this.txt_centerX.Name = "txt_centerX";
            this.txt_centerX.ReadOnly = true;
            this.txt_centerX.Size = new System.Drawing.Size(122, 23);
            this.txt_centerX.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "X坐标";
            // 
            // FindPointFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 662);
            this.Name = "FindPointFrm";
            this.Text = "FindPointFrm";
            this.Load += new System.EventHandler(this.FindLineFrm_Load);
            this.tbcrl_ToolEdit.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbox_shape;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private NumTextBox numTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private NumTextBox txt_max1;
        private NumTextBox txt_min1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_num;
        private System.Windows.Forms.TextBox txt_centerY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_centerX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}