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
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using SeaBattle.GuiManagers;

namespace SeaBattle.Pages
{

    enum UnitType {Ship2, Ship3, Ship4, Thank, AirCraft} 

    public partial class SetUnitsPositions : PhoneApplicationPage
    {

        private Map map;

        private bool canStart = false;

        private int maxCount;

        private int ship2;
        private int ship3;
        private int ship4;
        private int thanks;
        private int aircraft;

        private int current = 1;

        private Unit currentUnit;

        string name;


        public SetUnitsPositions()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (canStart)
            {
                string uriString = string.Format(@"/Pages/Game.xaml?param={0}@{1}@{2}@{3}@{4}@{5}", ship2.ToString(),
                ship3.ToString(), ship4.ToString(), thanks.ToString(), aircraft.ToString(), name);
                PhoneApplicationService.Current.State["UserMap"] = map;
                NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            #region inicjalizacja
            string[] parts = NavigationContext.QueryString["param"].Split('@'); 


            ship2 = int.Parse(parts[0]);
            ship3 = int.Parse(parts[1]);
            ship4 = int.Parse(parts[2]);
            thanks = int.Parse(parts[3]);
            aircraft = int.Parse(parts[4]);

            name = parts[5];

            if (PhoneApplicationService.Current.State.ContainsKey("Map"))
            {
                map = PhoneApplicationService.Current.State["Map"] as Map;
                if (map != null)
                    maxCount = map.UnitMaxCount;
            }
            #endregion

            #region inicjalizacja mapy

            int _width = map.Width;
            int _height = map.Hight;
            int _max = map.UnitMaxCount;

            cMap.Children.Clear();

            double imageWidth = cMap.Width / _width;
            double imageHeight = cMap.Height / _height;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Image img = new Image();
                    img.Name = "Image." + i.ToString() + '.' + j.ToString();
                    img.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    img.Width = imageWidth;
                    img.Height = imageHeight;

                    BitmapImage tn = new BitmapImage();
                    if (map.Fields[i, j].Type == FieldType.Water)
                    {
                        Uri uri = new Uri("/Images/Water.png", UriKind.Relative);
                        tn.UriSource = uri;
                    }
                    else
                    {
                        Uri uri = new Uri("/Images/Earth.png", UriKind.Relative);
                        tn.UriSource = uri;
                    }
                    img.Stretch = Stretch.Fill;
                    img.Source = tn;

                    cMap.Children.Add(img);
                    Canvas.SetLeft(img, imageWidth * j);
                    Canvas.SetTop(img, imageHeight * i);
                }
            }

            #endregion

            SetCurrentImage();

            base.OnNavigatedTo(e);
        }

        public void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            
            if (img != null)
            {
                string[] parts = img.Name.Split('.');

                int y = int.Parse(parts[1]);
                int x = int.Parse(parts[2]);

                if (currentUnit == null || currentUnit.Position.X != x || currentUnit.Position.Y != y)
                {
                    UnitType t = CurrentUnit();
                    currentUnit = CreateUnit(t, y, x);
                }
                else
                {
                    IntPoint[] points = currentUnit.GetUnitPoints();
                    string str = "";
                    foreach (Image item in cMap.Children)
                    {
                        string[] parts1 = item.Name.Split('.');

                        int y1 = int.Parse(parts1[1]);
                        int x1 = int.Parse(parts1[2]);

                        foreach (IntPoint point in points)
                        {
                            if (y1 == point.Y && x1 == point.X)
                            {
                                BitmapImage tn = new BitmapImage();
                                if (map.Fields[y1, x1].Type == FieldType.Water)
                                {
                                    Uri uri = new Uri("/Images/Water.png", UriKind.Relative);
                                    tn.UriSource = uri;
                                }
                                else
                                {
                                    Uri uri = new Uri("/Images/Earth.png", UriKind.Relative);
                                    tn.UriSource = uri;
                                }
                                item.Stretch = Stretch.Fill;
                                item.Source = tn;
                            }
                        }

                        str += item.Name;
                    }
                    int o = (int)currentUnit.Orientation;
                    o = (o + 1) % 4;
                    currentUnit.Orientation = (UnitOrientation)o;
                }
                while (!map.IsCorrectPosition(currentUnit))
                {
                    int o = (int)currentUnit.Orientation;
                    o = (o + 1) % 4;
                    currentUnit.Orientation = (UnitOrientation)o;
                }

                //DrawMapTamplate2(cMap, map);
                MapDrawingManager.DrawUnit(cMap, currentUnit);

            }


        }

        private UnitType CurrentUnit()
        {
            if (current <= ship2)
                return UnitType.Ship2;
            else if (current <= ship2 + ship3)
                return UnitType.Ship3;
            else if (current <= ship2 + ship3 + ship4)
                return UnitType.Ship4;
            else if (current <= ship2 + ship3 + ship4 + thanks)
                return UnitType.Thank;
            else return UnitType.AirCraft;
        }

        private void SetCurrentImage()
        {
            UnitType type = CurrentUnit();

            if (type == UnitType.Ship2)
                ImageManager.SetImageSource(image1, "/Images/Ship2.png");
            else if (type == UnitType.Ship3)
                ImageManager.SetImageSource(image1, "/Images/Ship3.png");
            else if (type == UnitType.Ship4)
                ImageManager.SetImageSource(image1, "/Images/Ship4.png");
            else if (type == UnitType.Thank)
                ImageManager.SetImageSource(image1, "/Images/Thank.png");
            else if (type == UnitType.AirCraft)
                ImageManager.SetImageSource(image1, "/Images/AirCraft.png");
        }

        private Unit CreateUnit(UnitType type,int y, int x)
        {
            if (type == UnitType.Ship2)
                return new Ship(2, new IntPoint(x, y), 0);
            else if (type == UnitType.Ship3)
                return new Ship(3, new IntPoint(x, y), 0);
            else if (type == UnitType.Ship4)
                return new Ship(4, new IntPoint(x, y), 0);
            else if (type == UnitType.Thank)
                return new Thank(new IntPoint(x, y), 0);
            else
                return new Aircraft(new IntPoint(x, y), 0);
        }

        private void tbnCommit_Click(object sender, RoutedEventArgs e)
        {
            map.AddUnit(currentUnit);
            current += 1;
            if (current > maxCount)
            {
                MessageBox.Show("Ustalono wszystkie pozycje ");
                canStart = true;
                return;
            }
            SetCurrentImage();
            currentUnit = null;
        }

        
    }
}