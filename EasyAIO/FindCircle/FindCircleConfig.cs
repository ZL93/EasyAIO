using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class FindCircleConfig : BaseConfig
    {
        internal List<ResultsDetermine> ResultsDetermines = new List<ResultsDetermine>() { 
        new ResultsDetermine(){ Name ="中心X",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="中心Y",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="直径",Enable =false,Min =0,Max =99999},
         new ResultsDetermine(){ Name ="周长",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="异常点数",Enable =false,Min =0,Max =99999},
        };
        private string _toolName = "圆形检测";
        public FindCircleConfig()
        {
            SamplingStep = 5;
            Threshold = 20;
            Passes = 2;
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
            get { return "EasyAIO.FindCircleFactory"; }
        }

        public float CenterX { get; set; }
        public float CenterY { get; set; }
        private bool full = true;
        public bool FullCircle
        {
            get { return full; }

            set
            {
                full = value;
                if (!full && Amplitude == 360)
                { Amplitude = 180; }
                else if (full && Amplitude != 360)
                {
                    Amplitude = 360;
                }
            }
        }
        public float Angle { get; set; }
        public float Amplitude { get; set; }
        public float Diameter { get; set; }
        public float Tolerance { get; set; }
        public int TransitionType { get; set; }
        public int TransitionChoice { get; set; }
        public int Passes { get; set; }
        public float SamplingStep { get; set; }
        public int Threshold { get; set; }

        public string ShapeName { get; set; }
    }
}
