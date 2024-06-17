using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pokemons.Model
{
    static class Converter
    {
        /// <summary>
        /// Кодирование изображения
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static byte[] EncodingImageToArray(BitmapImage bitmapImage)
        {
            using (MemoryStream memstr = new())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memstr);
                return memstr.ToArray();
            }
        }
        /// <summary>
        /// Декаодирование изображения
        /// </summary>
        /// <param name="bytesOfImage"></param>
        /// <returns></returns>
        public static BitmapImage DecodingArrayToImage(byte[] bytesOfImage)
        {
            using (var ms = new MemoryStream(bytesOfImage))
            {
                var image = new BitmapImage();
                ms.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
  
    }
}
