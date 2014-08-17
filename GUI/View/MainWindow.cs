using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HOP.GUI.View.API;
using HOP.GUI.Presenter.API;

namespace HOP.GUI.View
{
    public partial class MainWindow : Form, IView
    {
        IPresenter presenter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            StorageTree.Tag = "Tree Tag";
        }

        private void AddTreeLevel( TreeNode ParentNode, List<string> NodesToAdd )
        {
            foreach (var node in NodesToAdd)
            {
                TreeNode sub_node = new TreeNode(node);
                ParentNode.Nodes.Add(sub_node);
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> root_listing = presenter.Connect();
            TreeNode root = StorageTree.Nodes[0];

            foreach( var item in root_listing )
            {
                TreeNode sub_node = new TreeNode(item.Key);
                root.Nodes.Add(sub_node);

                // TODO: do we need to use a node tag?

                if (item.Value != null)
                {
                    AddTreeLevel(sub_node, item.Value);
                }
            }
            root.Expand();
            StorageTree.Focus();
        }

        private void AddEncryptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                byte[] file = File.ReadAllBytes(dialog.FileName);
                System.Console.WriteLine("Opened file:" + dialog.FileName + ", size: " + file.Length);
            }
        }

        private void StorageTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Event is sent when clicked on a Node
            if (e.Action == TreeViewAction.ByMouse)
            {
                Console.WriteLine("Clicked on Node {0}", e.Node.Text);
            }
        }

        #region IView interface
        public void SetConnectButtonState( bool active )
        {
            this.ConnectButton.Enabled = active; 
        }

        public void SetPresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }
        #endregion
    }
}
