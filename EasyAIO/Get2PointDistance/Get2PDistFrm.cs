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
    public partial class Get2PDistFrm : Form
    {
        Get2PDistEvent getEvent;
        public Get2PDistFrm(Get2PDistEvent getEvent)
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
                getEvent.config.LinkCenterX1 = btn_centerX1.Text;
            }
            else
            {
                getEvent.config.LinkCenterX1 = string.Empty;
            }
            if (btn_centerY1.Text != "...")
            {
                getEvent.config.LinkCenterY1 = btn_centerY1.Text;
            }
            else
            {
                getEvent.config.LinkCenterY1 = string.Empty;
            }
            if (btn_centerX2.Text != "...")
            {
                getEvent.config.LinkCenterX2 = btn_centerX2.Text;
            }
            else
            {
                getEvent.config.LinkCenterX2 = string.Empty;
            }
            if (btn_centerY2.Text != "...")
            {
                getEvent.config.LinkCenterY2 = btn_centerY2.Text;
            }
            else
            {
                getEvent.config.LinkCenterY2 = string.Empty;
            }

            getEvent.config.CanDrawToPicBox = chkBox_Draw.Checked;
            getEvent.config.ShapeName = cmbox_shape.SelectedItem.ToString();
        }
        private void UpdateEventToUI()
        {
            if (getEvent.config.LinkCenterX1 !=  null && getEvent.config.LinkCenterX1 != string.Empty)
            {
                btn_centerX1.Text = getEvent.config.LinkCenterX1;
            }
            else
            {
                btn_centerX1.Text = "...";
            }

            if (getEvent.config.LinkCenterY1 != null && getEvent.config.LinkCenterY1 != string.Empty)
            {
                btn_centerY1.Text = getEvent.config.LinkCenterY1;
            }
            else
            {
                btn_centerY1.Text = "...";
            }
            if (getEvent.config.LinkCenterX2 != null && getEvent.config.LinkCenterX2 != string.Empty)
            {
                btn_centerX2.Text = getEvent.config.LinkCenterX2;
            }
            else
            {
                btn_centerX2.Text = "...";
            }

            if (getEvent.config.LinkCenterY2 != null && getEvent.config.LinkCenterY2 != string.Empty)
            {
                btn_centerY2.Text = getEvent.config.LinkCenterY2;
            }
            else
            {
                btn_centerY2.Text = "...";
            }
            chkBox_Draw.Checked = getEvent.config.CanDrawToPicBox;
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
            lb_distance.Text = getEvent.Distance.ToString();
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
    }
}
