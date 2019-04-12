using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    [Serializable]
    public class GainOffsetConfig : ImgConfigBase
    {
        public override string Name
        {
            get { return "亮度调节"; }
        }
        public float Gain { get; set; }
        public float Offset { get; set; }
        public override EImageBW8 Run(EImageBW8 img)
        {
            if (!Enable)
            {
                return img;
            }
            EImageBW8 imgBw8 = new EImageBW8();
            imgBw8.SetSize(img);
            EasyImage.GainOffset(img, imgBw8, Gain, Offset);
            img.Dispose();
            img = new EImageBW8();
            return imgBw8;
        }
    }
}
