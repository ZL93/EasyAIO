using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public class Result
    {
        public string Name { get; set; }

        public Dictionary<string, float[]> ValueParams = new Dictionary<string, float[]>();

        public Dictionary<string, string> StrParams = new Dictionary<string, string>();
    }
}
