using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAIO
{
    /// <summary>
    ///  Build by ZL 19.01.29
    /// </summary>
    [Serializable]
    public abstract class BaseConfig
    {
        public bool CanDrawToPicBox { get; set; }
        public int SelectImgIndex { get; set; }
        public abstract string ToolName { get;set; }
        public abstract string FactoryTypeName { get;}
       

    }
}
