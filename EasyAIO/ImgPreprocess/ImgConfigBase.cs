using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    [Serializable]
    public abstract class ImgConfigBase
    {
        public abstract string Name { get;}
        public abstract EImageBW8 Run(EImageBW8 img);
       
        private bool _enable =true;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

    }
}
