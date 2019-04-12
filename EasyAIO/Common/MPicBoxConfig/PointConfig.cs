using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyAIO
{
    /*
    <Tolerance>78.19</Tolerance>
    <Angle>90.00</Angle>
    <Length>18.07</Length>
    <CenterX>971.10</CenterX>
    <CenterY>484.76</CenterY>
     */
    [Serializable]
    internal class PointConfig
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
        public int Threshold { get; set; }

    }
}
