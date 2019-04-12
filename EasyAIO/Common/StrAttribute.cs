using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public class StrAttribute
    {
        public StrAttribute(string text, Color strColor, Point pixLoc)
        {
            this.text = text;
            this.strColor = strColor;
            this.pixelsLocation = pixLoc;
        }
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private Color strColor;
        public Color StrColor
        {
            get { return strColor; }
            set { strColor = value; }
        }

        private Point pixelsLocation;

        public Point PixelsLocation
        {
            get { return pixelsLocation; }
            set { pixelsLocation = value; }
        }
        
    }

    public class LineAttribute
    {
        public LineAttribute(Pen pen, PointF startPoint, PointF endPoint)
        {
            this.linePen = pen;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        private Pen linePen;
        public Pen LinePen
        {
            get { return linePen; }
            set { linePen = value; }
        }

        private PointF startPoint;
        public PointF StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private PointF endPoint;
        public PointF EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
        
    }

    public class PointAttribute
    {
        public PointAttribute(Pen pen, PointF location)
        {
            this.pointPen = pen;
            this.pixelsLocation = location;
          
        }
        private Pen pointPen;
        public Pen PointPen
        {
            get { return pointPen; }
            set { pointPen = value; }
        }

        private PointF pixelsLocation;
        public PointF PixelsLocation
        {
            get { return pixelsLocation; }
            set { pixelsLocation = value; }
        }
    }

    public class MObjData
    {
        /// <summary>
        /// object数据
        /// </summary>
        /// <param name="codeImg">要画的图像</param>
        /// <param name="selection">选择条件</param>
        /// <param name="selectIndex">选择的序号（-1表示所有）</param>
        public MObjData(ECodedImage2 codeImg,EObjectSelection selection,int selectIndex)
        {
            CodedImage = codeImg;
            ObjectSelection = selection;
            SelectIndex = selectIndex;
        }
        public int SelectIndex;
        public EObjectSelection ObjectSelection;
        public ECodedImage2 CodedImage;
    }

}
