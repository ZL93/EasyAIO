using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    internal class ImgDictionary
    {
        private EImageBW8 _img;
        public EImageBW8 Img
        {
            get { return _img; }
            set { _img = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public BaseConfig Config { get; set; }

        public int Index { get; set; }
        public ImgDictionary(BaseConfig baseConfig, EImageBW8 img,int index)
        {
            _name = baseConfig.ToolName + ".Img";
            _img = img;
            Config = baseConfig;
            Index = index;
        }

    }
}
