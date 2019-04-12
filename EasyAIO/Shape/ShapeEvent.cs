using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public class ShapeEvent : BaseEvent
    {
        #region 公共

        internal ShapeConfig shapeConfig = new ShapeConfig();
        public override BaseConfig Config
        {
            get
            {
                return shapeConfig;
            }
            set
            {
                shapeConfig = value as ShapeConfig;
            }
        }
        public ShapeEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            ResultData.Name = shapeConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("原点X", new float[] { 0 });
            ResultData.ValueParams.Add("原点Y", new float[] { 0 });
            ResultData.ValueParams.Add("角度", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);

        }
        public override bool Run(bool isTaskRun)
        {
            float linkX, linkY, linkA;
            GetValueFromLinkStr(shapeConfig.LinkCenterX,out linkX);
            GetValueFromLinkStr(shapeConfig.LinkCenterY,out linkY);
            GetValueFromLinkStr(shapeConfig.LinkAngle,out linkA);

            OrgX = shapeConfig.OrgX = linkX;
            OrgY = shapeConfig.OrgY = linkY;
            Angle = shapeConfig.Angle = linkA;
            
            //输出结果

            ResultData.Name = shapeConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("原点X", new float[] { OrgX });
            ResultData.ValueParams.Add("原点Y", new float[] { OrgY });
            ResultData.ValueParams.Add("角度", new float[] { Angle });
            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }
            return true;
        }
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            ShapeFrm frm = new ShapeFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }
        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
               
            }
        }
        #endregion
        
        public float OrgX { get; set; }
        public float OrgY { get; set; }
        public float Angle { get; set; }
    }
}
