using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Euresys.Open_eVision_2_2;

namespace EasyAIO
{
    public delegate void Paint(PaintEventArgs e);
    public partial class PicBoxList : UserControl
    {
        private int selectImg;
        public int SelectImg
        {
            get { return selectImg; }
            set
            {
                if (selectImg >= 0 && selectImg != value)
                {
                    selectImg = value;
                    mPicBoxMain.Image = Imgs[selectImg];
                    mPicBoxMain.ImgAutoSize();
                    mPicBoxMain.Refresh();
                }
            }
        }
        public Paint PicBoxPaint = null;

        private EImageBW8[] Imgs;
        public PicBoxList()
        {
            InitializeComponent();
        }
        public void Initialize(EImageBW8[] imgs)
        {
            Imgs = imgs;
            if (Imgs != null && Imgs.Length > 0)
            {
                for (int i = 0; i < imgs.Length; i++)
                {
                    MPicBox picbox = new MPicBox();
                    picbox.Size = new Size(160, 120);
                    picbox.Image = imgs[i];
                    picbox.ImgAutoSize();
                    picbox.CanPicMove = false;
                    picbox.CanPicWheel = false;
                    picbox.Tag = i;
                    picbox.DoubleClick += picbox_DoubleClick;
                    flowLayoutPanel1.Controls.Add(picbox);
                }
            }
        }
        void picbox_DoubleClick(object sender, EventArgs e)
        {
            MPicBox picbox = sender as MPicBox;
            selectImg = Convert.ToInt32(picbox.Tag);
            mPicBoxMain.Image = picbox.Image;
            mPicBoxMain.ImgAutoSize();
            mPicBoxMain.Refresh();
        }

        private void mPicBoxMain_Paint(object sender, PaintEventArgs e)
        {
            if (PicBoxPaint!= null)
            {
                PicBoxPaint(e);
            }
        }
    }
}
