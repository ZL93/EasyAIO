using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    /// <summary>
    ///  Build by ZL 19.01.29
    /// </summary>
    public partial class ToolEditBaseFrm : Form
    {
        protected Stopwatch stopWatch = new Stopwatch();
        protected BaseEvent mEvent;
        protected Semaphore m_lock = new Semaphore(1, 1);
        public ToolEditBaseFrm()
        {
            InitializeComponent();
        }
        public ToolEditBaseFrm(BaseEvent be)
        {
            InitializeComponent();
            mEvent = be;
        }
        private void ToolEditBaseFrm_Load(object sender, EventArgs e)
        {
            comBox_img.SelectedIndex = 0;
            UpdateEventToUI();
            if (picbox_ToolEdit.Image!= null &&!picbox_ToolEdit.Image.IsVoid)
            {
                toolStripLabel1.Text = picbox_ToolEdit.Image.Width + "x" + picbox_ToolEdit.Image.Height;
            }
            this.comBox_img.SelectedIndexChanged += delegate { UpdateUIToEvent(); };
            this.chkBox_Draw.CheckedChanged += delegate { UpdateUIToEvent(); };
        }
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            UpdateUIToEvent();
        }
        private void picbox_ToolEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (picbox_ToolEdit.Image != null && !picbox_ToolEdit.Image.IsVoid)
            {
                PointF p = picbox_ToolEdit.GetLocation(e);
                Point p2 = Point.Round(p);
                toolStripLabel3.Text = p2.ToString();
                EBW8 gray = picbox_ToolEdit.GetGaryValue(e);
                toolStripLabel2.Text = "Gray = " + gray.Value.ToString();
            }
        }
        private void tlbtn_run_Click(object sender, EventArgs e)
        {
            Run();
        }
        protected virtual void UpdateUIToEvent()
        {
            if (mEvent.Config.SelectImgIndex != comBox_img.SelectedIndex)
            {
                int index = mEvent.Config.SelectImgIndex = comBox_img.SelectedIndex;
                if (index > 0)
                {
                    picbox_ToolEdit.Image = mEvent.InputImg = mEvent.ParentTask.Imgs[--index].Img;
                    picbox_ToolEdit.ImgAutoSize();
                    picbox_ToolEdit.Refresh();
                }
                else
                {
                    picbox_ToolEdit.Image = null;
                    picbox_ToolEdit.CanRoiMove = false;
                    picbox_ToolEdit.Refresh();
                }
            }
            mEvent.Config.CanDrawToPicBox = chkBox_Draw.Checked;
        }

        protected virtual void UpdateEventToUI()
        {
            if (mEvent ==null)
            {
                return;
            }
            if (mEvent is ImgPreProcessEvent)
            {
                for (int i = 0; i < mEvent.ParentTask.Imgs.Count; i++)
                {
                    if (mEvent.ParentTask.Imgs[i].Index < mEvent.ParentTask.Events.IndexOf(mEvent))
                    {
                        comBox_img.Items.Add(mEvent.ParentTask.Imgs[i].Name);
                    }
                }
            }
            else
            {
                for (int i = 0; i < mEvent.ParentTask.Imgs.Count; i++)
                {
                    comBox_img.Items.Add(mEvent.ParentTask.Imgs[i].Name);
                }
            }
            if (mEvent.Config.SelectImgIndex < comBox_img.Items.Count)
            {
                int index = comBox_img.SelectedIndex = mEvent.Config.SelectImgIndex;
                if (index > 0)
                {
                    picbox_ToolEdit.Image = mEvent.InputImg = mEvent.ParentTask.Imgs[--index].Img;
                    picbox_ToolEdit.ImgAutoSize();
                    picbox_ToolEdit.Refresh();
                }
                else
                {
                    picbox_ToolEdit.Image.Dispose();
                    picbox_ToolEdit.Image = null;
                    picbox_ToolEdit.Refresh();
                }
            }
            else
            {
                comBox_img.SelectedIndex = 0;
            }
            chkBox_Draw.Checked = mEvent.Config.CanDrawToPicBox;
        }

        protected virtual void Run()
        {

        }

        private void 帮助LToolStripButton_Click(object sender, EventArgs e)
        {

        }

    }
}
