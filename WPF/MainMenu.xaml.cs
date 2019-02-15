using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class MainMenu : UserControl
    {
        private readonly ScenarioSelect _scenarioSelect;

        public MainMenu()
        {
            InitializeComponent();

            _scenarioSelect = new ScenarioSelect();
            
            MainResources.MainWindow.NullVideos();
        }

        private void OnPlayClicked(object sender, RoutedEventArgs e)
        {
            MainResources.MainWindow.MainPanel.Children.Add(_scenarioSelect);
            MainResources.MainWindow.MainPanel.Children.Remove(this);
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}