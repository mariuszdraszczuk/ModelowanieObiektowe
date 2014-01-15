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
using System.Collections.Generic;


namespace SeaBattle.Players
{
    public class AI : Player
    {

        public bool isHard
        {
            get;
            set;
        }

        Random rand = new Random();

        List<IntPoint> points = new List<IntPoint>();

        public override bool Shoot(IntPoint point, Map targetsMap)
        {
            int unitIndex = targetsMap.Fields[point.Y, point.X].IndexOfUnit;
            targetsMap.Fields[point.Y, point.X].IsDiscoverd = true;

            if (unitIndex == -1)
            {
                return false;
            }

            else
            {
                targetsMap.units[unitIndex].UpdateUnitState(point);

                if (targetsMap.units[unitIndex].IsDestroyed())
                {
                    MessageBox.Show("Zniszczyłes jednostke wroga");
                }


                if (point.X - 1 >= 0)
                    points.Add(new IntPoint(point.X - 1, point.Y));
                if (point.X + 1 < targetsMap.Width )
                    points.Add(new IntPoint(point.X + 1, point.Y));
                if (point.Y - 1 >= 0)
                    points.Add(new IntPoint(point.X, point.Y - 1));
                if (point.Y + 1 < targetsMap.Hight)
                    points.Add(new IntPoint(point.X, point.Y+1));

                return true;
            }
        }

        public AI(int ship2,int ship3,int ship4, int thank,int aircraft, Map map)
        {
            //this.PlayerMap = map;

            Field[,] fi = new Field[map.Hight, map.Width];


            for (int i = 0; i < map.Hight; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    Field f = new Field(map.Fields[i,j].Type);
                    fi[i, j] = f;
                }
            }

            PlayerMap = new Map(map.Hight, map.Width, map.UnitMaxCount, fi);


            this.PlayerMap.units = new System.Collections.Generic.List<Unit>();

            Name = "AI";

            for (int i = 0; i < ship2; i++)
            {
                IntPoint point = new IntPoint(2, 2);
                Ship ship = new Ship(2,point,UnitOrientation.Down);

                while (!map.IsCorrectPosition(ship))
                {
                    int x = rand.Next(100) % map.Width;
                    int y = rand.Next(100) % map.Hight;
                    UnitOrientation o = (UnitOrientation)(rand.Next(10) % 4);
                    ship.Orientation = o;
                    ship.Position = new IntPoint(x, y);
                }

                PlayerMap.AddUnit(ship);
            }

            for (int i = 0; i < ship3; i++)
            {
                IntPoint point = new IntPoint(2, 4);
                Ship ship = new Ship(3, point, UnitOrientation.Down);

                while (!map.IsCorrectPosition(ship))
                {
                    int x = rand.Next(100) % map.Width;
                    int y = rand.Next(100) % map.Hight;
                    UnitOrientation o = (UnitOrientation)(rand.Next(10) % 4);
                    ship.Orientation = o;
                    ship.Position = new IntPoint(x, y);
                }

                PlayerMap.AddUnit(ship);
            }

            for (int i = 0; i < ship4; i++)
            {
                IntPoint point = new IntPoint(4, 2);
                Ship ship = new Ship(4, point, UnitOrientation.Down);

                while (!map.IsCorrectPosition(ship))
                {
                    int x = rand.Next(100) % map.Width;
                    int y = rand.Next(100) % map.Hight;
                    UnitOrientation o = (UnitOrientation)(rand.Next(10) % 4);
                    ship.Orientation = o;
                    ship.Position = new IntPoint(x, y);
                }

                PlayerMap.AddUnit(ship);
            }

            for (int i = 0; i < thank; i++)
            {
                IntPoint point = new IntPoint(5, 5);
                Thank than = new Thank(point, UnitOrientation.Down);

                while (!map.IsCorrectPosition(than))
                {
                    int x = rand.Next(100) % map.Width;
                    int y = rand.Next(100) % map.Hight;
                    UnitOrientation o = (UnitOrientation)(rand.Next(10) % 4);
                    than.Orientation = o;
                    than.Position = new IntPoint(x, y);
                }

                PlayerMap.AddUnit(than);
            }

            for (int i = 0; i < aircraft; i++)
            {
                IntPoint point = new IntPoint(2, 2);
                Aircraft air = new Aircraft(point, UnitOrientation.Down);

                while (!map.IsCorrectPosition(air))
                {
                    int x = rand.Next(100) % map.Width;
                    int y = rand.Next(100) % map.Hight;
                    UnitOrientation o = (UnitOrientation)(rand.Next(10) % 4);
                    air.Orientation = o;
                    air.Position = new IntPoint(x, y);
                }

                PlayerMap.AddUnit(air);
            }
        }


        public IntPoint AIShot(Map map)
        {

            if (isHard && points.Count > 0)
            {
                IntPoint p = points[0];
                points.Remove(p);
                return p;
            }

            int x = rand.Next(100) % map.Width;
            int y = rand.Next(100) % map.Hight;

            while (map.Fields[y, x].IsDiscoverd)
            {
                x = rand.Next(100) % map.Width;
                y = rand.Next(100) % map.Hight;
            }

            return new IntPoint(x, y);
        }
    }
}
