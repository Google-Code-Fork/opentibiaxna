using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTibiaXna.Library.Desktop.SpriteEngine;

namespace OpenTibiaXna.Tools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SpriteReader.GetSpriteImage(@"C:\DEV_WRITEFILES\Tibia.spr", );
        }

        private void openSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofdOpenSprite.ShowDialog();
        }

        private void ofdOpenSprite_FileOk(object sender, CancelEventArgs e)
        {
            this.SpriteFilePath = ofdOpenSprite.FileName;
            lblTotalSprites.Text = SpriteReader.GetSpriteImage(this.SpriteFilePath, 2).ToString();
        }

        #region Properties

        public bool HasLoadedSpriteFile
        {
            get { return !String.IsNullOrEmpty(this.ofdOpenSprite.FileName); }
        }

        public String SpriteFilePath { get; set; }

        #endregion
    }
}
