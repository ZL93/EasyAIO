using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class Get2PDistConfig : BaseConfig
    {
        private string _toolName = "求两点距离";
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
                return "EasyAIO.Get2PDistFactory";
            }

        }

        public string LinkCenterX1 { get; set; }

        public string LinkCenterY1 { get; set; }

        public string LinkCenterX2 { get; set; }

        public string LinkCenterY2 { get; set; }

        public float CenterX1 { get; set; }
        public float CenterY1 { get; set; }
        public float CenterX2 { get; set; }
        public float CenterY2 { get; set; }

        public string ShapeName { get; set; }
    }
}
