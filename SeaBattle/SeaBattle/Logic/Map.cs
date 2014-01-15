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
using System.Collections.Generic;

namespace SeaBattle.Logic
{
    public class Map
    {
        //variables
        private int _hight;
        private int _width;
        private int _unitMax;

        private List<Unit> _units = new List<Unit>();
        private Field[,] _fields;

        #region properties

        public int Hight 
        {
            get 
            { 
                return _hight; 
            }
        }

        public int Width
        {
            get 
            { 
                return _width; 
            }
        }

        public  List<Unit> units
        {
            get 
            { 
                return _units;
            }
            set 
            { 
                _units = value;
            }
        }

        public Field[,] Fields
        {
            get 
            { 
                return _fields; 
            }
        }

        public int UnitMaxCount
        {
            get { return _unitMax; }
        }

        #endregion

        //constructor
        public Map(int height, int width, int unitMaxCount, Field[,] fieldTypes)
        {
            _hight = height;
            _width = width;
            _unitMax = unitMaxCount;
            _fields = fieldTypes;
        }

        //methods
        public bool AddUnit(Unit unit)
        {
            if (_units.Count < _unitMax)
            {
                
                IntPoint[] points = unit.GetUnitPoints();

                //foreach (IntPoint p in points)
                //{
                //    if (_fields[p.Y,p.X].IndexOfUnit != -1 ||
                //        (_fields[p.Y,p.X].Type == FieldType.Land && unit.GetType() == typeof(Ship))||
                //         (_fields[p.Y,p.X].Type == FieldType.Water && unit.GetType() == typeof(Thank)))
                //        return false;
                //}

                _units.Add(unit);

                foreach (IntPoint p in points)
                {
                    _fields[p.Y,p.X].IndexOfUnit = _units.Count - 1;
                }



                return true;
            }
            else
                return false;
        }

        public bool IsCorrectPosition(Unit unit)
        {
            IntPoint[] points = unit.GetUnitPoints();

            foreach (IntPoint p in points)
            {
                if (p.X < 0 || p.Y < 0 || p.X >= this.Width || p.Y >= this.Hight)
                    return false;
            }

            foreach (IntPoint p in points)
            {
                if (_fields[p.Y, p.X].IndexOfUnit != -1 ||
                    (_fields[p.Y, p.X].Type == FieldType.Land && unit.GetType() == typeof(Ship)) ||
                     (_fields[p.Y, p.X].Type == FieldType.Water && unit.GetType() == typeof(Thank)))
                    return false;
            }

            return true;
        }

        #region To database methods

        public string MapStructureToString()
        {
            string result = "#" + _hight.ToString() + "|" + _width.ToString() + "|" + _unitMax.ToString();

            for (int i = 0; i < _hight; i++)
                for (int j = 0; j < _width; j++)
			    {
                    result += "#" + ((int)_fields[i, j].Type).ToString() + "|" + _fields[i, j].IsDiscoverd.ToString();
                }
            

            return result;
        }
        public string UnitsToString()
        {
            string result = "";

            foreach (Unit u in _units)
            {
                result += u.ToString();
            }

            return result;
        }

        public static Map FromString(string mapStructureString,string unitsString)
        {
            string[] parts = mapStructureString.Split('#');
            string[] parameters = parts[1].Split('|');

            int w = int.Parse(parameters[0]);
            int h = int.Parse(parameters[1]);
            int max = int.Parse(parameters[2]);

            Field[,] fields = new Field[h, w];

            for (int i = 2; i < parts.Length ; i++)
            {
                string[] fieldInfo = parts[i].Split('|');
                FieldType type = (FieldType)int.Parse(fieldInfo[0]);
                bool isDiscover = bool.Parse(fieldInfo[1]);
                Field f = new Field(type);
                f.IsDiscoverd = isDiscover;
                int y = (int) ((i-2) / w);
                int x = (i - 2) % w;
                fields[y,x] = f;
            }
                 

            Map result = new Map(h, w, max, fields);

            string[] u = unitsString.Split('#');

            if (u[0] != "")
            {
                for (int i = 0; i < u.Length; i++)
                {
                    Unit un = Unit.FromString(u[i]);
                    result.AddUnit(un);
                }
            }
            return result;
        }

        public bool isAllUnitsDestoyed()
        {
            foreach (Unit item in _units)
            {
                if (!item.IsDestroyed())
                    return false;
            }
            return true;
        }

        #endregion
    }
}