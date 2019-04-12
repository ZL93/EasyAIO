using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    internal class OcrFactory : AbstractFactory
    {
        public override BaseEvent CreateEvent(Task t)
        {
            return new OcrEvent(t);
        }
    }
}
