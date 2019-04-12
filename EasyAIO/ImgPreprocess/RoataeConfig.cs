using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    [Serializable]
    public class RoataeConfig : ImgConfigBase
    {
        public override string Name
        {
            get { return "旋转"; }
        }
        public int Value { get; set; }
        public override EImageBW8 Run(EImageBW8 img)
        {
            if (!Enable)
            {
                return img;
            }
            EImageBW8 imgBw8 = new EImageBW8();
            float a = 0f, x = 0f, y = 0f;
            if (Value % 180<90)
            {
                //当前角度
                a = (float)Math.Atan2((float)img.Height / 2, (float)img.Width / 2);
                //旋转后角度
                float b = a + Value % 90 * ((float)Math.PI / 180);
                float c = a - Value % 90 * ((float)Math.PI / 180);
                //斜边
                float z = (float)Math.Sqrt(img.Width * img.Width + img.Height * img.Height) / 2;
                //xy增量
                x = (float)(Math.Cos(c) - Math.Cos(a)) * z;
                y = (float)(Math.Sin(b) - Math.Sin(a)) * z;
                //设置新图片大小
                imgBw8.SetSize((int)(img.Width + 2 * x), (int)(img.Height + 2 * y));
            }
            else if (Value % 180 >= 90)
            {
                //当前角度
                a = (float)Math.Atan2((float)img.Height / 2, (float)img.Width / 2);
                //旋转后角度
                float b = a + Value % 90 * ((float)Math.PI / 180);
                float c = a - Value % 90 * ((float)Math.PI / 180);
                //斜边
                float z = (float)Math.Sqrt(img.Width * img.Width + img.Height * img.Height) / 2;
                //xy增量
                y = (float)(Math.Cos(c) - Math.Cos(a)) * z;
                x = (float)(Math.Sin(b) - Math.Sin(a)) * z;
                //设置新图片大小
                imgBw8.SetSize((int)(img.Height + 2 * x), (int)(img.Width + 2 * y));
            }
            EasyImage.ScaleRotate(img, (float)img.Width / 2, (float)img.Height / 2, (float)imgBw8.Width / 2, (float)imgBw8.Height / 2, 1, 1, Value, imgBw8, 0);
            img.Dispose();
            img = new EImageBW8();
            return imgBw8;
        }
    }
}
