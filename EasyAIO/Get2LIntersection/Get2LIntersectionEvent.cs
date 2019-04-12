using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public class Get2LIntersectionEvent : BaseEvent
    {
        internal Get2LIntersectionConfig config = new Get2LIntersectionConfig();
        public override BaseConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value as Get2LIntersectionConfig;
            }
        }
        public Get2LIntersectionEvent(Task task)
            : base(task)
        {
        }
        public override void Initialize()
        {
            ResultData.Name = Config.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("有无交点", new float[] { 0 });
            ResultData.ValueParams.Add("交点X坐标", new float[] { 0 });
            ResultData.ValueParams.Add("交点Y坐标", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }

        public override bool Run(bool isTaskRun)
        {
            if (config.ShapeName != null && config.ShapeName != string.Empty && config.ShapeName != "默认坐标系")
            {
                foreach (var item in ParentTask.Events)
                {
                    if (item is ShapeEvent)
                    {
                        ShapeEvent se = item as ShapeEvent;
                        if (se.Config.ToolName == config.ShapeName)
                        {
                            Shape.SetCenterXY(se.OrgX, se.OrgY);
                            Shape.Angle = se.Angle;
                            break;
                        }
                    }
                }
            }
            else
            {
                Shape.SetCenterXY(0, 0);
                Shape.Angle = 0;
            }

            float linkX1, linkY1, linkA1, linkX2, linkY2, linkA2;
            GetValueFromLinkStr(config.LinkL_CenterX1, out linkX1);
            GetValueFromLinkStr(config.LinkL_CenterY1, out linkY1);
            GetValueFromLinkStr(config.LinkL_Angle1, out linkA1);
            GetValueFromLinkStr(config.LinkL_CenterX2, out linkX2);
            GetValueFromLinkStr(config.LinkL_CenterY2, out linkY2);
            GetValueFromLinkStr(config.LinkL_Angle2, out linkA2);

            config.L_CenterX1 = linkX1;
            config.L_CenterY1 =linkY1;
            config.L_Angle1 = linkA1;
            config.L_CenterX2 =linkX2;
            config.L_CenterY2 =linkY2;
            config.L_Angle2 = linkA2;

            double k1, b1, k2, b2;
            k1 = Math.Tan(config.L_Angle1 * (Math.PI / 180));
            b1 = config.L_CenterY1 - k1 * config.L_CenterX1;
            
            k2 = Math.Tan(config.L_Angle2 * (Math.PI / 180));
            b2 = config.L_CenterY2 - k2 * config.L_CenterX2;

            HaveIntersection = k1 != k2;
            if (HaveIntersection)
            {
                double p_x ,p_y;
                if (config.L_Angle1 % 90 == 0 && config.L_Angle1 % 180 != 0)
                {
                    p_x = config.L_CenterX1;
                    p_y = k2 * config.L_CenterX1 + b2;
                }
                else if (config.L_Angle2 % 90 == 0 && config.L_Angle2 % 180 != 0)
                {
                    p_x = config.L_CenterX2;
                    p_y = k1 * config.L_CenterX2 + b1;
                }
                else
                {
                    p_x = (b1 - b2) / (k2 - k1);
                    p_y = k1 * p_x + b1;
                }

                P_Intersection = new PointF((float)p_x, (float)p_y);

                if (config.IsNormalShape)
                {
                    P_Intersection = ToolGroupEdit.GetRotatePoint(new PointF(), P_Intersection, Shape.Angle);
                    P_Intersection.X += Shape.CenterX;
                    P_Intersection.Y += Shape.CenterY;
                }
            }
            else
            {
                P_Intersection = new PointF();
            }

            ResultData.Name = config.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("有无交点", new float[] { Convert.ToSingle(HaveIntersection) });
            ResultData.ValueParams.Add("交点X坐标", new float[] { P_Intersection.X });
            ResultData.ValueParams.Add("交点Y坐标", new float[] { P_Intersection.Y });

            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }


            return true;
        }
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            Get2LIntersectionFrm frm = new Get2LIntersectionFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }
        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                if (config.IsNormalShape)
                {
                    picBox.points.Add(new PointAttribute(new Pen(Color.OrangeRed), P_Intersection));
                }
                else
                {
                    P_Intersection = ToolGroupEdit.GetRotatePoint(new PointF(), P_Intersection, Shape.Angle);
                    P_Intersection.X += Shape.CenterX;
                    P_Intersection.Y += Shape.CenterY;
                    picBox.points.Add(new PointAttribute(new Pen(Color.OrangeRed), P_Intersection));
                }
            }
        }

        internal bool HaveIntersection = false;
        internal PointF P_Intersection = new PointF();
        internal Euresys.Open_eVision_2_2.EFrameShape Shape = new Euresys.Open_eVision_2_2.EFrameShape();
    }
}
