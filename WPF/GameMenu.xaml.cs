using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WPF
{
    public partial class GameMenu : UserControl
    {
        private readonly string _path;
        private readonly int _storyPosition;

        private readonly Storyboard _fadeIn;
        private readonly Storyboard _fadeOut;

        private ButtonType _prevButtonType;
        private string _prevVideoPath;

        public GameMenu(string path, int storyPosition, string videoPath)
        {
            if (!Directory.Exists(Path.Combine(path, storyPosition.ToString())))
            {
                MainResources.MainWindow.MainPanel.Children.Add(new WinScreen(videoPath));
                MainResources.MainWindow.MainPanel.Children.Remove(this);
                return;
            }

            InitializeComponent();

            _fadeIn = FindResource("FadeInStoryboard") as Storyboard;
            Debug.Assert(_fadeIn != null, nameof(_fadeIn) + " != null");
            Storyboard.SetTarget(_fadeIn, GameMenuGrid);

            _fadeOut = FindResource("FadeOutStoryboard") as Storyboard;
            Debug.Assert(_fadeOut != null, nameof(_fadeOut) + " != null");
            _fadeOut.Completed += NextVideoStart;
            Storyboard.SetTarget(_fadeOut, GameMenuGrid);

            _path = path;
            _storyPosition = storyPosition;

            MainResources.MainWindow.PlayFile(videoPath, PrevVideoDone);

            AddButtons();
        }

        private void AddButtons()
        {
            List<GameButton> gameButtons = new List<GameButton>();
            using (StreamReader streamReader =
                new StreamReader(Path.Combine(_path, _storyPosition.ToString(), "options.txt")))
            {
                foreach (ButtonType b in Enum.GetValues(typeof(ButtonType)))
                {
                    string[] paths = Directory.GetFiles(Path.Combine(_path, _storyPosition.ToString()),
                        b.ToString().ToLower()[0] + "*.mp4");
                    foreach (string videoPath in paths)
                    {
                        GameButton gameButton = new GameButton(streamReader.ReadLine(), b, videoPath, ButtonClicked);
                        gameButtons.Add(gameButton);
                    }
                }
            }

            gameButtons.Shuffle();
            
            foreach (var gameButton in gameButtons)
                ButtonStackPanel.Children.Add(gameButton);
        }

        private void PrevVideoDone()
        {
            _fadeIn.Begin();
            IsEnabled = true;
        }

        private void ButtonClicked(ButtonType buttonType, string videoPath)
        {
            IsEnabled = false;

            _prevVideoPath = videoPath;
            _prevButtonType = buttonType;

            if (MainResources.Scores.Count < _storyPosition)
                MainResources.Scores.Add(buttonType != ButtonType.Lose);

            _fadeIn.Stop();
            _fadeOut.Begin();
        }

        private void NextVideoStart(object sender, EventArgs e)
        {
            GameMenu nextGameMenu;
            switch (_prevButtonType)
            {
                case ButtonType.Win:
                    nextGameMenu = new GameMenu(_path, _storyPosition + 1, _prevVideoPath);
                    MainResources.MainWindow.MainPanel.Children.Add(nextGameMenu);
                    MainResources.MainWindow.MainPanel.Children.Remove(this);
                    break;
                case ButtonType.Lose:
                    MainResources.MainWindow.PlayFile(_prevVideoPath, PrevVideoDone);
                    break;
                case ButtonType.End:
                    nextGameMenu = new GameMenu(_path, -1, _prevVideoPath);
                    MainResources.MainWindow.MainPanel.Children.Add(nextGameMenu);
                    MainResources.MainWindow.MainPanel.Children.Remove(this);
                    break;
                // This should never happen!
                default:
                    MainResources.MainWindow.PlayFile(_prevVideoPath, PrevVideoDone);
                    break;
            }
        }
    }
}