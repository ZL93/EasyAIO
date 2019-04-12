using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    internal class RoiConfig
    {
        public string  Nmae { get; set; }
        public int OrgX { get; set; }
        public int OrgY { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
       
    }
}
