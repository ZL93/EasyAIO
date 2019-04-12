using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class Get2LIntersectionConfig : BaseConfig
    {
        private string _toolName = "求两直线交点";
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
                return "EasyAIO.Get2LIntersectionFactory";
            }

        }

        public bool IsNormalShape { get; set; }
        
        public float L_CenterX1 { get; set; }
        public float L_CenterY1 { get; set; }
        public float L_Angle1 { get; set; }
        public float L_CenterX2 { get; set; }
        public float L_CenterY2 { get; set; }
        public float L_Angle2 { get; set; }

        public string LinkL_CenterX1 { get; set; }
        public string LinkL_CenterY1 { get; set; }
        public string LinkL_Angle1 { get; set; }
        public string LinkL_CenterX2 { get; set; }
        public string LinkL_CenterY2 { get; set; }
        public string LinkL_Angle2 { get; set; }


        public string ShapeName { get; set; }
    }
}
