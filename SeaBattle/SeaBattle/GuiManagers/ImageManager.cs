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
    public static class ImageManager
    {
        public static void SetImageSource(Image img, string path)
        {
            //img = new Image();
            BitmapImage tn = new BitmapImage();
            Uri uri = new Uri(path, UriKind.Relative);
            tn.UriSource = uri;
            img.Stretch = Stretch.Fill;
            img.Source = tn;
        }
    }
}
