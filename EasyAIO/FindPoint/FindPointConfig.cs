using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class FindPointConfig : BaseConfig
    {
        internal List<ResultsDetermine> ResultsDetermines = new List<ResultsDetermine>() { 
        new ResultsDetermine(){ Name ="点个数",Enable =false,Min =0,Max =99999},
        };
        private string _toolName = "点检测";
        public FindPointConfig()
        {
            Threshold = 20;
            Tolerance = 100;
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
            get { return "EasyAIO.FindPointFactory"; }
        }

        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Angle { get; set; }
        public float Tolerance { get; set; }
        public int TransitionType { get; set; }
        public int TransitionChoice { get; set; }
        public int Threshold { get; set; }

        public string ShapeName { get; set; }
    }
}
