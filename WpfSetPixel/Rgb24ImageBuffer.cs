using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfSetPixel
{
    /// <summary>
    /// 透過色を含まないRGB24形式の画像バッファーを表します。
    /// </summary>
    public class Rgb24ImageBuffer : ImageBuffer
    {
        /// <summary>
        /// 画像バッファーのサイズを指定してオブジェクトを初期化します。
        /// </summary>
        public Rgb24ImageBuffer(int width, int height) : base(width, height)
        {
            // nop
        }

        /// <summary>
        /// 指定した位置へ色を設定します。
        /// </summary>
        public override void SetPixel(int x, int y, Color c)
        {
            this.GetBufferIndex(x, y, out int xIndex, out int yIndex);
            this._buffer[xIndex + yIndex] = c.R;
            this._buffer[xIndex + yIndex + 1] = c.G;
            this._buffer[xIndex + yIndex + 2] = c.B;
        }

        /// <summary>
        /// 指定した位置へ色を設定します。
        /// </summary>
        public override Color GetPixel(int x, int y)
        {
            this.GetBufferIndex(x, y, out int xIndex, out int yIndex);

            return new Color()
            {
                R = this._buffer[xIndex + yIndex],
                G = this._buffer[xIndex + yIndex + 1],
                B = this._buffer[xIndex + yIndex + 2],
            };
        }

        // RGB24のRawStrideを計算する
        protected override int CalculateRawStride() => (this.Width * PixelFormats.Rgb24.BitsPerPixel + 7) / 8;

        // ユーザー値 → bute[]のR, G, Bへアクセスするための位置計算
        protected override void GetBufferIndex(int x, int y, out int xIndex, out int yIndex)
        {
            xIndex = x * 3;
            yIndex = y * this.RawStride;
        }

        public override BitmapSource ToImageSource()
        {
            return BitmapSource.Create(this.Width, this.Height, 96, 96, PixelFormats.Bgr24, null, this.GetBuffer(), this.RawStride);
        }
    }
}
