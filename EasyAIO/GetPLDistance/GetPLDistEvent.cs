using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public class GetPLDistEvent : BaseEvent
    {
        internal GetPLDistConfig config = new GetPLDistConfig();
        public override BaseConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value as GetPLDistConfig;
            }
        }
        public GetPLDistEvent(Task task)
            : base(task)
        {
        }
        public override void Initialize()
        {
            ResultData.Name = Config.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("距离", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }

        public override bool Run(bool isTaskRun)
        {
            float linkX1, linkY1, linkX2, linkY2, linkA2;
            GetValueFromLinkStr(config.LinkP_CenterX, out linkX1);
            GetValueFromLinkStr(config.LinkP_CenterY, out linkY1);
            GetValueFromLinkStr(config.LinkL_CenterX, out linkX2);
            GetValueFromLinkStr(config.LinkL_CenterY, out linkY2);
            GetValueFromLinkStr(config.LinkL_Angle, out linkA2);

            config.P_CenterX = linkX1;
            config.P_CenterY = linkY1;
            config.L_CenterX = linkX2;
            config.L_CenterY = linkY2;
            config.L_Angle = linkA2;


            double a = 0;
            double b = 0;
            double c = 0;
            if (config.L_Angle % 180 == 0)
            {
                a = 0;
                b = -1;
                c = config.L_CenterY;
                P_Intersection = new PointF(config.P_CenterX, config.L_CenterY);
            }
            else if (config.L_Angle % 90 == 0)
            {
                a = -1;
                b = 0;
                c = config.L_CenterX;
                P_Intersection = new PointF(config.L_CenterX, config.P_CenterY);
            }
            else
            {
                a = Math.Tan(config.L_Angle * (Math.PI / 180));
                b = -1;
                c = config.L_CenterY - a * config.L_CenterX;
                double k2 = -1 / a;
                double b2 = config.P_CenterY - k2 * config.P_CenterX;
                double p_x0 = (c - b2) / (k2 - a);
                double p_y0 = a * p_x0 + c;
                P_Intersection = new PointF((float)p_x0, (float)p_y0);
            }


            Distance = (float)Math.Abs((a * config.P_CenterX + b * config.P_CenterY + c) /
                       (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2))));

            ResultData.Name = config.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("距离", new float[] { Distance });
            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }

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
            return true;
        }
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            GetPLDistFrm frm = new GetPLDistFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }
        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                PointF p1 = new PointF(config.P_CenterX, config.P_CenterY);
                p1.X += Shape.CenterX;
                p1.Y += Shape.CenterY;
                p1 = ToolGroupEdit.GetRotatePoint(new PointF(Shape.CenterX, Shape.CenterY), p1, Shape.Angle);
                PointF p2 = P_Intersection;
                p2.X += Shape.CenterX;
                p2.Y += Shape.CenterY;
                p2 = ToolGroupEdit.GetRotatePoint(new PointF(Shape.CenterX, Shape.CenterY), p2, Shape.Angle);
                picBox.lines.Add(new LineAttribute(new Pen(Color.OrangeRed), p1, p2));
                PointF p3 = new PointF((config.P_CenterX + P_Intersection.X) / 2,
                            (config.P_CenterY + P_Intersection.Y) / 2);
                p3.X += Shape.CenterX;
                p3.Y += Shape.CenterY;
                p3 = ToolGroupEdit.GetRotatePoint(new PointF(Shape.CenterX, Shape.CenterY), p3, Shape.Angle);

                picBox.strs.Add(new StrAttribute(Distance.ToString(), Color.OrangeRed, new Point((int)p3.X, (int)p3.Y)));
            }
        }


        internal Euresys.Open_eVision_2_2.EFrameShape Shape = new Euresys.Open_eVision_2_2.EFrameShape();
        
        internal float Distance = 0f;
        internal PointF P_Intersection = new PointF();
    }
}
