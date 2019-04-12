////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//              佛祖保佑       永无BUG     永不修改                  //
////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    /// <summary>
    /// 图像处理任务类 Build by ZL 19.01.29
    /// </summary>
    public class Task :IDisposable
    {
        public delegate void StepStatus(int index,bool result);
        public StepStatus GetStepStatus;
        public delegate void RunStatus(List <BaseEvent> events, float ct);
        public RunStatus GetResultStatus;
        public Action ReadyProcess;
        internal List<ImgDictionary> Imgs = new List<ImgDictionary>();
        internal List<BaseEvent> Events = new List<BaseEvent>();
        public List<Result> ResultDatas = new List<Result>();
        public List<ICamera> Cameras = new List<ICamera>();
             
        internal ConfigGroup configGroup = new ConfigGroup();
        internal BaseEvent CreateEvent(string type, int index)
        {
            BaseEvent baseEvent = null;
            Assembly ass = null;
            AbstractFactory factory = null;
            try
            {

                int length = type.IndexOf('.');
                if (length > 0)
                {
                    string assemblyName = type.Substring(0, length);
                    ass = Assembly.Load(assemblyName);

                }
                if (ass != null)
                {
                    factory = (AbstractFactory)ass.CreateInstance(type);
                    if (factory != null)
                    {
                        baseEvent = factory.CreateEvent(this);
                        if (baseEvent != null)
                        {
                            Events.Insert(index, baseEvent);
                            configGroup.CfgGroup.Insert(index, baseEvent.Config);

                            if (baseEvent is ICamera)
                            {
                                ICamera cam = baseEvent as ICamera;
                                Cameras.Add(cam);
                            }
                        }
                        else
                        {
                            Console.WriteLine("加载" + type + "出错！");

                        }
                    }
                    else
                    {
                        Console.WriteLine("加载" + type + "工厂出错！");

                    }
                }
                else
                {
                    Console.WriteLine("加载程序集出错！");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("加载" + type + "出错！\r\n" + e.Message);
            }
            return baseEvent;
        }
        internal BaseEvent CreateEvent(BaseConfig config)
        {
            BaseEvent baseEvent = null;
            Assembly ass = null;
            AbstractFactory factory = null;
            try
            {
                string assemblyName = config.FactoryTypeName.Substring(0, config.FactoryTypeName.IndexOf('.'));
                ass = Assembly.Load(assemblyName);

                if (ass != null)
                {
                    factory = (AbstractFactory)ass.CreateInstance(config.FactoryTypeName);
                    if (factory != null)
                    {
                        baseEvent = factory.CreateEvent(this);
                        if (baseEvent != null)
                        {
                            baseEvent.Config = config;
                            Events.Add(baseEvent);

                            if (baseEvent is ICamera)
                            {
                                ICamera cam = baseEvent as ICamera;
                                Cameras.Add(cam);
                            }
                        }
                        else
                        {
                            Console.WriteLine("加载" + config.ToolName + "出错！");

                        }
                    }
                    else
                    {
                        Console.WriteLine("加载" + config.ToolName + "工厂出错！");

                    }
                }
                else
                {
                    Console.WriteLine("加载程序集出错！");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("加载" + config.FactoryTypeName + "出错！\r\n" + e.Message);
            }
            return baseEvent;
        }
        internal bool DeleteEvent(int index)
        {
            return true;
        }
        internal bool CreateEvents()
        {
            foreach (BaseConfig item in configGroup.CfgGroup)
            {
                CreateEvent(item);
            }
            return true;
        }
        /// <summary>
        /// 获取结果数据
        /// </summary>
        /// <param name="name">数据名</param>
        /// <returns>数据</returns>
        internal Result GetResultData(string name)
        {
            return ResultDatas.Find((s) =>
            {
                if (s.Name == name)
                {
                    return true;
                }
                return false;
            });
        }
        /// <summary>
        /// 获取结果数据
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>数据</returns>
        internal Result GetResultData(int index)
        {
            if (index < 0 || index >= ResultDatas.Count)
            {
                return null;
            }
            return ResultDatas[index];
        }

        public string Name { get; set; }
        /// <summary>
        /// 输入图像
        /// </summary>
        public EImageBW8 SourceImg { get; set; }
        /// <summary>
        /// 图像处理
        /// </summary>
        /// <returns>结果</returns>
        public bool ProcessImg()
        {
            bool result = true;
            List<EImageBW8> copyImgs = new List<EImageBW8>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (ReadyProcess != null)
            {
                ReadyProcess();
            }
            //将所有图片添加到CopyImgs集合内
            foreach (var item in Imgs)
            {
                if (item.Img != null)
                {
                    copyImgs.Add(item.Img);
                }
            }
            Imgs.Clear();
            ResultDatas.Clear();
            for (int i = 0; i < Events.Count; i++)
            {
                if (Events[i] == null || !Events[i].Run(true))
                {
                    result = false;
                    if (GetStepStatus != null)
                    {
                        GetStepStatus(i, false);
                    }
                }
                else
                {
                    if (GetStepStatus != null)
                    {
                        GetStepStatus(i, true);
                    }
                }
            }
            sw.Stop();
            if (GetResultStatus != null)
            {
                GetResultStatus(Events, (float)sw.ElapsedMilliseconds);
            }
            //在上一张图片被替换掉后 释放上一张图片
            foreach (var item in copyImgs)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }
            return result;
        }

        public bool ShowResultFrm(ref string strLink)
        {
            ResultFrm rf = new ResultFrm(ResultDatas);
            if (rf.ShowDialog() == DialogResult.OK)
            {
                string link;
                if (rf.SelectName == "...")
                {
                    link = rf.SelectName;
                }
                else
                {
                    link = rf.SelectName + "[" + rf.Index + "]";
                }
                strLink = link;
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            foreach (var item in Cameras)
            {
                item.Disconnect();
            }

            foreach (var item in Events)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }
        }
    }
}
