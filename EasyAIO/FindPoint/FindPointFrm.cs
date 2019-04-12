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
    public partial class FindPointFrm : ToolEditBaseFrm
    {
        #region 公共
        private FindPointEvent pointEvent = null;
        public FindPointFrm(FindPointEvent pointE)
            : base(pointE)
        {
            InitializeComponent();
            pointEvent = pointE;
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();
           
            pointEvent.pointConfig.TransitionType = comboBox1.SelectedIndex;
            pointEvent.pointConfig.TransitionChoice = comboBox2.SelectedIndex;
            pointEvent.pointConfig.Threshold = Convert.ToInt32(numTextBox1.Text);
           
            pointEvent.pointConfig.CenterX = pointEvent.PointGauge.CenterX;
            pointEvent.pointConfig.CenterY = pointEvent.PointGauge.CenterY;

            pointEvent.pointConfig.Angle = pointEvent.PointGauge.ToleranceAngle;
            pointEvent.pointConfig.Tolerance = pointEvent.PointGauge.Tolerance;
           
            if (cmbox_shape.SelectedItem != null)
            {
                pointEvent.pointConfig.ShapeName = cmbox_shape.SelectedItem.ToString();
            }
            
            Run();
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();
         
            comboBox1.SelectedIndex = pointEvent.pointConfig.TransitionType;
            comboBox2.SelectedIndex = pointEvent.pointConfig.TransitionChoice;
            numTextBox1.Text = pointEvent.pointConfig.Threshold.ToString();
           
           
            foreach (var item in pointEvent.pointConfig.ResultsDetermines)
            {
                switch (item.Name)
                {
                    case "点个数":
                        checkBox1.Checked = item.Enable;
                        txt_min1.Text = item.Min.ToString();
                        txt_max1.Text = item.Max.ToString();
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
            if (pointEvent.Run(false))
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            picbox_ToolEdit.MFrameShape = pointEvent.shape;
            picbox_ToolEdit.Refresh();
          
            txt_num.Text = pointEvent.numPoints.ToString();
            txt_centerX.Text = pointEvent.centerX.ToString();
            txt_centerY.Text = pointEvent.centerY.ToString();

            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }
        #endregion

        private void FindLineFrm_Load(object sender, EventArgs e)
        {
            cmbox_shape.SelectedIndex = 0;
            pointEvent.PointGauge.Attach(picbox_ToolEdit.MFrameShape);
            picbox_ToolEdit.MShapes.Add(pointEvent.PointGauge);

            foreach (var item in pointEvent.ParentTask.Events)
            {
                if (item is ShapeEvent)
                {
                    ShapeEvent se = item as ShapeEvent;
                    cmbox_shape.Items.Add(se.Config.ToolName);
                }
            }
            picbox_ToolEdit.MFrameShape = pointEvent.shape;
            for (int i = 0; i < cmbox_shape.Items.Count; i++)
            {
                string s = cmbox_shape.Items[i].ToString();
                if (s == pointEvent.pointConfig.ShapeName)
                {
                    cmbox_shape.SelectedIndex = i;
                    break;
                }
            }

            comboBox1.SelectedIndexChanged += delegate { UpdateUIToEvent(); };
            comboBox2.SelectedIndexChanged += delegate { UpdateUIToEvent(); };
            numTextBox1.TextChanged += delegate { UpdateUIToEvent(); };
            cmbox_shape.SelectedIndexChanged += delegate { UpdateUIToEvent(); };

            Run();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int index = Convert.ToInt32(chk.Tag);
            pointEvent.pointConfig.ResultsDetermines[index].Enable = chk.Checked;
        }

        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                pointEvent.pointConfig.ResultsDetermines[index].Min = value;
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
                pointEvent.pointConfig.ResultsDetermines[index].Max = value;
            }
            catch { }

        }
    }
}
