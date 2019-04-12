using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class ImgPreProcessConfig : BaseConfig
    {
        public List<ImgConfigBase> CfgGroup = new List<ImgConfigBase>();

        private string _toolName = "图像预处理";
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
            get { return "EasyAIO.ImgPreProcessFactory"; }
        }

    }
}
