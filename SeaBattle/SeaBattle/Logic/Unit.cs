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

    public enum UnitOrientation { Left, Right, Up, Down}

    public abstract class Unit
    {
        #region variables

        protected IntPoint position;
        protected UnitOrientation orientation;
        protected bool[] damagedPositions;

        #endregion

        #region properties

        public IntPoint Position
        {
            get { return position; }
            set { position = value; }
        }
        public UnitOrientation Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }
        public bool[] DamagedPosition
        {
            get
            {
                return damagedPositions;
            }
            set 
            {
                damagedPositions = value;
            }
        }

        public abstract int Size { get; }

        #endregion

        #region methods

        public bool IsDestroyed()
        {
            for (int i = 0; i < damagedPositions.Length; i++)
            {
                if (damagedPositions[i] == false)
                    return false;
            }
            return true;
        }


        public bool UpdateUnitState(IntPoint attackedField)
        {
            IntPoint[] points;
            if(this.GetType() == typeof(Thank))
                 points = UnitPointsCounter.GetThankPoints(position, orientation, Size);
            else if(this.GetType() == typeof(Ship))
                 points = UnitPointsCounter.GetShipPoints(position, orientation, Size);
            else
                points = UnitPointsCounter.GetAircraftPoints(position, orientation, Size);

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].X == attackedField.X &&
                    points[i].Y == attackedField.Y)
                {
                    damagedPositions[i] = true;
                    return true;
                }
            }
            return false;
        }

        public abstract IntPoint[] GetUnitPoints();

        public abstract string ToString();

        public static Unit FromString(string unitString)
        {
            string[] parts = unitString.Split('|');
            Unit unit;
            IntPoint anchor = new IntPoint(int.Parse(parts[1]),int.Parse(parts[2]));

            bool[] dPositions = new bool[parts.Length - 4];
            for (int i = 0; i < dPositions.Length; i++)
            {
                dPositions[i] = bool.Parse(parts[i + 4]);
            }

            UnitOrientation orientation = (UnitOrientation)int.Parse(parts[3]);
            if (parts[0] == "a")
                unit = new Aircraft(anchor, orientation);
            else if (parts[0] == "t")
                unit = new Thank(anchor, orientation);
            else if (parts[0] == "s2")
                unit = new Ship(2, anchor, orientation);
            else if (parts[0] == "s3")
                unit = new Ship(3, anchor, orientation);
            else
                unit = new Ship(4, anchor, orientation);

            unit.DamagedPosition = dPositions;

            return unit;
        }

        #endregion

    }
}
