using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    public partial class FindCircleFrm : ToolEditBaseFrm
    {
        #region 公共
        private FindCircleEvent circleEvent = null;
        public FindCircleFrm(FindCircleEvent circleE)
            : base(circleE)
        {
            InitializeComponent();
            circleEvent = circleE;
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();
            circleEvent.circleConfig.FullCircle = chkbox_fullcircle.Checked;
            circleEvent.circleConfig.TransitionType = comboBox1.SelectedIndex;
            circleEvent.circleConfig.TransitionChoice = comboBox2.SelectedIndex;
            circleEvent.circleConfig.Threshold = Convert.ToInt32(numTextBox1.Text);
            circleEvent.circleConfig.SamplingStep = Convert.ToInt32(numTextBox2.Text);
            circleEvent.circleConfig.Passes = Convert.ToInt32(numTextBox3.Text);

            circleEvent.circleConfig.CenterX = circleEvent.CircleGauge.CenterX;
            circleEvent.circleConfig.CenterY = circleEvent.CircleGauge.CenterY;
            if (!circleEvent.circleConfig.FullCircle &&circleEvent.CircleGauge.Amplitude != 360)
            {
                circleEvent.circleConfig.Amplitude = circleEvent.CircleGauge.Amplitude;
            }
            circleEvent.circleConfig.Angle = circleEvent.CircleGauge.Angle;
            circleEvent.circleConfig.Tolerance = circleEvent.CircleGauge.Tolerance;
            circleEvent.circleConfig.Diameter = circleEvent.CircleGauge.Diameter;
            if (cmbox_shape.SelectedItem != null)
            {
                circleEvent.circleConfig.ShapeName = cmbox_shape.SelectedItem.ToString();
            }
            
            Run();
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();

            chkbox_fullcircle.Checked = circleEvent.circleConfig.FullCircle;
            comboBox1.SelectedIndex = circleEvent.circleConfig.TransitionType;
            comboBox2.SelectedIndex = circleEvent.circleConfig.TransitionChoice;
            numTextBox1.Text = circleEvent.circleConfig.Threshold.ToString();
            numTextBox2.Text = circleEvent.circleConfig.SamplingStep.ToString();
            numTextBox3.Text = circleEvent.circleConfig.Passes.ToString();
           
            foreach (var item in circleEvent.circleConfig.ResultsDetermines)
            {
                switch (item.Name)
                {
                    case "中心X":
                        checkBox1.Checked = item.Enable;
                        txt_min1.Text = item.Min.ToString();
                        txt_max1.Text = item.Max.ToString();
                        break;
                    case "中心Y":
                        checkBox2.Checked = item.Enable;
                        txt_min2.Text = item.Min.ToString();
                        txt_max2.Text = item.Max.ToString();
                        break;
                    case "直径":
                        checkBox3.Checked = item.Enable;
                        txt_min3.Text = item.Min.ToString();
                        txt_max3.Text = item.Max.ToString();
                        break;
                    case "周长":
                        checkBox4.Checked = item.Enable;
                        txt_min4.Text = item.Min.ToString();
                        txt_max4.Text = item.Max.ToString();
                        break;
                    case "异常点数":
                        checkBox6.Checked = item.Enable;
                        txt_min6.Text = item.Min.ToString();
                        txt_max6.Text = item.Max.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void Run()
        {
            stopWatch.Reset();
            stopWatch.Start();
            if (circleEvent.Run(false))
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            picbox_ToolEdit.MFrameShape = circleEvent.shape;
            picbox_ToolEdit.Refresh();
            txt_centerX.Text = circleEvent.CircleGauge.MeasuredCircle.CenterX.ToString();
            txt_centerY.Text = circleEvent.CircleGauge.MeasuredCircle.CenterY.ToString();
            txt_diameter.Text = circleEvent.CircleGauge.MeasuredCircle.Diameter.ToString();
            txt_length.Text = circleEvent.CircleGauge.MeasuredCircle.ArcLength.ToString();
            int passNum = circleEvent.CircleGauge.NumSamples - circleEvent.CircleGauge.NumValidSamples;
            txt_passNum.Text = passNum.ToString();
            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }
        #endregion

       
        private void FindCircleFrm_Load(object sender, EventArgs e)
        {
            cmbox_shape.SelectedIndex = 0;
            circleEvent.CircleGauge.Attach(picbox_ToolEdit.MFrameShape);
            picbox_ToolEdit.MShapes.Add(circleEvent.CircleGauge);

            foreach (var item in circleEvent.ParentTask.Events)
            {
                if (item is ShapeEvent)
                {
                    ShapeEvent se = item as ShapeEvent;
                    cmbox_shape.Items.Add(se.Config.ToolName);
                }
            }
            picbox_ToolEdit.MFrameShape = circleEvent.shape;
            for (int i = 0; i < cmbox_shape.Items.Count; i++)
            {
                string s = cmbox_shape.Items[i].ToString();
                if (s == circleEvent.circleConfig.ShapeName)
                {
                    cmbox_shape.SelectedIndex = i;
                    break;
                }
            }

            comboBox1.SelectedIndexChanged += delegate { UpdateUIToEvent(); };
            comboBox2.SelectedIndexChanged += delegate { UpdateUIToEvent(); };
            numTextBox1.TextChanged += delegate { UpdateUIToEvent(); };
            numTextBox2.TextChanged += delegate { UpdateUIToEvent(); };
            numTextBox3.TextChanged += delegate { UpdateUIToEvent(); };
            chkbox_fullcircle.CheckedChanged += delegate { UpdateUIToEvent(); };
            cmbox_shape.SelectedIndexChanged += delegate { UpdateUIToEvent(); };

            Run();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int index = Convert.ToInt32(chk.Tag);
            circleEvent.circleConfig.ResultsDetermines[index].Enable = chk.Checked;
        }

        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                circleEvent.circleConfig.ResultsDetermines[index].Min = value;
            }
            catch { }
            
        }

        private void txt_max_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                circleEvent.circleConfig.ResultsDetermines[index].Max = value;
            }
            catch { }
           
        }
    }
}
