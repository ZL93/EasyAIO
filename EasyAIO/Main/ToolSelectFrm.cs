using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    public partial class ToolSelectFrm : Form
    {
        private ToolSelectFrm(List<ToolFactory> names)
        {
            InitializeComponent();
            foreach (var item in names)
            {
                treeView1.Nodes.Add(new TreeNode(item.ToolName) { Tag = item.FactoryName });
            }
        }

        private static ToolSelectFrm ts;
        public static ToolSelectFrm CreateToolSelect(List<ToolFactory> names)
        {
            if (ts==null||ts.IsDisposed)
            {
                ts = new ToolSelectFrm(names);
            }
            ts.DoubleClick = null;
            return ts;
        }

       
        private void ToolSelectFrm_Load(object sender, EventArgs e)
        {
            treeView1.ItemDrag += treeView1_ItemDrag;
        }

        void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode t = e.Item as TreeNode;
            treeView1.DoDragDrop(t.Tag, DragDropEffects.Copy);
        }

        public new event DoubleClick DoubleClick;

        protected void OnDoubleClick(DoubleClickEventArgs e)
        {
            if (DoubleClick != null)
            {
                DoubleClick(this, e);
            }
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode tn = treeView1.GetNodeAt(e.X, e.Y);
            if (tn!= null)
            {
                OnDoubleClick(new DoubleClickEventArgs() { Type = tn.Tag.ToString() });
            }
        }

    }

    public delegate void DoubleClick(object sender, DoubleClickEventArgs e);
    public class DoubleClickEventArgs : EventArgs
    {
        public string Type { get; set; }
    }
}
