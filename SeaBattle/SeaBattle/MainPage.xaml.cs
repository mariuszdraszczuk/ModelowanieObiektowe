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
using System.IO.IsolatedStorage;
using SeaBattle.DataBase;

namespace SeaBattle
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            //bool x = isf.FileExists("Data Source=isostore:/DataBase.sdf");
            DataBaseManager dbManager = DataBaseManager.GetInstance("Data Source=isostore:/DataBase.sdf");
            try
            {
                dbManager.CreateDatabase();
            }
            catch(DataBaseException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/NewGame.xaml", UriKind.Relative));
        }

        private void B_Load_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/LoadGame.xaml", UriKind.Relative));
        }

        private void B_Options_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Options.xaml", UriKind.Relative));
        }

        private void B_Statistics_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Statistics.xaml", UriKind.Relative));
        }

        private void B_MapGenerator_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MapGenerator.xaml", UriKind.Relative));
        }


       
    }
}