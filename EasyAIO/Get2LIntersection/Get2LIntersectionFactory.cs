using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    internal class Get2LIntersectionFactory : AbstractFactory
    {
        public override BaseEvent CreateEvent(Task t)
        {
            return new Get2LIntersectionEvent(t);
        }
    }
}
