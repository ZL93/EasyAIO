using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    public class ObjFeatureFilter
    {
        public int Feature { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
