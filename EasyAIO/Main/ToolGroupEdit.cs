////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//              佛祖保佑       永无BUG     永不修改                  //
////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace EasyAIO
{
    /// <summary>
    /// 图像处理工具编辑器 Build by ZL 19.01.29
    /// </summary>
    public partial class ToolGroupEdit : UserControl
    {
        SaveTaskGroup stg = new SaveTaskGroup();
        TreeNode m_DropNode = new TreeNode();
        StrAttribute str = new StrAttribute("", Color.Lime, new Point(100, 100));
        List<Task> tasks = new List<Task>();
        static List<ToolFactory> ToolList = new List<ToolFactory>();

        public MPicBox PicBox { get; set; }

        string configPath = Application.StartupPath+"\\CCDConfig.zl";
        public string ConfigPath
        {
            get { return configPath; }
            set { configPath = value; }
        }

        private string dllDirectoryPath = "";

        private bool _lock;
        public bool Lock
        {
            get { return _lock; }
            set { _lock = value;
                LockControl(_lock);
            }
        }
        
        public ToolGroupEdit()
        {
            InitializeComponent();
            ToolListInitial();
        }

        private void ToolListInitial()
        {
            ToolList.Clear();
            ToolList.Add(new ToolFactory("图像获取", "EasyAIO.ImgSourceFactory"));
            ToolList.Add(new ToolFactory("图像预处理", "EasyAIO.ImgPreProcessFactory"));
            ToolList.Add(new ToolFactory("斑点工具", "EasyAIO.ObjectFactory"));
            ToolList.Add(new ToolFactory("模板匹配", "EasyAIO.MatcherFactory"));
            ToolList.Add(new ToolFactory("坐标系", "EasyAIO.ShapeFactory"));
            ToolList.Add(new ToolFactory("点检测", "EasyAIO.FindPointFactory"));
            ToolList.Add(new ToolFactory("直线检测", "EasyAIO.FindLineFactory"));
            ToolList.Add(new ToolFactory("圆形检测", "EasyAIO.FindCircleFactory"));
            ToolList.Add(new ToolFactory("矩形检测", "EasyAIO.FindRectFactory"));
            ToolList.Add(new ToolFactory("求两点距离", "EasyAIO.Get2PDistFactory"));
            ToolList.Add(new ToolFactory("求点到直线距离", "EasyAIO.GetPLDistFactory"));
            ToolList.Add(new ToolFactory("求求两直线交点", "EasyAIO.Get2LIntersectionFactory"));
            ToolList.Add(new ToolFactory("字符识别", "EasyAIO.OcrFactory"));
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ToolSelectFrm tsf = ToolSelectFrm.CreateToolSelect(ToolList);
            //双击添加工具
            tsf.DoubleClick += (s, de) => {
                if (tabctrl_task.TabPages.Count > 0)
                {
                    TreeView tv = tabctrl_task.SelectedTab.Controls[0] as TreeView;
                    TreeNode tn = CreateNode(tv, de.Type, tv.Nodes.Count);
                    if (tn == null)
                    {
                        MessageBox.Show("创建工具失败！");
                        return;
                    }
                    tv.Nodes.Add(tn);
                }
            };
            tsf.Show();
            tsf.Focus();
        }
        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeView tv = sender as TreeView;
            TreeNode tn = tv.GetNodeAt(new Point(e.X, e.Y));
            BaseEvent be = tn.Tag as BaseEvent;
            if (be != null)
            {
                be.ShowDialog();
            }
        }
        private void tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (Lock)
            {
                e.CancelEdit = true;
                return;
            }
            TreeView tv = sender as TreeView;
            BaseEvent be = e.Node.Tag as BaseEvent;
            if (e.Label != null)
            {
                be.Config.ToolName = e.Label;
            }
        }
        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            string name = "Task" + tasks.Count.ToString();
            NewNameFrm nnf = new NewNameFrm();
            nnf.ToolName = name;
            if (nnf.ShowDialog() == DialogResult.OK)
            {
                Task t = new Task();
                CreatePage(nnf.ToolName, out t);
                stg.ConfigGroup.Add(t.configGroup);
            }
        }
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                // Set the effect based upon the KeyState.
                if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                TreeViewHitTestInfo info = tv.HitTest(tv.PointToClient(new Point(e.X, e.Y)));
                m_DropNode = info.Node;
            }
            else if (e.Data.GetDataPresent(typeof(System.Int32)))
            {
                 if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                {
                    e.Effect = DragDropEffects.Move;
                }
                TreeViewHitTestInfo info = tv.HitTest(tv.PointToClient(new Point(e.X, e.Y)));
                m_DropNode = info.Node;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                string item = (string)e.Data.GetData(typeof(System.String));
                if (e.Effect == DragDropEffects.Copy)
                {
                    //拖动添加工具
                    if (m_DropNode == null)
                    {
                        TreeNode tn = CreateNode(tv, item, tv.Nodes.Count);
                        if (tn == null)
                        {
                            MessageBox.Show("创建工具失败！");
                            return;
                        }
                        tv.Nodes.Add(tn);
                    }
                    else
                    {
                        int index = tv.Nodes.IndexOf(m_DropNode);
                        TreeNode tn = CreateNode(tv, item, index);
                        if (tn == null)
                        {
                            MessageBox.Show("创建工具失败！");
                            return;
                        }
                        tv.Nodes.Insert(index, tn);
                    }
                }
            }
            else if (e.Data.GetDataPresent(typeof(System.Int32)))
            {
                int index = (int)e.Data.GetData(typeof(System.Int32));
                if (e.Effect == DragDropEffects.Move)
                {
                    TreeNode tn = tv.Nodes[index];
                    BaseEvent be = tn.Tag as BaseEvent;
                    tasks[tabctrl_task.SelectedIndex].Events.Remove(be);
                    tasks[tabctrl_task.SelectedIndex].configGroup.CfgGroup.Remove(be.Config);
                    tv.Nodes.Remove(tn);
                    //拖动添加工具
                    if (m_DropNode == null)
                    {
                        tasks[tabctrl_task.SelectedIndex].Events.Add(be);
                        tasks[tabctrl_task.SelectedIndex].configGroup.CfgGroup.Add(be.Config);
                        tv.Nodes.Add(tn);
                    }
                    else
                    {
                        int newindex = tv.Nodes.IndexOf(m_DropNode);
                        tasks[tabctrl_task.SelectedIndex].Events.Insert(newindex, be);
                        tasks[tabctrl_task.SelectedIndex].configGroup.CfgGroup.Insert(newindex, be.Config);
                        tv.Nodes.Insert(newindex, tn);
                    }
                }
            }
        }
        private void tabctrl_task_DoubleClick(object sender, EventArgs e)
        {
            Task t = tabctrl_task.SelectedTab.Tag as Task;
            if (t != null && !Lock)
            {
                NewNameFrm nnf = new NewNameFrm();
                nnf.ToolName = t.Name;
                if (nnf.ShowDialog() == DialogResult.OK)
                {
                    t.Name = nnf.ToolName;
                    t.configGroup.TaskName = nnf.ToolName;
                    tabctrl_task.SelectedTab.Text = nnf.ToolName;
                }
            }
        }
        private void tabctrl_task_PageClosed(object sender, ClosedEventArgs e)
        {
            Task t =e.SelectPage.Tag as Task;
            tasks.Remove(t);
            stg.ConfigGroup.Remove(t.configGroup);
        }
        private void RunToolStripButton_Click(object sender, EventArgs e)
        {
            RunTask();
        }
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show("OK");
        }
        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.zl)|*.zl";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.Title = "打开";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadConfig(ofd.FileName);
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.zl)|*.zl";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            sfd.Title = "保存";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveConfig(sfd.FileName);
                MessageBox.Show("OK");
            }
        }
        private void tsbtn_delete_Click(object sender, EventArgs e)
        {
            if (tabctrl_task.TabPages.Count <= 0)
            {
                return;
            }
            Task t = tabctrl_task.SelectedTab.Tag as Task;
            TreeView tv = tabctrl_task.SelectedTab.Controls[0] as TreeView;
            if (tv.SelectedNode == null)
            {
                return;
            }
            if (MessageBox.Show("确认删除当前工具?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            int i = tv.SelectedNode.Index;
            t.configGroup.CfgGroup.RemoveAt(i);
            if (t.Events[i] is ICamera)
            {
                ICamera cam = t.Events[i] as ICamera;
                t.Cameras.Remove(cam);
            }
            t.Events.RemoveAt(i);
            tv.Nodes.Remove(tv.SelectedNode);
        }
        private void ToolGroupEdit_Load(object sender, EventArgs e)
        {
            if (!GetIsDesignMode())
            {
            }
        }
        private void 帮助LToolStripButton_Click(object sender, EventArgs e)
        {
            if (tabctrl_task.TabPages.Count <= 0)
            {
                return;
            }
            Task t = tabctrl_task.SelectedTab.Tag as Task;
            ResultFrm rf = new ResultFrm(t.ResultDatas);
            rf.ShowDialog();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MoudleFrm mf = new MoudleFrm();
            mf.ShowDialog();
        }


        #region 私有方法
        /// <summary>  
        /// 获取当前是否处于设计器模式  
        /// </summary>  
        /// <remarks>  
        /// 在程序初始化时获取一次比较准确，若需要时获取可能由于布局嵌套导致获取不正确，如GridControl-GridView组合。  
        /// </remarks>  
        /// <returns>是否为设计器模式</returns>  
        private bool GetIsDesignMode()
        {
            return (this.GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null
               || LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        }
       
        /// <summary>
        /// 运行当前任务
        /// </summary>
        private void RunTask()
        {
            if (tabctrl_task.TabPages.Count <= 0)
            {
                return;
            }
            mPicBox1.strs.Clear();
            Task t = tabctrl_task.SelectedTab.Tag as Task;
            if (t.ProcessImg())
            {
                str.Text = "OK";
                str.StrColor = Color.Lime;
            }
            else
            {
                str.Text = "NG";
                str.StrColor = Color.Red;
            }
            mPicBox1.strs.Add(str);
            try
            {
                str.PixelsLocation = new Point(mPicBox1.Image.Width / 20, mPicBox1.Image.Height / 20);
            }
            catch 
            {
            }
            mPicBox1.Refresh();
        }
        /// <summary>
        /// 画到外部图片框
        /// </summary>
        /// <param name="picBox"></param>
        /// <param name="be"></param>
        private void DrawPicBox(MPicBox picBox, List<BaseEvent> be)
        {
            picBox.objDatas.Clear();
            picBox.MShapes.Clear();
            picBox.MRois.Clear();
            picBox.MOcrs.Clear();
            picBox.MMatchers.Clear();
            picBox.FinderPatterns.Clear();
            picBox.lines.Clear();
            picBox.points.Clear();
            picBox.strs.Clear();
            picBox.Image = null;
            foreach (var item in be)
            {
                if (item.Config.CanDrawToPicBox)
                {
                     item.DrawToPicBox(picBox);
                }
            }
            if (picBox.Image !=null)
            {
                picBox.ImgAutoSize();
                picBox.Refresh();
            }
        }
        /// <summary>
        /// 锁定控件
        /// </summary>
        /// <param name="isLock"></param>
        private void LockControl(bool isLock)
        {
            打开OToolStripButton.Visible = toolStripButton1.Visible = 新建NToolStripButton.Visible = tsbtn_delete.Visible = !isLock;
            tabctrl_task.ShowCloseBtn = !isLock;
            foreach (TabPage item in tabctrl_task.TabPages)
            {
                TreeView tv = item.Controls[0] as TreeView;
                if (tv != null)
                {
                    tv.LabelEdit = !isLock;
                }
            }
        }
        private TreeView CreatePage(string name, out Task t)
        {
            TabPage tp = new TabPage(name);
            t = new Task();
            t.Name = name;
            t.configGroup.TaskName = name;
            TreeView tree = new TreeView();
            tree.AllowDrop = true;
            tree.Dock = DockStyle.Fill;
            tree.DragDrop += treeView_DragDrop;
            tree.DragOver += treeView_DragOver;
            tree.LabelEdit = true;
            tree.AfterLabelEdit += tree_AfterLabelEdit;
            tree.MouseDoubleClick += treeView_MouseDoubleClick;
            tree.Font = new System.Drawing.Font("微软雅黑", 11);
            tree.Tag = t;
            tree.ShowRootLines = false;
            tree.StateImageList = imageList1;
            tree.ShowRootLines = false;
            tree.ItemDrag += (sender, e) =>
            {
                TreeNode node = e.Item as TreeNode;
                tree.DoDragDrop(node.Index, DragDropEffects.Move);
            };

            tp.Tag = t;
            tp.Controls.Add(tree);
            tabctrl_task.Controls.Add(tp);
            tasks.Add(t);
            t.GetStepStatus = (index, r) =>
            {
                try
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (r)
                            {
                                tree.Nodes[index].StateImageIndex = 1;
                            }
                            else
                            {
                                tree.Nodes[index].StateImageIndex = 2;
                            }
                        }));
                    }
                    else
                    {
                        if (r)
                        {
                            tree.Nodes[index].StateImageIndex = 1;
                        }
                        else
                        {
                            tree.Nodes[index].StateImageIndex = 2;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            };
            t.GetResultStatus += (es, ct) =>
            {
                try
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            DrawPicBox(mPicBox1, es);
                            toolStripStatusLabel1.Text = "用时" + ct + "毫秒";
                            if (PicBox != null)
                            {
                                DrawPicBox(PicBox, es);
                            }
                        }));
                    }
                    else
                    {
                        DrawPicBox(mPicBox1, es);
                        toolStripStatusLabel1.Text = "用时" + ct + "毫秒";
                        if (PicBox != null)
                        {
                            DrawPicBox(PicBox, es);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            };
            return tree;
        }
        private TreeNode CreateNode(TreeView tv, string type, int index)
        {
            TreeNode tn = null;
            Task t = tv.Tag as Task;
            BaseEvent be = t.CreateEvent(type, index);
            if (be != null)
            {
                tn = new TreeNode(be.Config.ToolName);
                tn.StateImageIndex = 0;
                tn.Tag = be;
            }
            return tn;
        }

        [Obsolete("方法已经停用")]
        /// <summary>
        /// 扫描扩展
        /// </summary>
        private void ScanDirectory()
        {
            //遍历Ethercat卡的DLL查看所有Ethercat模块卡
            if (Directory.Exists(dllDirectoryPath))
            {
                DirectoryInfo theFolder = new DirectoryInfo(dllDirectoryPath);
                FileInfo[] dirInfo = theFolder.GetFiles();
                //遍历文件夹
                foreach (FileInfo file in dirInfo)
                {
                    if (file.Extension.ToUpper() == ".VIS")
                    {
                        LoadDLL(file.FullName);
                    }
                }
            }
        }
        
        [Obsolete("方法已经停用")]
        private void LoadDLL(string filepath)
        {
            try
            {
                do
                {
                    Assembly assemly = Assembly.LoadFile(filepath);
                    Type ParentType = System.Type.GetType("EasyAIO.BaseConfig");
                    Type[] types = assemly.GetExportedTypes();
                    foreach (var t in types)
                    {
                        if (t.IsClass && t.IsSubclassOf(ParentType))
                        {
                            BaseConfig obj = (BaseConfig)Activator.CreateInstance(t);
                            
                            break;
                        }
                    }
                } while (false);
            }
            catch
            {
                Console.WriteLine("LoadDLL error!");
            }
        }

        #endregion

        #region 公开方法
       

        /// <summary>
        /// 绕点旋转
        /// </summary>
        /// <param name="p0">旋转中心</param>
        /// <param name="p1">待转点</param>
        /// <param name="angle">逆时针旋转角度</param>
        /// <returns>旋转后点</returns>
        public static PointF GetRotatePoint(PointF p0, PointF p1, float angle)//p0是绕心，p1是待转点
        {
            return GetRotatePoint(p0.X, p0.Y, p1.X, p1.Y, angle);
        }
        /// <summary>
        /// 绕点旋转
        /// </summary>
        /// <param name="x0">旋转中心X</param>
        /// <param name="y0">旋转中心Y</param>
        /// <param name="x1">待转点X</param>
        /// <param name="y1">待转点Y</param>
        /// <param name="angle">逆时针旋转角度</param>
        /// <returns>旋转后点</returns>
        public static PointF GetRotatePoint(float x0, float y0, float x1, float y1, float angle)//p0是绕心，p1是待转点
        {
            //假设对图片上任意点(x1,y1),绕一个坐标点(x0,y0)逆时针旋转A角度后的新的坐标设为(x2,y2),有公式：
            //x2=(x1-x0)*cos(A)-(y1-y0)*sin(A)+x0
            //y2=(x1-x0)*sin(A)+(y1-y0)*cos(A)+y0
            if (x0 == 0 && y0 == 0 && angle == 0)
            {
                return new PointF(x1, y1);
            }
            angle = (float)(angle * Math.PI / 180);
            float x, y;
            x = (float)((x1 - x0) * Math.Cos(angle) - (y1 - y0) * Math.Sin(angle) + x0);
            y = (float)((x1 - x0) * Math.Sin(angle) + (y1 - y0) * Math.Cos(angle) + y0);
            return new PointF(x, y);
        }
        /// <summary>
        /// 初始化OpenEvision
        /// </summary>
        public static void InitializeOpeneVision()
        {
            CsWrapper_2_2.DynamicLoading.Initialize();
        }

        /// <summary>
        /// 添加工具
        /// </summary>
        /// <param name="assemly">引用的工具类库</param>
        public static void AddTool(Assembly assemly)
        {
            Type ParentType = System.Type.GetType("EasyAIO.BaseConfig");
            Type[] types = assemly.GetExportedTypes();
            foreach (var t in types)
            {
                if (t.IsClass && t.IsSubclassOf(ParentType))
                {
                    BaseConfig obj = (BaseConfig)Activator.CreateInstance(t);
                    ToolList.Add(new ToolFactory(obj.ToolName, obj.FactoryTypeName));
                    break;
                }
            }
        }

        /// <summary>
        /// 添加工具
        /// </summary>
        /// <param name="config"></param>
        public static void AddTool(BaseConfig config)
        {
            ToolList.Add(new ToolFactory(config.ToolName, config.FactoryTypeName));
        }

        /// <summary>
        /// 添加工具
        /// </summary>
        /// <param name="tool"></param>
        public static void AddTool(ToolFactory tool)
        {
            ToolList.Add(tool);
        }
       
        /// <summary>
        /// 加载ConfigPath路径
        /// </summary>
        /// <returns>结果</returns>
        public bool LoadConfig()
        {
            return LoadConfig(configPath);
        }
        /// <summary>
        /// 加载指定路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>结果</returns>
        public bool LoadConfig(string path)
        {
            if (Path.GetExtension(path).ToLower() != ".zl")
            {
                MessageBox.Show("图像处理工具加载失败，文件类型不正确，请设置.zl文件路径", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!File.Exists(path))
            {
                return false;
            }
            stg = BinarySerializer.DeSerialize<SaveTaskGroup>(path);
            if (stg != null)
            {
                //移除现有处理
                foreach (TabPage item in tabctrl_task.TabPages)
                {
                    tabctrl_task.TabPages.Remove(item);
                    item.Dispose();
                }
                while (tasks.Count > 0)
                {
                    tasks[0] = null;
                    tasks.RemoveAt(0);
                }
                //添加新处理
                for (int i = 0; i < stg.ConfigGroup.Count; i++)
                {
                    string name = stg.ConfigGroup[i].TaskName;
                    Task t;
                    TreeView tree = CreatePage(name, out t);
                    t.configGroup = stg.ConfigGroup[i];
                    t.CreateEvents();
                    if (t.Events.Count != t.configGroup.CfgGroup.Count)
                    {
                        Console.WriteLine("加载工具个数不正确");
                    }
                    for (int j = 0; j < t.configGroup.CfgGroup.Count; j++)
                    {
                        BaseConfig cb = t.configGroup.CfgGroup[j];
                        TreeNode tn = new TreeNode(cb.ToolName);
                        tn.Tag = t.Events[j];
                        tn.StateImageIndex = 0;
                        tree.Nodes.Add(tn);
                    }
                }
                //RunTask();
            }
            else
            {
                //MessageBox.Show(String.Format("图像处理工具加载失败，请使用{0}版本库打开当前文件。", stg.DllVersion), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                stg = new SaveTaskGroup();
            }

            return true;
        }
        /// <summary>
        /// 保存到ConfigPath路径
        /// </summary>
        /// <returns>结果</returns>
        public bool SaveConfig()
        {
            return SaveConfig(configPath);
        }
        /// <summary>
        /// 保存到指定路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>结果</returns>
        public bool SaveConfig(string path)
        {
            if (Path.GetExtension(path).ToLower() != ".zl")
            {
                MessageBox.Show("图像处理工具保存失败，文件类型不正确，请使用.zl后缀保存", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            stg.DllVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            BinarySerializer.Serialize<SaveTaskGroup>(stg, path);
            return true;
        }
        /// <summary>
        /// 获取任务个数
        /// </summary>
        /// <returns>个数</returns>
        public int GetTasksCount()
        {
            return tasks.Count;
        }
        /// <summary>
        /// 获取指定任务
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>任务</returns>
        public Task GetTask(int index)
        {
            if (index < 0 || index >= tasks.Count)
            {
                return null;
            }
            return tasks[index];
        }
        /// <summary>
        /// 获取指定任务
        /// </summary>
        /// <param name="name">任务名</param>
        /// <returns>任务</returns>
        public Task GetTask(string name)
        {
            return tasks.Find((s) =>
            {
                if (s.Name == name)
                {
                    return true;
                }
                return false;
            });
        }
        /// <summary>
        /// 获取浮点型结果数据
        /// </summary>
        /// <param name="code">条件码:Task.Tool.Result</param>
        /// <param name="data">储存数据变量</param>
        /// <returns>结果</returns>
        public bool GetResultFloatData(string code, out float[] data)
        {
            int fristP =code.IndexOf('.');
            int lastP =code.LastIndexOf('.');
            string taskName = code.Substring(0, fristP);
            string toolName = code.Substring(fristP + 1, lastP - fristP -1);
            string resultName = code.Substring(lastP + 1);

            Task t = GetTask(taskName);
            Result rd;
            if (t != null)
            {
                rd = t.GetResultData(toolName);
                if (rd != null)
                {
                    rd.ValueParams.TryGetValue(resultName, out data);
                    return true;
                }
            }
            data = null;
            return false;

        }
        /// <summary>
        /// 获取字符串结果数据
        /// </summary>
        /// <param name="code">条件码:Task.Tool.Result</param>
        /// <param name="data">储存数据变量</param>
        /// <returns>结果</returns>
        public bool GetResultStringData(string code, out string data)
        {
            int fristP = code.IndexOf('.');
            int lastP = code.LastIndexOf('.');
            string taskName = code.Substring(0, fristP);
            string toolName = code.Substring(fristP + 1, lastP - fristP - 1);
            string resultName = code.Substring(lastP + 1);

            Task t = GetTask(taskName);
            Result rd;
            if (t != null)
            {
                rd = t.GetResultData(toolName);
                if (rd != null)
                {
                    rd.StrParams.TryGetValue(resultName, out data);
                    return true;
                }
            }
            data = null;
            return false;

        }

        #endregion
        
    }

    public struct ToolFactory
    {
        public ToolFactory(string name ,string facName)
        {
            this.toolname = name;
            this.factoryName = facName;
        }
        private string toolname;

        public string ToolName
        {
            get { return toolname; }
            set { toolname = value; }
        }

        private string factoryName;

        public string FactoryName
        {
            get { return factoryName; }
            set { factoryName = value; }
        }

    }
}
