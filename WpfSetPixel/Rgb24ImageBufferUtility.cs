using System.Windows.Media;

namespace WpfSetPixel.Debug
{
    /// <summary>
    /// 画像操作に関する汎用処理を提供します。
    /// </summary>
    public static class Rgb24ImageBufferUtility
    {
        // needs "System.Drawing.dll"

        /// <summary>
        /// 指定したファイルパスからバッファーを作成します。
        /// </summary>
        public static Rgb24ImageBuffer CreateBuffer(string path)
        {
            var _bitmap = new System.Drawing.Bitmap(path);
            var buffer = new Rgb24ImageBuffer(_bitmap.Width, _bitmap.Height);

            buffer.ForEach((x, y) =>
            {
                System.Drawing.Color color = _bitmap.GetPixel(x, y);
                return Color.FromRgb(color.R, color.G, color.B);
            });

            return buffer;
        }
    }
}
