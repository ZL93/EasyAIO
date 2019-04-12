using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
     [Serializable]
    public class OcrConfig : BaseConfig
    {
        private string _toolName = "字符识别";
        public OcrConfig()
        {
        }
        public override string ToolName
        {
            get
            {
                return _toolName;
            }
            set
            {
                _toolName = value;
            }
        }

        public override string FactoryTypeName
        {
            get { return "EasyAIO.OcrFactory"; }
        }

      
        public bool UseRoi { get; set; }
        public int Roi_OrgX { get; set; }
        public int Roi_OrgY { get; set; }

        private int width = 300;
        public int Roi_Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height = 300;
        public int Roi_Height
        {
            get { return height; }
            set { height = value; }
        }
        public string OCRPath { get; set; }
    }
}
