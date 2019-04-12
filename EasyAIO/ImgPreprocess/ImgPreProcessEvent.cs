using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public class ImgPreProcessEvent : BaseEvent
    {

        internal ImgPreProcessConfig config = new ImgPreProcessConfig();
        public EImageBW8 OutputImg;

     
        public override BaseConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value as ImgPreProcessConfig;
            }
        }

        public ImgPreProcessEvent(Task task)
            : base(task)
        { }

        public override void Initialize()
        {

        }

        public override bool Run(bool isTaskRun = false)
        {
            int index = config.SelectImgIndex - 1;
            if (index < 0 || index >= ParentTask.Imgs.Count)
            {
                OutputImg = InputImg = null;
                ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Events.IndexOf(this)));
                return false;
            }
            InputImg = ParentTask.Imgs[index].Img;
            if (InputImg == null || InputImg.IsVoid)
            {
                OutputImg = null;
                ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Events.IndexOf(this)));
                return false;
            }
            OutputImg = new EImageBW8();
            OutputImg.SetSize(InputImg);
            EasyImage.Copy(InputImg, OutputImg);
            foreach (var item in config.CfgGroup)
            {
                OutputImg = item.Run(OutputImg);
            }

            if (isTaskRun)
            {
                ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Events.IndexOf(this)));
            }
            else
            {
                ImgDictionary imgdic = ParentTask.Imgs.Find(s => { return s.Config == Config; });
                if (imgdic != null)
                {
                    imgdic.Img = OutputImg;
                }
            }
            return true;
        }

        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            ImgPreprocessFrm frm = new ImgPreprocessFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                picBox.Image = OutputImg;
            }
        }
    }
}
