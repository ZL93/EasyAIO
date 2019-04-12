using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    internal partial class ResultFrm : Form
    {
        List<Result> resultDatas = null;
        StringBuilder sb = new StringBuilder();
        public int Index { get; set; }
        public string SelectName { get; set; }

        public float Value { get; set; }

        public ResultFrm(List<Result> datas)
        {
            InitializeComponent();
            resultDatas = datas;
        }

        private void ResultFrm_Load(object sender, EventArgs e)
        {
            foreach (var item in resultDatas)
            {
                TreeNode tn = new TreeNode(item.Name);

                foreach (var param in item.ValueParams)
                {
                    string str = param.Key + "-->";
                    sb.Clear();
                    sb.Append("[");
                    int length =param.Value.Length;
                    for (int i = 0; i < length; i++)
                    {
                        sb.Append(param.Value[i].ToString());
                        if (i != length - 1)
                        {
                            sb.Append(",");
                        }
                    }
                    sb.Append("]");
                    str += sb.ToString();
                    TreeNode tn1 = new TreeNode(str);
                    tn1.Tag = item.Name + "." + param.Key;
                    tn.Nodes.Add(tn1);
                }
                foreach (var param in item.StrParams)
                {
                    string str = param.Key + "-->"+ param.Value;
                    TreeNode tn2 = new TreeNode(str);
                    tn2.Tag = item.Name + "." + param.Key;
                    tn.Nodes.Add(tn2);
                }
                treeView1.Nodes.Add(tn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag != null)
                {
                    SelectName = treeView1.SelectedNode.Tag.ToString();
                    Index = Convert.ToInt32(textBox1.Text);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           TreeNode tn = treeView1.GetNodeAt(Location);
           if (tn != null)
           {
               if (tn.Tag != null)
               {
                   SelectName = tn.Tag.ToString();
                   Index = Convert.ToInt32(textBox1.Text);
                   DialogResult = System.Windows.Forms.DialogResult.OK;
               }
           }
        }
    }
}
