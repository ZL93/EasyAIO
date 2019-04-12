using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public class OcrEvent : BaseEvent
    {
        #region 公共

        internal OcrConfig ocrConfig = new OcrConfig();
        public override BaseConfig Config
        {
            get
            {
                return ocrConfig;
            }
            set
            {
                ocrConfig = value as OcrConfig;
            }
        }
        public OcrEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            ocr = new EOCR();
            Roi = new EROIBW8();
            Roi.SetPlacement(0, 0, 300, 300);
            ResultData.Name = ocrConfig.ToolName;
            ResultData.StrParams.Clear();
            ResultData.StrParams.Add("字符", "");
            ParentTask.ResultDatas.Add(ResultData);
        }
        public override bool Run(bool isTaskRun)
        {
            int index = ocrConfig.SelectImgIndex - 1;
            if (index < 0 || index >= ParentTask.Imgs.Count)
            {
                InputImg = null;
               
                return false;
            }
            InputImg = ParentTask.Imgs[index].Img;
            if (InputImg == null || InputImg.IsVoid)
            {
               
                return false;
            }
            if (ocrConfig.UseRoi)
            {
                Roi.SetPlacement(ocrConfig.Roi_OrgX, ocrConfig.Roi_OrgY, ocrConfig.Roi_Width, ocrConfig.Roi_Height);
                if (Roi.Width > InputImg.Width)
                {
                    Roi.Width = InputImg.Width;
                }
                if (Roi.Height > InputImg.Height)
                {
                    Roi.Height = InputImg.Height;
                }
                if (Roi.OrgX < 0)
                {
                    Roi.OrgX = 0;
                }
                if (Roi.OrgY < 0)
                {
                    Roi.OrgY = 0;
                }
                if (Roi.OrgX + Roi.Width > InputImg.Width)
                {
                    Roi.OrgX = InputImg.Width - Roi.Width;
                }
                if (Roi.OrgY + Roi.Height > InputImg.Height)
                {
                    Roi.OrgY = InputImg.Height - Roi.Height;
                }
                Roi.Attach(InputImg);
               
            }

            if (System.IO.File.Exists(ocrConfig.OCRPath))
            {
                ocr.Load(ocrConfig.OCRPath);
            }
            else
            {
                return false;
            }
            Code = "";
            try
            {
                if (ocrConfig.UseRoi)
                {
                    Code = ocr.Recognize(Roi, 200, (int)EOCRClass.AllClasses);
                }
                else
                {
                    Code = ocr.Recognize(InputImg, 200, (int)EOCRClass.AllClasses);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            ResultData.Name = ocrConfig.ToolName;
            ResultData.StrParams.Clear();
            ResultData.StrParams.Add("字符", Code);
            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }
            return true;
        }
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            OcrFrm frm = new OcrFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }
        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                picBox.MOcrs.Add(ocr);
            }
        }
        #endregion

        
        internal EROIBW8 Roi = null;
        internal EOCR ocr = null;
        internal string Code = "";
    }
}
