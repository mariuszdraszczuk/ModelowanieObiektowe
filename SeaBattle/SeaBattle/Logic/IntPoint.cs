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

namespace SeaBattle.Logic
{
    public class IntPoint
    {
        //variables
        private int _x;
        private int _y;

        //properties
        public int X
        {
            get { return _x; }
            set
            {
                if (value >= 0)
                    _x = value;
                else
                    _x = -1;
            }
        }
        public int Y
        {
            get { return _y; }
            set
            {
                if (value >= 0)
                    _y = value;
                else
                    _y = -1;
            }
        }

        //constructors
        public IntPoint()
        {
            _x = -1;
            _y = -1;
        }

        public IntPoint(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
