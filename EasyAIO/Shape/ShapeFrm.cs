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
    public partial class ShapeFrm : Form
    {
        private ShapeEvent shapeEvent = null;
        public ShapeFrm(ShapeEvent shapeE)
        {
            InitializeComponent();
            shapeEvent = shapeE;
        }
        private void btn_centerX_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ResultFrm rf = new ResultFrm(shapeEvent.ParentTask.ResultDatas);
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
            }
            rf.Dispose();
        }
        private void Frm_Load(object sender, EventArgs e)
        {
            UpdateEventToUI();
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            UpdateUIToEvent();
        }

        protected void UpdateUIToEvent()
        {
            shapeEvent.shapeConfig.OrgX = shapeEvent.OrgX;
            shapeEvent.shapeConfig.OrgY = shapeEvent.OrgY;
            shapeEvent.shapeConfig.Angle = shapeEvent.Angle;

            if (btn_centerX.Text != "...")
            {
                shapeEvent.shapeConfig.LinkCenterX = btn_centerX.Text;
            }
            else
            {
                shapeEvent.shapeConfig.LinkCenterX = string.Empty;
            }
            if (btn_centerY.Text != "...")
            {
                shapeEvent.shapeConfig.LinkCenterY = btn_centerY.Text;
            }
            else
            {
                shapeEvent.shapeConfig.LinkCenterY = string.Empty;
            }
            if (btn_angle.Text != "...")
            {
                shapeEvent.shapeConfig.LinkAngle = btn_angle.Text;
            }
            else
            {
                shapeEvent.shapeConfig.LinkAngle = string.Empty;
            }
         
        }

        protected void UpdateEventToUI()
        {
            if (shapeEvent.shapeConfig.LinkCenterX != string.Empty)
            {
                btn_centerX.Text = shapeEvent.shapeConfig.LinkCenterX;
            }
            else
            {
                btn_centerX.Text = "...";
            }

            if (shapeEvent.shapeConfig.LinkCenterY != string.Empty)
            {
                btn_centerY.Text = shapeEvent.shapeConfig.LinkCenterY;
            }
            else
            {
                btn_centerY.Text = "...";
            }

            if (shapeEvent.shapeConfig.LinkAngle != string.Empty)
            {
                btn_angle.Text = shapeEvent.shapeConfig.LinkAngle;
            }
            else
            {
                btn_angle.Text = "...";
            }
        }

    }
}
