using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    internal class ImgSourceConfig : BaseConfig
    {
        public int SelectMode { get; set; }

        public string ImgPath { get; set; }

        public string FolderPath { get; set; }

        public int SelectCCD { get; set; }

        private string _toolName = "图像获取";
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
            get
            {
                return "EasyAIO.ImgSourceFactory";
            }
           
        }
    }
}
