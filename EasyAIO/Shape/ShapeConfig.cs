using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class ShapeConfig : BaseConfig
    {
        private string _toolName = "坐标系";
        public ShapeConfig()
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
            get { return "EasyAIO.ShapeFactory"; }
        }
        
        public float OrgX { get; set; }
        public float OrgY { get; set; }
        public float Angle { get; set; }


        private string linkCenterX = string.Empty;
        public string LinkCenterX
        {
            get { return linkCenterX; }
            set { linkCenterX = value; }
        }

        private string linkCenterY = string.Empty;
        public string LinkCenterY
        {
            get { return linkCenterY; }
            set { linkCenterY = value; }
        }

        private string linkAngle = string.Empty;
        public string LinkAngle
        {
            get { return linkAngle; }
            set { linkAngle = value; }
        }
        
    }
}
