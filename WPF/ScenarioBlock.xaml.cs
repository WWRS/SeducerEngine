using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF
{
    public partial class ScenarioBlock : UserControl
    {
        private readonly string _path;
        private readonly Action<string> _loadPreGame;
        
        public ScenarioBlock(string path, Action<string> loadPreGame)
        {
            InitializeComponent();

            _path = path;
            _loadPreGame = loadPreGame;

            BitmapImage bitmapImage = new BitmapImage(new Uri(Path.Combine(path, "bg.png")));
            BgImage.Source = bitmapImage;

            StreamReader streamReader = new StreamReader(Path.Combine(path, "scenario.txt"));
            string title = streamReader.ReadLine();
            TitleLabel.Content = new TextBlock{ Text = title, TextWrapping = TextWrapping.Wrap };
            streamReader.Close();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            _loadPreGame.Invoke(_path);
        }
    }
}