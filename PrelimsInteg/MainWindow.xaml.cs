using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace PrelimsInteg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _name = "";
        private int _score = 0;
        private int _finalTime = 0;
        private int _difficulty = 1;

        DispatcherTimer _dt = null;
        Random _rnd = new Random();
        private int _round = 1;
        private double _elapsedTime = 0;
        private double _gameTime = 0;
        private int _points = 0;

        public MainWindow(int difficulty, string name, double time)
        {
            _name = name;
            _difficulty = difficulty;
            _gameTime = time;
            _points = PointSystem();

            InitializeComponent();

            _dt = new DispatcherTimer();
            _dt.Tick += _dt_Tick;
            _dt.Interval = GetDifficultyInterval();
            _dt.Start();

            tbConvNum.Text = _rnd.Next(1, GetNumberRange()).ToString();
            lbRoundCount.Content = _round;
            lbTimer.Content = _gameTime;

        }

        #region Button Events
        private void btn128_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin128.Text == "0")
            {
                tbBin128.Text = "1";
                btn128.Background = Brushes.Green;
            }
            else
            {
                tbBin128.Text = "0";
                btn128.Background = Brushes.Red;
            }
        }

        private void btn64_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin64.Text == "0")
            {
                tbBin64.Text = "1";
                btn64.Background = Brushes.Green;
            }
            else
            {
                tbBin64.Text = "0";
                btn64.Background = Brushes.Red;
            }
        }

        private void btn32_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin32.Text == "0")
            {
                tbBin32.Text = "1";
                btn32.Background = Brushes.Green;
            }
            else
            {
                tbBin32.Text = "0";
                btn32.Background = Brushes.Red;
            }
        }

        private void btn16_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin16.Text == "0")
            {
                tbBin16.Text = "1";
                btn16.Background = Brushes.Green;
            }
            else
            {
                tbBin16.Text = "0";
                btn16.Background = Brushes.Red;
            }
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin8.Text == "0")
            {
                tbBin8.Text = "1";
                btn8.Background = Brushes.Green;
            }
            else
            {
                tbBin8.Text = "0";
                btn8.Background = Brushes.Red;
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin4.Text == "0")
            {
                tbBin4.Text = "1";
                btn4.Background = Brushes.Green;
            }
            else
            {
                tbBin4.Text = "0";
                btn4.Background = Brushes.Red;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin2.Text == "0")
            {
                tbBin2.Text = "1";
                btn2.Background = Brushes.Green;
            }
            else
            {
                tbBin2.Text = "0";
                btn2.Background = Brushes.Red;
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tbBin1.Text == "0")
            {
                tbBin1.Text = "1";
                btn1.Background = Brushes.Green;
            }
            else
            {
                tbBin1.Text = "0";
                btn1.Background = Brushes.Red;
            }
        }
        #endregion
        private void _dt_Tick(object sender, EventArgs e)
        {
            _elapsedTime++;
            int time = int.Parse(lbTimer.Content.ToString());

            if (time != 0)
            {
                time--;
                lbTimer.Content = time.ToString();
            }
            else
            {
                _dt.Stop();
                _finalTime = (int)_elapsedTime;
                MessageBox.Show($"Game Over! Your total game time is {_finalTime} seconds. You also scored {_score} points in total.");
                Leaderboards lb = new Leaderboards(_name, _score, _finalTime, _difficulty);
                lb.Show();
                this.Close();
            }

            if (_round == 10)
            {
                _dt.Stop();
                _finalTime = (int)_elapsedTime;
                MessageBox.Show($"Game Over! Your total game time is {_finalTime} seconds. You also scored {_score} points in total.");
                Leaderboards lb = new Leaderboards(_name, _score, _finalTime, _difficulty);
                lb.Show();
                this.Close();
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int userAns = 0;
            if (tbBin1.Text == "1")
            {
                userAns = userAns + 1;
            }
            if (tbBin2.Text == "1")
            {
                userAns = userAns + 2;
            }
            if (tbBin4.Text == "1")
            {
                userAns = userAns + 4;
            }
            if (tbBin8.Text == "1")
            {
                userAns = userAns + 8;
            }
            if (tbBin16.Text == "1")
            {
                userAns = userAns + 16;
            }
            if (tbBin32.Text == "1")
            {
                userAns = userAns + 32;
            }
            if (tbBin64.Text == "1")
            {
                userAns = userAns + 64;
            }
            if (tbBin128.Text == "1")
            {
                userAns = userAns + 128;
            }
            if (userAns == int.Parse(tbConvNum.Text))
            {
                _dt.Stop();
                _score += _points;
                _round++;
                tbConvNum.Text = _rnd.Next(1, GetNumberRange()).ToString();
                lbScore.Content = _score;
                lbRoundCount.Content = _round;
                MessageBox.Show("Correct!");
                ResetTbBtn();
                ResetTimer();
            }
            else
            {
                if (_difficulty == 3)
                {
                    _score--;
                }
                MessageBox.Show("Wrong! Try Again!");
                lbScore.Content = _score;
            }
        }

        #region Resetters
        private void ResetTimer()
        {
            _gameTime = _gameTime * 0.66;
            lbTimer.Content = (int)_gameTime;
            _dt.Start();
        }
        private void ResetTbBtn()
        {
            tbBin128.Text = "0";
            tbBin64.Text = "0";
            tbBin32.Text = "0";
            tbBin16.Text = "0";
            tbBin8.Text = "0";
            tbBin4.Text = "0";
            tbBin2.Text = "0";
            tbBin1.Text = "0";

            btn128.Background = Brushes.Red;
            btn64.Background = Brushes.Red;
            btn32.Background = Brushes.Red;
            btn16.Background = Brushes.Red;
            btn8.Background = Brushes.Red;
            btn4.Background = Brushes.Red;
            btn2.Background = Brushes.Red;
            btn1.Background = Brushes.Red;
        } 
        #endregion
        #region Menu Items
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow main = new MainWindow(_difficulty, _name, gameTime());
            main.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region Difficulty Settings 
        private TimeSpan GetDifficultyInterval()
        {
            switch (_difficulty)
            {
                case 1:
                    return new TimeSpan(0, 0, 0, 1, 250);
                case 2:
                    return new TimeSpan(0, 0, 0, 1, 0);
                case 3:
                    return new TimeSpan(0, 0, 0, 0, 750);
                default:
                    return new TimeSpan(0, 0, 0, 1, 250);
            }
        }
        private int GetNumberRange()
        {
            switch (_difficulty)
            {
                case 1:
                    return 85;
                case 2:
                    return 170;
                case 3:
                    return 255;
                default:
                    return 75;
            }
        }

        private int PointSystem()
        {
            switch (_difficulty)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                default:
                    return 1;
            }
        } 

        private double gameTime()
        {
            switch (_difficulty)
            {
                case 1:
                    return 90;
                case 2:
                    return 60;
                case 3:
                    return 30;
                default:
                    return 90;
            }
        }
        #endregion
    }
}

