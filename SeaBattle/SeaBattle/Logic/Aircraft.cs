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
    public class Aircraft : Unit
    {
        #region variables

        private int size;

        #endregion

        #region properties

        public override int Size 
        {
            get { return size; }
        }

        #endregion

        #region constructors

        public Aircraft(IntPoint _position, UnitOrientation _orientation)
        {
            size = 4;
            position = _position;
            orientation = _orientation;

            damagedPositions = new bool[size];
        }

        #endregion

        #region methods

        public override IntPoint[] GetUnitPoints()
        {
            return UnitPointsCounter.GetAircraftPoints(position, orientation, size);
        }

        public override string ToString()
        {
            string result = "#a|" + position.X.ToString() + "|" + position.Y.ToString() + "|";
            for (int i = 0; i < damagedPositions.Length; i++)
            {
                result += damagedPositions[i].ToString() + "|"; 
            }
            return result;
        }

        #endregion
    }
}
