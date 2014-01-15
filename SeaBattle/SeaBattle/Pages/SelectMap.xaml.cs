using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SeaBattle.Logic;
using SeaBattle.DataBase;
using SeaBattle.ViewModels;
using SeaBattle.GuiManagers;
using Microsoft.Phone.Shell;

namespace SeaBattle.Pages
{
    public partial class SelectMap : PhoneApplicationPage
    {

        private List<Map> maps;
        private List<string> models = new List<string>();

        public SelectMap()
        {
            InitializeComponent();
            DataBaseManager dbm = DataBaseManager.GetInstance();
            maps = dbm.GetMapTemplate();

            if(maps != null)
            {
                for (int i = 0; i < maps.Count; i++)
                {
                    MapViewModel mvm = MapViewModel.FromMap(maps[i]);
                    mvm.Id = i;
                    models.Add(mvm.ToString());
                }
            }

            lbSelectMap.ItemsSource = models;

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string uristring = "/Pages/GameSettings.xaml";

                PhoneApplicationService.Current.State["Map"] = maps[lbSelectMap.SelectedIndex];

                NavigationService.Navigate(new Uri(uristring, UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wybierz mape ");
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/Pages/NewGame.xaml", UriKind.Relative));
        }

        private void lbSelectMap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            GuiManagers.MapDrawingManager.DrawMapTamplate(cSelectedMap, maps[lbSelectMap.SelectedIndex]);
        }

        
    }
}