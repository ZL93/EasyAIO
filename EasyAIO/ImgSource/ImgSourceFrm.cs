using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EasyAIO
{
    internal partial class ImgSourceFrm : Form
    {
        ImgSourceEvent imgEvent;
        internal ImgSourceFrm(ImgSourceEvent imgevent)
        {
            InitializeComponent();
            imgEvent = imgevent;
        }
        private void ImgSourceFrm_Load(object sender, EventArgs e)
        {
            foreach (var item in imgEvent.ParentTask.Cameras)
            {
                comboBox1.Items.Add(item.ToolName);
            }
            UpdateEventToUI();
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            UpdateUIToEvent();
        }
        private void UpdateUIToEvent()
        {
            if (imgEvent ==null)
            {
                return;
            }
            if (radioButton4.Checked)
            {
                imgEvent.config.SelectMode = 0;
            }
            else if (radioButton1.Checked)
            {
                imgEvent.config.SelectMode = 1;
            }
            else if (radioButton2.Checked)
            {
                imgEvent.config.SelectMode = 2;
            }
            else if (radioButton3.Checked)
            {
                imgEvent.config.SelectMode = 3;
            }
            imgEvent.config.ImgPath = textBox1.Text;
            imgEvent.config.FolderPath = textBox2.Text;
            imgEvent.config.SelectCCD = comboBox1.SelectedIndex;
        }

        private void UpdateEventToUI()
        {
            if (imgEvent == null)
            {
                return;
            }
            Text = imgEvent.config.ToolName;
            switch (imgEvent.config.SelectMode)
            {
                case 0:
                    radioButton4.Checked = true;
                    break;
                case 1: 
                    radioButton1.Checked = true;
                    break;
                case 2:
                    radioButton2.Checked = true;
                    break;
                case 3:
                    radioButton3.Checked = true;
                    break;
                default: radioButton1.Checked = true;
                    break;
            }
            textBox1.Text = imgEvent.config.ImgPath;
            textBox2.Text = imgEvent.config.FolderPath;
            comboBox1.SelectedIndex = imgEvent.config.SelectCCD;
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = button1.Enabled = radioButton1.Checked;
            textBox2.Enabled = button2.Enabled = radioButton2.Checked;
            panel1.Enabled = radioButton3.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.bmp)|*.bmp|(*.tif)|*.tif|(*.jpeg)|*.jpeg|(*.png)|*.png|All Files|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.Title = "选择路径";
            if (File.Exists(textBox1.Text))
            {
                ofd.InitialDirectory = Path.GetDirectoryName(textBox1.Text);
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imgEvent.selectImg = 0;
                textBox2.Text = fbd.SelectedPath;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

       
    }
}
