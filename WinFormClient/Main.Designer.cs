namespace WinFormClient
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.structureTreeView = new System.Windows.Forms.TreeView();
            this.menuStripTop = new System.Windows.Forms.MenuStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanelTopMenu = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(256, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1088, 549);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Text = "dataGridView1";
            // 
            // structureTreeView
            // 
            this.structureTreeView.Location = new System.Drawing.Point(12, 60);
            this.structureTreeView.Name = "structureTreeView";
            this.structureTreeView.Size = new System.Drawing.Size(238, 549);
            this.structureTreeView.TabIndex = 1;
            this.structureTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.structureTreeView_NodeMouseClick);
            // 
            // menuStripTop
            // 
            this.menuStripTop.Location = new System.Drawing.Point(0, 0);
            this.menuStripTop.Name = "menuStripTop";
            this.menuStripTop.Size = new System.Drawing.Size(1356, 24);
            this.menuStripTop.TabIndex = 2;
            this.menuStripTop.Text = "menuStripTop";
            // 
            // flowLayoutPanelTopMenu
            // 
            this.flowLayoutPanelTopMenu.Location = new System.Drawing.Point(12, 27);
            this.flowLayoutPanelTopMenu.Name = "flowLayoutPanelTopMenu";
            this.flowLayoutPanelTopMenu.Size = new System.Drawing.Size(1332, 27);
            this.flowLayoutPanelTopMenu.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 621);
            this.Controls.Add(this.flowLayoutPanelTopMenu);
            this.Controls.Add(this.structureTreeView);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStripTop);
            this.MainMenuStrip = this.menuStripTop;
            this.Name = "Main";
            this.Text = "Database managment system";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TreeView structureTreeView;
        private System.Windows.Forms.MenuStrip menuStripTop;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTopMenu;
    }
}

