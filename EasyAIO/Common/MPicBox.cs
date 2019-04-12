////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//              佛祖保佑       永无BUG     永不修改                  //
////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    /// <summary>
    /// OpenEvision图像框 Build by ZL 19.01.29
    /// </summary>
    [ToolboxBitmap(typeof(PictureBox))]
    public partial class MPicBox : PictureBox
    {
        EImageBW8 srcImg = null;
        /// <summary>
        /// 要显示的图像
        /// </summary>
        public new EImageBW8 Image
        {
            get {
                if (srcImg ==null && !isDesignMode)
                {
                    srcImg = new EImageBW8();
                }
                return srcImg; 
            }
            set { srcImg = value; }
        }

        private EFrameShape mFrameShape = null;
        /// <summary>
        /// 坐标系
        /// </summary>
        public EFrameShape MFrameShape
        {
            get
            {
                if (mFrameShape == null && !isDesignMode)
                {
                    mFrameShape = new EFrameShape();
                }
                return mFrameShape;
            }
            set { mFrameShape = value; }
        }

        float imgScale = 1.0f;
        /// <summary>
        /// 图像显示比例
        /// </summary>
        public float MImgScale
        {
            get { return imgScale; }
        }

        private float panX =0f;
        /// <summary>
        /// 图像X方向偏移
        /// </summary>
        public float MPanX
        {
            get { return panX; }
        }

        private float panY =0f;
        /// <summary>
        /// 图像Y方向偏移
        /// </summary>
        public float MPanY
        {
            get { return panY; }
        }

        private bool canPicMove =true;
        /// <summary>
        /// 能否移动图片
        /// </summary>
        [Description("能否移动图片"), DefaultValueAttribute(true)]
        public bool CanPicMove
        {
            get { return canPicMove; }
            set { canPicMove = value; }
        }
        private bool canPicWheel =true;
        /// <summary>
        /// 能否缩放图片
        /// </summary>
        [Description("能否缩放"), DefaultValueAttribute(true)]
        public bool CanPicWheel
        {
            get { return canPicWheel; }
            set { canPicWheel = value; }
        }

        private bool canToolMove =true;
        /// <summary>
        /// 能否移动工具
        /// </summary>
        [Description("能否移动工具"), DefaultValueAttribute(true)]
        public bool CanToolMove
        {
            get { return canToolMove; }
            set { canToolMove = value; }
        }

        private bool canRoiMove =true;
        /// <summary>
        /// 能否移动ROI框
        /// </summary>
        [Description("能否移动ROI"), DefaultValueAttribute(true)]
        public bool CanRoiMove
        {
            get { return canRoiMove; }
            set { canRoiMove = value; }
        }

        private bool canDrawObj = true;
        /// <summary>
        /// 是否画object结果
        /// </summary>
        [Description("是否画object结果"), DefaultValueAttribute(true)]
        public bool CanDrawObj
        {
            get { return canDrawObj; }
            set
            {
                canDrawObj = value;
                Refresh();
            }
        }

        private bool _showCenterCross = false;
        /// <summary>
        /// 画中心十字
        /// </summary>
        [Description("画中心十字"), DefaultValueAttribute(false)]
        public bool ShowCenterCross
        {
            get { return _showCenterCross; }
            set { _showCenterCross = value; Refresh(); }
        }

        private bool _showPropertyGrid = true;
        /// <summary>
        /// 显示属性窗口
        /// </summary>
        [Description("显示属性窗口"), DefaultValueAttribute(true)]
        public bool ShowPropertyGrid
        {
            get { return _showPropertyGrid; }
            set { _showPropertyGrid = value; Refresh(); }
        }

        private bool _showShape = false;
        /// <summary>
        /// 画坐标系
        /// </summary>
        [Description("画坐标系"), DefaultValueAttribute(false)]
        public bool ShowShape
        {
            get { return _showShape; }
            set { _showShape = value; Refresh(); }
        }

        private int point_x = 0, point_y = 0;
        /// <summary>
        /// 框工具集合（最多50个）
        /// </summary>
        public List<EROIBW8> MRois = new List<EROIBW8>();
        /// <summary>
        /// 选中roi编号
        /// </summary>
        protected int? selectRoi = null;
        private EDragHandle[] Ehandles =new EDragHandle[50];
        /// <summary>
        /// 选中shape编号
        /// </summary>
        protected int? selectIndex = null;
        /// <summary>
        /// 测量工具集合
        /// </summary>
        public List<EShape> MShapes = new List<EShape>();

        /// <summary>
        /// 匹配工具集合
        /// </summary>
        public List<EMatcher> MMatchers = new List<EMatcher>();

        public List<EOCR> MOcrs = new List<EOCR>();
        /// <summary>
        /// 自定义字符串集合
        /// </summary>
        public List<StrAttribute> strs = new List<StrAttribute>();
        /// <summary>
        /// 自定义线段集合
        /// </summary>
        public List<LineAttribute> lines = new List<LineAttribute>();
        /// <summary>
        /// 自定义点集合
        /// </summary>
        public List<PointAttribute> points = new List<PointAttribute>();
        /// <summary>
        /// Object画图需要的数据
        /// </summary>
        public List<MObjData> objDatas = new List<MObjData>();

        public List<EFoundPattern[]> FinderPatterns =new List<EFoundPattern[]>();
        /// <summary>
        /// 工具保存数据汇总
        /// </summary>
        SaveConfigGroup scg = new SaveConfigGroup();
        /// <summary>
        /// 是否处于设计器模式
        /// </summary>
        protected bool isDesignMode = true;
        
        public MPicBox()
        {
            InitializeComponent();
            isDesignMode = GetIsDesignMode();
        }
        /// <summary>  
        /// 获取当前是否处于设计器模式  
        /// </summary>  
        /// <remarks>  
        /// 在程序初始化时获取一次比较准确，若需要时获取可能由于布局嵌套导致获取不正确，如GridControl-GridView组合。  
        /// </remarks>  
        /// <returns>是否为设计器模式</returns>  
        private bool GetIsDesignMode()
        {
            return (this.GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null
               || LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        }
        /// <summary>
        /// 获取picturebox上的图像
        /// </summary>
        /// <returns></returns>
        public Bitmap GetPic()
        {
            Bitmap b =new Bitmap(this.Width,this.Height);
            this.DrawToBitmap(b, new Rectangle(this.Location, this.Size));
            return b;
        }

        /// <summary>
        /// 保存参数到.zl文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SaveConfig(string path)
        {
            if (Path.GetExtension(path).ToLower() != ".zlc")
            {
                MessageBox.Show("文件类型不正确,请使用.zlc类型");
                return false;
            }

            scg = new SaveConfigGroup();
            foreach (var item in MRois)
            {
                scg.RoiConfigs.Add(new RoiConfig()
                {
                    OrgX = item.OrgX,
                    OrgY = item.OrgY,
                    Nmae = item.Title,
                    Height = item.Height,
                    Width = item.Width,
                });
            }
            foreach (var item in MShapes)
            {
                if (item is EPointGauge)
                {
                    EPointGauge pGauge = item as EPointGauge;
                    scg.PointConfigs.Add(new PointConfig()
                    {
                        Name = pGauge.Name,
                        CenterX = pGauge.CenterX,
                        CenterY = pGauge.CenterY,
                        Angle = pGauge.ToleranceAngle,
                        Tolerance = pGauge.Tolerance,
                        Dragable = pGauge.Dragable,
                        Rotatable = pGauge.Rotatable,
                        Resizable = pGauge.Resizable,
                        TransitionType = (int)pGauge.TransitionType,
                        TransitionChoice = (int)pGauge.TransitionChoice
                    });
                }
                else if (item is ELineGauge)
                {
                    ELineGauge lGauge = item as ELineGauge;
                    scg.LineConfigs.Add(new LineConfig()
                    {
                        Name = lGauge.Name,
                        CenterX = lGauge.CenterX,
                        CenterY = lGauge.CenterY,
                        Angle = lGauge.Angle,
                        Length = lGauge.Length,
                        Tolerance = lGauge.Tolerance,
                        Dragable = lGauge.Dragable,
                        Rotatable = lGauge.Rotatable,
                        Resizable = lGauge.Resizable,
                        TransitionType = (int)lGauge.TransitionType,
                        TransitionChoice = (int)lGauge.TransitionChoice,
                        Passes = lGauge.NumFilteringPasses,
                        SamplingStep = lGauge.SamplingStep
                    });
                }
                else if (item is ECircleGauge)
                {
                    ECircleGauge cGauge = item as ECircleGauge;
                    scg.CircleConfigs.Add(new CircleConfig()
                    {
                        Name = cGauge.Name,
                        CenterX = cGauge.CenterX,
                        CenterY = cGauge.CenterY,
                        Angle = cGauge.Angle,
                        Tolerance = cGauge.Tolerance,
                        Dragable = cGauge.Dragable,
                        Rotatable = cGauge.Rotatable,
                        Resizable = cGauge.Resizable,
                        TransitionType = (int)cGauge.TransitionType,
                        TransitionChoice = (int)cGauge.TransitionChoice,
                        Passes = cGauge.NumFilteringPasses,
                        SamplingStep = cGauge.SamplingStep,
                        Amplitude = cGauge.Amplitude,
                        Diameter = cGauge.Diameter
                    });
                }
                else if (item is ERectangleGauge)
                {
                    ERectangleGauge rGauge = item as ERectangleGauge;
                    scg.RectConfigs.Add(new RectConfig()
                    {
                        Name = rGauge.Name,
                        CenterX = rGauge.CenterX,
                        CenterY = rGauge.CenterY,
                        Angle = rGauge.Angle,
                        Tolerance = rGauge.Tolerance,
                        Dragable = rGauge.Dragable,
                        Rotatable = rGauge.Rotatable,
                        Resizable = rGauge.Resizable,
                        TransitionType = (int)rGauge.TransitionType,
                        TransitionChoice = (int)rGauge.TransitionChoice,
                        Passes = rGauge.NumFilteringPasses,
                        SamplingStep = rGauge.SamplingStep,
                        Threshold = rGauge.Threshold,
                        SizeX = rGauge.SizeX,
                        SizeY = rGauge.SizeY,
                    });
                }
            }
            BinarySerializer.Serialize<SaveConfigGroup>(scg, path);
            return true;
        }
        /// <summary>
        /// 从.zl文件中导入参数
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("文件不存在");
                return false;
            }
            if (Path.GetExtension(path).ToLower() != ".zlc")
            {
                MessageBox.Show("文件类型不正确,请使用.zlc类型");
                return false;
            }
            scg = BinarySerializer.DeSerialize<SaveConfigGroup>(path);
            if (scg == null)
            {
                return false;
            }
            /*         Roi           */
            for (int i = 0; i < scg.RoiConfigs.Count; i++)
            {
                if (MRois.Count > i)
                {
                    MRois[i].Title = scg.RoiConfigs[i].Nmae;
                    MRois[i].OrgX = scg.RoiConfigs[i].OrgX;
                    MRois[i].OrgY = scg.RoiConfigs[i].OrgY;
                    MRois[i].Width = scg.RoiConfigs[i].Width;
                    MRois[i].Height = scg.RoiConfigs[i].Height;
                }
            }
            /*         Point           */
            List<EPointGauge> pointGauges = new List<EPointGauge>();
            foreach (var item in MShapes)
            {
                if (item is EPointGauge)
                {
                    EPointGauge pGauge = item as EPointGauge;
                    pointGauges.Add(pGauge);
                }
            }
            for (int i = 0; i < scg.PointConfigs.Count; i++)
            {
                if (pointGauges.Count > i)
                {
                    pointGauges[i].Name = scg.PointConfigs[i].Name;
                    pointGauges[i].Tolerance = scg.PointConfigs[i].Tolerance;
                    pointGauges[i].ToleranceAngle = scg.PointConfigs[i].Angle;
                    pointGauges[i].SetCenterXY(scg.PointConfigs[i].CenterX, scg.PointConfigs[i].CenterY);

                    pointGauges[i].Dragable = scg.PointConfigs[i].Dragable;
                    pointGauges[i].Resizable = scg.PointConfigs[i].Resizable;
                    pointGauges[i].Rotatable = scg.PointConfigs[i].Rotatable;
                    pointGauges[i].TransitionType = (ETransitionType)scg.PointConfigs[i].TransitionType;
                    pointGauges[i].TransitionChoice = (ETransitionChoice)scg.PointConfigs[i].TransitionChoice;
                    pointGauges[i].Threshold = scg.PointConfigs[i].Threshold;
                }
            }
            /*         Line           */
            List<ELineGauge> lineGauges = new List<ELineGauge>();
            foreach (var item in MShapes)
            {
                if (item is ELineGauge)
                {
                    ELineGauge lGauge = item as ELineGauge;
                    lineGauges.Add(lGauge);
                }
            }
            for (int i = 0; i < scg.LineConfigs.Count; i++)
            {
                if (lineGauges.Count > i)
                {
                    lineGauges[i].Name = scg.LineConfigs[i].Name;
                    lineGauges[i].Angle = scg.LineConfigs[i].Angle;
                    lineGauges[i].Length = scg.LineConfigs[i].Length;
                    lineGauges[i].Tolerance = scg.LineConfigs[i].Tolerance;
                    lineGauges[i].SetCenterXY(scg.LineConfigs[i].CenterX, scg.LineConfigs[i].CenterY);

                    lineGauges[i].Dragable = scg.LineConfigs[i].Dragable;
                    lineGauges[i].Resizable = scg.LineConfigs[i].Resizable;
                    lineGauges[i].Rotatable = scg.LineConfigs[i].Rotatable;
                    lineGauges[i].TransitionType = (ETransitionType)scg.LineConfigs[i].TransitionType;
                    lineGauges[i].TransitionChoice = (ETransitionChoice)scg.LineConfigs[i].TransitionChoice;
                    lineGauges[i].NumFilteringPasses = scg.LineConfigs[i].Passes;
                    lineGauges[i].SamplingStep = scg.LineConfigs[i].SamplingStep;
                    lineGauges[i].Threshold = scg.LineConfigs[i].Threshold;
                }
            }
            /*         Circle           */
            List<ECircleGauge> circleGauges = new List<ECircleGauge>();
            foreach (var item in MShapes)
            {
                if (item is ECircleGauge)
                {
                    ECircleGauge cGauge = item as ECircleGauge;
                    circleGauges.Add(cGauge);
                }
            }
            for (int i = 0; i < scg.CircleConfigs.Count; i++)
            {
                if (circleGauges.Count > i)
                {
                    circleGauges[i].Name = scg.CircleConfigs[i].Name;
                    circleGauges[i].Angle = scg.CircleConfigs[i].Angle;
                    circleGauges[i].Tolerance = scg.CircleConfigs[i].Tolerance;
                    circleGauges[i].SetCenterXY(scg.CircleConfigs[i].CenterX, scg.CircleConfigs[i].CenterY);

                    circleGauges[i].Dragable = scg.CircleConfigs[i].Dragable;
                    circleGauges[i].Resizable = scg.CircleConfigs[i].Resizable;
                    circleGauges[i].Rotatable = scg.CircleConfigs[i].Rotatable;
                    circleGauges[i].TransitionType = (ETransitionType)scg.CircleConfigs[i].TransitionType;
                    circleGauges[i].TransitionChoice = (ETransitionChoice)scg.CircleConfigs[i].TransitionChoice;
                    circleGauges[i].NumFilteringPasses = scg.CircleConfigs[i].Passes;
                    circleGauges[i].SamplingStep = scg.CircleConfigs[i].SamplingStep;
                    circleGauges[i].Diameter = scg.CircleConfigs[i].Diameter;
                    circleGauges[i].Amplitude = scg.CircleConfigs[i].Amplitude;
                    circleGauges[i].Threshold = scg.CircleConfigs[i].Threshold;
                }
            }

            /*         Rect           */
            List<ERectangleGauge> rectGauges = new List<ERectangleGauge>();
            foreach (var item in MShapes)
            {
                if (item is ERectangleGauge)
                {
                    ERectangleGauge rGauge = item as ERectangleGauge;
                    rectGauges.Add(rGauge);
                }
            }
            for (int i = 0; i < scg.RectConfigs.Count; i++)
            {
                if (rectGauges.Count > i)
                {
                    rectGauges[i].Name = scg.RectConfigs[i].Name;
                    rectGauges[i].Angle = scg.RectConfigs[i].Angle;
                    rectGauges[i].Tolerance = scg.RectConfigs[i].Tolerance;
                    rectGauges[i].SetCenterXY(scg.RectConfigs[i].CenterX, scg.RectConfigs[i].CenterY);
                    rectGauges[i].SetSize(scg.RectConfigs[i].SizeX, scg.RectConfigs[i].SizeY);
                    rectGauges[i].Dragable = scg.RectConfigs[i].Dragable;
                    rectGauges[i].Resizable = scg.RectConfigs[i].Resizable;
                    rectGauges[i].Rotatable = scg.RectConfigs[i].Rotatable;
                    rectGauges[i].TransitionType = (ETransitionType)scg.RectConfigs[i].TransitionType;
                    rectGauges[i].TransitionChoice = (ETransitionChoice)scg.RectConfigs[i].TransitionChoice;
                    rectGauges[i].NumFilteringPasses = scg.RectConfigs[i].Passes;
                    rectGauges[i].SamplingStep = scg.RectConfigs[i].SamplingStep;
                    rectGauges[i].Threshold = scg.RectConfigs[i].Threshold;

                }
            }
            Measure();
            this.Refresh();
            return true;
        }
        /// <summary>
        /// 导入图片
        /// </summary>
        /// <returns>结果</returns>
        public bool LoadImg()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.bmp)|*.bmp|(*.tif)|*.tif|(*.jpeg)|*.jpeg|(所有文件)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.Title = "导入图片";
            if (ofd.ShowDialog() == DialogResult.OK)
            { return LoadImg(ofd.FileName); }
            return false;
            
        }
        /// <summary>
        /// 导入图片
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>结果</returns>
        public bool LoadImg(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            try
            {
                Image.Load(path);
                ImgAutoSize();
                Measure();
                Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <returns></returns>
        public bool SaveImg()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "(*.bmp)|*.bmp|(*.tif)|*.tiff|(*.jpeg)|*.jpeg";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "保存图片";
            if (srcImg == null || srcImg.IsVoid)
            { return false; }
            else if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                srcImg.Save(saveFileDialog1.FileName);
            }
            return true;
        }
        /// <summary>
        /// 保存图片到指定路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public bool SaveImg(string path)
        {
            try
            {
                srcImg.Save(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 图片自动适应
        /// </summary>
        public void ImgAutoSize()
        {
            panX = panY = 0;
            if (Image.Width == 0 || Image.Height == 0)
            {
                return;
            }
            float scaleX =(float)Width / (float)Image.Width;
            float scaleY =(float)Height / (float)Image.Height;
            imgScale = scaleX < scaleY ? scaleX : scaleY;
        }
        /// <summary>
        /// 处理所有测量工具
        /// </summary>
        public void Measure()
        {
            foreach (var item in MShapes)
            {
                if (item is EPointGauge)
                {
                    EPointGauge pg = item as EPointGauge;
                    pg.Measure(srcImg);
                }
                else if (item is ELineGauge)
                {
                    ELineGauge lg = item as ELineGauge;
                    lg.Measure(srcImg);
                }
                else if (item is ECircleGauge)
                {
                    ECircleGauge cg = item as ECircleGauge;
                    cg.Measure(srcImg);
                }
                else if (item is ERectangleGauge)
                {
                    ERectangleGauge rg = item as ERectangleGauge;
                    rg.Measure(srcImg);
                }
            }
            foreach (EROIBW8 item in MRois)
            {
                item.Attach(srcImg);
            }
        }
        /// <summary>
        /// 获取鼠标位置对应图像坐标
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public PointF GetLocation(MouseEventArgs e)
        {
            return new PointF((e.X - MPanX) / MImgScale,(e.Y - MPanY) / MImgScale);
        }

        /// <summary>
        /// 获取鼠标位置灰度值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public EBW8 GetGaryValue(MouseEventArgs e)
        {
            if (Image == null || Image.IsVoid)
            {
                return new EBW8();
            }
            PointF pf = GetLocation(e);
            int x = pf.X >= Image.Width ? Image.Width - 1 : (int)pf.X;
            int y = pf.Y >= Image.Height ? Image.Height - 1 : (int)pf.Y;
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            return Image.GetPixel(x, y);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {

            if (!isDesignMode && srcImg != null)
            {

                ERGBColor Red = new ERGBColor(255, 0, 0);
                ERGBColor Green = new ERGBColor(0, 255, 0);
                ERGBColor Blue = new ERGBColor(0, 0, 255);
                ERGBColor Yellow = new ERGBColor(255, 255, 0);
                ERGBColor Pink = new ERGBColor(255, 128, 255);
                ERGBColor DarkRed = new ERGBColor(254, 67, 101);
                ERGBColor Purple = new ERGBColor(85, 26, 139);
                ERGBColor OrangeRed = new ERGBColor(255, 69, 0);
                ERGBColor RosyBrown = new ERGBColor(188, 143, 143);
                ERGBColor[] Colors = new ERGBColor[] { Purple, OrangeRed, RosyBrown, Red, Green, Blue, Yellow, Pink, DarkRed };
                MFrameShape.SetZoom(imgScale);
                MFrameShape.SetPan(panX / imgScale, panY / imgScale);
                if (srcImg != null && !srcImg.IsVoid)
                {
                    srcImg.Draw(pe.Graphics, imgScale, imgScale, panX, panY);
                }
                if (objDatas.Count > 0 && canDrawObj)
                {
                    int i = 0;
                    foreach (MObjData item in objDatas)
                    {
                        if (item.ObjectSelection != null)
                        {
                            if (item.SelectIndex >= 0 && item.ObjectSelection.ElementCount > item.SelectIndex)
                            {
                                item.CodedImage.Draw(pe.Graphics, Colors[0], item.ObjectSelection, item.SelectIndex, imgScale, imgScale, panX / imgScale, panY / imgScale);
                            }
                            else
                            {
                                for (int j = 0; j < item.ObjectSelection.ElementCount; j++)
                                {
                                    item.CodedImage.Draw(pe.Graphics, Colors[i], item.ObjectSelection, j, imgScale, imgScale, panX / imgScale, panY / imgScale);
                                    i++;
                                    if (i == Colors.Length)
                                    {
                                        i = 0;
                                    }
                                }
                            }
                            //item.CodedImage.DrawFeature(pe.Graphics, Red, EDrawableFeature.BoundingBox, item.ObjectSelection, imgScale, imgScale, panX / imgScale, panY / imgScale);
                        }
                    }
                }

                if (MRois != null)
                {
                    for (int i = 0; i < MRois.Count; i++)
                    {
                        if (!MRois[i].Comment.Contains("NoShow") && MRois[i].Parent != null)
                        {
                            if (i == selectRoi)
                            {
                                if (MRois[i].Date.Contains("Red"))
                                {
                                    MRois[i].DrawFrame(pe.Graphics, Red, true, imgScale, imgScale, panX / imgScale, panY / imgScale);
                                }
                                else
                                {
                                    MRois[i].DrawFrame(pe.Graphics, Blue, true, imgScale, imgScale, panX / imgScale, panY / imgScale);
                                }

                            }
                            else
                            {
                                if (MRois[i].Date.Contains("Red"))
                                { MRois[i].DrawFrame(pe.Graphics, Red, false, imgScale, imgScale, panX / imgScale, panY / imgScale); }
                                else
                                {
                                    MRois[i].DrawFrame(pe.Graphics, Blue, false, imgScale, imgScale, panX / imgScale, panY / imgScale);
                                }
                            }

                            pe.Graphics.DrawString(MRois[i].Title, new Font("微软雅黑", 12), Brushes.Red, (int)((MRois[i].OrgX - 5) * imgScale + panX), (int)((MRois[i].OrgY - 16) * imgScale) + panY);
                        }
                    }
                }

                if (_showShape)
                {
                    mFrameShape.SetZoom(imgScale);
                    mFrameShape.SetPan(panX / imgScale, panY / imgScale);
                    mFrameShape.Draw(pe.Graphics, OrangeRed, EDrawingMode.Nominal, false);
                    //mFrameShape.DrawCrossGrid(pe.Graphics);
                }

                foreach (EShape item in MShapes)
                {
                    if (item.Visible)
                    {
                        item.Draw(pe.Graphics, Blue);
                        item.Draw(pe.Graphics, Green, EDrawingMode.Actual);
                        if (item.ActualShape)
                        {
                            item.Draw(pe.Graphics, Green, EDrawingMode.SampledPoints);
                            item.Draw(pe.Graphics, Red, EDrawingMode.InvalidSampledPoints);
                        }
                        float fx = 0, fy = 0;
                        if (item is EPointGauge)
                        {
                            EPointGauge pGauge = item as EPointGauge;
                            EFrameShape fshape = pGauge.Mother as EFrameShape;
                            if (fshape != null)
                            {
                                fx = fshape.CenterX + pGauge.CenterX + 5;
                                fy = fshape.CenterY + pGauge.CenterY + 5;
                            }
                            else
                            {
                                fx = MFrameShape.CenterX + pGauge.CenterX + 5;
                                fy = MFrameShape.CenterY + pGauge.CenterY + 5;
                            }
                        }
                        else if (item is ELineGauge)
                        {
                            ELineGauge lGauge = item as ELineGauge;
                            EFrameShape fshape = lGauge.Mother as EFrameShape;
                            if (fshape != null)
                            {
                                fx = fshape.CenterX + lGauge.CenterX + 5;
                                fy = fshape.CenterY + lGauge.CenterY + 5;
                            }
                            else
                            {
                                fx = MFrameShape.CenterX + lGauge.CenterX + 5;
                                fy = MFrameShape.CenterY + lGauge.CenterY + 5;
                            }
                        }
                        else if (item is ECircleGauge)
                        {
                            ECircleGauge cGauge = item as ECircleGauge;
                            EFrameShape fshape = cGauge.Mother as EFrameShape;
                            if (fshape != null)
                            {
                                fx = fshape.CenterX + cGauge.CenterX + 5;
                                fy = fshape.CenterY + cGauge.CenterY + 5;
                            }
                            else
                            {
                                fx = MFrameShape.CenterX + cGauge.CenterX + 5;
                                fy = MFrameShape.CenterY + cGauge.CenterY + 5;
                            }
                        }
                        else if (item is ERectangleGauge)
                        {
                            ERectangleGauge rGauge = item as ERectangleGauge;
                            EFrameShape fshape = rGauge.Mother as EFrameShape;
                            if (fshape != null)
                            {
                                fx = fshape.CenterX + rGauge.CenterX + 5;
                                fy = fshape.CenterY + rGauge.CenterY + 5;
                            }
                            else
                            {
                                fx = MFrameShape.CenterX + rGauge.CenterX + 5;
                                fy = MFrameShape.CenterY + rGauge.CenterY + 5;
                            }
                        }

                        if (item.Mother is EFrameShape)
                        {
                            EFrameShape fShape = item.Mother as EFrameShape;
                            PointF p = RotatePoint(new PointF(fShape.CenterX, fShape.CenterY), new PointF(fx, fy), fShape.Angle);
                            fx = p.X;
                            fy = p.Y;
                        }
                        int x = (int)(fx * imgScale + panX);
                        int y = (int)(fy * imgScale + panY);
                        pe.Graphics.DrawString(item.Name, new Font("Arial", 11), Brushes.Red, x, y);
                    }
                }
                foreach (StrAttribute item in strs)
                {
                    pe.Graphics.DrawString(item.Text, new Font("微软雅黑", 11), new SolidBrush(item.StrColor), item.PixelsLocation.X * imgScale + panX, item.PixelsLocation.Y * imgScale + panY);
                }

                pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                foreach (LineAttribute item in lines)
                {
                    pe.Graphics.DrawLine(item.LinePen, item.StartPoint.X * imgScale + panX, item.StartPoint.Y * imgScale + panY, item.EndPoint.X * imgScale + panX, item.EndPoint.Y * imgScale + panY);
                }

                foreach (PointAttribute item in points)
                {
                    pe.Graphics.DrawLine(item.PointPen, item.PixelsLocation.X * imgScale + panX - 5, item.PixelsLocation.Y * imgScale + panY - 5, item.PixelsLocation.X * imgScale + panX + 5, item.PixelsLocation.Y * imgScale + panY + 5);
                    pe.Graphics.DrawLine(item.PointPen, item.PixelsLocation.X * imgScale + panX - 5, item.PixelsLocation.Y * imgScale + panY + 5, item.PixelsLocation.X * imgScale + panX + 5, item.PixelsLocation.Y * imgScale + panY - 5);
                }

                foreach (EMatcher item in MMatchers)
                {
                    for (int i = 0; i < item.NumPositions; i++)
                    {
                        item.DrawPosition(pe.Graphics, Green, i, false, imgScale, imgScale, panX / imgScale, panY / imgScale);
                    }
                }

                foreach (EOCR item in MOcrs)
                {
                    item.DrawChars(pe.Graphics, Green, imgScale, imgScale, panX / imgScale, panY / imgScale);
                }

                foreach (EFoundPattern[] item in FinderPatterns)
                {
                    if (item != null)
                    {
                        for (int i = 0; i < item.Length; i++)
                        {
                            item[i].Draw(pe.Graphics, Green, imgScale, imgScale, panX / imgScale, panY / imgScale);
                        }
                    }
                }


                if (srcImg != null && !srcImg.IsVoid)
                {
                    if (_showCenterCross)
                    {
                        pe.Graphics.DrawLine(new Pen(Color.Red, 2), panX, srcImg.Height / 2 * imgScale + panY, srcImg.Width * imgScale + panX, srcImg.Height / 2 * imgScale + panY);
                        pe.Graphics.DrawLine(new Pen(Color.Red, 2), srcImg.Width / 2 * imgScale + panX, panY, srcImg.Width / 2 * imgScale + panX, srcImg.Height * imgScale + panY);
                    }
                }
            }
            base.OnPaint(pe);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.Select();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (srcImg != null)
            {
                this.Select();
                point_x = e.X - (int)panX;
                point_y = e.Y - (int)panY;
                if (CanToolMove)
                {
                    for (int i = 0; i < MShapes.Count; i++)
                    {
                        MShapes[i].SetCursor(e.X, e.Y);
                        MShapes[i].HitTest();
                        if (MShapes[i].HitShape != null)
                        {
                            selectIndex = i;
                            Cursor = System.Windows.Forms.Cursors.Cross;
                            //select = true;
                            return;
                        }
                    }
                    if (canRoiMove)
                    {
                        int offset = 10;
                        if (MRois.Count > 0)
                        {
                            int i = 0;
                            foreach (EROIBW8 item in MRois)
                            {
                                if ((e.X - panX) > item.OrgX * imgScale - offset && (e.Y - panY) > item.OrgY * imgScale - offset && (e.X - panX) < (item.OrgX + item.Width) * imgScale + offset && (e.Y - panY) < (item.OrgY + item.Height) * imgScale + offset)
                                {
                                    //CanPicMove = false;
                                    selectRoi = i;
                                    this.Refresh();
                                    Ehandles[i] = item.HitTest((int)((e.X - panX) / imgScale), (int)((e.Y - panY) / imgScale));
                                    return;
                                }
                                i++;
                            }
                            selectRoi = null;
                        }
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (srcImg != null)
            {
                if (CanToolMove)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (selectIndex != null)
                        {
                            MShapes[(int)selectIndex].Drag(e.X, e.Y);
                            if (MShapes[(int)selectIndex] is EPointGauge)
                            {
                                EPointGauge pGauge = MShapes[(int)selectIndex] as EPointGauge;
                                pGauge.Measure(srcImg);
                            }
                            else if (MShapes[(int)selectIndex] is ELineGauge)
                            {
                                ELineGauge lGauge = MShapes[(int)selectIndex] as ELineGauge;
                                lGauge.Measure(srcImg);
                            }
                            else if (MShapes[(int)selectIndex] is ECircleGauge)
                            {
                                ECircleGauge cGauge = MShapes[(int)selectIndex] as ECircleGauge;
                                cGauge.Measure(srcImg);
                            }
                            else if (MShapes[(int)selectIndex] is ERectangleGauge)
                            {
                                ERectangleGauge rGauge = MShapes[(int)selectIndex] as ERectangleGauge;
                                rGauge.Measure(srcImg);
                            }
                            this.Refresh();
                            return;
                        }
                    }

                    if (e.Button == MouseButtons.Left)
                    {
                        for (int i = 0; i < MRois.Count; i++)
                        {
                            if (!MRois[i].Comment.Contains("NoShow"))
                            {
                                if (Ehandles[i] != EDragHandle.NoHandle)
                                {
                                    MRois[i].Drag(Ehandles[i], (int)((e.X - panX) / imgScale), (int)((e.Y - panY) / imgScale));
                                    //刷新显示
                                    this.Refresh();
                                    return;
                                }
                            }
                        }

                    }
                    for (int i = 0; i < MShapes.Count; i++)
                    {
                        if (MShapes[i].Visible)
                        {
                            MShapes[i].SetCursor(e.X, e.Y);
                            MShapes[i].HitTest();
                            if (MShapes[i].HitShape != null)
                            {
                                selectIndex = i;
                                Cursor = System.Windows.Forms.Cursors.Cross;
                                return;
                            }
                            else
                            {
                                selectIndex = null;
                                Cursor = System.Windows.Forms.Cursors.Arrow;
                            }
                        }
                    }
                }

                if (e.Button == MouseButtons.Left && CanPicMove)
                {
                    if (panX == 0 && panY == 0 && point_x == 0 && point_y == 0)
                    {
                        return;
                    }
                    panX = e.X - point_x;
                    panY = e.Y - point_y;
                    this.Refresh();
                    return;
                }
                if (CanToolMove && canRoiMove)
                {
                    int offset = 5;
                    for (int i = 0; i < MRois.Count; i++)
                    {
                        if (!MRois[i].Comment.Contains("NoShow"))
                        {
                            if (e.X - panX > MRois[i].OrgX * imgScale - offset && e.Y - panY > MRois[i].OrgY * imgScale - offset && e.X - panX < (MRois[i].OrgX + MRois[i].Width) * imgScale + offset && e.Y - panY < (MRois[i].OrgY + MRois[i].Height) * imgScale + offset)
                            {
                                Ehandles[i] = MRois[i].HitTest((int)((e.X - panX) / imgScale), (int)((e.Y - panY) / imgScale));
                                switch (Ehandles[i])
                                {
                                    case EDragHandle.North:
                                        Cursor = Cursors.SizeNS;
                                        break;
                                    case EDragHandle.South:
                                        Cursor = Cursors.SizeNS;
                                        break;
                                    case EDragHandle.West:
                                        Cursor = Cursors.SizeWE;
                                        break;
                                    case EDragHandle.East:
                                        Cursor = Cursors.SizeWE;
                                        break;
                                    case EDragHandle.NorthEast:
                                        Cursor = Cursors.SizeNESW;
                                        break;
                                    case EDragHandle.SouthWest:
                                        Cursor = Cursors.SizeNESW;
                                        break;
                                    case EDragHandle.NorthWest:
                                        Cursor = Cursors.SizeNWSE;
                                        break;
                                    case EDragHandle.SouthEast:
                                        Cursor = Cursors.SizeNWSE;
                                        break;
                                    case EDragHandle.Inside:
                                        Cursor = Cursors.SizeAll;
                                        break;
                                    case EDragHandle.NoHandle:
                                        Cursor = Cursors.Arrow;
                                        break;
                                }
                                if (Ehandles[i] != EDragHandle.NoHandle)
                                {
                                    selectRoi = i;
                                }
                                return;
                            }
                            else
                            {
                                selectRoi = null;
                                Ehandles[i] = EDragHandle.NoHandle;
                            }
                        }
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (CanToolMove && srcImg != null)
            {
                for (int i = 0; i < Ehandles.Length; i++)
                {
                    Ehandles[i] = EDragHandle.NoHandle;
                }
                if (canRoiMove)
                {
                    foreach (EROIBW8 item in MRois)
                    {
                        if (item.Width > srcImg.Width)
                        {
                            item.Width = srcImg.Width;
                        }
                        if (item.Height > srcImg.Height)
                        {
                            item.Height = srcImg.Height;
                        }
                        if (item.OrgX < 0)
                        {
                            item.OrgX = 0;
                        }
                        if (item.OrgY < 0)
                        {
                            item.OrgY = 0;
                        }
                        if (item.OrgX + item.Width > srcImg.Width)
                        {
                            item.OrgX = srcImg.Width - item.Width;
                        }
                        if (item.OrgY + item.Height > srcImg.Height)
                        {
                            item.OrgY = srcImg.Height - item.Height;
                        }
                        this.Refresh();
                    }
                }
            }
            Cursor = Cursors.Default;
            if (srcImg != null)
            {
                if (-panX > srcImg.Width * imgScale || -panY > srcImg.Height * imgScale
                    || panX > this.Width || panY > this.Height)
                {
                    panX = 0;
                    panY = 0;
                    this.Refresh();
                }
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (srcImg != null)
            {
                if (canPicWheel)
                {
                    float old_X = (e.X - panX) / imgScale;//原鼠标位置像素点
                    float old_Y = (e.Y - panY) / imgScale;
                    imgScale = imgScale * (float)(1 + (float)e.Delta / 500);
                    if (imgScale < 0.1F)
                    { imgScale = 0.1F; }
                    if (imgScale > 10F)
                    { imgScale = 10F; }
                    if (canPicMove)
                    {
                        panX += ((e.X - panX) / imgScale - old_X) * imgScale;
                        panY += ((e.Y - panY) / imgScale - old_Y) * imgScale;
                    }
                    this.Refresh();
                }
            }
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (ShowPropertyGrid)
            {
                if (selectIndex != null)
                {
                    using (Form f = new Form())
                    {
                        //f.Text = "属性";
                        f.Size = new System.Drawing.Size(350, 600);
                        Button btn = new Button();
                        btn.Click += delegate { f.Close(); };
                        f.CancelButton = btn;
                        PropertyGrid pg = new PropertyGrid();
                        pg.Dock = DockStyle.Fill;
                        f.Controls.Add(pg);
                        pg.SelectedObject = MShapes[(int)selectIndex];
                        f.Text = MShapes[(int)selectIndex].Name + "属性";
                        pg.PropertyValueChanged += delegate
                        {
                            if (MShapes[(int)selectIndex] is EPointGauge)
                            {
                                EPointGauge pGauge = MShapes[(int)selectIndex] as EPointGauge;
                                pGauge.Measure(Image);
                            }
                            else if (MShapes[(int)selectIndex] is ELineGauge)
                            {
                                ELineGauge lGauge = MShapes[(int)selectIndex] as ELineGauge;
                                lGauge.Measure(Image);
                            }
                            else if (MShapes[(int)selectIndex] is ECircleGauge)
                            {
                                ECircleGauge cGauge = MShapes[(int)selectIndex] as ECircleGauge;
                                cGauge.Measure(Image);
                            }
                            else if (MShapes[(int)selectIndex] is ERectangleGauge)
                            {
                                ERectangleGauge rGauge = MShapes[(int)selectIndex] as ERectangleGauge;
                                rGauge.Measure(Image);
                            }
                            this.Refresh();
                        };
                        pg.HelpVisible = false;
                        f.ShowDialog();
                        pg.Dispose();
                        btn.Dispose();
                    }
                }
                else if (selectRoi != null)
                {
                    using (Form f = new Form())
                    {
                        //f.Text = "属性";
                        f.Size = new System.Drawing.Size(350, 600);
                        Button btn = new Button();
                        btn.Click += delegate { f.Close(); };
                        f.CancelButton = btn;
                        PropertyGrid pg = new PropertyGrid();
                        pg.Dock = DockStyle.Fill;
                        f.Controls.Add(pg);
                        pg.SelectedObject = MRois[(int)selectRoi];
                        f.Text = MRois[(int)selectRoi].Title + "属性";
                        pg.PropertyValueChanged += delegate { this.Refresh(); };
                        pg.HelpVisible = false;
                        f.ShowDialog();
                        pg.Dispose();
                        btn.Dispose();
                    }
                }
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                    OnMouseDoubleClick(new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 2, 0, 0, 0));
                    break;
                default:
                    break;
            }
            base.OnKeyPress(e);
        }

        private PointF RotatePoint(PointF p1, PointF p2, float A)//p1是绕心，p2是待转点
        {
            //假设对图片上任意点(x1,y1),绕一个坐标点(x0,y0)逆时针旋转A角度后的新的坐标设为(x2,y2),有公式：
            //x2=(x1-x0)*cos(A)-(y1-y0)*sin(A)+x0
            //y2=(x1-x0)*sin(A)+(y1-y0)*cos(A)+y0
            A = (float)(A * 3.1415926 / 180);
            PointF p = new PointF();
            p.X = (float)((p2.X - p1.X) * Math.Cos(A) - (p2.Y - p1.Y) * Math.Sin(A) + p1.X);
            p.Y = (float)((p2.X - p1.X) * Math.Sin(A) + (p2.Y - p1.Y) * Math.Cos(A) + p1.Y);
            return p;
        }
    }
}
