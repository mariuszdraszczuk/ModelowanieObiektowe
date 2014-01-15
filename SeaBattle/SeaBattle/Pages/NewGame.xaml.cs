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


namespace SeaBattle.Pages
{
    public partial class NewGame : PhoneApplicationPage
    {
        public NewGame()
        {
            InitializeComponent();
        }

        private void B_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SelectMap.xaml" , UriKind.Relative));
            //DataBaseManager dbm = DataBaseManager.GetInstance();
            //List<Map> maps = dbm.GetMapTemplate();
            //bool x = true;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Jeszcze nie ma tej funkcjonalności ... ");
        }
    }
}