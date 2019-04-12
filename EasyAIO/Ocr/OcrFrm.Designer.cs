namespace EasyAIO
{
    partial class OcrFrm
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
            this.ckBox_Roi = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_ocrPath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lb_result = new System.Windows.Forms.Label();
            this.lb_err = new System.Windows.Forms.Label();
            this.tbcrl_ToolEdit.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ckBox_Roi);
            this.tabPage1.Controls.SetChildIndex(this.ckBox_Roi, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lb_err);
            this.tabPage2.Controls.Add(this.lb_result);
            this.tabPage2.Controls.Add(this.label29);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.txt_ocrPath);
            this.tabPage2.Controls.Add(this.label10);
            // 
            // picbox_ToolEdit
            // 
            this.picbox_ToolEdit.Location = new System.Drawing.Point(0, 22);
            this.picbox_ToolEdit.Size = new System.Drawing.Size(750, 590);
            this.picbox_ToolEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.picbox_ToolEdit_Paint);
            // 
            // cmbox_ToolEdit
            // 
            this.cmbox_ToolEdit.Size = new System.Drawing.Size(750, 22);
            // 
            // ckBox_Roi
            // 
            this.ckBox_Roi.AutoSize = true;
            this.ckBox_Roi.Location = new System.Drawing.Point(63, 132);
            this.ckBox_Roi.Name = "ckBox_Roi";
            this.ckBox_Roi.Size = new System.Drawing.Size(47, 18);
            this.ckBox_Roi.TabIndex = 15;
            this.ckBox_Roi.Text = "Roi";
            this.ckBox_Roi.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(381, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 52;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_ocrPath
            // 
            this.txt_ocrPath.Location = new System.Drawing.Point(136, 57);
            this.txt_ocrPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_ocrPath.Name = "txt_ocrPath";
            this.txt_ocrPath.ReadOnly = true;
            this.txt_ocrPath.Size = new System.Drawing.Size(232, 23);
            this.txt_ocrPath.TabIndex = 51;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 14);
            this.label10.TabIndex = 50;
            this.label10.Text = "OCR文件路径";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.Location = new System.Drawing.Point(48, 132);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(63, 14);
            this.label29.TabIndex = 59;
            this.label29.Text = "执行结果";
            // 
            // lb_result
            // 
            this.lb_result.AutoSize = true;
            this.lb_result.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_result.Location = new System.Drawing.Point(132, 126);
            this.lb_result.Name = "lb_result";
            this.lb_result.Size = new System.Drawing.Size(19, 20);
            this.lb_result.TabIndex = 60;
            this.lb_result.Text = "0";
            // 
            // lb_err
            // 
            this.lb_err.AutoSize = true;
            this.lb_err.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_err.ForeColor = System.Drawing.Color.Red;
            this.lb_err.Location = new System.Drawing.Point(17, 569);
            this.lb_err.Name = "lb_err";
            this.lb_err.Size = new System.Drawing.Size(149, 19);
            this.lb_err.TabIndex = 61;
            this.lb_err.Text = "匹配文件不存在";
            this.lb_err.Visible = false;
            // 
            // OcrFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 662);
            this.Name = "OcrFrm";
            this.Text = "OcrFrm";
            this.Load += new System.EventHandler(this.OcrFrm_Load);
            this.tbcrl_ToolEdit.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_ToolEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckBox_Roi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_ocrPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lb_result;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lb_err;
    }
}