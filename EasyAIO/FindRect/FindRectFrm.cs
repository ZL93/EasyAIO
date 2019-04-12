using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EasyAIO
{
    public partial class FindRectFrm : ToolEditBaseFrm
    {
        #region 公共
        private FindRectEvent rectEvent = null;
        public FindRectFrm(FindRectEvent rectE)
            : base(rectE)
        {
            InitializeComponent();
            rectEvent = rectE;
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();

            rectEvent.rectConfig.TransitionType = comboBox1.SelectedIndex;
            rectEvent.rectConfig.TransitionChoice = comboBox2.SelectedIndex;
            rectEvent.rectConfig.Threshold = Convert.ToInt32(numTextBox1.Text);
            rectEvent.rectConfig.SamplingStep = Convert.ToInt32(numTextBox2.Text);
            rectEvent.rectConfig.Passes = Convert.ToInt32(numTextBox3.Text);
            
            rectEvent.rectConfig.CenterX=rectEvent.RectGauge.CenterX;
            rectEvent.rectConfig.CenterY=rectEvent.RectGauge.CenterY;
            rectEvent.rectConfig.SizeX=rectEvent.RectGauge.SizeX;
            rectEvent.rectConfig.SizeY=rectEvent.RectGauge.SizeY;
            rectEvent.rectConfig.Angle=rectEvent.RectGauge.Angle;
            if (rectEvent.RectGauge.Tolerance > rectEvent.RectGauge.SizeX /2)
            {
                rectEvent.rectConfig.Tolerance = rectEvent.RectGauge.SizeX / 2;
            }
            else if (rectEvent.RectGauge.Tolerance > rectEvent.RectGauge.SizeY / 2)
            { rectEvent.rectConfig.Tolerance = rectEvent.RectGauge.SizeY / 2; }
            else
            {
                rectEvent.rectConfig.Tolerance = rectEvent.RectGauge.Tolerance;
            }

            if (btn_centerX.Text != "...")
            {
                rectEvent.rectConfig.LinkCenterX = btn_centerX.Text;
            }
            else
            {
                rectEvent.rectConfig.LinkCenterX = string.Empty;
            }
            if (btn_centerY.Text != "...")
            {
                rectEvent.rectConfig.LinkCenterY = btn_centerY.Text;
            }
            else
            {
                rectEvent.rectConfig.LinkCenterY = string.Empty;
            }
            if (btn_angle.Text != "...")
            {
                rectEvent.rectConfig.LinkAngle = btn_angle.Text;
            }
            else
            {
                rectEvent.rectConfig.LinkAngle = string.Empty;
            }

            if (rectEvent.linkcenterx != 0)
            {   
                rectEvent.rectConfig.LinkOffsetCenterX = (int)(rectEvent.rectConfig.CenterX - rectEvent.linkcenterx);
            }   
            if (rectEvent.linkcentery != 0)
            {
                rectEvent.rectConfig.LinkOffsetCenterY = (int)(rectEvent.rectConfig.CenterY - rectEvent.linkcentery);
            }    
            if (rectEvent.linkangle != 0)
            {
                rectEvent.rectConfig.LinkOffsetAngle = (int)(rectEvent.rectConfig.Angle - rectEvent.linkangle);
            }
            if (cmbox_shape.SelectedItem != null)
            {
                rectEvent.rectConfig.ShapeName = cmbox_shape.SelectedItem.ToString();
            }
            Run();
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();
            comboBox1.SelectedIndex = rectEvent.rectConfig.TransitionType;
            comboBox2.SelectedIndex = rectEvent.rectConfig.TransitionChoice;
            numTextBox1.Text = rectEvent.rectConfig.Threshold.ToString();
            numTextBox2.Text = rectEvent.rectConfig.SamplingStep.ToString();
            numTextBox3.Text = rectEvent.rectConfig.Passes.ToString();
            foreach (var item in rectEvent.rectConfig.ResultsDetermines)
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
                    case "宽度":
                        checkBox4.Checked = item.Enable;
                        txt_min4.Text = item.Min.ToString();
                        txt_max4.Text = item.Max.ToString();
                        break;
                    case "高度":
                        checkBox5.Checked = item.Enable;
                        txt_min5.Text = item.Min.ToString();
                        txt_max5.Text = item.Max.ToString();
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

            if (rectEvent.rectConfig.LinkCenterX != string.Empty)
            {
                btn_centerX.Text = rectEvent.rectConfig.LinkCenterX;
            }
            else
            {
                btn_centerX.Text = "...";
            }

            if (rectEvent.rectConfig.LinkCenterY != string.Empty)
            {
                btn_centerY.Text = rectEvent.rectConfig.LinkCenterY;
            }
            else
            {
                btn_centerY.Text = "...";
            }

            if (rectEvent.rectConfig.LinkAngle != string.Empty)
            {
                btn_angle.Text = rectEvent.rectConfig.LinkAngle;
            }
            else
            {
                btn_angle.Text = "...";
            }
        }

        protected override void Run()
        {
            stopWatch.Reset();
            stopWatch.Start();
            if (rectEvent.Run(false))
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            picbox_ToolEdit.Refresh();
            txt_centerX.Text = rectEvent.RectGauge.MeasuredRectangle.CenterX.ToString();
            txt_centerY.Text = rectEvent.RectGauge.MeasuredRectangle.CenterY.ToString();
            txt_angle.Text = rectEvent.RectGauge.MeasuredRectangle.Angle.ToString();
            txt_width.Text = rectEvent.RectGauge.MeasuredRectangle.SizeX.ToString();
            txt_height.Text = rectEvent.RectGauge.MeasuredRectangle.SizeY.ToString();
            int passNum = rectEvent.RectGauge.NumSamples - rectEvent.RectGauge.NumValidSamples;
            txt_passNum.Text = passNum.ToString();
            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }
        #endregion

        private void FindRectFrm_Load(object sender, EventArgs e)
        {
            cmbox_shape.SelectedIndex = 0;
            rectEvent.RectGauge.Attach(picbox_ToolEdit.MFrameShape); 
            picbox_ToolEdit.MShapes.Add(rectEvent.RectGauge);

            foreach (var item in rectEvent.ParentTask.Events)
            {
                if (item is ShapeEvent)
                {
                    ShapeEvent se = item as ShapeEvent;
                    cmbox_shape.Items.Add(se.Config.ToolName);
                }
            }
            picbox_ToolEdit.MFrameShape = rectEvent.shape;
            for (int i = 0; i < cmbox_shape.Items.Count; i++)
            {
                string s = cmbox_shape.Items[i].ToString();
                if (s == rectEvent.rectConfig.ShapeName)
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
            rectEvent.rectConfig.ResultsDetermines[index].Enable = chk.Checked;
        }

        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                rectEvent.rectConfig.ResultsDetermines[index].Min = value;
            }
            catch
            { }
        }

        private void txt_max_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                rectEvent.rectConfig.ResultsDetermines[index].Max = value;
            }
            catch { }
        }

        private void btn_centerX_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ResultFrm rf = new ResultFrm(rectEvent.ParentTask.ResultDatas);
            if (rf.ShowDialog() == DialogResult.OK)
            {
                string link;
                if (rf.SelectName == "...")
                {
                    link = rf.SelectName;
                }
                else
                {
                    link = rf.SelectName + "[" + rf.Index + "]";
                }
                btn.Text = link;
                UpdateUIToEvent();
            }
            rf.Dispose();
        }
    }
}
