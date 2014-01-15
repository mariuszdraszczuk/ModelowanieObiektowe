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
using System.Windows.Media.Imaging;


namespace SeaBattle.GuiManagers
{
    public static class MapDrawingManager
    {
        public static void DrawMapTamplate(Canvas canvas, Map map)
        {
            int _width = map.Width;
            int _height = map.Hight;
            int _max = map.UnitMaxCount;

            canvas.Children.Clear();

            double imageWidth = canvas.Width / _width;
            double imageHeight = canvas.Height / _height;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Image img = new Image();
                    img.Name = "Image." + i.ToString() + '.' + j.ToString() + ".W";
                    //img.MouseLeftButtonDown += new MouseButtonEventHandler();
                    img.Width = imageWidth;
                    img.Height = imageHeight;

                    BitmapImage tn = new BitmapImage();
                    if(map.Fields[i,j].Type == FieldType.Water)
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

                    canvas.Children.Add(img);
                    Canvas.SetLeft(img, imageWidth * j);
                    Canvas.SetTop(img, imageHeight * i);
                }
            }
        }

        public static void DrawMap(Canvas canvas, Map map)
        {
            DrawMapTamplate(canvas, map);

            foreach (Unit item in map.units)
            {
                IntPoint[] points = item.GetUnitPoints();

                foreach (Image img in canvas.Children)
                {
                    string[] parts = img.Name.Split('.');

                    int y = int.Parse(parts[1]);
                    int x = int.Parse(parts[2]);

                    foreach (IntPoint point in points)
                    {
                        if (point.X == x && point.Y == y)
                            ImageManager.SetImageSource(img, "/Images/circle.png");
                    }
                }
            }
        }

        public static void DrawUnit(Canvas canvas, Unit unit)
        {
            IntPoint[] points = unit.GetUnitPoints();

            foreach (Image img in canvas.Children)
            {
                string[] parts = img.Name.Split('.');

                int y = int.Parse(parts[1]);
                int x = int.Parse(parts[2]);

                foreach (IntPoint point in points)
                {
                    if (point.X == x && point.Y == y)
                        ImageManager.SetImageSource(img, "/Images/circle.png");
                }
            }
        }

        public static void DrawPlayerState(Canvas canvas, Map map)
        {
            int _width = map.Width;
            int _height = map.Hight;
            int _max = map.UnitMaxCount;

            canvas.Children.Clear();

            double imageWidth = canvas.Width / _width;
            double imageHeight = canvas.Height / _height;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Image img = new Image();
                    img.Name = "Image." + i.ToString() + '.' + j.ToString();
                    img.Width = imageWidth;
                    img.Height = imageHeight;

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

                    canvas.Children.Add(img);
                    Canvas.SetLeft(img, imageWidth * j);
                    Canvas.SetTop(img, imageHeight * i);
                }
            }
        }

    }
}
