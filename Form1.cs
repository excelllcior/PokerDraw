using PokerDraw.MVVM.Model;
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
    public partial class Form1 : Form
    {
        private readonly Table _table = new Table(20);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _table.AddPlayer("Sergey", 20);
            _table.AddPlayer("Anton", 20);
            _table.AddPlayer("Arthur", 20);
            _table.AddPlayer("Alexandr", 20);
            _table.AddGame();
            _table.StartCurrentGame();

            labelPlayer.Text = _table.CurrentPlayer.Name;
            labelDealer.Text = _table.DealerPosition.ToString();
        }

        private void buttonChangeDealer_Click(object sender, EventArgs e)
        {
            _table.SwitchToNextDealerInGame();
            labelDealer.Text = _table.DealerPosition.ToString();
        }

        private void buttonToNextPlayer_Click(object sender, EventArgs e)
        {
            _table.SwitchToNextPlayerInGame();
            labelPlayer.Text = _table.CurrentPlayer.Name;
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            _table.AddGame();
            _table.StartCurrentGame();
        }
    }
}
