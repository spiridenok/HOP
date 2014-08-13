using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tree.Tag = "Tree Tag";
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            TreeNode sub_node = new TreeNode("Sub Node");
            TreeNode root = tree.Nodes[0];
            root.Nodes.Add(sub_node);
            System.Console.WriteLine("Root tag:" + root.Tag);
            sub_node.Tag = "Sub Node Tag";

            TreeNode another_sub_node = new TreeNode("Sub Node 2");
            root.Nodes.Add(another_sub_node);

            TreeNode sub_sub_node = new TreeNode("Sub sub node");
            sub_node.Nodes.Add(sub_sub_node);
        }

        private void AddEncryptButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                byte[] file = File.ReadAllBytes(dialog.FileName );
                System.Console.WriteLine("Opened file:" + dialog.FileName + ", size: " + file.Length);
            }
        }
    }
}
