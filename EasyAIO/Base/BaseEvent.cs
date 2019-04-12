using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    /// <summary>
    ///  Build by ZL 19.01.29
    /// </summary>
    public abstract class BaseEvent : IDisposable
    {
        public BaseEvent(Task task)
        {
            ParentTask = task;
            ResultData = new Result();
            InputImg = new EImageBW8();
            Initialize();
        }

        public Task ParentTask { get; set; }

        public EImageBW8 InputImg { get; set; }

        public Result ResultData { get; set; }

        public abstract BaseConfig Config { get; set; }
        public abstract void Initialize();
        public abstract bool Run(bool isTaskRun);
        public abstract DialogResult ShowDialog();

        public abstract void DrawToPicBox(MPicBox picbox);

        protected bool GetValueFromLinkStr(string linkStr, out float value)
        {
            string toolName;
            string key;
            int part1, part2, valueIndex;
            float[] values;
            if (linkStr != null && linkStr != string.Empty)
            {
                part1 = linkStr.IndexOf('.');
                part2 = linkStr.IndexOf('[');
                toolName = linkStr.Substring(0, part1);
                key = linkStr.Substring(part1 + 1, part2 - part1 - 1);
                valueIndex = Convert.ToInt16(linkStr.Substring(part2 + 1, 1));
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
                        value = values[valueIndex];
                        return true;
                    }
                }
            }
            value = 0;
            return false;
        }


        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }
            
            // Free any unmanaged objects here.
            if (InputImg != null)
            {
                InputImg.Dispose();
                InputImg = null;
            }
            

            disposed = true;
        }

        ~BaseEvent()
        {
            Dispose(false);
        }
    }
}
