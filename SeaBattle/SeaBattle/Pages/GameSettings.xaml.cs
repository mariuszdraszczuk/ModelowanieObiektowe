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
using System.Windows.Media.Imaging;
using SeaBattle.GuiManagers;
using SeaBattle.Logic;
using Microsoft.Phone.Shell;

namespace SeaBattle.Pages
{
    public partial class GameSettings : PhoneApplicationPage
    {

        private string mapTemplateString;
        private int maxCount;

        

        public GameSettings()
        {

            InitializeComponent();

            ImageManager.SetImageSource(iShip2, "/Images/Ship2.png");
            ImageManager.SetImageSource(iShip3, "/Images/ship3.png");
            ImageManager.SetImageSource(iShip4, "/Images/Ship4.png");
            ImageManager.SetImageSource(iThank, "/Images/Thank.png");
            ImageManager.SetImageSource(iAirCraft, "/Images/aircraft.png");

            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("Map"))
	        {
	                Map m = PhoneApplicationService.Current.State["Map"] as Map;
                    if (m != null)
                        maxCount = m.UnitMaxCount;
	        }

            base.OnNavigatedTo(e);
        }

        private void bNext_Click(object sender, RoutedEventArgs e)
        {

            string uriString = string.Format(@"/Pages/SetUnitsPositions.xaml?param={0}@{1}@{2}@{3}@{4}@{5}", tbShip2.Text,
                tbShip3.Text, tbShip4.Text, tbThank.Text, tbAirCraft.Text,tbxNick.Text);



            NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
        }
    }
}