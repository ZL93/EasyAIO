using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public partial class ObjectFrm : ToolEditBaseFrm
    {
        ObjectEvent objEvent = null;
        MObjData objdata = null;
        public ObjectFrm(ObjectEvent objE)
            : base(objE)
        {
            InitializeComponent();
            objEvent = objE;
        }
        private void ObjectFrm_Load(object sender, EventArgs e)
        {
            objdata = objEvent.objData;
            picbox_ToolEdit.MRois.Add(objEvent.Roi);
            picbox_ToolEdit.objDatas.Add(objdata);
            if (objEvent.InputImg!= null)
            {
                objEvent.Roi.Attach(objEvent.InputImg);
            }
            this.comboBox2.SelectedIndexChanged += delegate { UpdateUIToEvent(); Run(); };
            this.ckBox_Roi.CheckedChanged += delegate {
                panel3.Visible = ckBox_Roi.Checked;
                UpdateUIToEvent(); };
            
            chkbox_Sort.CheckedChanged += delegate { UpdateUIToEvent(); };
            comboBox3.SelectedIndexChanged += delegate { UpdateUIToEvent(); };
            radbtn_Ascend.CheckedChanged += delegate { UpdateUIToEvent(); };
            radbtn_Descend.CheckedChanged += delegate { UpdateUIToEvent(); };
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();
            dataGridView1.DataSource = objEvent.objConfig.FeatureFilters;
            ckBox_Roi.Checked = objEvent.objConfig.UseRoi;
            objEvent.Roi.Comment = ckBox_Roi.Checked ? "" : "NoShow";
            panel3.Visible = ckBox_Roi.Checked;
            objEvent.Roi.SetPlacement(objEvent.objConfig.Roi_OrgX, objEvent.objConfig.Roi_OrgY, objEvent.objConfig.Roi_Width, objEvent.objConfig.Roi_Height);

            if (objEvent.objConfig.SelectImgIndex < comBox_img.Items.Count)
            {
                if (objEvent.objConfig.SelectImgIndex > 0)
                {
                    objEvent.Roi.Attach(objEvent.InputImg);
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

            if (objEvent.objConfig.WhiteEncode)
            {
                comboBox2.SelectedIndex = 1;
            }
            else
            {
                comboBox2.SelectedIndex = 0;
            }

            chkbox_Sort.Checked = objEvent.objConfig.CanSort;
            comboBox3.SelectedIndex = objEvent.objConfig.SortIndex;
            if (objEvent.objConfig.IsAscending)
            {
                radbtn_Ascend.Checked = true;
            }
            else
            {
                radbtn_Descend.Checked = true;
            }

            foreach (var item in objEvent.objConfig.ResultsDetermines)
            {
                switch (item.Name)
                {
                    case "数量":
                        checkBox1.Checked = item.Enable;
                        txt_min1.Text = item.Min.ToString();
                        txt_max1.Text = item.Max.ToString();
                        break;
                    case "面积":
                        checkBox2.Checked = item.Enable;
                        txt_min2.Text = item.Min.ToString();
                        txt_max2.Text = item.Max.ToString();
                        break;
                    case "重心X":
                        checkBox3.Checked = item.Enable;
                        txt_min3.Text = item.Min.ToString();
                        txt_max3.Text = item.Max.ToString();
                        break;
                    case "重心Y":
                        checkBox4.Checked = item.Enable;
                        txt_min4.Text = item.Min.ToString();
                        txt_max4.Text = item.Max.ToString();
                        break;
                    case "宽度":
                        checkBox5.Checked = item.Enable;
                        txt_min5.Text = item.Min.ToString();
                        txt_max5.Text = item.Max.ToString();
                        break;
                    case "高度":
                        checkBox6.Checked = item.Enable;
                        txt_min6.Text = item.Min.ToString();
                        txt_max6.Text = item.Max.ToString();
                        break;
                    default:
                        break;
                }
            }

            if (objEvent.objConfig.LinkOrgX != string.Empty)
            {
                btn_orgX.Text = objEvent.objConfig.LinkOrgX;
            }
            else
            {
                btn_orgX.Text = "...";
            }

            if (objEvent.objConfig.LinkOrgY != string.Empty)
            {
                btn_orgY.Text = objEvent.objConfig.LinkOrgY;
            }
            else
            {
                btn_orgY.Text = "...";
            }

            if (objEvent.objConfig.LinkWidth != string.Empty)
            {
                btn_width.Text = objEvent.objConfig.LinkWidth;
            }
            else
            {
                btn_width.Text = "...";
            }

            if (objEvent.objConfig.LinkHeight != string.Empty)
            {
                btn_height.Text = objEvent.objConfig.LinkHeight;
            }
            else
            {
                btn_height.Text = "...";
            }
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();
            if (comBox_img.SelectedIndex > 0)
            {
                objEvent.Roi.Attach(objEvent.InputImg);
                picbox_ToolEdit.CanRoiMove = true;

            }
            else
            {
                picbox_ToolEdit.CanRoiMove = false;
            }
            objEvent.objConfig.UseRoi = ckBox_Roi.Checked;
            objEvent.Roi.Comment = ckBox_Roi.Checked ? "" : "NoShow";
            objEvent.objConfig.Roi_OrgX = objEvent.Roi.OrgX;
            objEvent.objConfig.Roi_OrgY = objEvent.Roi.OrgY;
            objEvent.objConfig.Roi_Width = objEvent.Roi.Width;
            objEvent.objConfig.Roi_Height = objEvent.Roi.Height;

            if (objEvent.linkorgx != 0)
            {
                objEvent.objConfig.LinkOffsetOrgX = objEvent.objConfig.Roi_OrgX - (int)objEvent.linkorgx;
            }

            if (objEvent.linkorgy != 0)
            {
                objEvent.objConfig.LinkOffsetOrgY = objEvent.objConfig.Roi_OrgY - (int)objEvent.linkorgy;
            }

            if (objEvent.linkwidth != 0)
            {
                objEvent.objConfig.LinkOffsetWidth = objEvent.objConfig.Roi_Width - (int)objEvent.linkwidth;
            }

            if (objEvent.linkheight != 0)
            {
                objEvent.objConfig.LinkOffsetHeight = objEvent.objConfig.Roi_Height - (int)objEvent.linkheight;
            }
            picbox_ToolEdit.Refresh();

            if (comboBox2.SelectedIndex == 1)
            {
                objEvent.objConfig.WhiteEncode = true;
            }
            else
            {
                objEvent.objConfig.WhiteEncode = false;
            }

            objEvent.objConfig.CanSort = chkbox_Sort.Checked;
            objEvent.objConfig.SortIndex = comboBox3.SelectedIndex;
            objEvent.objConfig.IsAscending = radbtn_Ascend.Checked;

            if (btn_orgX.Text != "...")
            {
                objEvent.objConfig.LinkOrgX = btn_orgX.Text;
            }
            else
            {
                objEvent.objConfig.LinkOrgX = string.Empty;
            }

            if (btn_orgY.Text != "...")
            {
                objEvent.objConfig.LinkOrgY = btn_orgY.Text;
            }
            else
            {
                objEvent.objConfig.LinkOrgY = string.Empty;
            }

            if (btn_width.Text != "...")
            {
                objEvent.objConfig.LinkWidth = btn_width.Text;
            }
            else
            {
                objEvent.objConfig.LinkWidth = string.Empty;
            }

            if (btn_height.Text != "...")
            {
                objEvent.objConfig.LinkHeight = btn_height.Text;
            }
            else
            {
                objEvent.objConfig.LinkHeight = string.Empty;
            }

        }

        protected override void Run()
        {
            stopWatch.Reset();
            stopWatch.Start();
            if (objEvent.Run())
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            picbox_ToolEdit.Refresh();
           
            listView1.Items.Clear();
            for (int i = 0; i < objEvent.count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = i.ToString();
                item.SubItems.Add(objEvent.area[i].ToString());
                item.SubItems.Add(objEvent.gravityX[i].ToString());
                item.SubItems.Add(objEvent.gravityY[i].ToString());
                item.SubItems.Add(objEvent.centerX[i].ToString());
                item.SubItems.Add(objEvent.centerY[i].ToString());
                item.SubItems.Add(objEvent.width[i].ToString());
                item.SubItems.Add(objEvent.height[i].ToString());
                listView1.Items.Add(item);
            }
            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button1, new Point(0, button1.Height));
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
                return;
            DataGridView view = (DataGridView)sender;
          
            if (view.Columns[e.ColumnIndex].DataPropertyName == "Feature")
            {
                int originalValue = (int)e.Value;
                switch (originalValue)
                {
                    case 3: e.Value = "面积";
                        break;
                    case 11: e.Value = "重心X";
                        break;
                    case 12: e.Value = "重心Y";
                        break;
                    case 41: e.Value = "主轴角度";
                        break;
                    case 13: e.Value = "矩形中心X";
                        break;
                    case 14: e.Value = "矩形中心Y";
                        break;
                    case 15: e.Value = "矩形宽度";
                        break;
                    case 16: e.Value = "矩形高度";
                        break;
                    default:
                        e.Value = originalValue;
                        break;
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ObjFeatureFilter ff = new ObjFeatureFilter() { Feature = Convert.ToInt32(item.Tag), Min = 0, Max = 999999 };
            objEvent.objConfig.FeatureFilters.Add(ff);
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count<=0)
            {
                return;
            }
            objEvent.objConfig.FeatureFilters.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int index =Convert.ToInt32(chk.Tag);
            objEvent.objConfig.ResultsDetermines[index].Enable = chk.Checked;
        }

        private void txt_min_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NumTextBox txt = sender as NumTextBox;
                int index = Convert.ToInt32(txt.Tag);
                int value = Convert.ToInt32(txt.Text);
                objEvent.objConfig.ResultsDetermines[index].Min = value;
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
                objEvent.objConfig.ResultsDetermines[index].Max = value;
            }
            catch
            {
                
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                objdata.SelectIndex = listView1.SelectedItems[0].Index;
                picbox_ToolEdit.Refresh();
            }
        }

        private void btn_orgX_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ResultFrm rf = new ResultFrm(objEvent.ParentTask.ResultDatas);
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
