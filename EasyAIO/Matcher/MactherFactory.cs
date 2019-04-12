using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    internal class MatcherFactory : AbstractFactory
    {
        public override BaseEvent CreateEvent(Task t)
        {
            return new MatcherEvent(t);
        }
    }
}
