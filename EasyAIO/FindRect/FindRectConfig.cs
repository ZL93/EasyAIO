using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class FindRectConfig:BaseConfig
    {
        internal List<ResultsDetermine> ResultsDetermines = new List<ResultsDetermine>() { 
        new ResultsDetermine(){ Name ="中心X",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="中心Y",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="角度",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="宽度",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="高度",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="异常点数",Enable =false,Min =0,Max =99999},
        };
        private string _toolName = "矩形检测";
        public FindRectConfig()
        {
            SamplingStep = 5;
            Threshold = 20;
            Passes = 2;
        }
        public override string ToolName
        {
            get
            {
                return _toolName;
            }
            set
            {
                _toolName =value;
            }
        }

        public override string FactoryTypeName
        {
            get { return "EasyAIO.FindRectFactory"; }
        }

        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public float Angle { get; set; }
        public float Tolerance { get; set; }
        public int TransitionType { get; set; }
        public int TransitionChoice { get; set; }
        public int Passes { get; set; }
        public float SamplingStep { get; set; }
        public int Threshold { get; set; }

        public string ShapeName { get; set; }


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
        public int LinkOffsetCenterX { get; set; }
        public int LinkOffsetCenterY { get; set; }
        public int LinkOffsetAngle { get; set; }
    }
}
