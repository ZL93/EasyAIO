using Euresys.Open_eVision_2_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    public class MatcherEvent : BaseEvent
    {
        #region 公共

        internal MatcherConfig matcherConfig = new MatcherConfig();
        public override BaseConfig Config
        {
            get
            {
                return matcherConfig;
            }
            set
            {
                matcherConfig = value as MatcherConfig;
            }
        }
        public MatcherEvent(Task task)
            : base(task)
        { }
        public override void Initialize()
        {
            Roi = new EROIBW8();
            Roi.SetPlacement(0, 0, 300, 300);
            Matcher = new EMatcher();
            Finder = new EPatternFinder();
            ResultData.Name = matcherConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("数量", new float[]{ 0 });
            ResultData.ValueParams.Add("中心X", new float[] { 0 });
            ResultData.ValueParams.Add("中心Y", new float[] { 0 });
            ResultData.ValueParams.Add("角度", new float[] { 0 });
            ParentTask.ResultDatas.Add(ResultData);
        }
        public override bool Run(bool isTaskRun)
        {
            int index = matcherConfig.SelectImgIndex - 1;
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
            if (matcherConfig.UseRoi)
            {
                Roi.SetPlacement(matcherConfig.Roi_OrgX, matcherConfig.Roi_OrgY, matcherConfig.Roi_Width, matcherConfig.Roi_Height);
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
            }

            if (matcherConfig.MatchType == 0)
            {
                if (!Matcher.PatternLearnt)
                {
                    if (!System.IO.File.Exists(matcherConfig.MatcherPath))
                    {
                        return false;
                    }
                    Matcher.Load(matcherConfig.MatcherPath);
                }
               
                if (matcherConfig.UseRoi)
                {
                    Matcher.Match(Roi);
                }
                else
                {
                    Matcher.Match(InputImg);
                }
                //输出结果
                count = Matcher.NumPositions;
                if (count > 0)
                {
                    centerXs = new float[count];
                    centerYs = new float[count];
                    angles = new float[count];
                    scores = new float[count];
                    if (matcherConfig.UseRoi)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            centerXs[i] = Matcher.GetPosition(i).CenterX + matcherConfig.Roi_OrgX;
                            centerYs[i] = Matcher.GetPosition(i).CenterY + matcherConfig.Roi_OrgY;
                            angles[i] = Matcher.GetPosition(i).Angle;
                            scores[i] = Matcher.GetPosition(i).Score;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            centerXs[i] = Matcher.GetPosition(i).CenterX;
                            centerYs[i] = Matcher.GetPosition(i).CenterY;
                            angles[i] = Matcher.GetPosition(i).Angle;
                            scores[i] = Matcher.GetPosition(i).Score;
                        }
                    }
                }
                else
                {
                    centerXs = new float[1];
                    centerYs = new float[1];
                    angles = new float[1];
                    scores = new float[1];
                }
            }
            else if (matcherConfig.MatchType == 1)
            {
                if (!Finder.LearningDone)
                {
                    if (!System.IO.File.Exists(matcherConfig.FinderPath))
                    {
                        return false;
                    }
                    Finder.Load(matcherConfig.FinderPath);
                }

                if (matcherConfig.UseRoi)
                {
                   FinderPatterns = Finder.Find(Roi);
                }
                else
                {
                   FinderPatterns = Finder.Find(InputImg);
                }

                //输出结果
                count = FinderPatterns.Length;
                if (count > 0)
                {
                    centerXs = new float[count];
                    centerYs = new float[count];
                    angles = new float[count];
                    scores = new float[count];
                    if (matcherConfig.UseRoi)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            centerXs[i] = FinderPatterns[i].Center.X + matcherConfig.Roi_OrgX;
                            centerYs[i] = FinderPatterns[i].Center.Y + matcherConfig.Roi_OrgY;
                            angles[i] = FinderPatterns[i].Angle;
                            scores[i] = FinderPatterns[i].Score;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            centerXs[i] = FinderPatterns[i].Center.X;
                            centerYs[i] = FinderPatterns[i].Center.Y;
                            angles[i] = FinderPatterns[i].Angle;
                            scores[i] = FinderPatterns[i].Score;
                        }
                    }
                }
                else
                {
                    centerXs = new float[1];
                    centerYs = new float[1];
                    angles = new float[1];
                    scores = new float[1];
                }
            }
            ResultData.Name = matcherConfig.ToolName;
            ResultData.ValueParams.Clear();
            ResultData.ValueParams.Add("数量", new float[] { count });
            if (count > 0)
            {
                ResultData.ValueParams.Add("中心X", centerXs);
                ResultData.ValueParams.Add("中心Y", centerYs);
                ResultData.ValueParams.Add("角度", angles);
            }
           
           
            if (isTaskRun)
            {
                ParentTask.ResultDatas.Add(ResultData);
            }
          
            //判定
            bool result = true;
            foreach (var item in matcherConfig.ResultsDetermines)
            {
                if (item.Enable)
                {
                    switch (item.Name)
                    {
                        case "中心X":
                            for (int i = 0; i < count; i++)
                            {
                                if (centerXs[i] < item.Min || centerXs[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            
                            break;
                        case "中心Y":
                            for (int i = 0; i < count; i++)
                            {
                                if (centerYs[i] < item.Min || centerYs[i] > item.Max)
                                {
                                    result = false;
                                }
                            }
                            
                            break;
                        case "数量":
                            if (count < item.Min || count > item.Max)
                            {
                                result = false;
                            }
                            break;
                        case "角度":
                            for (int i = 0; i < count; i++)
                            {
                                if (angles[i] < item.Min || angles[i] > item.Max)
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
            
            if (!result)
            {
                return false;
            }
            
            return true;
        }
        public override System.Windows.Forms.DialogResult ShowDialog()
        {
            System.Windows.Forms.DialogResult result;
            MatcherFrm frm = new MatcherFrm(this);
            frm.Text = Config.ToolName;
            result = frm.ShowDialog();
            frm.Dispose();
            return result;
        }

        public override void DrawToPicBox(MPicBox picBox)
        {
            if (Config.CanDrawToPicBox)
            {
                if (matcherConfig.MatchType == 0)
                {
                    if (Matcher != null)
                    {
                        picBox.MMatchers.Add(Matcher);
                    }
                }
                else if (matcherConfig.MatchType == 1)
                {
                    if (FinderPatterns != null)
                    {
                        picBox.FinderPatterns.Add(FinderPatterns);
                    }
                }
            }
        }
        #endregion
        protected override void Dispose( bool disposing)
        {
            if (Roi != null)
            {
                Roi.Dispose();
                Roi = null;
            }
            if (Matcher != null)
            {
                Matcher.Dispose();
                Matcher = null;
            }
            if (Finder !=null)
            {
                Finder.Dispose();
                Finder = null;
            }
            base.Dispose(disposing);
        }
       
       
        internal EROIBW8 Roi = null;
        internal EMatcher Matcher;
        internal EPatternFinder Finder;
        internal EFoundPattern[] FinderPatterns;

        internal float[] centerXs, centerYs, angles, scores;
        internal int count;
    }
}
