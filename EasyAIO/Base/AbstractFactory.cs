using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    /// <summary>
    /// 抽象工厂类 Build by ZL 19.01.29
    /// </summary>
    public abstract class AbstractFactory
    {
        public abstract BaseEvent CreateEvent(Task t);
    }
}
