using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EasyAIO
{
    public partial class ImgPreprocessFrm : ToolEditBaseFrm
    {
        ImgPreProcessEvent imgEvent = null;
        int index = 0;
        public ImgPreprocessFrm(ImgPreProcessEvent imgE)
            : base(imgE)
        {
            InitializeComponent();
            imgEvent = imgE;
        }
        private void ImgPreprocessEditFrm_Load(object sender, EventArgs e)
        {
            tabControl1.ItemSize = new Size(0, 1);
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
        }
        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();

            foreach (var o in imgEvent.config.CfgGroup)
            {
                ListViewItem item = new ListViewItem();
                if (o is DilateConfig)
                {
                    DilateConfig config = o as DilateConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.SubItems.Add(config.Width.ToString() + "," + config.Height.ToString());
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is ErodeConfig)
                {
                    ErodeConfig config = o as ErodeConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.SubItems.Add(config.Width.ToString() + "," + config.Height.ToString());
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is OpenConfig)
                {
                    OpenConfig config = o as OpenConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.SubItems.Add(config.Width.ToString() + "," + config.Height.ToString());
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is CloseConfig)
                {
                    CloseConfig config = o as CloseConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.SubItems.Add(config.Width.ToString() + "," + config.Height.ToString());
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is ThresholdConfig)
                {
                    ThresholdConfig config = o as ThresholdConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    switch (config.Mode)
                    {
                        case ThresholdConfig.MyThresholdMode.Auto:
                            item.SubItems.Add("Auto");
                            break;
                        case ThresholdConfig.MyThresholdMode.Absolute:
                            item.SubItems.Add(config.AbsoluteValue.ToString());
                            break;
                        case ThresholdConfig.MyThresholdMode.Relative:
                            item.SubItems.Add(config.RelativeValue.ToString());
                            break;
                        default:
                            break;
                    }
                    
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is GainOffsetConfig)
                {
                    GainOffsetConfig config = o as GainOffsetConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.SubItems.Add(config.Gain.ToString()+","+config.Offset.ToString());
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is RoataeConfig)
                {
                    RoataeConfig config = o as RoataeConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.SubItems.Add(config.Value.ToString());
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is MedianConfig)
                {
                    MedianConfig config = o as MedianConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                else if (o is SobelConfig)
                {
                    SobelConfig config = o as SobelConfig;
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(config.Name);
                    item.Tag = config;
                    item.Checked = config.Enable;
                }
                listView1.Items.Add(item);
            }
        }

        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button1, new Point(0, button1.Height));
        }

        protected override void Run()
        {
            stopWatch.Reset();
            stopWatch.Start();
            if (imgEvent.Run())
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            picbox_ToolEdit.Image = imgEvent.OutputImg;
            picbox_ToolEdit.Refresh();
            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = sender as ToolStripItem;
            ListViewItem item = new ListViewItem();
            switch (tsi.Text)
            {
                case "膨胀":
                    DilateConfig dc = new DilateConfig();
                    imgEvent.config.CfgGroup.Add(dc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(dc.Name);
                    item.SubItems.Add(dc.Width.ToString() + "," + dc.Height.ToString());
                    item.Tag = dc;
                    break;
                case "腐蚀":
                    ErodeConfig ec = new ErodeConfig();
                    imgEvent.config.CfgGroup.Add(ec);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(ec.Name);
                    item.SubItems.Add(ec.Width.ToString() + "," + ec.Height.ToString());
                    item.Tag = ec;
                    break;
                case "开运算":
                    OpenConfig oc = new OpenConfig();
                    imgEvent.config.CfgGroup.Add(oc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(oc.Name);
                    item.SubItems.Add(oc.Width.ToString() + "," + oc.Height.ToString());
                    item.Tag = oc;
                    break;
                case "闭运算":
                    CloseConfig cc = new CloseConfig();
                    imgEvent.config.CfgGroup.Add(cc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(cc.Name);
                    item.SubItems.Add(cc.Width.ToString() + "," + cc.Height.ToString());
                    item.Tag = cc;
                    break;
                case "二值化":
                    ThresholdConfig tc = new ThresholdConfig();
                    imgEvent.config.CfgGroup.Add(tc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(tc.Name);
                    switch (tc.Mode)
                    {
                        case ThresholdConfig.MyThresholdMode.Auto:
                            item.SubItems.Add("Auto");
                            break;
                        case ThresholdConfig.MyThresholdMode.Absolute:
                            item.SubItems.Add(tc.AbsoluteValue.ToString());
                            break;
                        case ThresholdConfig.MyThresholdMode.Relative:
                            item.SubItems.Add(tc.RelativeValue.ToString());
                            break;
                        default:
                            break;
                    }
                   
                    item.Tag = tc;
                    break;
                case "亮度调节":
                    GainOffsetConfig gc = new GainOffsetConfig();
                    imgEvent.config.CfgGroup.Add(gc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(gc.Name);
                    item.SubItems.Add(gc.Gain.ToString()+","+gc.Offset.ToString());
                    item.Tag = gc;
                    break;
                case "旋转":
                    RoataeConfig rc = new RoataeConfig();
                    imgEvent.config.CfgGroup.Add(rc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(rc.Name);
                    item.SubItems.Add(rc.Value.ToString());
                    item.Tag = rc;
                    break;
                case "中值运算":
                    MedianConfig mc = new MedianConfig();
                    imgEvent.config.CfgGroup.Add(mc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(mc.Name);
                    item.Tag = mc;
                    break;
                case "Sobel运算":
                    SobelConfig sc = new SobelConfig();
                    imgEvent.config.CfgGroup.Add(sc);
                    item.SubItems[0].Text = index++.ToString();
                    item.SubItems.Add(sc.Name);
                    item.Tag = sc;
                    break;
                default:
                    break;
            }
            item.Checked = true;
            
            listView1.Items.Add(item);
            Run();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count<=0)
            {
                return;
            }
            object o = listView1.SelectedItems[0].Tag;
            if (o is DilateConfig)
            {
                tabControl1.SelectedTab = tabPage_Morphology;
                tabPage_Morphology.Tag = o;
                DilateConfig config = o as DilateConfig;
                vlctrl_width.Value = config.Width;
                vlctrl_height.Value = config.Height;
                switch (config.Type)
                {
                    case MorphologyType.Square: rdbtn_square.Checked = true;
                        break;
                    case MorphologyType.Rectangle: rdbtn_rect.Checked = true;
                        break;
                    case MorphologyType.Circle: rdbtn_circle.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            else if (o is ErodeConfig)
            {
                tabControl1.SelectedTab = tabPage_Morphology;
                tabPage_Morphology.Tag = o;
                ErodeConfig config = o as ErodeConfig;
                vlctrl_width.Value = config.Width;
                vlctrl_height.Value = config.Height;
                switch (config.Type)
                {
                    case MorphologyType.Square: rdbtn_square.Checked = true;
                        break;
                    case MorphologyType.Rectangle: rdbtn_rect.Checked = true;
                        break;
                    case MorphologyType.Circle: rdbtn_circle.Checked = true;
                        break;
                    default:
                        break;
                }
                
            }
            else if (o is OpenConfig)
            {
                tabControl1.SelectedTab = tabPage_Morphology;
                tabPage_Morphology.Tag = o;
                OpenConfig config = o as OpenConfig;
                vlctrl_width.Value = config.Width;
                vlctrl_height.Value = config.Height;
                switch (config.Type)
                {
                    case MorphologyType.Square: rdbtn_square.Checked = true;
                        break;
                    case MorphologyType.Rectangle: rdbtn_rect.Checked = true;
                        break;
                    case MorphologyType.Circle: rdbtn_circle.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            else if (o is CloseConfig)
            {
                tabControl1.SelectedTab = tabPage_Morphology;
                tabPage_Morphology.Tag = o;
                CloseConfig config = o as CloseConfig;
                vlctrl_width.Value = config.Width;
                vlctrl_height.Value = config.Height;
                switch (config.Type)
                {
                    case MorphologyType.Square: rdbtn_square.Checked = true;
                        break;
                    case MorphologyType.Rectangle: rdbtn_rect.Checked = true;
                        break;
                    case MorphologyType.Circle: rdbtn_circle.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            else if (o is ThresholdConfig)
            {
                tabControl1.SelectedTab = tabPage_Threshold;
                tabPage_Threshold.Tag = o;
                ThresholdConfig config = o as ThresholdConfig;
                switch (config.Mode)
                {
                    case ThresholdConfig.MyThresholdMode.Auto:
                        rdbtn_Auto.Checked = true;
                        valueControl1.Value = config.AbsoluteValue;
                        break;
                    case ThresholdConfig.MyThresholdMode.Absolute:
                        rdbtn_Absolute.Checked = true;
                        valueControl1.Value = config.AbsoluteValue;
                        break;
                    case ThresholdConfig.MyThresholdMode.Relative:
                        rdbtn_Relative.Checked = true;
                        valueControl1.Value = (int)(config.RelativeValue * 100);
                        break;
                    default:
                        break;
                }
               
            }
            else if (o is GainOffsetConfig)
            {
                tabControl1.SelectedTab = tabPage_GainOffset;
                tabPage_GainOffset.Tag = o;
                GainOffsetConfig config = o as GainOffsetConfig;
                floatValueControl1.Value = config.Gain;
                valueControl4.Value = (int)config.Offset;
            }
            else if (o is RoataeConfig)
            {
                tabControl1.SelectedTab = tabPage_Roatae;
                tabPage_Roatae.Tag = o;
                RoataeConfig config = o as RoataeConfig;
                valueControl3.Value = config.Value;
            }
            else if (o is MedianConfig || o is SobelConfig)
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            object o = e.Item.Tag;
            if (o is DilateConfig)
            {
                DilateConfig config = o as DilateConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is ErodeConfig)
            {
                ErodeConfig config = o as ErodeConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is OpenConfig)
            {
                OpenConfig config = o as OpenConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is CloseConfig)
            {
                CloseConfig config = o as CloseConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is ThresholdConfig)
            {
                ThresholdConfig config = o as ThresholdConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is GainOffsetConfig)
            {
                GainOffsetConfig config = o as GainOffsetConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is RoataeConfig)
            {
                RoataeConfig config = o as RoataeConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is MedianConfig )
            {
                MedianConfig config = o as MedianConfig;
                config.Enable = e.Item.Checked;
            }
            else if (o is SobelConfig)
            {
                SobelConfig config = o as SobelConfig;
                config.Enable = e.Item.Checked;
            }
            //Run();
        }
        private void valueControl1_ValueChanged(object o, ValueControl.ValueChangedEventArgs e)
        {
            if (this.m_lock.WaitOne(0))
            {
                ThresholdConfig config = tabPage_Threshold.Tag as ThresholdConfig;
                if (config != null)
                {
                    if (config.Mode == ThresholdConfig.MyThresholdMode.Absolute)
                    {
                        config.AbsoluteValue = e.Value;
                    }
                    else if(config.Mode == ThresholdConfig.MyThresholdMode.Relative)
                    {
                        config.RelativeValue = (float)e.Value / 100f;
                    }

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Tag == config)
                        {
                            if (config.Mode == ThresholdConfig.MyThresholdMode.Absolute)
                            {
                                item.SubItems[2].Text = config.AbsoluteValue.ToString();
                            }
                            else if (config.Mode == ThresholdConfig.MyThresholdMode.Relative)
                            {
                                item.SubItems[2].Text = config.RelativeValue.ToString();
                            }
                        }
                    }
                    Run();
                }
                
                this.m_lock.Release();
            }
        }

        private void valueControl2_ValueChanged(object o, ValueControl.ValueChangedEventArgs e)
        {
            if (this.m_lock.WaitOne(0))
            {
                ValueControl vlctrl = o as ValueControl;
                object config = tabPage_Morphology.Tag;
                switch (vlctrl.ValueName)
                {
                    case "宽度":

                        if (config is DilateConfig)
                        {
                            DilateConfig dc = config as DilateConfig;
                            if (dc != null)
                            {
                                dc.Width = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = dc.Width.ToString() + "," + dc.Height.ToString();
                                }
                            }
                        }
                        else if (config is ErodeConfig)
                        {
                            ErodeConfig ec = config as ErodeConfig;
                            if (ec != null)
                            {
                                ec.Width = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = ec.Width.ToString() + "," + ec.Height.ToString();
                                }
                            }
                        }
                        else if (config is OpenConfig)
                        {
                            OpenConfig oc = config as OpenConfig;
                            if (oc != null)
                            {
                                oc.Width = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = oc.Width.ToString() + "," + oc.Height.ToString();
                                }
                            }
                        }
                        else if (config is CloseConfig)
                        {
                            CloseConfig cc = config as CloseConfig;
                            if (cc != null)
                            {
                                cc.Width = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = cc.Width.ToString() + "," + cc.Height.ToString();
                                }
                            }
                        }
                        break;
                    case "高度":

                        if (config is DilateConfig)
                        {
                            DilateConfig dc = config as DilateConfig;
                            if (dc != null)
                            {
                                dc.Height = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = dc.Width.ToString() + "," + dc.Height.ToString();
                                }
                            }
                        }
                        else if (config is ErodeConfig)
                        {
                            ErodeConfig ec = config as ErodeConfig;
                            if (ec != null)
                            {
                                ec.Height = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = ec.Width.ToString() + "," + ec.Height.ToString();
                                }
                            }
                        }
                        else if (config is OpenConfig)
                        {
                            OpenConfig oc = config as OpenConfig;
                            if (oc != null)
                            {
                                oc.Height = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = oc.Width.ToString() + "," + oc.Height.ToString();
                                }
                            }
                        }
                        else if (config is CloseConfig)
                        {
                            CloseConfig cc = config as CloseConfig;
                            if (cc != null)
                            {
                                cc.Height = e.Value;
                            }
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Tag == config)
                                {
                                    item.SubItems[2].Text = cc.Width.ToString() + "," + cc.Height.ToString();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                Run();
                this.m_lock.Release();
            }
            
        }

        private void floatValueControl1_ValueChanged(object o, FloatValueControl.ValueChangedEventArgs e)
        {
            if (this.m_lock.WaitOne(0))
            {
                GainOffsetConfig config = tabPage_GainOffset.Tag as GainOffsetConfig;
                if (config != null)
                {
                    config.Gain = e.Value;
                }
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Tag == config)
                    {
                        item.SubItems[2].Text = config.Gain + "," + config.Offset;
                    }
                }
                Run();
                this.m_lock.Release();
            }
            
        }

        private void valueControl4_ValueChanged(object o, ValueControl.ValueChangedEventArgs e)
        {
            if (this.m_lock.WaitOne(0))
            {
                GainOffsetConfig config = tabPage_GainOffset.Tag as GainOffsetConfig;
                if (config != null)
                {
                    config.Offset = e.Value;
                }
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Tag == config)
                    {
                        item.SubItems[2].Text = config.Gain + "," + config.Offset;
                    }
                }
                Run();
                this.m_lock.Release();
            }
            
        }

        private void valueControl3_ValueChanged(object o, ValueControl.ValueChangedEventArgs e)
        {
            if (this.m_lock.WaitOne(0))
            {
                RoataeConfig config = tabPage_Roatae.Tag as RoataeConfig;
                if (config != null)
                {
                    config.Value = e.Value;
                }
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Tag == config)
                    {
                        item.SubItems[2].Text = config.Value.ToString();
                    }
                }
                Run();
                this.m_lock.Release();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            int i = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            imgEvent.config.CfgGroup.RemoveAt(i);
            listView1.Items.Remove(listView1.SelectedItems[0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            ListViewItem item =listView1.SelectedItems[0];
            int i = listView1.Items.IndexOf(item);
            if (i - 1 < 0)
            {
                return;
            }
            ListViewItem upitem = listView1.Items[i - 1];
            string str = item.SubItems[0].Text;
            item.SubItems[0].Text = upitem.SubItems[0].Text;
            upitem.SubItems[0].Text = str;
            listView1.Items.Remove(item);
            listView1.Items.Insert(i-1, item);
            ImgConfigBase ipc = imgEvent.config.CfgGroup[i];
            imgEvent.config.CfgGroup.Remove(ipc);
            imgEvent.config.CfgGroup.Insert(i - 1, ipc);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            ListViewItem item = listView1.SelectedItems[0];
            int i = listView1.Items.IndexOf(item);
            if (i+1>=listView1.Items.Count)
            {
                return;
            }
            ListViewItem dowmitem = listView1.Items[i + 1];
            string str = item.SubItems[0].Text;
            item.SubItems[0].Text = dowmitem.SubItems[0].Text;
            dowmitem.SubItems[0].Text = str;
            listView1.Items.Remove(item);
            listView1.Items.Insert(i+1, item);
            ImgConfigBase ipc = imgEvent.config.CfgGroup[i];
            imgEvent.config.CfgGroup.Remove(ipc);
            imgEvent.config.CfgGroup.Insert(i+1, ipc);

        }

        private void rdbtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdbtn = sender as RadioButton;
            if (rdbtn.Checked)
            {
                object config = tabPage_Morphology.Tag;
                if (config is DilateConfig)
                {
                    DilateConfig dc = config as DilateConfig;
                    switch (rdbtn.Text)
                    {
                        case "正方形": dc.Type = MorphologyType.Square;
                            break;
                        case "矩形": dc.Type = MorphologyType.Rectangle;
                            break;
                        case "圆形": dc.Type = MorphologyType.Circle;
                            break;
                        default:
                            break;
                    }
                }
                else if (config is ErodeConfig)
                {
                    ErodeConfig ec = config as ErodeConfig;
                    switch (rdbtn.Text)
                    {
                        case "正方形": ec.Type = MorphologyType.Square;
                            break;
                        case "矩形": ec.Type = MorphologyType.Rectangle;
                            break;
                        case "圆形": ec.Type = MorphologyType.Circle;
                            break;
                        default:
                            break;
                    }
                }
                else if (config is OpenConfig)
                {
                    OpenConfig oc = config as OpenConfig;
                    switch (rdbtn.Text)
                    {
                        case "正方形": oc.Type = MorphologyType.Square;
                            break;
                        case "矩形": oc.Type = MorphologyType.Rectangle;
                            break;
                        case "圆形": oc.Type = MorphologyType.Circle;
                            break;
                        default:
                            break;
                    }
                }
                else if (config is CloseConfig)
                {
                    CloseConfig cc = config as CloseConfig;
                    switch (rdbtn.Text)
                    {
                        case "正方形": cc.Type = MorphologyType.Square;
                            break;
                        case "矩形": cc.Type = MorphologyType.Rectangle;
                            break;
                        case "圆形": cc.Type = MorphologyType.Circle;
                            break;
                        default:
                            break;
                    }
                }

            }
        }

        private void rdbtn_Auto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Auto.Checked)
            {
                valueControl1.Enabled = false;
                valueControl1.Max = 255;
                ThresholdConfig config = tabPage_Threshold.Tag as ThresholdConfig;
                if (config != null)
                {
                    config.Mode = ThresholdConfig.MyThresholdMode.Auto;
                    Run();
                    valueControl1.Value = config.AbsoluteValue;

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Tag == config)
                        {
                            item.SubItems[2].Text = "Auto";
                        }
                    }
                }
            }
            
        }

        private void rdbtn_Absolute_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Absolute.Checked)
            {
                valueControl1.Enabled = true;
                valueControl1.Max = 255;
                ThresholdConfig config = tabPage_Threshold.Tag as ThresholdConfig;
                if (config != null)
                {
                    config.Mode = ThresholdConfig.MyThresholdMode.Absolute;
                    Run();
                    valueControl1.Value = config.AbsoluteValue;

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Tag == config)
                        {
                            item.SubItems[2].Text = config.AbsoluteValue.ToString();

                        }
                    }
                }
            }
        }

        private void rdbtn_Relative_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Relative.Checked)
            {
                valueControl1.Enabled = true;
                valueControl1.Max = 99;
                ThresholdConfig config = tabPage_Threshold.Tag as ThresholdConfig;
                if (config != null)
                {
                    config.Mode = ThresholdConfig.MyThresholdMode.Relative;
                    Run();
                    valueControl1.Value = (int)(config.RelativeValue * 100);
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Tag == config)
                        {
                            item.SubItems[2].Text = config.RelativeValue.ToString();
                        }
                    }
                }
            }
        }

       

    }
}
