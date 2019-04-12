using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    internal class MatcherConfig : BaseConfig
    {
        internal List<ResultsDetermine> ResultsDetermines = new List<ResultsDetermine>() { 
        new ResultsDetermine(){ Name ="数量",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="中心X",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="中心Y",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="角度",Enable =false,Min =0,Max =99999},
        };
        private string _toolName = "模板匹配";
        public MatcherConfig()
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
            get { return "EasyAIO.MatcherFactory"; }
        }

        public bool UseRoi { get; set; }
        public int Roi_OrgX { get; set; }
        public int Roi_OrgY { get; set; }

        private int width = 300;
        public int Roi_Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height = 300;
        public int Roi_Height
        {
            get { return height; }
            set { height = value; }
        }
        public string MatcherPath { get; set; }

        public string FinderPath { get; set; }

        public int MatchType { get; set; }
    }
}
