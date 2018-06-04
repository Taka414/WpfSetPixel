using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfSetPixel
{
    /// <summary>
    /// 透過色を含むRGBA形式の画像バッファを表します。
    /// </summary>
    public class ArgbImageBuffer : ImageBuffer
    {
        /// <summary>
        /// 画像バッファーのサイズを指定してオブジェクトを初期化します。
        /// </summary>
        public ArgbImageBuffer(int width, int height) : base(width, height)
        {
            // nop
        }

        /// <summary>
        /// 指定した座標の色を更新します。
        /// </summary>
        public override void SetPixel(int x, int y, Color c)
        {
            this.GetBufferIndex(x, y, out int xIndex, out int yIndex);

            // チェックは必要なければコメントアウトしてもよい
            if (xIndex + yIndex < 0 || xIndex + yIndex + 3 > this._buffer.Length)
            {
                return;
            }

            // BGRA形式なので青→緑→緑→アルファの順になる
            this._buffer[xIndex + yIndex] = c.B;
            this._buffer[xIndex + yIndex + 1] = c.G;
            this._buffer[xIndex + yIndex + 2] = c.R;
            this._buffer[xIndex + yIndex + 3] = c.A;
        }

        /// <summary>
        /// 指定した位置へ色を取得します。
        /// </summary>
        public override Color GetPixel(int x, int y)
        {
            this.GetBufferIndex(x, y, out int xIndex, out int yIndex);
            return new Color()
            {
                B = this._buffer[xIndex + yIndex],
                G = this._buffer[xIndex + yIndex + 1],
                R = this._buffer[xIndex + yIndex + 2],
                A = this._buffer[xIndex + yIndex + 3],
            };
        }

        // RGB32のRawStrideを計算します。
        protected override int CalculateRawStride() => (this.Width * PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        // ユーザー値 → bute[]のA, R, G, Bへアクセスするためのインデックスの計算
        protected override void GetBufferIndex(int x, int y, out int xIndex, out int yIndex)
        {
            xIndex = x * 4;
            yIndex = y * this.RawStride;
        }

        public override BitmapSource ToImageSource()
        {
            return BitmapSource.Create(this.Width, this.Height, 96, 96, PixelFormats.Bgra32, null, this.GetBuffer(), this.RawStride);
        }
    }
}
