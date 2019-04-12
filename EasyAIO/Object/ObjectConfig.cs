using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class ObjectConfig : BaseConfig
    {
        internal BindingList<ObjFeatureFilter> FeatureFilters = new BindingList<ObjFeatureFilter>();
        internal List<ResultsDetermine> ResultsDetermines = new List<ResultsDetermine>() { 
        new ResultsDetermine(){ Name ="数量",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="面积",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="重心X",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="重心Y",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="宽度",Enable =false,Min =0,Max =99999},
        new ResultsDetermine(){ Name ="高度",Enable =false,Min =0,Max =99999},
        };
        private string _toolName = "斑点工具";
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
                return "EasyAIO.ObjectFactory";
            }

        }
        public bool UseRoi { get; set; }
        public int Roi_OrgX { get; set; }
        public int Roi_OrgY { get; set; }

        private int width =300;
        public int Roi_Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height =300;
        public int Roi_Height
        {
            get { return height; }
            set { height = value; }
        }

        public bool WhiteEncode { get; set; }

        public bool CanSort { get; set; }

        public int SortIndex { get; set; }

        public bool IsAscending { get; set; }

        private string linkorgx = string.Empty;
        public string LinkOrgX
        {
            get { return linkorgx; }
            set { linkorgx = value; }
        }

        private string linkorgy = string.Empty;

        public string LinkOrgY
        {
            get { return linkorgy; }
            set { linkorgy = value; }
        }

        private string linkwidth = string.Empty;
        public string LinkWidth
        {
            get { return linkwidth; }
            set { linkwidth = value; }
        }

        private string linkheight = string.Empty;
        public string LinkHeight
        {
            get { return linkheight; }
            set { linkheight = value; }
        }

        public int LinkOffsetOrgX { get; set; }
        public int LinkOffsetOrgY { get; set; }
        public int LinkOffsetWidth { get; set; }
        public int LinkOffsetHeight { get; set; }
    }
}
