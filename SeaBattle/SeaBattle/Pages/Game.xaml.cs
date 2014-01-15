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
using SeaBattle.Players;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using SeaBattle.GuiManagers;
using System.Windows.Media.Imaging;
using SeaBattle.Logic;


namespace SeaBattle.Pages
{
    public partial class Game : PhoneApplicationPage
    {

        //private LGame game;

        private int time;

        private int ship2;
        private int ship3;
        private int ship4;
        private int thanks;
        private int aircraft;
        private bool isUserTour;
        private UserPlayer player1;
        private AI computer;
        private DispatcherTimer dt = new DispatcherTimer();

        string name;

        bool isBegin = true;

        public Game()
        {
            InitializeComponent();
            dt.Interval = TimeSpan.FromSeconds(1.0);
            time = 10;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            string[] parts = NavigationContext.QueryString["param"].Split('@');


            ship2 = int.Parse(parts[0]);
            ship3 = int.Parse(parts[1]);
            ship4 = int.Parse(parts[2]);
            thanks = int.Parse(parts[3]);
            aircraft = int.Parse(parts[4]);
            
            name = parts[5];

            tbUser.Text = name;
            tbOponent.Text = "AI";

            player1 = new UserPlayer();

            Map  aimap = PhoneApplicationService.Current.State["Map"] as Map;
            
            computer = new AI(ship2, ship3, ship4, thanks, aircraft, aimap);

            if (PhoneApplicationService.Current.State.ContainsKey("UserMap"))
            {
                player1.PlayerMap = PhoneApplicationService.Current.State["UserMap"] as Map;
            }
            if (PhoneApplicationService.Current.State.ContainsKey("IsHard"))
            {
                computer.isHard = (bool)PhoneApplicationService.Current.State["IsHard"];
            }
            //MapDrawingManager.DrawMap(cView, computer.PlayerMap);

            base.OnNavigatedTo(e);
        }

        private void tbnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void tbnStartStop_Click(object sender, RoutedEventArgs e)
        {
            isUserTour = true;
            dt.Start();
            dt.Tick += new EventHandler(dt_Tick);

            int _width = computer.PlayerMap.Width;
            int _height = computer.PlayerMap.Hight;
            int _max = computer.PlayerMap.UnitMaxCount;

            cView.Children.Clear();

            double imageWidth = cView.Width / _width;
            double imageHeight = cView.Height / _height;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Image img = new Image();
                    img.Name = "Image." + i.ToString() + '.' + j.ToString() + ".W";
                    img.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseLeftButtonDown);
                    img.Width = imageWidth;
                    img.Height = imageHeight;

                        BitmapImage tn = new BitmapImage();
                        if (computer.PlayerMap.Fields[i, j].Type == FieldType.Water)
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
                    
                    cView.Children.Add(img);
                    Canvas.SetLeft(img, imageWidth * j);
                    Canvas.SetTop(img, imageHeight * i);
                }
            }
        }

        void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;


            if (img != null)
            {
                string[] parts = img.Name.Split('.');

                int y = int.Parse(parts[1]);
                int x = int.Parse(parts[2]);


                if (!computer.PlayerMap.Fields[y, x].IsDiscoverd)
                {
                    bool result = player1.Shoot(new IntPoint(x, y), computer.PlayerMap);

                    if (result == true)
                    {
                        bool w = computer.PlayerMap.isAllUnitsDestoyed();

                        if (w)
                            MessageBox.Show("Gratulacje, wygrałeś");
                        else
                        {
                            time = 10;

                            BitmapImage tn = new BitmapImage();
                            Uri uri = new Uri("/Images/Score.png", UriKind.Relative);
                            tn.UriSource = uri;
                            img.Stretch = Stretch.Fill;
                            img.Source = tn;
                        }
                    }

                    else
                    {
                        time = 10;
                        isUserTour = false;

                       MapDrawingManager.DrawPlayerState(cView,player1.PlayerMap);


                    }

                }

            }
        }


        private void DrawComputerState(Map map)
        {
            int _width = map.Width;
            int _height = map.Hight;
            int _max = map.UnitMaxCount;

            cView.Children.Clear();

            double imageWidth = cView.Width / _width;
            double imageHeight = cView.Height / _height;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Image img = new Image();
                    img.Name = "Image." + i.ToString() + '.' + j.ToString();
                    img.Width = imageWidth;
                    img.Height = imageHeight;

                    img.MouseLeftButtonDown +=new MouseButtonEventHandler(img_MouseLeftButtonDown);

                    if (map.Fields[i, j].IsDiscoverd == false)
                    {
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
                    }

                    else
                    {
                        BitmapImage tn = new BitmapImage();
                        if (map.Fields[i, j].HaveUnit == true)
                        {
                            Uri uri = new Uri("/Images/Score.png", UriKind.Relative);
                            tn.UriSource = uri;
                        }
                        else
                        {
                            Uri uri = new Uri("/Images/Missed.png", UriKind.Relative);
                            tn.UriSource = uri;
                        }
                        img.Stretch = Stretch.Fill;
                        img.Source = tn;
                    }

                    cView.Children.Add(img);
                    Canvas.SetLeft(img, imageWidth * j);
                    Canvas.SetTop(img, imageHeight * i);
                }
            }
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                isUserTour = false;
            }
            else
            {
                time = time - 1;
                if (time == 5 && isUserTour == false)
                {
                    IntPoint point = computer.AIShot(player1.PlayerMap);
                    bool result = computer.Shoot(point, player1.PlayerMap);


                    if (result == true)
                    {
                        bool w = player1.PlayerMap.isAllUnitsDestoyed();

                        if (w)
                            MessageBox.Show("Niestety przegrałeś");
                        else
                        {
                            time = 10;

                            BitmapImage tn = new BitmapImage();
                            Uri uri = new Uri("/Images/Score.png", UriKind.Relative);
                            tn.UriSource = uri;

                            foreach (Image item in cView.Children)
                            {
                                string[] parts = item.Name.Split('.');

                                int y = int.Parse(parts[1]);
                                int x = int.Parse(parts[2]);

                                if (point.X == x && point.Y == y)
                                {
                                    item.Stretch = Stretch.Fill;
                                    item.Source = tn;
                                }

                            }

                        }
                    }
                    else
                    {
                        time = 10;
                        isUserTour = true;

                        DrawComputerState(computer.PlayerMap);

                    }


                }
            }
        }
        
       
       

    }
}