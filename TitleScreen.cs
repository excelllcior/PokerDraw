﻿using PokerDraw;
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
        private int NumberOfPlayers = 2;

        public TitleScreen()
        {
            InitializeComponent();
        }

        private void radioButton2Players_CheckedChanged(object sender, EventArgs e)
        {
            NumberOfPlayers = 2;
        }

        private void radioButton3Players_CheckedChanged(object sender, EventArgs e)
        {
            NumberOfPlayers = 3;
        }

        private void radioButton4Players_CheckedChanged(object sender, EventArgs e)
        {
            NumberOfPlayers = 4;
        }

        private void buttonJoinTable_Click(object sender, EventArgs e)
        {
            //var names = new List<string> { "studski", "frog11", "hahahaha", "richie" };
            //PokerTableScreen form = new PokerTableScreen(names);
            //form.Show();

            NameEntryScreen form = new NameEntryScreen(NumberOfPlayers);
            form.Show();
        }
    }
}
