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
    public static class UnitPointsCounter
    {
        public static IntPoint[] GetShipPoints(IntPoint position, UnitOrientation orientation, int size)
        {
            IntPoint[] points = new IntPoint[size];
            points[0] = position;
            if (orientation == UnitOrientation.Up)
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X, position.Y - 1 - i);
                    points[i + 1] = p;
                }
            }
            else if (orientation == UnitOrientation.Down)
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X, position.Y + 1 + i);
                    points[i + 1] = p;
                }
            }
            else if (orientation == UnitOrientation.Left)
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X - 1 - i, position.Y);
                    points[i + 1] = p;
                }
            }
            else
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X + 1 + i, position.Y);
                    points[i + 1] = p;
                }
            }
            return points;
        }

        public static IntPoint[] GetThankPoints(IntPoint position, UnitOrientation orientation, int size)
        {
            IntPoint[] points = new IntPoint[size];
            points[0] = position;
            if (orientation == UnitOrientation.Up)
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X, position.Y - 1 - i);
                    points[i + 1] = p;
                }
            }
            else if (orientation == UnitOrientation.Down)
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X, position.Y + 1 + i);
                    points[i + 1] = p;
                }
            }
            else if (orientation == UnitOrientation.Left)
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X - 1 - i, position.Y);
                    points[i + 1] = p;
                }
            }
            else
            {
                for (int i = 0; i < size-1; i++)
                {
                    IntPoint p = new IntPoint(position.X + 1 + i, position.Y);
                    points[i + 1] = p;
                }
            }
            return points;
        }

        public static IntPoint[] GetAircraftPoints(IntPoint position, UnitOrientation orientation, int size)
        {
            IntPoint[] points = new IntPoint[size];
            points[0] = position;
            if (orientation == UnitOrientation.Up)
            {
                points[1] = new IntPoint(position.X - 1, position.Y - 1);
                points[2] = new IntPoint(position.X , position.Y - 1);
                points[3] = new IntPoint(position.X + 1, position.Y - 1);
            }
            else if (orientation == UnitOrientation.Down)
            {
                points[1] = new IntPoint(position.X - 1, position.Y + 1);
                points[2] = new IntPoint(position.X, position.Y + 1);
                points[3] = new IntPoint(position.X + 1, position.Y + 1);
            }
            else if (orientation == UnitOrientation.Left)
            {
                points[1] = new IntPoint(position.X - 1, position.Y - 1);
                points[2] = new IntPoint(position.X - 1, position.Y );
                points[3] = new IntPoint(position.X - 1, position.Y + 1);
            }
            else
            {
                points[1] = new IntPoint(position.X + 1, position.Y - 1);
                points[2] = new IntPoint(position.X + 1, position.Y);
                points[3] = new IntPoint(position.X + 1, position.Y + 1);
            }
            return points;
        }
    }
}
