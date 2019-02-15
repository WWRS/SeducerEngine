using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WPF
{
    public partial class WinScreen : UserControl
    {
        public WinScreen(string videoPath)
        {
            InitializeComponent();
            
            MainResources.MainWindow.PlayFile(videoPath, PrevVideoDone);
        }
        
        private void PrevVideoDone()
        {
            Storyboard fadeIn = FindResource("FadeInStoryboard") as Storyboard;
            Debug.Assert(fadeIn != null, nameof(fadeIn) + " != null");
            Storyboard.SetTarget(fadeIn, MainGrid);
            fadeIn.Begin();
            IsEnabled = true;
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            MainResources.MainWindow.AddBackground();
            MainResources.MainWindow.MainPanel.Children.Add(new MainMenu());
            MainResources.MainWindow.MainPanel.Children.Remove(this);
        }
    }
}
