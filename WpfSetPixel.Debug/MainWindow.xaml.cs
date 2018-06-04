using MahApps.Metro.Controls;
using System.Windows.Media;

namespace WpfSetPixel.Debug
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // 画像サイズを指定してオブジェクトを作成
            var buffer = new Rgb24ImageBuffer(850, 600);
            // 透過画像を扱い以下を指定する
            // var buffer = new ArgbImageBuffer(850, 600);

            // 全ての画素を列挙する場合、ImageBufferのForEachを使用できる
            buffer.ForEach((x, y) =>
            {
                if(x%10 > 6)
                {
                    return Color.FromRgb(160, 160, 160); // しましまを書く
                }

                return Color.FromRgb(40, 40, 40); // 基本的に灰色

                // 透過する場合は以下のように指定
                // Color.FromArgb(128, 40, 40, 40);
            });

            // BitmapImageに画像を設定する
            this.img.Source = buffer.ToImageSource();

            // ブラシを作成する場合以下のように書く
            var brush = new ImageBrush() { ImageSource = buffer.ToImageSource() };
        }
    }
}
