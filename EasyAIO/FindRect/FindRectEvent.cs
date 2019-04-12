using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public class FindRectEvent:BaseEvent
    {
        #region 公共
        
        internal FindRectConfig rectConfig = new FindRectConfig();
        public override BaseConfig Config
        {
            get
            {
                return rectConfig;
            }
            set
            {
                rectConfig = value as FindRectConfig;
            }
        }
        public FindRectEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            RectGauge = new ERectangleGauge();
            RectGauge.Rotatable = true;
            RectGauge.Resizable = true;
            RectGauge.Dragable = true;
            RectGauge.SetCenterXY(0, 0);
            ResultData.Name = rectConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("中心X", new float[] { 0 });
            ResultData.ValueParams.Add("中心Y", new float[]{0});
            ResultData.ValueParams.Add("角度", new float[] { 0 });
            ResultData.ValueParams.Add("宽度", new float[] { 0 });
            ResultData.ValueParams.Add("高度", new float[] { 0 });
            ResultData.ValueParams.Add("异常点数", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }
        public override bool Run(bool isTaskRun)
        {
            int index = rectConfig.SelectImgIndex - 1;
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
            if (rectConfig.ShapeName != null && rectConfig.ShapeName != string.Empty && rectConfig.ShapeName != "默认坐标系")
            {
                foreach (var item in ParentTask.Events)
                {
                    if (item is ShapeEvent)
                    {
                        ShapeEvent se = item as ShapeEvent;
                        if (se.Config.ToolName == rectConfig.ShapeName)
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
                shape.SetCenterXY(0, 0);
                shape.Angle = 0;
            }
            RectGauge.Name = Config.ToolName;
            RectGauge.Attach(shape);
            string toolName;
            string key;
            int part1, part2, valueIndex;
            float[] values;
            if (rectConfig.LinkCenterX != null && rectConfig.LinkCenterX != string.Empty)
            {
                part1 = rectConfig.LinkCenterX.IndexOf('.');
                part2 = rectConfig.LinkCenterX.IndexOf('[');
                toolName = rectConfig.LinkCenterX.Substring(0, part1);
                key = rectConfig.LinkCenterX.Substring(part1 + 1, part2 - part1 - 1);
                valueIndex = Convert.ToInt16(rectConfig.LinkCenterX.Substring(part2 + 1, 1));
                Result data = ParentTask.ResultDatas.Find((m) =>
                {
                    if (m.Name == toolName)
                    {
                        return true;
                    }
                    return false;
                });
                if (data != null)
                {
                    data.ValueParams.TryGetValue(key, out values);

                    if (values != null && values.Length > valueIndex)
                    {
                        linkcenterx = values[valueIndex];
                        rectConfig.CenterX = linkcenterx + rectConfig.LinkOffsetCenterX;
                    }
                }
            }

            if (rectConfig.LinkCenterY != null && rectConfig.LinkCenterY != string.Empty)
            {
                part1 = rectConfig.LinkCenterY.IndexOf('.');
                part2 = rectConfig.LinkCenterY.IndexOf('[');
                toolName = rectConfig.LinkCenterY.Substring(0, part1);
                key = rectConfig.LinkCenterY.Substring(part1 + 1, part2 - part1 - 1);
                valueIndex = Convert.ToInt16(rectConfig.LinkCenterY.Substring(part2 + 1, 1));
                Result data = ParentTask.ResultDatas.Find((m) =>
                {
                    if (m.Name == toolName)
                    {
                        return true;
                    }
                    return false;
                });
                if (data != null)
                {
                    data.ValueParams.TryGetValue(key, out values);

                    if (values != null && values.Length > valueIndex)
                    {
                        linkcentery = values[valueIndex];
                        rectConfig.CenterY = linkcentery + rectConfig.LinkOffsetCenterY;
                    }
                }
            }

            if (rectConfig.LinkAngle != null && rectConfig.LinkAngle != string.Empty)
            {
                part1 = rectConfig.LinkAngle.IndexOf('.');
                part2 = rectConfig.LinkAngle.IndexOf('[');
                toolName = rectConfig.LinkAngle.Substring(0, part1);
                key = rectConfig.LinkAngle.Substring(part1 + 1, part2 - part1 - 1);
                valueIndex = Convert.ToInt16(rectConfig.LinkAngle.Substring(part2 + 1, 1));
                Result data = ParentTask.ResultDatas.Find((m) =>
                {
                    if (m.Name == toolName)
                    {
                        return true;
                    }
                    return false;
                });
                if (data != null)
                {
                    data.ValueParams.TryGetValue(key, out values);

                    if (values != null && values.Length > valueIndex)
                    {
                        linkangle = values[valueIndex];
                        rectConfig.Angle = linkangle + rectConfig.LinkOffsetAngle;
                    }
                }
            }

            RectGauge.SetCenterXY(rectConfig.CenterX, rectConfig.CenterY);
            RectGauge.SetSize(rectConfig.SizeX, rectConfig.SizeY);
            RectGauge.Angle = rectConfig.Angle;
            RectGauge.Tolerance = rectConfig.Tolerance;

            RectGauge.Threshold = rectConfig.Threshold;
            RectGauge.TransitionChoice = (ETransitionChoice)rectConfig.TransitionChoice;
            RectGauge.TransitionType = (ETransitionType)rectConfig.TransitionType;
            RectGauge.SamplingStep = rectConfig.SamplingStep;
            RectGauge.NumFilteringPasses = rectConfig.Passes;


            RectGauge.Measure(InputImg);
            //输出结果
            float centerX = RectGauge.MeasuredRectangle.CenterX;
            float centerY = RectGauge.MeasuredRectangle.CenterY;
            float angle = RectGauge.MeasuredRectangle.Angle;
            float width = RectGauge.MeasuredRectangle.SizeX;
            float height = RectGauge.MeasuredRectangle.SizeY;
            float notPassNum = RectGauge.NumSamples - RectGauge.NumValidSamples;
           
            ResultData.Name = rectConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("中心X",new float[]{centerX});
            ResultData.ValueParams.Add("中心Y", new float[]{centerY});
            ResultData.ValueParams.Add("角度", new float[]{ angle});
            ResultData.ValueParams.Add("宽度",  new float[]{width});
            ResultData.ValueParams.Add("高度", new float[]{height});
            ResultData.ValueParams.Add("异常点数",new float[]{notPassNum});
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
            foreach (var item in rectConfig.ResultsDetermines)
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
                        case "宽度":
                            if (width < item.Min || width > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "高度":
                            if (height < item.Min || height > item.Max)
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
            FindRectFrm frm = new FindRectFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                picBox.MShapes.Add(RectGauge);
            }
        }
        #endregion
        internal EFrameShape shape = new EFrameShape();
        internal ERectangleGauge RectGauge;
        internal float linkcenterx, linkcentery, linkangle;
    }
}
