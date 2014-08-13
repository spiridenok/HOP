namespace TreeView
{
    partial class Form1
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Root Dir");
            this.tree = new System.Windows.Forms.TreeView();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.AddEncryptButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.AllowDrop = true;
            this.tree.Dock = System.Windows.Forms.DockStyle.Left;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            treeNode5.Name = "root";
            treeNode5.Text = "Root Dir";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.tree.Size = new System.Drawing.Size(353, 419);
            this.tree.TabIndex = 0;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(387, 24);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(121, 50);
            this.DownloadButton.TabIndex = 1;
            this.DownloadButton.Text = "Download Structure";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 419);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.AddEncryptButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.tree);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button AddEncryptButton;
        private System.Windows.Forms.Button UploadButton;
    }
}

