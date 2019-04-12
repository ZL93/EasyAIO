using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public interface ICamera
    {
        string CameraName { get; }

        string ToolName { get; }

        bool IsConnect { get; }

        bool Connect();

        ImgAtt Grab();

        bool Disconnect();
    }

    public struct ImgAtt
    {
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int ImgWidth { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public int ImgHeight { get; set; }
        /// <summary>
        /// 图片指针
        /// </summary>
        public IntPtr ImgPointer { get; set; }
    }
}
