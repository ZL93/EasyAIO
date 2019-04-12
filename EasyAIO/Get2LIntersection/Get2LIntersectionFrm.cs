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
    public partial class Get2LIntersectionFrm : Form
    {
        Get2LIntersectionEvent getEvent;
        public Get2LIntersectionFrm(Get2LIntersectionEvent getEvent)
        {
            InitializeComponent();
            this.getEvent = getEvent;
        }

        private void btn_centerX1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ResultFrm rf = new ResultFrm(getEvent.ParentTask.ResultDatas);
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

        private void UpdateUIToEvent()
        {
            if (btn_centerX1.Text != "...")
            {
                getEvent.config.LinkL_CenterX1 = btn_centerX1.Text;
            }
            else
            {
                getEvent.config.LinkL_CenterX1 = string.Empty;
            }
            if (btn_centerY1.Text != "...")
            {
                getEvent.config.LinkL_CenterY1 = btn_centerY1.Text;
            }
            else
            {
                getEvent.config.LinkL_CenterY1 = string.Empty;
            }
            if (btn_angle1.Text != "...")
            {
                getEvent.config.LinkL_Angle1 = btn_angle1.Text;
            }
            else
            {
                getEvent.config.LinkL_Angle1 = string.Empty;
            }

            if (btn_centerX2.Text != "...")
            {
                getEvent.config.LinkL_CenterX2 = btn_centerX2.Text;
            }
            else
            {
                getEvent.config.LinkL_CenterX2 = string.Empty;
            }
            if (btn_centerY2.Text != "...")
            {
                getEvent.config.LinkL_CenterY2 = btn_centerY2.Text;
            }
            else
            {
                getEvent.config.LinkL_CenterY2 = string.Empty;
            }

            if (btn_angle2.Text != "...")
            {
                getEvent.config.LinkL_Angle2 = btn_angle2.Text;
            }
            else
            {
                getEvent.config.LinkL_Angle2 = string.Empty;
            }
            getEvent.config.CanDrawToPicBox = chkBox_Draw.Checked;

            getEvent.config.ShapeName = cmbox_shape.SelectedItem.ToString();

        }
        private void UpdateEventToUI()
        {
            if (getEvent.config.LinkL_CenterX1 != null && getEvent.config.LinkL_CenterX1 != string.Empty)
            {
                btn_centerX1.Text = getEvent.config.LinkL_CenterX1;
            }
            else
            {
                btn_centerX1.Text = "...";
            }
            if (getEvent.config.LinkL_CenterY1 != null && getEvent.config.LinkL_CenterY1 != string.Empty)
            {
                btn_centerY1.Text = getEvent.config.LinkL_CenterY1;
            }
            else
            {
                btn_centerY1.Text = "...";
            }
            if (getEvent.config.LinkL_Angle1 != null && getEvent.config.LinkL_Angle1 != string.Empty)
            {
                btn_angle1.Text = getEvent.config.LinkL_Angle1;
            }
            else
            {
                btn_angle1.Text = "...";
            }

            if (getEvent.config.LinkL_CenterX2 != null && getEvent.config.LinkL_CenterX2 != string.Empty)
            {
                btn_centerX2.Text = getEvent.config.LinkL_CenterX2;
            }
            else
            {
                btn_centerX2.Text = "...";
            }

            if (getEvent.config.LinkL_CenterY2 != null && getEvent.config.LinkL_CenterY2 != string.Empty)
            {
                btn_centerY2.Text = getEvent.config.LinkL_CenterY2;
            }
            else
            {
                btn_centerY2.Text = "...";
            }

            if (getEvent.config.LinkL_Angle2 != null && getEvent.config.LinkL_Angle2 != string.Empty)
            {
                btn_angle2.Text = getEvent.config.LinkL_Angle2;
            }
            else
            {
                btn_angle2.Text = "...";
            }
            chkBox_Draw.Checked = getEvent.config.CanDrawToPicBox;
            checkBox1.Checked = getEvent.config.IsNormalShape;

            for (int i = 0; i < cmbox_shape.Items.Count; i++)
            {
                string s = cmbox_shape.Items[i].ToString();
                if (s == getEvent.config.ShapeName)
                {
                    cmbox_shape.SelectedIndex = i;
                    break;
                }
            }
        }
        private void Run() 
        {
            getEvent.Run(false);
            lb_distance.Text = getEvent.P_Intersection.ToString();
        }

        private void Get2PDistFrm_Load(object sender, EventArgs e)
        {
            this.chkBox_Draw.CheckedChanged += delegate 
            {
                panel1.Visible = chkBox_Draw.Checked;
            };
            
            cmbox_shape.SelectedIndex = 0;

            foreach (var item in getEvent.ParentTask.Events)
            {
                if (item is ShapeEvent)
                {
                    ShapeEvent se = item as ShapeEvent;
                    cmbox_shape.Items.Add(se.Config.ToolName);
                }
            }

            UpdateEventToUI();

            
        }

        private void btn_calculate_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            UpdateUIToEvent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            getEvent.config.IsNormalShape = checkBox1.Checked;
        }
    }
}
