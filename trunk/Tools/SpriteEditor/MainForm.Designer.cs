namespace OpenTibiaXna.Tools
{
    partial class MainForm
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
            this.msMenuBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssStatusMenu = new System.Windows.Forms.StatusStrip();
            this.lblTotalSprites = new System.Windows.Forms.ToolStripStatusLabel();
            this.ofdOpenSprite = new System.Windows.Forms.OpenFileDialog();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.msMenuBar.SuspendLayout();
            this.ssStatusMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.SuspendLayout();
            // 
            // msMenuBar
            // 
            this.msMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.msMenuBar.Location = new System.Drawing.Point(0, 0);
            this.msMenuBar.Name = "msMenuBar";
            this.msMenuBar.Size = new System.Drawing.Size(554, 24);
            this.msMenuBar.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSpriteToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSpriteToolStripMenuItem
            // 
            this.openSpriteToolStripMenuItem.Name = "openSpriteToolStripMenuItem";
            this.openSpriteToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.openSpriteToolStripMenuItem.Text = "Open Sprite";
            this.openSpriteToolStripMenuItem.Click += new System.EventHandler(this.openSpriteToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // ssStatusMenu
            // 
            this.ssStatusMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotalSprites});
            this.ssStatusMenu.Location = new System.Drawing.Point(0, 303);
            this.ssStatusMenu.Name = "ssStatusMenu";
            this.ssStatusMenu.Size = new System.Drawing.Size(554, 22);
            this.ssStatusMenu.TabIndex = 1;
            // 
            // lblTotalSprites
            // 
            this.lblTotalSprites.Name = "lblTotalSprites";
            this.lblTotalSprites.Size = new System.Drawing.Size(0, 17);
            // 
            // ofdOpenSprite
            // 
            this.ofdOpenSprite.DefaultExt = global::OpenTibiaXna.Tools.Properties.Settings.Default.DefaultSpriteExtension;
            this.ofdOpenSprite.Filter = global::OpenTibiaXna.Tools.Properties.Settings.Default.DefaultSpriteFileExtensionFilter;
            this.ofdOpenSprite.RestoreDirectory = true;
            this.ofdOpenSprite.Title = "Open Sprite File";
            this.ofdOpenSprite.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdOpenSprite_FileOk);
            // 
            // picSprite
            // 
            this.picSprite.Location = new System.Drawing.Point(100, 128);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(32, 32);
            this.picSprite.TabIndex = 2;
            this.picSprite.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 325);
            this.Controls.Add(this.picSprite);
            this.Controls.Add(this.ssStatusMenu);
            this.Controls.Add(this.msMenuBar);
            this.Name = "MainForm";
            this.Text = "OpenTibiaXna Sprite Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.msMenuBar.ResumeLayout(false);
            this.msMenuBar.PerformLayout();
            this.ssStatusMenu.ResumeLayout(false);
            this.ssStatusMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip ssStatusMenu;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalSprites;
        private System.Windows.Forms.ToolStripMenuItem openSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdOpenSprite;
        private System.Windows.Forms.PictureBox picSprite;
    }
}

