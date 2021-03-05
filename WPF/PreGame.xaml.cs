using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF
{
    public partial class PreGame : UserControl
    {
        private readonly string _path;

        public PreGame(string path)
        {
            InitializeComponent();

            _path = path;

            BitmapImage bitmapImage = new BitmapImage(new Uri(Path.Combine(path, "bg.png")));
            BgImage.Source = bitmapImage;

            StreamReader streamReader = new StreamReader(Path.Combine(path, "scenario.txt"));
            string title = streamReader.ReadLine();
            TitleLabel.Content = new TextBlock{ Text = title, TextWrapping = TextWrapping.Wrap };
            string description = streamReader.ReadToEnd();
            DescriptionLabel.Content = new TextBlock{ Text = description, TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Left };
            streamReader.Close();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ScenarioSelect scenarioSelect = new ScenarioSelect();
            MainResources.MainWindow.MainPanel.Children.Add(scenarioSelect);
            MainResources.MainWindow.MainPanel.Children.Remove(this);
        }

        private void PlayButtonClicked(object sender, RoutedEventArgs e)
        {
            MainResources.Scores = new List<bool>();
            // life points represent the number of times a user can choose a wrong answer before losing the game
            MainResources.SetLifePoints(1);
            string startingVideo = Path.Combine(_path, "pre.avi");
            GameMenu gameMenu = new GameMenu(_path, 1, startingVideo);
            MainResources.MainWindow.MainPanel.Children.Add(gameMenu);
            MainResources.MainWindow.MainPanel.Children.Remove(this);
            MainResources.MainWindow.RemoveBackground();
        }
    }
}