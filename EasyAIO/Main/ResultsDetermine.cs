using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    internal class ResultsDetermine
    {
        public string Name { get; set; }
        public bool Enable { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
