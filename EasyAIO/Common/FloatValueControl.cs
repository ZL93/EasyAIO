////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//              佛祖保佑       永无BUG     永不修改                  //
////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    public partial class FloatValueControl : UserControl
    {
        public FloatValueControl()
        {
            InitializeComponent();
        }

        private string _valuName;
        public string ValueName
        {
            get { return _valuName; }
            set
            {
                label1.Text = _valuName = value;
            }
        }

        private float _min;

        public float Min
        {
            get { return _min; }
            set
            {
                _min = value;
                trackBar1.Minimum = (int)(_min * 1000);
            }
        }
        private float _max;

        public float Max
        {
            get { return _max; }
            set
            {
                _max = value;
                trackBar1.Maximum = (int)(_max * 1000);
            }
        }

        private float _value;

        public float Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    trackBar1.Value = (int)(_value * 1000);
                    textBox1.Text = _value.ToString();
                    OnValueChanged(new ValueChangedEventArgs(_value));
                }
            }
        }
        public event ValueChangedEventHandler ValueChanged;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Value = (float)trackBar1.Value / 1000;
            textBox1.Text = Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                return;
            }
            int value =(int)(Convert.ToSingle(textBox1.Text) *1000);
            if (value > trackBar1.Maximum)
            {
                value = trackBar1.Maximum;
                textBox1.Text = ((float)trackBar1.Maximum/1000).ToString(); 
            }
            if (value < trackBar1.Minimum)
            {
                value = trackBar1.Minimum;
                textBox1.Text = ((float)trackBar1.Minimum / 1000).ToString();
            }
            trackBar1.Value = value;
            Value = Convert.ToSingle(textBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46 && e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
        }

        protected void OnValueChanged(ValueChangedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
        public delegate void ValueChangedEventHandler(object o, ValueChangedEventArgs e);
        public class ValueChangedEventArgs : EventArgs
        {
            public float Value { get; set; }
            public ValueChangedEventArgs(float value)
            {
                Value = value;
            }
        }
    }
    
}
