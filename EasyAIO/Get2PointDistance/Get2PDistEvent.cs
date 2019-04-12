using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public class Get2PDistEvent : BaseEvent
    {
        internal Get2PDistConfig config = new Get2PDistConfig();
        public override BaseConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value as Get2PDistConfig;
            }
        }
        public Get2PDistEvent(Task task)
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
            float linkX1, linkY1, linkX2, linkY2;
            GetValueFromLinkStr(config.LinkCenterX1, out linkX1);
            GetValueFromLinkStr(config.LinkCenterY1, out linkY1);
            GetValueFromLinkStr(config.LinkCenterX2, out linkX2);
            GetValueFromLinkStr(config.LinkCenterY2, out linkY2);

            config.CenterX1 = linkX1;
            config.CenterY1 = linkY1;
            config.CenterX2 = linkX2;
            config.CenterY2 = linkY2;

            Distance = (float)Math.Sqrt(Math.Pow(config.CenterX1 - config.CenterX2, 2) 
                        + Math.Pow(config.CenterY1 - config.CenterY2, 2));

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
            Get2PDistFrm frm = new Get2PDistFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                PointF p1 = new PointF(config.CenterX1, config.CenterY1);
                PointF p2 = new PointF(config.CenterX2, config.CenterY2);
                p1.X += Shape.CenterX;
                p1.Y += Shape.CenterY;
                p1 = ToolGroupEdit.GetRotatePoint(new PointF(Shape.CenterX, Shape.CenterY), p1, Shape.Angle);

                p2.X += Shape.CenterX;
                p2.Y += Shape.CenterY;
                p2 = ToolGroupEdit.GetRotatePoint(new PointF(Shape.CenterX, Shape.CenterY), p2, Shape.Angle);

                PointF p3 = new PointF((config.CenterX1 + config.CenterX2) / 2,
                            (config.CenterY1 + config.CenterY2) / 2);
                p3.X += Shape.CenterX;
                p3.Y += Shape.CenterY;
                p3 = ToolGroupEdit.GetRotatePoint(new PointF(Shape.CenterX, Shape.CenterY), p3, Shape.Angle);

                picBox.lines.Add(new LineAttribute(new Pen(Color.OrangeRed), p1, p2));
                picBox.strs.Add(new StrAttribute(Distance.ToString(), Color.OrangeRed, new Point((int)p3.X, (int)p3.Y)));
            }
        }

        internal Euresys.Open_eVision_2_2.EFrameShape Shape = new Euresys.Open_eVision_2_2.EFrameShape();
        
        internal float Distance = 0f;
    }
}
