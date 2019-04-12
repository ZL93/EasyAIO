namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolGroupEdit1 = new EasyAIO.ToolGroupEdit();
            this.SuspendLayout();
            // 
            // toolGroupEdit1
            // 
            this.toolGroupEdit1.ConfigPath = "C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\CCDConfig.zl";
            this.toolGroupEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolGroupEdit1.Location = new System.Drawing.Point(0, 0);
            this.toolGroupEdit1.Lock = false;
            this.toolGroupEdit1.Name = "toolGroupEdit1";
            this.toolGroupEdit1.PicBox = null;
            this.toolGroupEdit1.Size = new System.Drawing.Size(1006, 661);
            this.toolGroupEdit1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 661);
            this.Controls.Add(this.toolGroupEdit1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private EasyAIO.ToolGroupEdit toolGroupEdit1;
    }
}

