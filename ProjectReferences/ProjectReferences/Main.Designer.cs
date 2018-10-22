namespace ProjectReferences
{
    partial class Main
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
            this.hierarchicalReferences = new System.Windows.Forms.TreeView();
            this.flatReferences = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // hierarchicalReferences
            // 
            this.hierarchicalReferences.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hierarchicalReferences.Location = new System.Drawing.Point(12, 12);
            this.hierarchicalReferences.Name = "hierarchicalReferences";
            this.hierarchicalReferences.ShowNodeToolTips = true;
            this.hierarchicalReferences.Size = new System.Drawing.Size(427, 637);
            this.hierarchicalReferences.TabIndex = 0;
            // 
            // flatReferences
            // 
            this.flatReferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flatReferences.Location = new System.Drawing.Point(445, 12);
            this.flatReferences.Name = "flatReferences";
            this.flatReferences.ShowNodeToolTips = true;
            this.flatReferences.Size = new System.Drawing.Size(484, 637);
            this.flatReferences.TabIndex = 1;
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 661);
            this.Controls.Add(this.flatReferences);
            this.Controls.Add(this.hierarchicalReferences);
            this.Name = "Main";
            this.Text = "Project References";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView hierarchicalReferences;
        private System.Windows.Forms.TreeView flatReferences;
    }
}

