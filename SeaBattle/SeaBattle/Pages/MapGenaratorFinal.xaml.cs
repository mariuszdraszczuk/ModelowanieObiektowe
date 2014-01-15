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
using System.Windows.Media.Imaging;
using SeaBattle.Logic;

namespace SeaBattle.Pages
{
    public partial class MapGenaratorFinal : PhoneApplicationPage
    {

        private int _width;
        private int _height;
        private int _max;

        public MapGenaratorFinal()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string str = NavigationContext.QueryString["param"];
            string[] parts = str.Split('X');
            _width = int.Parse(parts[0]);
            _height = int.Parse(parts[1]);
            _max = int.Parse(parts[2]);

            double imageWidth = canvas1.Width / _width;
            double imageHeight = canvas1.Height / _height;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Image img = new Image();
                    img.Name = "Image." + i.ToString() + '.' + j.ToString()+".W";
                    img.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    img.Width = imageWidth;
                    img.Height = imageHeight;

                    BitmapImage tn = new BitmapImage();
                    Uri uri = new Uri("/Images/Water.png", UriKind.Relative);
                    tn.UriSource = uri;

                    img.Stretch = Stretch.Fill;
                    img.Source = tn;

                    canvas1.Children.Add(img);
                    Canvas.SetLeft(img, imageWidth * j);
                    Canvas.SetTop(img, imageHeight * i);
                }
            }

            base.OnNavigatedTo(e);
        }

        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            string[] parts = img.Name.Split('.');
            if (parts[parts.Length - 1] == "W")
            {

                string name = "";

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    name += parts[i] + '.';
                }

                img.Name = name + "E";

                BitmapImage tn = new BitmapImage();
                Uri uri = new Uri("/Images/Earth.png", UriKind.Relative);
                tn.UriSource = uri;
                img.Source = tn;
            }

            else
            {
                string name = "";

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    name += parts[i] + '.';
                }

                img.Name = name + "W";

                BitmapImage tn = new BitmapImage();
                Uri uri = new Uri("/Images/Water.png", UriKind.Relative);
                tn.UriSource = uri;
                img.Source = tn;
            }
        }

        private void tbnAddMap_Click(object sender, RoutedEventArgs e)
        {
            Field[,] fieldTypes = new Field[_height,_width];

            foreach (Image img in canvas1.Children)
            {
                string[] parts = img.Name.Split('.');

                int y = int.Parse(parts[1]);
                int x = int.Parse(parts[2]);
                bool isWater = (parts[3] == "W") ? true : false;

                fieldTypes[y, x] = (isWater == true) ? new Field(FieldType.Water) : new Field(FieldType.Land);
            }

            Map map = new Map(_height, _width, _max, fieldTypes);


            DataBase.DataBaseManager dm = DataBase.DataBaseManager.GetInstance();
            dm.InsertMapTemplate(map);

            MessageBox.Show("Mape dodano do bazy danych");

        }

        private void tbnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/MapGenerator.xaml", UriKind.Relative));
        }


    }
}