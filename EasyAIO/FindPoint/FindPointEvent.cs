using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
   public class FindPointEvent : BaseEvent
    {
        #region 公共

       internal FindPointConfig pointConfig = new FindPointConfig();
        public override BaseConfig Config
        {
            get
            {
                return pointConfig;
            }
            set
            {
                pointConfig = value as FindPointConfig;
            }
        }
        public FindPointEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            PointGauge = new EPointGauge();
            PointGauge.Rotatable = true;
            PointGauge.Resizable = true;
            PointGauge.Dragable = true;
            PointGauge.SetCenterXY(0, 0);
            PointGauge.Tolerance = 100;
            ResultData.Name = pointConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("点个数", new float[] { 0 });
            ResultData.ValueParams.Add("X坐标", new float[]{ 0 });
            ResultData.ValueParams.Add("Y坐标", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }
        public override bool Run(bool isTaskRun)
        {
            int index = pointConfig.SelectImgIndex - 1;
            if (index < 0 || index >= ParentTask.Imgs.Count)
            {
                InputImg = null;
                return false;
            }
            InputImg = ParentTask.Imgs[index].Img;
            if (InputImg == null || InputImg.IsVoid)
            {
                return false;
            }

            if (pointConfig.ShapeName != null && pointConfig.ShapeName != string.Empty && pointConfig.ShapeName != "默认坐标系")
            {
                foreach (var item in ParentTask.Events)
                {
                    if (item is ShapeEvent)
                    {
                        ShapeEvent se = item as ShapeEvent;
                        if (se.Config.ToolName == pointConfig.ShapeName)
                        {
                            shape.SetCenterXY(se.OrgX, se.OrgY);
                            shape.Angle = se.Angle;
                            break;
                        }
                    }
                }
            }
            else
            {
                shape.SetCenterXY(0,0);
                shape.Angle = 0;
            }
            PointGauge.Name = Config.ToolName;
            PointGauge.Attach(shape);
            PointGauge.SetCenterXY(pointConfig.CenterX, pointConfig.CenterY);
           
            PointGauge.Tolerance = pointConfig.Tolerance;
            PointGauge.ToleranceAngle = pointConfig.Angle;
            PointGauge.Threshold = pointConfig.Threshold;
            PointGauge.TransitionChoice = (ETransitionChoice)pointConfig.TransitionChoice;
            PointGauge.TransitionType = (ETransitionType)pointConfig.TransitionType;
         
            PointGauge.Measure(InputImg);
            //输出结果
            if (PointGauge.Valid)
            {
                numPoints = 1;
            }
            else
            {
                numPoints = 0;
            }
            EPoint p = PointGauge.Shape;
            centerX = p.X;
            centerY = p.Y;
            ResultData.Name = pointConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("点个数", new float[] { numPoints });
            ResultData.ValueParams.Add("X坐标", new float[] { centerX });
            ResultData.ValueParams.Add("Y坐标", new float[] { centerY });
            
            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }
           
            //判定
            bool result = true;
            foreach (var item in pointConfig.ResultsDetermines)
            {
                if (item.Enable)
                {
                    switch (item.Name)
                    {
                        case "点个数":
                            if (numPoints < item.Min || numPoints > item.Max)
                            {
                                result = false;
                            }
                            break;
                        
                        default:
                            break;
                    }
                }
            }
            if (!result)
            {
                return false;
            }
            return true;
        }
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            FindPointFrm frm = new FindPointFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                picBox.MShapes.Add(PointGauge);
            }
        }
        #endregion

        internal EFrameShape shape = new EFrameShape();
        internal EPointGauge PointGauge;
        internal int numPoints;
        internal float centerX = 0f;
        internal float centerY = 0f;
    }
}
