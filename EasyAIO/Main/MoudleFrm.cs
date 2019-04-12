using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public partial class MoudleFrm : Form
    {
        EImageBW8 copyImg;
        EROIBW8 roi;
        EMatcher match;
        EPatternFinder find;
        EFoundPattern[] finderPatterns;
        EOCR ocr;
        string CodeChars;
        enum Type
        {
            None,
            Match,
            Find,
            Ocr
        }
        Type modleType = Type.None;
        public MoudleFrm()
        {
            InitializeComponent();
        }

        private void MoudleFrm_Load(object sender, EventArgs e)
        {
            roi = new EROIBW8();
            roi.SetPlacement(100, 100, 300, 300);
            mPicBox1.MRois.Add(roi);
            HideTabTitle(tabControl1);
            match = new EMatcher();
            find = new EPatternFinder();
            find.ContrastMode = EFindContrastMode.Normal;
        }

        /// <summary>
        /// 隐藏tabControl标题
        /// </summary>
        private void HideTabTitle(TabControl tab)
        {
            tab.Appearance = TabAppearance.FlatButtons;
            tab.SizeMode = TabSizeMode.Fixed;
            tab.ItemSize = new Size(0, 1);
            foreach (TabPage page in tab.TabPages)
            {
                page.Text = "";
            }
        }
        private void btn_loadImg_Click(object sender, EventArgs e)
        {
            if (mPicBox1.LoadImg())
            {
                roi.Attach(mPicBox1.Image);
                btn_action.Visible = true;
                btn_learn.Visible = true;
                btn_loadMoudle.Visible = true;
                btn_save.Visible = true;
                btn_process.Visible = true;
                ckbox_inRoi.Visible = true;
                copyImg = new EImageBW8();
                copyImg.SetSize(mPicBox1.Image);
                EasyImage.Copy(mPicBox1.Image, copyImg);
            }
        }
        private void btn_loadMoudle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "导入模板";
            //ofd.RestoreDirectory = true;
            try
            {
                switch (modleType)
                {
                    case Type.None: MessageBox.Show("请先选择模板类型！");
                        break;
                    case Type.Match:
                        ofd.Filter = "(*.MCH)|*.MCH";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            match.Load(ofd.FileName);
                        }
                        break;
                    case Type.Find:
                        ofd.Filter = "(*.FND)|*.FND";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            find.Load(ofd.FileName);
                        }
                        break;
                    case Type.Ocr:
                        ofd.Filter = "(*.ocr)|*.ocr";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            ocr.Load(ofd.FileName);
                        }
                        break;
                    default:
                        break;
                }
                UpdateToUI();
            }
            catch 
            {
                MessageBox.Show("无法导入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private void rdbtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdbtn = sender as RadioButton;
            if (rdbtn.Checked)
            {
                if (!tabControl1.Visible)
                {
                    tabControl1.Visible = true;
                }
                ckbox_inRoi.Checked = false;
                ckbox_inRoi.Enabled = true;
                switch (rdbtn.Text)
                {
                    case "Match":
                        modleType = Type.Match;
                        tabControl1.SelectedIndex = 0;
                        mPicBox1.CanRoiMove = true;
                        break;
                    case "Find":
                        modleType = Type.Find;
                        tabControl1.SelectedIndex = 1;
                        mPicBox1.CanRoiMove = true;
                        break;
                    case "OCR":
                        modleType = Type.Ocr;
                        tabControl1.SelectedIndex = 2;
                        if (ocr == null)
                        {
                            ocr = new EOCR();
                            mPicBox1.MOcrs.Add(ocr);
                        }
                        mPicBox1.CanRoiMove = false;
                        ckbox_inRoi.Checked = true;
                        ckbox_inRoi.Enabled = false;
                        
                        break;
                    default: modleType = Type.None;
                        break;
                }
                mPicBox1.Refresh();
            }
        }

        private void btn_learn_Click(object sender, EventArgs e)
        {
            if (mPicBox1.Image ==null)
            {
                return;
            }
            switch (modleType)
            {
                case Type.None: MessageBox.Show("请先选择模板类型！");
                    break;
                case Type.Match:
                    match.LearnPattern(roi);
                    break;
                case Type.Find:
                    find.Learn(roi);
                    break;
                case Type.Ocr:
                    MessageBox.Show("请右键字符学习！");
                    break;
                default:
                    break;
            }
            UpdateToUI();
        }

        private void btn_action_Click(object sender, EventArgs e)
        {
            if (mPicBox1.Image == null)
            {
                return;
            }
            mPicBox1.MMatchers.Clear();
            mPicBox1.FinderPatterns.Clear();
            mPicBox1.strs.Clear();
            if (ckbox_inRoi.Checked)
            {
                switch (modleType)
                {
                    case Type.None: MessageBox.Show("请先选择模板类型！");
                        break;
                    case Type.Match:
                        if (match.PatternLearnt)
                        {
                            match.Match(roi);
                            mPicBox1.MMatchers.Add(match);

                            lstview_match.Items.Clear();
                            for (int i = 0; i < match.NumPositions; i++)
                            {
                                float score, centerX, centerY;
                                score =match.GetPosition(i).Score;
                                centerX = match.GetPosition(i).CenterX + roi.OrgX;
                                centerY = match.GetPosition(i).CenterY + roi.OrgY;
                                ListViewItem item = new ListViewItem();
                                item.SubItems[0].Text = (i + 1).ToString();
                                item.SubItems.Add(score.ToString("#0.00"));
                                item.SubItems.Add((centerX).ToString("#0.00"));
                                item.SubItems.Add((centerY).ToString("#0.00"));
                                item.SubItems.Add(match.GetPosition(i).Angle.ToString("#0.00"));
                                lstview_match.Items.Add(item);

                                mPicBox1.strs.Add(new StrAttribute(score.ToString("#0.00"), Color.Coral, new Point((int)centerX, (int)centerY)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("请先学习模板或者导入模板！");
                        }
                        break;
                    case Type.Find:
                        if (find.LearningDone)
                        {
                            finderPatterns = find.Find(roi);
                            mPicBox1.FinderPatterns.Add(finderPatterns);

                            lstview_find.Items.Clear();
                            for (int i = 0; i < finderPatterns.Length; i++)
                            {
                                float score, centerX, centerY;
                                score = finderPatterns[i].Score;
                                centerX = finderPatterns[i].Center.X + roi.OrgX;
                                centerY = finderPatterns[i].Center.Y + roi.OrgY;
                                ListViewItem item = new ListViewItem();
                                item.SubItems[0].Text = (i + 1).ToString();
                                item.SubItems.Add(score.ToString("#0.00"));
                                item.SubItems.Add(centerX.ToString("#0.00"));
                                item.SubItems.Add(centerY.ToString("#0.00"));
                                item.SubItems.Add(finderPatterns[i].Angle.ToString("#0.00"));
                                lstview_find.Items.Add(item);

                                mPicBox1.strs.Add(new StrAttribute(score.ToString("#0.00"), Color.Coral, new Point((int)centerX, (int)centerY)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("请先学习模板或者导入模板！");
                        }
                        break;
                    case Type.Ocr:
                        try
                        {
                            CodeChars = ocr.Recognize(roi, 200, (int)EOCRClass.AllClasses);
                            lb_result.Text = CodeChars;
                        }
                        catch
                        {
                            CodeChars = "";
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (modleType)
                {
                    case Type.None: MessageBox.Show("请先选择模板类型！");
                        break;
                    case Type.Match:
                        if (match.PatternLearnt)
                        {
                            match.Match(mPicBox1.Image);
                            mPicBox1.MMatchers.Add(match);

                            lstview_match.Items.Clear();
                            for (int i = 0; i < match.NumPositions; i++)
                            {
                                float score, centerX, centerY;
                                score = match.GetPosition(i).Score;
                                centerX = match.GetPosition(i).CenterX;
                                centerY = match.GetPosition(i).CenterY;
                                ListViewItem item = new ListViewItem();
                                item.SubItems[0].Text = (i + 1).ToString();
                                item.SubItems.Add(score.ToString("#0.00"));
                                item.SubItems.Add((centerX).ToString("#0.00"));
                                item.SubItems.Add((centerY).ToString("#0.00"));
                                item.SubItems.Add(match.GetPosition(i).Angle.ToString("#0.00"));
                                lstview_match.Items.Add(item);

                                mPicBox1.strs.Add(new StrAttribute(score.ToString("#0.00"), Color.Coral, new Point((int)centerX, (int)centerY)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("请先学习模板或者导入模板！");
                        }
                        break;
                    case Type.Find:
                        if (find.LearningDone)
                        {
                            finderPatterns = find.Find(mPicBox1.Image);
                            mPicBox1.FinderPatterns.Add(finderPatterns);

                            lstview_find.Items.Clear();
                            for (int i = 0; i < finderPatterns.Length; i++)
                            {
                                float score, centerX, centerY;
                                score = finderPatterns[i].Score;
                                centerX = finderPatterns[i].Center.X;
                                centerY = finderPatterns[i].Center.Y;
                                ListViewItem item = new ListViewItem();
                                item.SubItems[0].Text = (i + 1).ToString();
                                item.SubItems.Add(score.ToString("#0.00"));
                                item.SubItems.Add(centerX.ToString("#0.00"));
                                item.SubItems.Add(centerY.ToString("#0.00"));
                                item.SubItems.Add(finderPatterns[i].Angle.ToString("#0.00"));
                                lstview_find.Items.Add(item);

                                mPicBox1.strs.Add(new StrAttribute(score.ToString("#0.00"), Color.Coral, new Point((int)centerX, (int)centerY)));
                            }
                        }
                        else
                        {
                            MessageBox.Show("请先学习模板或者导入模板！");
                        }
                        break;
                    case Type.Ocr:
                        try
                        {
                            lb_result.Text = ocr.Recognize(mPicBox1.Image, 100, (int)EOCRClass.AllClasses);
                        }
                        catch
                        {
                            lb_result.Text = "";
                        }
                        break;
                    default:
                        break;
                }
            }


            mPicBox1.Refresh();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存模板";
            switch (modleType)
            {
                case Type.None: MessageBox.Show("请先选择模板类型！");
                    break;
                case Type.Match:
                    sfd.Filter = "(*.MCH)|*.MCH";
                    if (sfd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
                    {
                        match.Save(sfd.FileName);
                    }
                    break;
                case Type.Find:
                    sfd.Filter = "(*.FND)|*.FND";
                    if (sfd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
                    {
                        find.Save(sfd.FileName);
                    }
                    break;
                case Type.Ocr:
                    sfd.Filter = "(*.OCR)|*.OCR";
                    if (sfd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
                    {
                        ocr.Save(sfd.FileName);
                    }
                    break;
                default:
                    break;
            }
            sfd.Dispose();
        }

        private void UpdateToUI()
        {
            switch (modleType)
            {
                case Type.None:
                    break;
                case Type.Match:
                    txt_MinAngle.Text = match.MinAngle.ToString();
                    txt_MaxAngle.Text = match.MaxAngle.ToString();
                    txt_MaxPositions.Text = match.MaxPositions.ToString();
                    txt_MinScale.Text = match.MinScale.ToString();
                    txt_MaxScale.Text = match.MaxScale.ToString();
                    txt_MinScore.Text = match.MinScore.ToString();
                    break;
                case Type.Find:
                    txt_angleMid.Text = find.AngleBias.ToString();
                    txt_angleRange.Text = find.AngleTolerance.ToString();
                    txt_scaleMid.Text = find.ScaleBias.ToString();
                    txt_scaleRange.Text = find.ScaleTolerance.ToString();
                    txt_maxNum.Text = find.MaxInstances.ToString();
                    txt_minScroe.Text = find.MinScore.ToString();
                    break;
                case Type.Ocr:
                     nUpDown_minWidth.Value = ocr.MinCharWidth;
                     nUpDown_minHeight.Value = ocr.MinCharHeight;
                     nUpDown_maxWidth.Value = ocr.MaxCharWidth;
                     nUpDown_maxHeight.Value = ocr.MaxCharHeight;
                     nUpDown_spacing.Value = ocr.CharSpacing;
                     nUpDown_area.Value = ocr.NoiseArea;

                     comboBox2.SelectedIndex = (int)ocr.TextColor;
                     checkBox1.Checked = ocr.CutLargeChars;
                     checkBox2.Checked = ocr.RemoveNarrowOrFlat;
                    break;
                default:
                    break;
            }

        }

        private void txt_match_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            int index = Convert.ToInt32(txt.Tag);
            try
            {
                switch (index)
                {
                    case 0: match.MinAngle = Convert.ToSingle(txt.Text);
                        break;
                    case 1: match.MaxAngle = Convert.ToSingle(txt.Text);
                        break;
                    case 2: match.MinScale = Convert.ToSingle(txt.Text);
                        break;
                    case 3: match.MaxScale = Convert.ToSingle(txt.Text);
                        break;
                    case 4: match.MaxPositions = int.Parse(txt.Text);
                        break;
                    case 5: match.MinScore = Convert.ToSingle(txt.Text);
                        break;
                    default:
                        break;
                }
                btn_action.PerformClick();
            }
            catch 
            {
            }
        }

        private void txt_find_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            int index = Convert.ToInt32(txt.Tag);
            try
            {
                switch (index)
                {
                    case 0: find.AngleBias = Convert.ToSingle(txt.Text);
                        break;
                    case 1: find.AngleTolerance = Convert.ToSingle(txt.Text);
                        break;
                    case 2: find.ScaleBias = Convert.ToSingle(txt.Text);
                        break;
                    case 3: find.ScaleTolerance = Convert.ToSingle(txt.Text);
                        break;
                    case 4: find.MaxInstances = int.Parse(txt.Text);
                        break;
                    case 5: find.MinScore = Convert.ToSingle(txt.Text);
                        break;
                    default:
                        break;
                }
                btn_action.PerformClick();
            }
            catch
            {
            }
        }
         int charIndex;
        private void mPicBox1_MouseDown(object sender, MouseEventArgs e)
        {
           
            if (e.Button == System.Windows.Forms.MouseButtons.Right &&
                ocr.HitChars((int)((e.X - mPicBox1.MPanX) / mPicBox1.MImgScale), (int)((e.Y - mPicBox1.MPanY) / mPicBox1.MImgScale), out charIndex))
            {
                ocr.DrawChar(mPicBox1.CreateGraphics(), new ERGBColor(255, 255, 0), charIndex, mPicBox1.MImgScale, mPicBox1.MImgScale, mPicBox1.MPanX / mPicBox1.MImgScale, mPicBox1.MPanY / mPicBox1.MImgScale);
                groupBox2.Enabled = true;
                lb_FontWidth.Text = ocr.CharGetWidth(charIndex).ToString();
                lb_FontHeight.Text = ocr.CharGetHeight(charIndex).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ocrClass = 0;
            switch (comboBox3.SelectedIndex)
            {
                case 0: ocrClass = 1;
                    break;
                case 1: ocrClass = 2;
                    break;
                case 2: ocrClass = 4;
                    break;
                case 3: ocrClass = 8;
                    break;
                default: ocrClass = 2147483647;
                    break;
            }
            ocr.LearnPattern(roi, charIndex, Char_to_Ascii(txt_char.Text), ocrClass);
            groupBox2.Enabled = false;
            
        }
        private int Char_to_Ascii(string c)
        {
            try
            {
                byte[] arr = new byte[1];
                arr = System.Text.ASCIIEncoding.ASCII.GetBytes(c.Trim());
                int asc = (int)(arr[0]);
                return asc;
            }
            catch
            {
                return 0;
            }
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            Task t = new Task();
            ImgSourceEvent ise = new ImgSourceEvent(t);
            ImgPreProcessEvent ipe = new ImgPreProcessEvent(t);
            t.Events.Add(ise);
            t.Events.Add(ipe);
            ipe.config.SelectImgIndex = 1;
            t.Imgs.Add(new ImgDictionary(new ImgPreProcessConfig(), copyImg, 0));
            ipe.ShowDialog();
            if (ipe.OutputImg != null)
            {
                mPicBox1.Image = ipe.OutputImg;
                roi.Attach(mPicBox1.Image);
                mPicBox1.Refresh();
            }
        }

        private void mPicBox1_Paint(object sender, PaintEventArgs e)
        {
            if (ocr != null && rdbtn_ocr.Checked)
            {
                float imgScale = mPicBox1.MImgScale;
                e.Graphics.DrawRectangle(new Pen(Color.Red), 200 - (ocr.MinCharWidth / 2) * imgScale, 200 - (ocr.MinCharHeight / 2) * imgScale, ocr.MinCharWidth * imgScale, ocr.MinCharHeight * imgScale);
                e.Graphics.DrawRectangle(new Pen(Color.Red), 200 - (ocr.MaxCharWidth / 2) * imgScale, 200 - (ocr.MaxCharHeight / 2) * imgScale, ocr.MaxCharWidth * imgScale, ocr.MaxCharHeight * imgScale);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocr.TextColor = (EOCRColor) comboBox2.SelectedIndex;
            btn_action.PerformClick();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ocr.CutLargeChars = checkBox1.Checked;
            btn_action.PerformClick();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ocr.RemoveNarrowOrFlat = checkBox2.Checked;
            btn_action.PerformClick();
        }

        private void nUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            switch (nud.Tag.ToString())
            {
                case "0": ocr.MinCharWidth = (int)nud.Value;
                    break;
                case "1": ocr.MaxCharWidth = (int)nud.Value;
                    break;
                case "2": ocr.MinCharHeight = (int)nud.Value;
                    break;
                case "3": ocr.MaxCharHeight = (int)nud.Value;
                    break;
                case "4": ocr.CharSpacing = (int)nud.Value;
                    break;
                case "5": ocr.NoiseArea = (int)nud.Value;
                    break;
                default:
                    break;
            }
            btn_action.PerformClick();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
        }
    }
}
