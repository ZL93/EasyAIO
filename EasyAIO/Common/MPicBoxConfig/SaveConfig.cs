using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    internal class SaveConfigGroup
    {
        public List<RoiConfig> RoiConfigs = new List<RoiConfig>();
        public List<PointConfig> PointConfigs = new List<PointConfig>();
        public List<LineConfig> LineConfigs = new List<LineConfig>();
        public List<CircleConfig> CircleConfigs = new List<CircleConfig>();
        public List<RectConfig> RectConfigs = new List<RectConfig>();
    }
}
