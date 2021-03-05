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
            bool gameOver = MainResources.GetLifePoints() < 0;

            // if we are at -1 story position or no life points, it's game over
            if (storyPosition == -1 || gameOver)
            {
                // lost the game, show LoseScreen
                var lost = new LoseScreen(videoPath);
                MainResources.MainWindow.MainPanel.Children.Add(lost);
                MainResources.MainWindow.MainPanel.Children.Remove(this);
                return;
            }

            // if there are no more directories for the scenario, consider it a win
            if (!Directory.Exists(Path.Combine(path, storyPosition.ToString())))
            {
                // show win screen
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
                        b.ToString().ToLower()[0] + "*.avi");
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
                    // will still advance to the next video, only a "Lose" button type will reduce the life points
                    MainResources.ReduceLifePoints(1);
                    nextGameMenu = new GameMenu(_path, _storyPosition + 1, _prevVideoPath);
                    MainResources.MainWindow.MainPanel.Children.Add(nextGameMenu);
                    MainResources.MainWindow.MainPanel.Children.Remove(this);
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