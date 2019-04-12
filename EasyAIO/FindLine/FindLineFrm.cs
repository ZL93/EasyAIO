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
    public partial class FindLineFrm : ToolEditBaseFrm
    {
        #region 公共
        private FindLineEvent lineEvent = null;
        public FindLineFrm(FindLineEvent lineE)
            : base(lineE)
        {
            InitializeComponent();
            lineEvent = lineE;
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();
           
            lineEvent.lineConfig.TransitionType = comboBox1.SelectedIndex;
            lineEvent.lineConfig.TransitionChoice = comboBox2.SelectedIndex;
            lineEvent.lineConfig.Threshold = Convert.ToInt32(numTextBox1.Text);
            lineEvent.lineConfig.SamplingStep = Convert.ToInt32(numTextBox2.Text);
            lineEvent.lineConfig.Passes = Convert.ToInt32(numTextBox3.Text);

            lineEvent.lineConfig.CenterX = lineEvent.LineGauge.CenterX;
            lineEvent.lineConfig.CenterY = lineEvent.LineGauge.CenterY;
            lineEvent.lineConfig.Length = lineEvent.LineGauge.Length;
            lineEvent.lineConfig.Angle = lineEvent.LineGauge.Angle;
            lineEvent.lineConfig.Tolerance = lineEvent.LineGauge.Tolerance;
           
            if (cmbox_shape.SelectedItem != null)
            {
                lineEvent.lineConfig.ShapeName = cmbox_shape.SelectedItem.ToString();
            }
            
            Run();
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();
         
            comboBox1.SelectedIndex = lineEvent.lineConfig.TransitionType;
            comboBox2.SelectedIndex = lineEvent.lineConfig.TransitionChoice;
            numTextBox1.Text = lineEvent.lineConfig.Threshold.ToString();
            numTextBox2.Text = lineEvent.lineConfig.SamplingStep.ToString();
            numTextBox3.Text = lineEvent.lineConfig.Passes.ToString();
           
            foreach (var item in lineEvent.lineConfig.ResultsDetermines)
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
                    case "角度":
                        checkBox3.Checked = item.Enable;
                        txt_min3.Text = item.Min.ToString();
                        txt_max3.Text = item.Max.ToString();
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
            if (lineEvent.Run(false))
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            picbox_ToolEdit.MFrameShape = lineEvent.shape;
            picbox_ToolEdit.Refresh();
            txt_centerX.Text = lineEvent.LineGauge.MeasuredLine.CenterX.ToString();
            txt_centerY.Text = lineEvent.LineGauge.MeasuredLine.CenterY.ToString();
            txt_angle.Text = lineEvent.LineGauge.MeasuredLine.Angle.ToString();
            int passNum = lineEvent.LineGauge.NumSamples - lineEvent.LineGauge.NumValidSamples;
            txt_passNum.Text = passNum.ToString();
            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }
        #endregion

        private void FindLineFrm_Load(object sender, EventArgs e)
        {
            cmbox_shape.SelectedIndex = 0;
            lineEvent.LineGauge.Attach(picbox_ToolEdit.MFrameShape);
            picbox_ToolEdit.MShapes.Add(lineEvent.LineGauge);

            foreach (var item in lineEvent.ParentTask.Events)
            {
                if (item is ShapeEvent)
                {
                    ShapeEvent se = item as ShapeEvent;
                    cmbox_shape.Items.Add(se.Config.ToolName);
                }
            }
            picbox_ToolEdit.MFrameShape = lineEvent.shape;
            for (int i = 0; i < cmbox_shape.Items.Count; i++)
            {
                string s = cmbox_shape.Items[i].ToString();
                if (s == lineEvent.lineConfig.ShapeName)
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
            cmbox_shape.SelectedIndexChanged += delegate { UpdateUIToEvent(); };

            Run();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int index = Convert.ToInt32(chk.Tag);
            lineEvent.lineConfig.ResultsDetermines[index].Enable = chk.Checked;
        }

        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                lineEvent.lineConfig.ResultsDetermines[index].Min = value;
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
                lineEvent.lineConfig.ResultsDetermines[index].Max = value;
            }
            catch { }

        }
    }
}
