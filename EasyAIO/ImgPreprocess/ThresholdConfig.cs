using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    [Serializable]
    public class ThresholdConfig:ImgConfigBase
    {
        public enum MyThresholdMode
        {
            Auto,
            Absolute,
            Relative
        }
        public ThresholdConfig()
        {
            Mode = MyThresholdMode.Auto;
            AbsoluteValue = 100;
            RelativeValue = 0.7f;
        }
        public override string Name
        {
            get { return "二值化"; }
        }

        public MyThresholdMode Mode { get; set; }

        public int AbsoluteValue { get; set; }

        public float RelativeValue { get; set; }

        public override EImageBW8 Run(EImageBW8 img)
        {
            if (!Enable)
            {
                return img;
            }
            EImageBW8 imgBw8 = new EImageBW8();
            imgBw8.SetSize(img);
            switch (Mode)
            {
                case MyThresholdMode.Auto:
                    EBW8 value1 = EasyImage.AutoThreshold(img, EThresholdMode.MinResidue);
                    AbsoluteValue = value1.Value;
                    break;
                case MyThresholdMode.Absolute:
                    break;
                case MyThresholdMode.Relative:
                    EBW8 value2 = EasyImage.AutoThreshold(img, EThresholdMode.Relative, RelativeValue);
                    AbsoluteValue = value2.Value;
                    break;
                default:
                    break;
            }
            EasyImage.Threshold(img, imgBw8, AbsoluteValue);
            img.Dispose();
            img = new EImageBW8();
            return imgBw8;
        }
    }
}
