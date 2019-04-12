using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    internal class ImgSourceEvent : BaseEvent
    {
        internal ImgSourceConfig config = new ImgSourceConfig();

        public EImageBW8 OutputImg =new EImageBW8();
     
        public ImgSourceEvent(Task task)
            :base(task)
        {
        }
        public override void Initialize()
        {
            config.CanDrawToPicBox = true;
        }

        public int selectImg = 0;
        public override bool Run(bool isTaskRun =false) 
        {
            OutputImg = new EImageBW8();
            switch (config.SelectMode)
            {
                case 0:
                    if (ParentTask.SourceImg!=null)
                    {
                        OutputImg.SetSize(ParentTask.SourceImg);
                        EasyImage.Copy(ParentTask.SourceImg, OutputImg);
                    }
                    else
                    {
                        OutputImg = null;
                    }
                    
                    if (isTaskRun)
                    {
                        ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Imgs.Count));
                    }
                    break;
                case 1:
                    if (!File.Exists(config.ImgPath))
                    {
                        return false;
                    }
                    
                    OutputImg.Load(config.ImgPath);
                    if (isTaskRun)
                    {
                        ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Imgs.Count));
                    }
                    break;
                case 2:
                    if (!Directory.Exists(config.FolderPath))
                    {
                        return false;
                    }
                    DirectoryInfo dir = new DirectoryInfo(config.FolderPath);
                    FileInfo[] fil = dir.GetFiles("*.bmp", SearchOption.TopDirectoryOnly);
                    if (fil.Length==0)
                    {
                        return false;
                    }
                    if (selectImg >= fil.Length)
                    {
                        selectImg = 0;
                    }
                    OutputImg.Load(fil[selectImg++].FullName);
                    if (isTaskRun)
                    {
                        ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Imgs.Count));
                    }
                    break;
                case 3:
                    if (config.SelectCCD > 0)
                    {
                        int select = config.SelectCCD - 1;
                        if (ParentTask.Cameras[select].IsConnect)
                        {
                            ImgAtt att = ParentTask.Cameras[select].Grab();
                            OutputImg = new EImageBW8();
                            OutputImg.SetImagePtr(att.ImgWidth, att.ImgHeight, att.ImgPointer);

                            if (isTaskRun)
                            {
                                ParentTask.Imgs.Add(new ImgDictionary(Config, OutputImg, ParentTask.Imgs.Count));
                            }
                        }
                        else
                        {
                            OutputImg = null;
                            return false;
                        }
                    }
                    else
                    {
                        OutputImg = null;
                        return false;
                    }
                   
                    break;
                default:
                    return false;
            }
            return true;
        }
          
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            ImgSourceFrm frm = new ImgSourceFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override BaseConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value as ImgSourceConfig;
            }
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            picBox.Image = OutputImg;
        }
    }
}
