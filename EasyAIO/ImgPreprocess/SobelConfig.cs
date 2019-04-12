using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    [Serializable]
    public class SobelConfig : ImgConfigBase
    {
        public override string Name
        {
            get { return "Sobel"; }
        }

        public override EImageBW8 Run(EImageBW8 img)
        {
            if (!Enable)
            {
                return img;
            }
            EImageBW8 imgBw8 = new EImageBW8();
            imgBw8.SetSize(img);
            EasyImage.ConvolSobel(img, imgBw8);
            img.Dispose();
            img = new EImageBW8();
            return imgBw8;
        }
    }
}
