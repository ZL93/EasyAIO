using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    [Serializable]
    public class ErodeConfig : ImgConfigBase
    {
        public override string Name
        {
            get { return "腐蚀"; }
        }
        public MorphologyType Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public override EImageBW8 Run(EImageBW8 img)
        {
            if (!Enable)
            {
                return img;
            }
            EImageBW8 imgBw8 = new EImageBW8();
            imgBw8.SetSize(img);
            switch (Type)
            {
                case MorphologyType.Square:
                    EasyImage.ErodeBox(img, imgBw8, Width);
                    break;
                case MorphologyType.Rectangle:
                    EasyImage.ErodeBox(img, imgBw8, Width, Height);
                    break;
                case MorphologyType.Circle:
                    EasyImage.ErodeDisk(img, imgBw8, Width);
                    break;
                default: EasyImage.ErodeBox(img, imgBw8, Width);
                    break;
            }
            img.Dispose();
            img = new EImageBW8();
            return imgBw8;
        }
    }
}
