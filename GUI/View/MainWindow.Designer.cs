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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Root Dir");
            this.StorageTree = new System.Windows.Forms.TreeView();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.AddEncryptButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StorageTree
            // 
            this.StorageTree.AllowDrop = true;
            this.StorageTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.StorageTree.Location = new System.Drawing.Point(0, 0);
            this.StorageTree.Name = "StorageTree";
            treeNode1.Name = "root";
            treeNode1.Text = "Root Dir";
            this.StorageTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.StorageTree.Size = new System.Drawing.Size(353, 419);
            this.StorageTree.TabIndex = 0;
            this.StorageTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.StorageTree_AfterSelect);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(387, 24);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(121, 50);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // AddEncryptButton
            // 
            this.AddEncryptButton.Location = new System.Drawing.Point(387, 188);
            this.AddEncryptButton.Name = "AddEncryptButton";
            this.AddEncryptButton.Size = new System.Drawing.Size(121, 49);
            this.AddEncryptButton.TabIndex = 2;
            this.AddEncryptButton.Text = "Add and Encrypt File";
            this.AddEncryptButton.UseVisualStyleBackColor = true;
            this.AddEncryptButton.Click += new System.EventHandler(this.AddEncryptButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(399, 109);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(97, 30);
            this.UploadButton.TabIndex = 3;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 419);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.AddEncryptButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.StorageTree);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView StorageTree;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button AddEncryptButton;
        private System.Windows.Forms.Button UploadButton;
    }
}