using System;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class GameButton : UserControl
    {
        private readonly Action<ButtonType, string> _callback;
        private readonly ButtonType _buttonType;
        private readonly string _videoPath;
        
        public GameButton(string text, ButtonType buttonType, string videoPath, Action<ButtonType, string> callback)
        {
            InitializeComponent();

            _buttonType = buttonType;
            _videoPath = videoPath;
            _callback = callback;

            ButtonLabel.Content = text;
            Button.Click += OnClicked;
        }

        private void OnClicked(object sender, RoutedEventArgs e)
        {
            _callback.Invoke(_buttonType, _videoPath);
        }
    }
}
