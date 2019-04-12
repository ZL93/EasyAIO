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
    public partial class OcrFrm : ToolEditBaseFrm
    {
        #region 公共
        private OcrEvent ocrEvent = null;
        public OcrFrm(OcrEvent ocrE)
            : base(ocrE)
        {
            InitializeComponent();
            ocrEvent = ocrE;
        }
        protected override void UpdateUIToEvent()
        {
            base.UpdateUIToEvent();
           
            if (comBox_img.SelectedIndex > 0)
            {
                ocrEvent.ocrConfig.UseRoi = ckBox_Roi.Checked;
                ocrEvent.Roi.Comment = ckBox_Roi.Checked ? "" : "NoShow";
                ocrEvent.Roi.Attach(ocrEvent.InputImg);
                picbox_ToolEdit.CanRoiMove = true;
            }
            else
            {
                picbox_ToolEdit.CanRoiMove = false;
            }

            ocrEvent.ocrConfig.Roi_OrgX = ocrEvent.Roi.OrgX;
            ocrEvent.ocrConfig.Roi_OrgY = ocrEvent.Roi.OrgY;
            ocrEvent.ocrConfig.Roi_Width = ocrEvent.Roi.Width;
            ocrEvent.ocrConfig.Roi_Height = ocrEvent.Roi.Height;

            ocrEvent.ocrConfig.OCRPath = txt_ocrPath.Text;
            Run();
        }

        protected override void UpdateEventToUI()
        {
            base.UpdateEventToUI();
            txt_ocrPath.Text = ocrEvent.ocrConfig.OCRPath;
            ckBox_Roi.Checked = ocrEvent.ocrConfig.UseRoi;
            ocrEvent.Roi.Comment = ckBox_Roi.Checked ? "" : "NoShow";
            ocrEvent.Roi.SetPlacement(ocrEvent.ocrConfig.Roi_OrgX, ocrEvent.ocrConfig.Roi_OrgY,
                                          ocrEvent.ocrConfig.Roi_Width, ocrEvent.ocrConfig.Roi_Height);

            if (ocrEvent.ocrConfig.SelectImgIndex < comBox_img.Items.Count)
            {
                if (ocrEvent.ocrConfig.SelectImgIndex > 0)
                {
                    ocrEvent.Roi.Attach(ocrEvent.InputImg);
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
        }

        protected override void Run()
        {
            stopWatch.Reset();
            stopWatch.Start();
            if (ocrEvent.Run(false))
            {
                tsl_status.Image = Properties.Resources.green;
            }
            else
            {
                tsl_status.Image = Properties.Resources.red;
            }
            lb_err.Visible = false;
            if (!System.IO.File.Exists(ocrEvent.ocrConfig.OCRPath))
            {
                lb_err.Visible = true;
            }
            lb_result.Text = ocrEvent.Code;
            picbox_ToolEdit.Refresh();
            stopWatch.Stop();
            tsl_CT.Text = stopWatch.ElapsedMilliseconds + "毫秒";
        }
        #endregion

        private void picbox_ToolEdit_Paint(object sender, PaintEventArgs e)
        {
            if (ocrEvent.ocr != null)
            {
                float imgScale = picbox_ToolEdit.MImgScale;
                e.Graphics.DrawRectangle(new Pen(Color.Red), 300 - (ocrEvent.ocr.MinCharWidth / 2) * imgScale, 300 - (ocrEvent.ocr.MinCharHeight / 2) * imgScale,
                    ocrEvent.ocr.MinCharWidth * imgScale, ocrEvent.ocr.MinCharHeight * imgScale);
                e.Graphics.DrawRectangle(new Pen(Color.Red), 300 - (ocrEvent.ocr.MaxCharWidth / 2) * imgScale, 300 - (ocrEvent.ocr.MaxCharHeight / 2) * imgScale,
                    ocrEvent.ocr.MaxCharWidth * imgScale, ocrEvent.ocr.MaxCharHeight * imgScale);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "字符文件|*.OCR";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txt_ocrPath.Text = ofd.FileName;
                ocrEvent.ocr.Load(ofd.FileName);
                UpdateUIToEvent();
            }
        }

        private void OcrFrm_Load(object sender, EventArgs e)
        {
            if (ocrEvent.InputImg != null)
            {
                ocrEvent.Roi.Attach(ocrEvent.InputImg);
            }
            picbox_ToolEdit.MRois.Add(ocrEvent.Roi);
            picbox_ToolEdit.MOcrs.Add(ocrEvent.ocr);
            ckBox_Roi.CheckedChanged += delegate { UpdateUIToEvent(); };
        }
    }
}
