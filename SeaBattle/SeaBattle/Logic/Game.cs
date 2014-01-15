using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SeaBattle.Logic;
using SeaBattle.Players;

namespace SeaBattle.Logic
{
    public class LGame
    {

        private Player player1;
        private Player player2;

        public bool IsPlayer1Tur
        {
            get;
            set;
        }

        public Player Player1
        {
            get
            {
                return player1;
            }
            set
            {
                player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return player2;
            }
            set
            {
                player2 = value;
            }
        }

        public LGame(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
        }
    }
}
