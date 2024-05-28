using PokerDraw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerDraw
{
    public partial class TitleScreen : Form
    {
        public TitleScreen()
        {
            InitializeComponent();
            StartFor2PLayersButton.Parent = BackgroundImage;
            StartFor3PLayersButton.Parent = BackgroundImage;
            StartFor4PLayersButton.Parent = BackgroundImage;
        }

        private void StartFor2PLayersButton_Click(object sender, EventArgs e)
        {
            NameEntryScreen form = new NameEntryScreen(2);
            form.Show();
        }

        private void StartFor3PLayersButton_Click(object sender, EventArgs e)
        {
            NameEntryScreen form = new NameEntryScreen(3);
            form.Show();
        }

        private void StartFor4PLayersButton_Click(object sender, EventArgs e)
        {
            NameEntryScreen form = new NameEntryScreen(4);
            form.Show();
        }
    }
}
