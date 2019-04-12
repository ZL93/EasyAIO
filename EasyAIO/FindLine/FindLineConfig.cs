using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class FindLineConfig : BaseConfig
    {
        internal List<ResultsDetermine> ResultsDetermines = new List<ResultsDetermine>() { 
        new ResultsDetermine(){ Name ="中心X",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="中心Y",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="角度",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="异常点数",Enable =false,Min =0,Max =99999},
        };
        private string _toolName = "直线检测";
        public FindLineConfig()
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
            get { return "EasyAIO.FindLineFactory"; }
        }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Angle { get; set; }
        public float Length { get; set; }
        public float Tolerance { get; set; }
        public int TransitionType { get; set; }
        public int TransitionChoice { get; set; }
        public int Passes { get; set; }
        public float SamplingStep { get; set; }
        public int Threshold { get; set; }

        public string ShapeName { get; set; }
    }
}
