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

        #region Button click operations
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            // FIXME: Pretty dirty dependency...
            if (ConnectButton.Text == "Disconnect")
            {
                presenter.Disconnect();
                return;
            }
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
//                byte[] file = File.ReadAllBytes(dialog.FileName);
                System.Console.WriteLine("Opened file:" + dialog.FileName); // + ", size: " + file.Length);
            }

            string full_path = StorageTree.SelectedNode.FullPath.Replace( "Root Dir", string.Empty );
            presenter.AddFileToUpload(full_path.Split('\\').ToList(),dialog.FileName);
            var new_node = new TreeNode("(*)" + Path.GetFileName(dialog.FileName));

            StorageTree.SelectedNode.Expand();
            StorageTree.SelectedNode.Nodes.Add(new_node);
            StorageTree.SelectedNode = new_node;
            StorageTree.Focus();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            presenter.Upload();
        }
        #endregion

        #region IView interface
        public void SetConnectionButtonText( string text )
        {
            this.ConnectButton.Text = text;
        }

        public void SetPresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void ClearTree()
        {
            StorageTree.Nodes.Clear();
            StorageTree.Nodes.Add( new TreeNode("Root Dir") ) ;
        }

        public void SetUploadButton(bool enable)
        {
            UploadButton.Enabled = enable;
        }

        public void SetAddFilesButton(bool enable)
        {
            AddButton.Enabled = enable;
        }

        #endregion

        #region TreeEvents
        private void StorageTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            Console.WriteLine("Expanding {0}", e.Node.Text);
        }

        private void StorageTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Event is sent when clicked on a Node
            if (e.Action == TreeViewAction.ByMouse)
            {
                Console.WriteLine("Clicked on Node {0}", e.Node.Text);
            }
        }
        #endregion
    }
}
