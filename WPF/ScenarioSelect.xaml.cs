using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WPF
{
    public partial class ScenarioSelect : UserControl
    {
        public ScenarioSelect()
        {
            InitializeComponent();

            string scenarioPath = Path.Combine("file:///", Directory.GetCurrentDirectory(), @"Assets\Scenarios");
            string[] paths = Directory.GetDirectories(scenarioPath);
            foreach (string path in paths)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition {Width = GridLength.Auto};
                ScrollGrid.ColumnDefinitions.Add(columnDefinition);
                ScenarioBlock scenarioBlock = new ScenarioBlock(path, StartGame);
                ScrollGrid.Children.Add(scenarioBlock);
                Grid.SetColumn(scenarioBlock, ScrollGrid.ColumnDefinitions.Count - 1);
                Grid.SetRow(scenarioBlock, 0);
            }
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            Debug.Assert(scrollViewer != null, nameof(scrollViewer) + " != null");

            for (int i = 0; i < 50; i++)
                if (e.Delta > 0)
                    scrollViewer.LineLeft();
                else
                    scrollViewer.LineRight();

            e.Handled = true;
        }

        private void StartGame(string path)
        {
            PreGame preGame = new PreGame(path);
            MainResources.MainWindow.MainPanel.Children.Add(preGame);
            MainResources.MainWindow.MainPanel.Children.Remove(this);
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class ScrollGridHeightConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return Math.Max((double) value - 40 , 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.Assert(value != null, nameof(value) + " != null");
            return (double) value + 40;
        }
    }
}