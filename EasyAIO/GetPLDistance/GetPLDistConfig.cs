using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class GetPLDistConfig : BaseConfig
    {
        private string _toolName = "求点到线距离";
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
                return "EasyAIO.GetPLDistFactory";
            }

        }

        public float P_CenterX { get; set; }
        public float P_CenterY { get; set; }
        public float L_CenterX { get; set; }
        public float L_CenterY { get; set; }
        public float L_Angle { get; set; }

        public string LinkP_CenterX { get; set; }
        public string LinkP_CenterY { get; set; }
        public string LinkL_CenterX { get; set; }
        public string LinkL_CenterY { get; set; }
        public string LinkL_Angle { get; set; }

        public string ShapeName { get; set; }
    }
}
