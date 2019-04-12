using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    /// <summary>
    /// 可删除页TabControl Build by ZL 19.01.29
    /// </summary>
    [ToolboxBitmap(typeof(TabControl))]
    internal partial class MTabControl : TabControl
    {
        const int CLOSE_SIZE = 15;

        public bool ShowCloseMsg { get; set; }

        private bool _showCloseBtn = true;

        public bool ShowCloseBtn
        {
            get { return _showCloseBtn; }
            set { _showCloseBtn = value;
            this.Invalidate();
            }
        }


        private Color color = Color.Turquoise;

        public Color SelectColor
        {
            get { return color; }
            set { color = value;
            this.Refresh();
            }
        }

        public MTabControl()
        {
            InitializeComponent();

            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.Padding = new System.Drawing.Point(CLOSE_SIZE, 5);
        }

        public event TabPageClosed PageClosed;
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //try
            {
                //获取当前Tab选项卡的绘图区域
                Rectangle myTabRect = this.GetTabRect(e.Index);
               
                //绘制标签头背景色
                using (Brush brBack = new SolidBrush(SelectColor))
                {
                    if (e.Index == this.SelectedIndex)
                    {
                        e.Graphics.FillRectangle(brBack, myTabRect); //设置当前选中的tabgage的背景色
                    }
                }
                
                //先添加TabPage属性
                e.Graphics.DrawString(this.TabPages[e.Index].Text,
                    e.Font, SystemBrushes.ControlText, myTabRect.X + 3, myTabRect.Y + 5);


                //再画一个矩形框
                using (Pen p = new Pen(Color.Transparent))
                {
                    myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                    myTabRect.Width = CLOSE_SIZE;
                    myTabRect.Height = CLOSE_SIZE;
                    e.Graphics.DrawRectangle(p, myTabRect);
                }
                //填充矩形框
                Color recColor = (e.State == DrawItemState.Selected) ? Color.Transparent : Color.Transparent;
                using (Brush b = new SolidBrush(recColor))
                {
                    e.Graphics.FillRectangle(b, myTabRect);
                }

                if (_showCloseBtn)
                {
                    //画Tab选项卡右上方关闭按钮   
                    using (Pen objpen = new Pen(Color.SlateBlue, 1.8f))
                    {
                        //自己画X
                        //"\"线
                        Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 3);
                        Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                        e.Graphics.DrawLine(objpen, p1, p2);
                        //"/"线
                        Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                        Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                        e.Graphics.DrawLine(objpen, p3, p4);


                        ////使用图片
                        //Bitmap bt = new Bitmap(image);
                        //Point p5 = new Point(myTabRect.X, 4);//获取绘图区域的开始坐标位置
                        //e.Graphics.DrawImage(bt, p5);
                    }
                }
                
                e.Graphics.Dispose();
            }
            //catch (Exception)
            //{

            //}

            base.OnDrawItem(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (DrawMode == TabDrawMode.OwnerDrawFixed)
            {
                if (SelectedTab ==null)
                {
                    return;
                }
                //if (!String.IsNullOrEmpty(this.SelectedTab.Text))
                {
                    if (e.Button == MouseButtons.Left && _showCloseBtn)
                    {
                        int x = e.X, y = e.Y;

                        //计算关闭区域      
                        Rectangle myTabRect = this.GetTabRect(this.SelectedIndex); ;

                        //myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                        myTabRect.Offset(myTabRect.Width - 0x12, 2);
                        myTabRect.Width = CLOSE_SIZE;
                        myTabRect.Height = CLOSE_SIZE;

                        //如果鼠标在区域内就关闭选项卡      
                        bool isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;
                        if (isClose == true)
                        {
                            if (ShowCloseMsg)
                            {
                              DialogResult dr = MessageBox.Show("确认关闭当前页？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                              if (dr!= DialogResult.Yes)
                              {
                                  return;
                              }
                            }
                            TabPage t = SelectedTab;
                            this.TabPages.Remove(t);
                            OnPageClosed(new ClosedEventArgs() { SelectPage = t});
                            t.Dispose();
                        }
                    }
                }
            }
            
        }

        protected void OnPageClosed(ClosedEventArgs e)
        {
            if (PageClosed != null)
            {
                PageClosed(this, e);
            }
        
        }
    }
    public delegate void TabPageClosed(object sender, ClosedEventArgs e);
    public class ClosedEventArgs : EventArgs
    {
        public TabPage SelectPage { get; set; }
    }

}
