using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    [ToolboxBitmap(typeof(TextBox))]
    internal partial class NumTextBox : TextBox
    {
       
        private int _min = 0;
        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue
        {
            get { return _min; }
            set { _min = value; }
        }

      
        private int _max = 999999;
        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue
        {
            get { return _max; }
            set { _max = value; }
        }

        public NumTextBox()
        {
            InitializeComponent();
            BackColor = Color.AliceBlue;
            this.Size = new Size(178, 29);
            Text = "";
        }

        protected override void OnTextChanged(EventArgs e)
        {
            double d ;
            if (double.TryParse(Text,out d))
            {
                if (d < MinValue)
                {
                    Text = MinValue.ToString();
                }
                if (d > MaxValue)
                {
                    Text = MaxValue.ToString();
                }
            }
            base.OnTextChanged(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {

            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46 && e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 46 && Text.Contains("."))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e); 
        }
    }
}
