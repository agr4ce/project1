using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameStarts.Classes
{
    static internal class Img
    {
        static public BitmapImage ConvertImg(byte[] mass)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = new MemoryStream(mass);
            img.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            img.CacheOption = BitmapCacheOption.Default;
            img.EndInit();
            return img;
        }
    }
}
