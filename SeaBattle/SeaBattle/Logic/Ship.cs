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
    public class Ship : Unit
    {
        
        #region variables

        int size;

        #endregion

        #region properties

        public override int  Size { get { return size; } }

        #endregion

        #region constructors

        public Ship(int _size, IntPoint _position, UnitOrientation _orientation)
        {
            size = _size;
            orientation = _orientation;
            position = _position;

            damagedPositions = new bool[_size];
        }

        #endregion

        #region methods
        public override IntPoint[] GetUnitPoints()
        {
            return UnitPointsCounter.GetShipPoints(position, orientation, size);
        }

        public override string ToString()
        {
            string result = "#s" + size.ToString()+"|" + position.X.ToString() + "|" + position.Y.ToString() + "|";
            for (int i = 0; i < damagedPositions.Length; i++)
            {
                result += damagedPositions[i].ToString() + "|";
            }
            return result;
        }

        #endregion
    }
}
