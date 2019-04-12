using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
   public class FindLineEvent : BaseEvent
    {
        #region 公共

        internal FindLineConfig lineConfig = new FindLineConfig();
        public override BaseConfig Config
        {
            get
            {
                return lineConfig;
            }
            set
            {
                lineConfig = value as FindLineConfig;
            }
        }
        public FindLineEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            LineGauge = new ELineGauge();
            LineGauge.Rotatable = true;
            LineGauge.Resizable = true;
            LineGauge.Dragable = true;
            LineGauge.SetCenterXY(0, 0);
            LineGauge.Length = 200;
            LineGauge.Tolerance = 50;
            ResultData.Name = lineConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("中心X", new float[]{ 0 });
            ResultData.ValueParams.Add("中心Y", new float[] { 0 });
            ResultData.ValueParams.Add("角度", new float[] { 0 });
            ResultData.ValueParams.Add("异常点数", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }
        public override bool Run(bool isTaskRun)
        {
            int index = lineConfig.SelectImgIndex - 1;
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

            if (lineConfig.ShapeName != null && lineConfig.ShapeName != string.Empty && lineConfig.ShapeName != "默认坐标系")
            {
                foreach (var item in ParentTask.Events)
                {
                    if (item is ShapeEvent)
                    {
                        ShapeEvent se = item as ShapeEvent;
                        if (se.Config.ToolName == lineConfig.ShapeName)
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
            LineGauge.Name = Config.ToolName;
            LineGauge.Attach(shape);
            LineGauge.SetCenterXY(lineConfig.CenterX, lineConfig.CenterY);
            LineGauge.Length = lineConfig.Length;
            LineGauge.Angle = lineConfig.Angle;
            LineGauge.Tolerance = lineConfig.Tolerance;
            LineGauge.Threshold = lineConfig.Threshold;
            LineGauge.TransitionChoice = (ETransitionChoice)lineConfig.TransitionChoice;
            LineGauge.TransitionType = (ETransitionType)lineConfig.TransitionType;
            LineGauge.SamplingStep = lineConfig.SamplingStep;
            LineGauge.NumFilteringPasses = lineConfig.Passes;
            LineGauge.Measure(InputImg);
            //输出结果
            float centerX = LineGauge.MeasuredLine.CenterX;
            float centerY = LineGauge.MeasuredLine.CenterY;
            float angle = LineGauge.MeasuredLine.Angle;
            float notPassNum = LineGauge.NumSamples - LineGauge.NumValidSamples;

            ResultData.Name = lineConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("中心X", new float[]{ centerX });
            ResultData.ValueParams.Add("中心Y", new float[] { centerY });
            ResultData.ValueParams.Add("角度", new float[] { angle });
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
            foreach (var item in lineConfig.ResultsDetermines)
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
                        case "角度":
                            if (angle < item.Min || angle > item.Max)
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
            FindLineFrm frm = new FindLineFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                picBox.MShapes.Add(LineGauge);
            }
        }
        #endregion

        internal EFrameShape shape = new EFrameShape();
        internal ELineGauge LineGauge;
    }
}
