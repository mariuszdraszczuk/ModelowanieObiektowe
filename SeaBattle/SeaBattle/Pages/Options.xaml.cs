﻿using System;
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
using Microsoft.Phone.Shell;

namespace SeaBattle.Pages
{
    public partial class Options : PhoneApplicationPage
    {
        public Options()
        {
            InitializeComponent();
        }

        private void B_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["IsHard"] = true;
        }
    }
}