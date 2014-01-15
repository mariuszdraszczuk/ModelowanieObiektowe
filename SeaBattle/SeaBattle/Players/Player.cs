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

namespace SeaBattle.Players
{
    public abstract class Player
    {
        public abstract bool Shoot(IntPoint point, Map targetsMap);

        public Map PlayerMap
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
