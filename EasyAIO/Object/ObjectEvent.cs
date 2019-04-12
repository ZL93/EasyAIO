using System;
using System.Collections.Generic;
using System.Text;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public class ObjectEvent : BaseEvent
    {
        internal ObjectConfig objConfig = new ObjectConfig();
        internal EROIBW8 Roi = null;

        private EObjectSelection ObjectSelection = new EObjectSelection();
        private ECodedImage2 CodedImage = new ECodedImage2(); // ECodedImage2 instance
        private EImageEncoder ImageEncoder = new EImageEncoder(); // EImageEncoder instance

        internal MObjData objData = null;
        public ObjectEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            objData = new MObjData(CodedImage, ObjectSelection, -1);
            Roi = new EROIBW8();
            Roi.SetPlacement(0, 0, 300, 300);
            ResultData.Name = objConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("斑点数量", new float[] { 0 });
            ResultData.ValueParams.Add("斑点面积", new float[] { 0 });
            ResultData.ValueParams.Add("斑点中心X", new float[] { 0 });
            ResultData.ValueParams.Add("斑点中心Y", new float[] { 0 });
            ResultData.ValueParams.Add("斑点重心X", new float[] { 0 });
            ResultData.ValueParams.Add("斑点重心Y", new float[] { 0 });
            ResultData.ValueParams.Add("斑点宽度", new float[] { 0 });
            ResultData.ValueParams.Add("斑点高度", new float[] { 0 });
            ResultData.ValueParams.Add("斑点角度", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }

        public int selectImg = 0;
        public override bool Run(bool isTaskRun =false)
        {
            int index = objConfig.SelectImgIndex - 1;
            if (index < 0 || index >= ParentTask.Imgs.Count)
            {
                InputImg = null;
                count = 0;
                return false;
            }
            InputImg = ParentTask.Imgs[index].Img;
            if (InputImg == null || InputImg.IsVoid)
            {
                count = 0;
                return false;
            }
            if (objConfig.WhiteEncode && !ImageEncoder.GrayscaleSingleThresholdSegmenter.WhiteLayerEncoded)
            {
                ImageEncoder.GrayscaleSingleThresholdSegmenter.BlackLayerEncoded = false;
                ImageEncoder.GrayscaleSingleThresholdSegmenter.WhiteLayerEncoded = true;
            }
            else if(!objConfig.WhiteEncode && ImageEncoder.GrayscaleSingleThresholdSegmenter.WhiteLayerEncoded)
            {
                ImageEncoder.GrayscaleSingleThresholdSegmenter.BlackLayerEncoded = true;
                ImageEncoder.GrayscaleSingleThresholdSegmenter.WhiteLayerEncoded = false;
            }
            if (objConfig.UseRoi)
            {
                Roi.Comment = "";
                if (GetValueFromLinkStr(objConfig.LinkOrgX, out linkorgx))
                {
                    objConfig.Roi_OrgX = (int)linkorgx + objConfig.LinkOffsetOrgX;
                }
                if (GetValueFromLinkStr(objConfig.LinkOrgY, out linkorgy))
                {
                    objConfig.Roi_OrgY = (int)linkorgy + objConfig.LinkOffsetOrgY;
                }
                if (GetValueFromLinkStr(objConfig.LinkWidth, out linkwidth))
                {
                    objConfig.Roi_Width = (int)linkwidth + objConfig.LinkOffsetWidth;
                }
                if (GetValueFromLinkStr(objConfig.LinkHeight, out linkheight))
                {
                    objConfig.Roi_Height = (int)linkheight + objConfig.LinkOffsetHeight;
                }
                
                Roi.SetPlacement(objConfig.Roi_OrgX, objConfig.Roi_OrgY, objConfig.Roi_Width, objConfig.Roi_Height);
                if (Roi.Width > InputImg.Width)
                {
                    Roi.Width = InputImg.Width;
                }
                if (Roi.Height > InputImg.Height)
                {
                    Roi.Height = InputImg.Height;
                }
                if (Roi.OrgX < 0)
                {
                    Roi.OrgX = 0;
                }
                if (Roi.OrgY < 0)
                {
                    Roi.OrgY = 0;
                }
                if (Roi.OrgX + Roi.Width > InputImg.Width)
                {
                    Roi.OrgX = InputImg.Width - Roi.Width;
                }
                if (Roi.OrgY + Roi.Height > InputImg.Height)
                {
                    Roi.OrgY = InputImg.Height - Roi.Height;
                }

                Roi.Attach(InputImg);
                ImageEncoder.Encode(Roi, CodedImage);
            }
            else
            {
                Roi.Comment = "NoShow";
                ImageEncoder.Encode(InputImg, CodedImage);
            }

            ObjectSelection.Clear();
            ObjectSelection.AddObjects(CodedImage);
            if (objConfig.UseRoi)
            {
                foreach (var item in objConfig.FeatureFilters)
                {
                    switch (item.Feature)
                    {
                        case 3: ObjectSelection.RemoveUsingUnsignedIntegerFeature((EFeature)item.Feature, item.Min, item.Max, EDoubleThresholdMode.Outside);
                            break;
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16: 
                        case 41:
                        ObjectSelection.RemoveUsingFloatFeature((EFeature)item.Feature, item.Min - Roi.OrgX, item.Max - Roi.OrgY, EDoubleThresholdMode.Outside);
                            break;
                        default:
                            break;
                    }
                    
                }
            }
            else
            {
                foreach (var item in objConfig.FeatureFilters)
                {
                    switch (item.Feature)
                    {
                        case 3: ObjectSelection.RemoveUsingUnsignedIntegerFeature((EFeature)item.Feature, item.Min, item.Max, EDoubleThresholdMode.Outside);
                            break;
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 41:
                        ObjectSelection.RemoveUsingFloatFeature((EFeature)item.Feature, item.Min, item.Max, EDoubleThresholdMode.Outside);
                            break;
                        default:
                            break;
                    }

                }
            }
            //排序
            if (objConfig.CanSort)
            {
                int feature = 0;
                switch (objConfig.SortIndex)
                {
                    case 0: feature = 3;
                        break;
                    case 1: feature = 11;
                        break;
                    case 2: feature = 12;
                        break;
                    case 3: feature = 41;
                        break;
                    case 4: feature = 13;
                        break;
                    case 5: feature = 14;
                        break;
                    case 6: feature = 15;
                        break;
                    case 7: feature = 16;
                        break;
                    default:
                        break;
                }
                if (objConfig.IsAscending)
                {
                    ObjectSelection.Sort((EFeature)feature, ESortDirection.Ascending);
                }
                else
                {
                    ObjectSelection.Sort((EFeature)feature, ESortDirection.Descending);
                }
            }
            //输出结果
            ResultData.Name = objConfig.ToolName;
            ResultData.ValueParams.Clear();
            count = ObjectSelection.ElementCount;
            ResultData.ValueParams.Add("斑点数量",new float[]{count});
            if (count > 0)
            {
                area = new float[count];
                centerX = new float[count];
                centerY = new float[count];
                gravityX = new float[count];
                gravityY = new float[count];
                width = new float[count];
                height = new float[count];
                angle = new float[count];
                for (int i = 0; i < count; i++)
                {
                    area[i] = ObjectSelection.GetUnsignedIntegerFeature(i, EFeature.Area);
                    centerX[i] = ObjectSelection.GetFloatFeature(i, EFeature.BoundingBoxCenterX);
                    centerY[i] = ObjectSelection.GetFloatFeature(i, EFeature.BoundingBoxCenterY);
                    gravityX[i] = ObjectSelection.GetFloatFeature(i, EFeature.GravityCenterX);
                    gravityY[i] = ObjectSelection.GetFloatFeature(i, EFeature.GravityCenterY);
                    width[i] = ObjectSelection.GetFloatFeature(i, EFeature.BoundingBoxWidth);
                    height[i] = ObjectSelection.GetFloatFeature(i, EFeature.BoundingBoxHeight);
                    angle[i] = ObjectSelection.GetFloatFeature(i, EFeature.EllipseAngle);
                    if (objConfig.UseRoi)
                    {
                        centerX[i] += objConfig.Roi_OrgX;
                        gravityX[i] += objConfig.Roi_OrgX;

                        centerY[i] += objConfig.Roi_OrgY;
                        gravityY[i] += objConfig.Roi_OrgY;
                    }
                }

                ResultData.ValueParams.Add("斑点面积", area);
                ResultData.ValueParams.Add("斑点中心X", centerX);
                ResultData.ValueParams.Add("斑点中心Y", centerY);
                ResultData.ValueParams.Add("斑点重心X", gravityX);
                ResultData.ValueParams.Add("斑点重心Y", gravityY);
                ResultData.ValueParams.Add("斑点宽度", width);
                ResultData.ValueParams.Add("斑点高度", height);
                ResultData.ValueParams.Add("斑点角度", angle);
            }
            if (isTaskRun)
            {
               ParentTask.ResultDatas.Add(ResultData);
            }
            
            //判定
            bool result = true;
            foreach (var item in objConfig.ResultsDetermines)
            {
                if (item.Enable)
                {
                    switch (item.Name)
                    {
                        case "数量":
                            if (count < item.Min||count > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "面积":
                            for (int i = 0; i < count; i++)
                            {
                                if (area[i] < item.Min || area[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            break;
                        case "重心X":
                            for (int i = 0; i < count ; i++)
                            {
                                if (gravityX[i] < item.Min || gravityX[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            break;
                        case "重心Y":
                            for (int i = 0; i < count; i++)
                            {
                                if (gravityY[i] < item.Min || gravityY[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            break;
                        case "宽度":
                            for (int i = 0; i < count; i++)
                            {
                                if (width[i] < item.Min || width[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            break;
                        case "高度":
                            for (int i = 0; i < count; i++)
                            {
                                if (height[i] < item.Min || height[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            Roi.Title = Config.ToolName;
            Roi.Date = result ? "" : "Red";
            return result;
        }

        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            ObjectFrm frm = new ObjectFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override BaseConfig Config
        {
            get
            {
                return objConfig;
            }
            set
            {
                objConfig = value as ObjectConfig;
            }
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                if (objConfig != null && objConfig.UseRoi)
                {
                    picBox.MRois.Add(Roi);
                }
                picBox.objDatas.Add(objData);
            }
        }

        internal int count;
        internal float[] area, centerX, centerY, gravityX, gravityY, width, height, angle;

        internal float linkorgx, linkorgy, linkwidth, linkheight;
    }
}
