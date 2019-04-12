using Euresys.Open_eVision_2_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public class FindCircleEvent : BaseEvent
    {
        #region 公共

        internal FindCircleConfig circleConfig = new FindCircleConfig();
        public override BaseConfig Config
        {
            get
            {
                return circleConfig;
            }
            set
            {
                circleConfig = value as FindCircleConfig;
            }
        }
        public FindCircleEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            CircleGauge = new ECircleGauge();
            CircleGauge.Rotatable = true;
            CircleGauge.Resizable = true;
            CircleGauge.Dragable = true;
            CircleGauge.SetCenterXY(0, 0);
            ResultData.Name = circleConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("中心X", new float[] { 0 });
            ResultData.ValueParams.Add("中心Y", new float[] { 0 });
            ResultData.ValueParams.Add("直径", new float[] { 0 });
            ResultData.ValueParams.Add("周长", new float[] { 0 });
            ResultData.ValueParams.Add("异常点数", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }
        public override bool Run(bool isTaskRun)
        {
            int index = circleConfig.SelectImgIndex - 1;
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

            if (circleConfig.ShapeName != null && circleConfig.ShapeName != string.Empty && circleConfig.ShapeName != "默认坐标系")
            {
                foreach (var item in ParentTask.Events)
                {
                    if (item is ShapeEvent)
                    {
                        ShapeEvent se = item as ShapeEvent;
                        if (se.Config.ToolName == circleConfig.ShapeName)
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
            CircleGauge.Name = Config.ToolName;
            CircleGauge.Attach(shape);
            CircleGauge.SetCenterXY(circleConfig.CenterX, circleConfig.CenterY);
            CircleGauge.Diameter = circleConfig.Diameter;
            CircleGauge.Amplitude = circleConfig.Amplitude;
            CircleGauge.Angle = circleConfig.Angle;
            CircleGauge.Tolerance = circleConfig.Tolerance;
            CircleGauge.Threshold = circleConfig.Threshold;
            CircleGauge.TransitionChoice = (ETransitionChoice)circleConfig.TransitionChoice;
            CircleGauge.TransitionType = (ETransitionType)circleConfig.TransitionType;
            CircleGauge.SamplingStep = circleConfig.SamplingStep;
            CircleGauge.NumFilteringPasses = circleConfig.Passes;
            CircleGauge.Measure(InputImg);
            //输出结果
            float centerX = CircleGauge.MeasuredCircle.CenterX;
            float centerY = CircleGauge.MeasuredCircle.CenterY;
            float diameter = CircleGauge.MeasuredCircle.Diameter;
            float length = CircleGauge.MeasuredCircle.ArcLength;
            float notPassNum = CircleGauge.NumSamples - CircleGauge.NumValidSamples;

            ResultData.Name = circleConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("中心X", new float[] { centerX });
            ResultData.ValueParams.Add("中心Y", new float[] { centerY });
            ResultData.ValueParams.Add("直径", new float[] { diameter });
            ResultData.ValueParams.Add("周长", new float[] { length });
            ResultData.ValueParams.Add("异常点数", new float[] { notPassNum });
            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }
            //if (!RectGauge.Valid)
            //{
            //    return false;
            //}
            //判定
            bool result = true;
            foreach (var item in circleConfig.ResultsDetermines)
            {
                if (item.Enable)
                {
                    switch (item.Name)
                    {
                        case "中心X":
                            if (centerX < item.Min || centerX > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "中心Y":
                            if (centerY < item.Min || centerY > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "直径":
                            if (diameter < item.Min || diameter > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "周长":
                            if (length < item.Min || length > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "异常点数":
                            if (notPassNum < item.Min || notPassNum > item.Max)
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
            FindCircleFrm frm = new FindCircleFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                picBox.MShapes.Add(CircleGauge);
            }
        }
        #endregion

        internal EFrameShape shape =new EFrameShape();
        internal ECircleGauge CircleGauge;

    }
}
