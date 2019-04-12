using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    public partial class ValueControl : UserControl
    {
        public ValueControl()
        {
            InitializeComponent();
        }

        private string  _valuName;
        public string  ValueName
        {
            get { return _valuName; }
            set
            {
                label1.Text = _valuName = value;
            }
        }

        private int _min;

        public int Min
        {
            get { return _min; }
            set { 
               trackBar1.Minimum = _min = value; }
        }
        private int _max;

        public int Max
        {
            get { return _max; }
            set
            {
                trackBar1.Maximum = _max = value;
            }
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    trackBar1.Value = _value = value;
                    textBox1.Text = _value.ToString();
                    OnValueChanged(new ValueChangedEventArgs(_value));
                }
            }
        }

        public event ValueChangedEventHandler ValueChanged;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Value = trackBar1.Value;
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                return;
            }
            int value =Convert.ToInt32(textBox1.Text);
            if (value > trackBar1.Maximum)
            {
                value = trackBar1.Maximum;
                textBox1.Text = trackBar1.Maximum.ToString(); 
            }
            if (value < trackBar1.Minimum)
            {
                value = trackBar1.Minimum;
                textBox1.Text = trackBar1.Minimum.ToString(); 
            }
            Value = trackBar1.Value = value;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45)
            {
                e.Handled = true;
            }
        }

        protected void OnValueChanged(ValueChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this,e);
            }
        }

        public delegate void ValueChangedEventHandler(object o, ValueChangedEventArgs e);
        public class ValueChangedEventArgs : EventArgs
        {
            public int Value { get; set; }
            public ValueChangedEventArgs(int value)
            {
                Value = value;
            }
        }
    }
   
}
