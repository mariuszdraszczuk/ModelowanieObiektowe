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

namespace SeaBattle.ViewModels
{
    public class MapViewModel
    {

        public int Id
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int MaxUnitCount
        {
            get;
            set;
        }

        public static MapViewModel FromMap(Map map)
        {
            MapViewModel result = new MapViewModel();

            result.Height = map.Hight;
            result.Width = map.Width;
            result.MaxUnitCount = map.UnitMaxCount;

            return result;
        }

        public override string ToString()
        {
            //return Id.ToString() + " " + Width.ToString() + " " + Height.ToString() + " " + MaxUnitCount.ToString() ;
            return string.Format("Id {0} Szerokość {1} Wyskość {2} Max Jedn {3}",
                Id.ToString(), Width.ToString(), Height.ToString(), MaxUnitCount.ToString());
        }
    }
}
