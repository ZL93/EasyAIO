namespace EasyAIO
{
    partial class ShapeFrm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_angle = new System.Windows.Forms.Button();
            this.btn_centerY = new System.Windows.Forms.Button();
            this.btn_centerX = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_angle);
            this.panel3.Controls.Add(this.btn_centerY);
            this.panel3.Controls.Add(this.btn_centerX);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Location = new System.Drawing.Point(5, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(394, 185);
            this.panel3.TabIndex = 29;
            // 
            // btn_angle
            // 
            this.btn_angle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_angle.Location = new System.Drawing.Point(136, 121);
            this.btn_angle.Margin = new System.Windows.Forms.Padding(2);
            this.btn_angle.Name = "btn_angle";
            this.btn_angle.Size = new System.Drawing.Size(240, 22);
            this.btn_angle.TabIndex = 24;
            this.btn_angle.Tag = "angle";
            this.btn_angle.Text = "...";
            this.btn_angle.UseVisualStyleBackColor = true;
            this.btn_angle.Click += new System.EventHandler(this.btn_centerX_Click);
            // 
            // btn_centerY
            // 
            this.btn_centerY.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_centerY.Location = new System.Drawing.Point(136, 71);
            this.btn_centerY.Margin = new System.Windows.Forms.Padding(2);
            this.btn_centerY.Name = "btn_centerY";
            this.btn_centerY.Size = new System.Drawing.Size(240, 22);
            this.btn_centerY.TabIndex = 23;
            this.btn_centerY.Tag = "centery";
            this.btn_centerY.Text = "...";
            this.btn_centerY.UseVisualStyleBackColor = true;
            this.btn_centerY.Click += new System.EventHandler(this.btn_centerX_Click);
            // 
            // btn_centerX
            // 
            this.btn_centerX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_centerX.Location = new System.Drawing.Point(136, 21);
            this.btn_centerX.Margin = new System.Windows.Forms.Padding(2);
            this.btn_centerX.Name = "btn_centerX";
            this.btn_centerX.Size = new System.Drawing.Size(240, 22);
            this.btn_centerX.TabIndex = 22;
            this.btn_centerX.Tag = "centerx";
            this.btn_centerX.Text = "...";
            this.btn_centerX.UseVisualStyleBackColor = true;
            this.btn_centerX.Click += new System.EventHandler(this.btn_centerX_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 127);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 16;
            this.label15.Text = "链接角度";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 76);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 12);
            this.label16.TabIndex = 15;
            this.label16.Text = "链接原心Y坐标";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 25);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 12);
            this.label17.TabIndex = 14;
            this.label17.Text = "链接原心X坐标";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(322, 398);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 31;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_OK.Location = new System.Drawing.Point(241, 398);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 30;
            this.btn_OK.Text = "确认";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ShapeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 433);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShapeFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "坐标系";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_angle;
        private System.Windows.Forms.Button btn_centerY;
        private System.Windows.Forms.Button btn_centerX;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
    }
}