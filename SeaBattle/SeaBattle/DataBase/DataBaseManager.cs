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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Phone.Data.Linq;
using System.Collections.Generic;


namespace SeaBattle.DataBase
{


    public class DataBaseManager
    {
        //variables
        private string fileLocation;
        private static DataBaseManager instance;
        private MapDataContext dataContext;

        //properties
        public string FileLocation
        {
            get 
            {
                return fileLocation;
            }

            set
            {
                string[] parts = value.Split('.');

                if (parts[parts.Length - 1] == "sdf")
                    fileLocation = value;

                else
                    fileLocation = "wrong location";
            }
        }

        //constructor
        private DataBaseManager(string location)
        {
            FileLocation = location;
        }

        //methods
        public static DataBaseManager GetInstance(string location)
        {
            if (instance == null)
                instance = new DataBaseManager(location);

            else
                instance.FileLocation = location;

            return instance;
        }

        public static DataBaseManager GetInstance()
        {
            if (instance == null)
                instance = new DataBaseManager("");

            return instance;
        }

        public void CreateDatabase()
        {
            try
            {
                dataContext = new MapDataContext(fileLocation);

                if (!dataContext.DatabaseExists())
                {
                    dataContext.CreateDatabase();
                }

            }
            catch(Exception ex)
            {
                DataBaseException exception = new DataBaseException(DataBaseExceptionType.WrongDBContext, ex.Message);
                throw exception;
            }
        }

        public void InsertMapTemplate(Map map)
        {
            MapTemplate mt = new MapTemplate();
            mt.MapString = map.MapStructureToString();
            mt.UnitsString = map.UnitsToString();
            dataContext.MapsTemplates.InsertOnSubmit(mt);
            dataContext.SubmitChanges();
        }


        public List<Map> GetMapTemplate()
        {
            List<Map> maps = new List<Map>();

            foreach (MapTemplate item in dataContext.MapsTemplates)
            {
               maps.Add(Map.FromString(item.MapString, item.UnitsString));
            }

            return maps;
        }
        
    }
}
