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
using System.IO.IsolatedStorage;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq;

namespace SeaBattle.DataBase
{
    public class MapDataContext : DataContext
    {
        public MapDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<MapTemplate> MapsTemplates;


    }
}
