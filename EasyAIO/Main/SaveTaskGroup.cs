using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAIO
{
    [Serializable]
    internal class SaveTaskGroup
    {
        public SaveTaskGroup()
        {
            DllVersion = string.Empty;
        }
        internal string DllVersion { get; set; }
        internal List<ConfigGroup> ConfigGroup = new List<ConfigGroup>();
    }

    [Serializable]
    internal class ConfigGroup
    {
        public ConfigGroup()
        {
            TaskName = string.Empty;
        }
        internal string TaskName { get; set; }
        internal List<BaseConfig> CfgGroup = new List<BaseConfig>();
    }
}
