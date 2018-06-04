using System.IO;
using System.Windows.Media.Imaging;

namespace WpfSetPixel
{
    /// <summary>
    /// WPFのImage関係の汎用操作を提供します。
    /// </summary>
    public static class WpfImageUtil
    {
        /// <summary>
        /// 現在のオブジェクトの内容を指定した位置へ保存します。encoderを指定しない場合PNG形式で保存します。
        /// </summary>
        public static void SaveImage(BitmapSource source, string path, BitmapEncoder encoder = null)
        {
            // .NETでは以下クラスが用意されている
            //   System.Windows.Media.Imaging.BmpBitmapEncoder
            //   System.Windows.Media.Imaging.GifBitmapEncoder
            //   System.Windows.Media.Imaging.JpegBitmapEncoder
            //   System.Windows.Media.Imaging.PngBitmapEncoder
            //   System.Windows.Media.Imaging.TiffBitmapEncoder
            //   System.Windows.Media.Imaging.WmpBitmapEncoder

            BitmapEncoder _temp_encoder = encoder;
            if (encoder == null)
            {
                _temp_encoder = new PngBitmapEncoder();
            }

            using (FileStream fs = File.Create(path))
            {
                _temp_encoder.Frames.Add(BitmapFrame.Create(source));
                _temp_encoder.Save(fs);
            }
        }
    }
}
