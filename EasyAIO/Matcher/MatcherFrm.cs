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
    public partial class MatcherFrm : ToolEditBaseFrm
    {
        #region 公共
        private MatcherEvent matcherEvent = null;
        public MatcherFrm(MatcherEvent circleE)
            : base(circleE)
        {
            InitializeComponent();
            matcherEvent = circleE;
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();

            if (comBox_img.SelectedIndex > 0)
            {
                matcherEvent.matcherConfig.UseRoi = ckBox_Roi.Checked;
                matcherEvent.Roi.Comment = ckBox_Roi.Checked ? "" : "NoShow";
                matcherEvent.Roi.Attach(matcherEvent.InputImg);
                picbox_ToolEdit.CanRoiMove = true;
            }
            else
            {
                picbox_ToolEdit.CanRoiMove = false;
            }

            matcherEvent.matcherConfig.MatcherPath = txt_MatchPath.Text;
            matcherEvent.matcherConfig.FinderPath = txt_FindPath.Text;
            if (radioButton1.Checked)
            {
                matcherEvent.matcherConfig.MatchType = 0;
            }
            else if (radioButton2.Checked)
            {
                matcherEvent.matcherConfig.MatchType = 1;
            }
            
            matcherEvent.matcherConfig.Roi_OrgX = matcherEvent.Roi.OrgX;
            matcherEvent.matcherConfig.Roi_OrgY = matcherEvent.Roi.OrgY;
            matcherEvent.matcherConfig.Roi_Width = matcherEvent.Roi.Width;
            matcherEvent.matcherConfig.Roi_Height = matcherEvent.Roi.Height;
            picbox_ToolEdit.Refresh();
            Run();
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();
            switch (matcherEvent.matcherConfig.MatchType)
            {
                case 0: radioButton1.Checked = true;
                    break;
                case 1: radioButton2.Checked = true;
                    break;
                default:
                    break;
            }
            txt_MatchPath.Text = matcherEvent.matcherConfig.MatcherPath;
            txt_FindPath.Text = matcherEvent.matcherConfig.FinderPath;
            ckBox_Roi.Checked = matcherEvent.matcherConfig.UseRoi;
            matcherEvent.Roi.Comment = ckBox_Roi.Checked ? "" : "NoShow";
            matcherEvent.Roi.SetPlacement(matcherEvent.matcherConfig.Roi_OrgX, matcherEvent.matcherConfig.Roi_OrgY,
                                          matcherEvent.matcherConfig.Roi_Width, matcherEvent.matcherConfig.Roi_Height);

            if (matcherEvent.matcherConfig.SelectImgIndex < comBox_img.Items.Count)
            {
                if (matcherEvent.matcherConfig.SelectImgIndex > 0)
                {
                    matcherEvent.Roi.Attach(matcherEvent.InputImg);
                    picbox_ToolEdit.CanRoiMove = true;

                }
                else
                {
                    picbox_ToolEdit.CanRoiMove = false;
                }
            }
            else
            {
                picbox_ToolEdit.CanRoiMove = false;
            }

            foreach (var item in matcherEvent.matcherConfig.ResultsDetermines)
            {
                switch (item.Name)
                {
                    case "数量":
                        checkBox1.Checked = item.Enable;
                        txt_min1.Text = item.Min.ToString();
                        txt_max1.Text = item.Max.ToString();
                        break;
                    case "中心X":
                        checkBox2.Checked = item.Enable;
                        txt_min2.Text = item.Min.ToString();
                        txt_max2.Text = item.Max.ToString();
                        break;
                    case "中心Y":
                        checkBox3.Checked = item.Enable;
                        txt_min3.Text = item.Min.ToString();
                        txt_max3.Text = item.Max.ToString();
                        break;
                    case "角度":
                        checkBox4.Checked = item.Enable;
                        txt_min4.Text = item.Min.ToString();
                        txt_max4.Text = item.Max.ToString();
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
            if (matcherEvent.Run(false))
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            
            if (radioButton1.Checked)
            {
                txt_MinAngle.Text = matcherEvent.Matcher.MinAngle.ToString();
                txt_MaxAngle.Text = matcherEvent.Matcher.MaxAngle.ToString();
                txt_MinScore.Text = matcherEvent.Matcher.MinScore.ToString();
                txt_MaxPositions.Text = matcherEvent.Matcher.MaxPositions.ToString();
                txt_MinScale.Text = matcherEvent.Matcher.MinScale.ToString();
                txt_MaxScale.Text = matcherEvent.Matcher.MaxScale.ToString();
                picbox_ToolEdit.FinderPatterns.Clear();
                picbox_ToolEdit.MMatchers.Clear();
                picbox_ToolEdit.MMatchers.Add(matcherEvent.Matcher);
                lb_err.Visible = false;
                if (!System.IO.File.Exists(matcherEvent.matcherConfig.MatcherPath))
                {
                    lb_err.Visible = true;
                }
            }
            else if (radioButton2.Checked)
            {
                txt_angle.Text = matcherEvent.Finder.AngleBias.ToString();
                txt_angleRange.Text = matcherEvent.Finder.AngleTolerance.ToString();
                txt_Scale.Text = matcherEvent.Finder.ScaleBias.ToString();
                txt_scaleRange.Text = matcherEvent.Finder.ScaleTolerance.ToString();
                txt_ScoreMin.Text = matcherEvent.Finder.MinScore.ToString();
                txt_maxNum.Text = matcherEvent.Finder.MaxInstances.ToString();
                picbox_ToolEdit.MMatchers.Clear();
                picbox_ToolEdit.FinderPatterns.Clear();
                picbox_ToolEdit.FinderPatterns.Add(matcherEvent.FinderPatterns);
                lb_err.Visible = false;
                if (!System.IO.File.Exists(matcherEvent.matcherConfig.FinderPath))
                {
                    lb_err.Visible = true;
                }
            }
            picbox_ToolEdit.Refresh();

            listView1.Items.Clear();
            for (int i = 0; i < matcherEvent.count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = (i+1).ToString();
                item.SubItems.Add(matcherEvent.scores[i].ToString("#0.00"));
                item.SubItems.Add(matcherEvent.centerXs[i].ToString("#0.00"));
                item.SubItems.Add(matcherEvent.centerYs[i].ToString("#0.00"));
                item.SubItems.Add(matcherEvent.angles[i].ToString("#0.00"));
                listView1.Items.Add(item);
            }
            

            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }
        #endregion

        private void MatchFrm_Load(object sender, EventArgs e)
        {
            if (matcherEvent.InputImg != null)
            {
                matcherEvent.Roi.Attach(matcherEvent.InputImg);
            }
            picbox_ToolEdit.MRois.Add(matcherEvent.Roi);

            ckBox_Roi.CheckedChanged += delegate { UpdateUIToEvent(); };

            radioButton1.CheckedChanged += delegate
            {
                if (radioButton1.Checked)
                {
                    panel4.Enabled = true;
                    panel5.Enabled = false;
                    UpdateUIToEvent();
                }
            };
            radioButton2.CheckedChanged += delegate
            {
                if (radioButton2.Checked)
                {
                    panel5.Enabled = true;
                    panel4.Enabled = false;
                    UpdateUIToEvent();
                }
            };
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int index = Convert.ToInt32(chk.Tag);
            matcherEvent.matcherConfig.ResultsDetermines[index].Enable = chk.Checked;
        }

        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                matcherEvent.matcherConfig.ResultsDetermines[index].Min = value;
            }
            catch
            {  
            }
        }

        private void txt_max_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                matcherEvent.matcherConfig.ResultsDetermines[index].Max = value;
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "模板文件|*.MCH";
            if (ofd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                txt_MatchPath.Text = ofd.FileName;
                matcherEvent.Matcher.Load(ofd.FileName);
                UpdateUIToEvent();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "模板文件|*.FND";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txt_FindPath.Text = ofd.FileName;
                matcherEvent.Finder.Load(ofd.FileName);
                UpdateUIToEvent();
            }
        }
    }
}
