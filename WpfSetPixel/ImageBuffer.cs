using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfSetPixel
{
    // 画像バッファーの基底クラス
    public abstract class ImageBuffer
    {
        //
        // Fields
        // - - - - - - - - - - - - - - - - - - - -

        protected byte[] _buffer;

        //
        // Props
        // - - - - - - - - - - - - - - - - - - - -

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int RawStride { get; private set; }

        //
        // Constructors
        // - - - - - - - - - - - - - - - - - - - -

        /// <summary>
        /// 画像バッファーのサイズを指定してオブジェクトを初期化します。
        /// </summary>
        public ImageBuffer(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.RawStride = this.CalculateRawStride();
            this._buffer = new byte[this.RawStride * height];
        }

        //
        // Methods
        // - - - - - - - - - - - - - - - - - - - -

        /// <summary>
        /// 指定した座標の色を更新します。
        /// </summary>
        public abstract void SetPixel(int x, int y, Color c);

        /// <summary>
        /// 指定した位置へ色を取得します。
        /// </summary>
        public abstract Color GetPixel(int x, int y);

        /// <summary>
        /// 現在のバッファーを取得します。
        /// </summary>
        public virtual byte[] GetBuffer()
        {
            byte[] _tempBuffer = new byte[this._buffer.Length]; // 複製して返す
            for (int i = 0; i < this._buffer.Length; i++)
            {
                _tempBuffer[i] = this._buffer[i];
            }
            return _tempBuffer;
        }

        //
        // Abstract Methods
        // - - - - - - - - - - - - - - - - - - - -

        // バッファーの1行の長さの計算方法を実装します。
        protected abstract int CalculateRawStride();

        // バッファーの各ピクセルにアクセスするためにインデックスの計算方法を実装します。
        protected abstract void GetBufferIndex(int x, int y, out int xIndex, out int yIndex);

        // 現在のオブジェクトの内容をBitmapSourceへ変換する方法を実装します。
        public abstract BitmapSource ToImageSource();

        //
        // Utilities
        // - - - - - - - - - - - - - - - - - - - -

        /// <summary>
        /// 全ピクセルを列挙して述語による処理を行います。
        /// </summary>
        public void ForEach(Func<int, int, Color> action)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.SetPixel(x, y, action.Invoke(x, y));
                }
            }
        }

        /// <summary>
        /// 現在のオブジェクトの内容を指定した位置へ保存します。encoderを指定しない場合PNG形式で保存します。
        /// </summary>
        public void SaveImage(string path, BitmapEncoder encoder = null)
        {
            WpfImageUtil.SaveImage(this.ToImageSource(), path, encoder);
        }
    }
}
