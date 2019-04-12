using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    /*
     <Tolerance>47.81</Tolerance>
    <Angle>-180.00</Angle>
    <CenterX>615.03</CenterX>
    <CenterY>495.61</CenterY>
     */
    [Serializable]
    internal class CircleConfig
    {
        public string Name { get; set; }
        public float CenterX { get; set; }
        public float CenterY { get; set; }
        public float Angle { get; set; }
        public float Tolerance { get; set; }

        public bool Dragable { get; set; }
        public bool Rotatable { get; set; }
        public bool Resizable { get; set; }
        public int TransitionType { get; set; }
        public int TransitionChoice { get; set; }
        public int Passes { get; set; }
        public float SamplingStep { get; set; }
        public float Diameter { get; set; }
        public float Amplitude { get; set; }
        public int Threshold { get; set; }
    }
}
