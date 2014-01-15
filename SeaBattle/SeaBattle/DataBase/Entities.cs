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
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq;

namespace SeaBattle.DataBase
{

    #region Map Template
    [Table(Name = "MapTemplates")]
    public class MapTemplate : PropertyChangedBase
    {
        private int _id;
        private string _mapString;
        private string _unitsString;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "Int NOT NULL IDENTITY", 
            CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
	        get
            {
                return _id;
            }
            set
            {
                if(value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        [Column(DbType = "NVarChar(1000) NOT NULL", CanBeNull = false)]
        public string MapString
        {
            get
            {
                return _mapString;
            }
            set
            {
                if (value != _mapString)
                {
                    _mapString = value;
                    NotifyPropertyChanged("MapString");
                }
            }
        }

        [Column(DbType = "NVarChar(1000) NOT NULL", CanBeNull = false)]
        public string UnitsString
        {
            get
            {
                return _unitsString;
            }
            set
            {
                if (value != _unitsString)
                {
                    _unitsString = value;
                    NotifyPropertyChanged("MapString");
                }
            }
        }
    }
    #endregion

}
