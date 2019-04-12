namespace EasyAIO
{
    partial class ToolEditBaseFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolEditBaseFrm));
            this.tbcrl_ToolEdit = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkBox_Draw = new System.Windows.Forms.CheckBox();
            this.comBox_img = new System.Windows.Forms.ComboBox();
            this.lb_1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlbtn_run = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.帮助LToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsl_status = new System.Windows.Forms.ToolStripLabel();
            this.tsl_CT = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picbox_ToolEdit = new EasyAIO.MPicBox();
            this.cmbox_ToolEdit = new System.Windows.Forms.ComboBox();
            this.tbcrl_ToolEdit.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // tbcrl_ToolEdit
            // 
            this.tbcrl_ToolEdit.Controls.Add(this.tabPage1);
            this.tbcrl_ToolEdit.Controls.Add(this.tabPage2);
            this.tbcrl_ToolEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbcrl_ToolEdit.Location = new System.Drawing.Point(0, 0);
            this.tbcrl_ToolEdit.Name = "tbcrl_ToolEdit";
            this.tbcrl_ToolEdit.SelectedIndex = 0;
            this.tbcrl_ToolEdit.Size = new System.Drawing.Size(449, 637);
            this.tbcrl_ToolEdit.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkBox_Draw);
            this.tabPage1.Controls.Add(this.comBox_img);
            this.tabPage1.Controls.Add(this.lb_1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(441, 609);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工具";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkBox_Draw
            // 
            this.chkBox_Draw.AutoSize = true;
            this.chkBox_Draw.Location = new System.Drawing.Point(63, 91);
            this.chkBox_Draw.Name = "chkBox_Draw";
            this.chkBox_Draw.Size = new System.Drawing.Size(138, 18);
            this.chkBox_Draw.TabIndex = 14;
            this.chkBox_Draw.Text = "绘制到外部图片框";
            this.chkBox_Draw.UseVisualStyleBackColor = true;
            // 
            // comBox_img
            // 
            this.comBox_img.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBox_img.FormattingEnabled = true;
            this.comBox_img.Items.AddRange(new object[] {
            "<NULL>"});
            this.comBox_img.Location = new System.Drawing.Point(157, 41);
            this.comBox_img.Name = "comBox_img";
            this.comBox_img.Size = new System.Drawing.Size(232, 22);
            this.comBox_img.TabIndex = 13;
            // 
            // lb_1
            // 
            this.lb_1.AutoSize = true;
            this.lb_1.Location = new System.Drawing.Point(60, 44);
            this.lb_1.Name = "lb_1";
            this.lb_1.Size = new System.Drawing.Size(63, 14);
            this.lb_1.TabIndex = 12;
            this.lb_1.Text = "输入图像";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(441, 609);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbtn_run,
            this.保存SToolStripButton,
            this.帮助LToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1199, 25);
            this.toolStrip1.TabIndex = 6;
            // 
            // tlbtn_run
            // 
            this.tlbtn_run.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbtn_run.Image = ((System.Drawing.Image)(resources.GetObject("tlbtn_run.Image")));
            this.tlbtn_run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtn_run.Name = "tlbtn_run";
            this.tlbtn_run.Size = new System.Drawing.Size(23, 22);
            this.tlbtn_run.Text = "运行";
            this.tlbtn_run.Click += new System.EventHandler(this.tlbtn_run_Click);
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.保存SToolStripButton.Text = "保存(&S)";
            this.保存SToolStripButton.ToolTipText = "保存";
            this.保存SToolStripButton.Click += new System.EventHandler(this.保存SToolStripButton_Click);
            // 
            // 帮助LToolStripButton
            // 
            this.帮助LToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.帮助LToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("帮助LToolStripButton.Image")));
            this.帮助LToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.帮助LToolStripButton.Name = "帮助LToolStripButton";
            this.帮助LToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.帮助LToolStripButton.Text = "帮助(&L)";
            this.帮助LToolStripButton.Click += new System.EventHandler(this.帮助LToolStripButton_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl_status,
            this.tsl_CT,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolStripSeparator3,
            this.toolStripLabel2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 612);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(750, 25);
            this.toolStrip2.TabIndex = 7;
            // 
            // tsl_status
            // 
            this.tsl_status.AutoSize = false;
            this.tsl_status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsl_status.Image = global::EasyAIO.Properties.Resources.green;
            this.tsl_status.Name = "tsl_status";
            this.tsl_status.Size = new System.Drawing.Size(23, 22);
            this.tsl_status.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tsl_CT
            // 
            this.tsl_CT.Name = "tsl_CT";
            this.tsl_CT.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.tsl_CT.Size = new System.Drawing.Size(66, 22);
            this.tsl_CT.Text = "0 ms";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.toolStripLabel1.Size = new System.Drawing.Size(154, 22);
            this.toolStripLabel1.Text = "ImgSize";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Padding = new System.Windows.Forms.Padding(150, 0, 0, 0);
            this.toolStripLabel3.Size = new System.Drawing.Size(197, 22);
            this.toolStripLabel3.Text = "X=,Y=,";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Padding = new System.Windows.Forms.Padding(60, 0, 0, 0);
            this.toolStripLabel2.Size = new System.Drawing.Size(108, 22);
            this.toolStripLabel2.Text = "Gray =";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tbcrl_ToolEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 637);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picbox_ToolEdit);
            this.panel2.Controls.Add(this.cmbox_ToolEdit);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(449, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(750, 637);
            this.panel2.TabIndex = 2;
            // 
            // picbox_ToolEdit
            // 
            this.picbox_ToolEdit.BackColor = System.Drawing.Color.MidnightBlue;
            this.picbox_ToolEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbox_ToolEdit.Image = null;
            this.picbox_ToolEdit.Location = new System.Drawing.Point(0, 22);
            this.picbox_ToolEdit.MFrameShape = null;
            this.picbox_ToolEdit.Name = "picbox_ToolEdit";
            this.picbox_ToolEdit.ShowPropertyGrid = false;
            this.picbox_ToolEdit.Size = new System.Drawing.Size(750, 590);
            this.picbox_ToolEdit.TabIndex = 1;
            this.picbox_ToolEdit.TabStop = false;
            this.picbox_ToolEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picbox_ToolEdit_MouseMove);
            // 
            // cmbox_ToolEdit
            // 
            this.cmbox_ToolEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbox_ToolEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbox_ToolEdit.FormattingEnabled = true;
            this.cmbox_ToolEdit.Location = new System.Drawing.Point(0, 0);
            this.cmbox_ToolEdit.Name = "cmbox_ToolEdit";
            this.cmbox_ToolEdit.Size = new System.Drawing.Size(750, 22);
            this.cmbox_ToolEdit.TabIndex = 0;
            // 
            // ToolEditBaseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 662);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolEditBaseFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToolEditBaseFrm";
            this.Load += new System.EventHandler(this.ToolEditBaseFrm_Load);
            this.tbcrl_ToolEdit.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TabControl tbcrl_ToolEdit;
        protected System.Windows.Forms.TabPage tabPage1;
        protected System.Windows.Forms.TabPage tabPage2;
        protected System.Windows.Forms.ToolStripButton tlbtn_run;
        protected System.Windows.Forms.ToolStripButton 保存SToolStripButton;
        protected System.Windows.Forms.ToolStripButton 帮助LToolStripButton;
        protected System.Windows.Forms.ToolStrip toolStrip2;
        protected System.Windows.Forms.ToolStripLabel tsl_status;
        protected System.Windows.Forms.ToolStripLabel tsl_CT;
        private System.Windows.Forms.Panel panel1;
        protected MPicBox picbox_ToolEdit;
        protected System.Windows.Forms.ComboBox cmbox_ToolEdit;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Label lb_1;
        internal System.Windows.Forms.ComboBox comBox_img;
        private System.Windows.Forms.CheckBox chkBox_Draw;
    }
}