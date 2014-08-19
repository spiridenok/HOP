namespace HOP.GUI.View
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Root Dir");
            this.StorageTree = new System.Windows.Forms.TreeView();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StorageTree
            // 
            this.StorageTree.AllowDrop = true;
            this.StorageTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.StorageTree.Location = new System.Drawing.Point(0, 0);
            this.StorageTree.Name = "StorageTree";
            treeNode3.Name = "root";
            treeNode3.Text = "Root Dir";
            this.StorageTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.StorageTree.Size = new System.Drawing.Size(434, 539);
            this.StorageTree.TabIndex = 0;
            this.StorageTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.StorageTree_BeforeExpand);
            this.StorageTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.StorageTree_AfterSelect);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(471, 12);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(121, 50);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(471, 216);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(121, 49);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add File...";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddEncryptButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(471, 125);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(121, 46);
            this.UploadButton.TabIndex = 3;
            this.UploadButton.Text = "Encrypt and Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 539);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.StorageTree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView StorageTree;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button UploadButton;
    }
}