using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    internal class ImgSourceFactory : AbstractFactory
    {
        public override BaseEvent CreateEvent(Task t)
        {
            return new ImgSourceEvent(t);
        }
    }
}
