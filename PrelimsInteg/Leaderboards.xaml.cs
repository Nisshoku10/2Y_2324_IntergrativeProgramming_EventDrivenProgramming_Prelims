using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PrelimsInteg
{
    /// <summary>
    /// Interaction logic for Leaderboards.xaml
    /// </summary>
    public partial class Leaderboards : Window
    {
        private List<LeaderboardEntry> _EasyEntry = new List<LeaderboardEntry>();
        private List<LeaderboardEntry> _MediumEntry = new List<LeaderboardEntry>();
        private List<LeaderboardEntry> _HardEntry = new List<LeaderboardEntry>();

        private string path1 = "EasyLeaderboards.csv";
        private string path2 = "MediumLeaderboards.csv";
        private string path3 = "HardLeaderboards.csv";

        private string _name = "";
        private int _score = 0;
        private int _finalTime = 0;
        private int _difficulty = 0;
        public Leaderboards()
        {
            InitializeComponent();
            ReadLeaderboards();
            lbDiff.Content = "Easy";
            lvEasy.Visibility = Visibility.Visible;
            lvMedium.Visibility = Visibility.Hidden;
            lvHard.Visibility = Visibility.Hidden;

            cbSort.Items.Add("Score");
            cbSort.Items.Add("Time");
        }
        public Leaderboards(string name, int score, int finalTime, int difficulty)
        {
            InitializeComponent();
            ReadLeaderboards();
            _name = name;
            _score = score;
            _finalTime = finalTime;
            _difficulty = difficulty;
            lbDiff.Content = "Easy";
        }

        #region Read File
        private void ReadLeaderboards()
        {
            ReadFile(path1, _EasyEntry, 1);
            ReadFile(path2, _MediumEntry, 2);
            ReadFile(path3, _HardEntry, 3);
        }

        private void ReadFile(string path, List<LeaderboardEntry> leaderboards, int difficulty)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = string.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        leaderboards.Add(new LeaderboardEntry { Nickname = values[0], Score = int.Parse(values[1]), Time = int.Parse(values[2]) });
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("Name,Score,Time");
                }
            }

            SortLeaderboardScore(leaderboards);

            switch(difficulty)
            {
                case 1:
                    lvEasy.ItemsSource = leaderboards;
                    break;
                case 2:
                    lvMedium.ItemsSource = leaderboards;
                    break;
                case 3:
                    lvHard.ItemsSource = leaderboards;
                    break;
            }
        }
        #endregion
        #region Navigations 
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (lbDiff.Content == "Easy")
            {
                lbDiff.Content = "Hard";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Visible;
            }
            else if (lbDiff.Content == "Hard")
            {
                lbDiff.Content = "Medium";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Visible;
                lvHard.Visibility = Visibility.Hidden;
            }
            else if (lbDiff.Content == "Medium")
            {
                lbDiff.Content = "Easy";
                lvEasy.Visibility = Visibility.Visible;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Hidden;
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (lbDiff.Content == "Easy")
            {
                lbDiff.Content = "Medium";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Visible;
                lvHard.Visibility = Visibility.Hidden;
            }
            else if (lbDiff.Content == "Medium")
            {
                lbDiff.Content = "Hard";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Visible;
            }
            else if (lbDiff.Content == "Hard")
            {
                lbDiff.Content = "Easy";
                lvEasy.Visibility = Visibility.Visible;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Hidden;
            }
        } 
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //WriteLeaderboards();
        }

        private void SortLeaderboardScore(List<LeaderboardEntry> leaderboards)
        {
            List<LeaderboardEntry> sortedByScore = leaderboards.OrderBy(lb => lb.Score).ToList();
        }

        private void SortLeaderboardTime(List<LeaderboardEntry> leaderboards)
        {
            List<LeaderboardEntry> sortedByScore = leaderboards.OrderBy(lb => lb.Time).ToList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDiff.Content.ToString() == "Easy")
            {
                if (cbSort.SelectedIndex == 0)
                {
                    SortLeaderboardScore(_EasyEntry);
                }
                else if (cbSort.SelectedIndex == 1)
                {
                    SortLeaderboardTime(_EasyEntry);
                }
                lvEasy.ItemsSource = _EasyEntry;
            }
            else if (lbDiff.Content.ToString() == "Medium")
            {
                if (cbSort.SelectedIndex == 0)
                {
                    SortLeaderboardScore(_MediumEntry);
                }
                else if(cbSort.SelectedIndex == 1)
                {
                    SortLeaderboardTime(_MediumEntry);
                }
                lvEasy.ItemsSource = _MediumEntry;
            }
            else if (lbDiff.Content.ToString() == "Hard")
            {
                if (cbSort.SelectedIndex == 0)
                {
                    SortLeaderboardScore(_HardEntry);
                }
                else if (cbSort.SelectedIndex == 1)
                {
                    SortLeaderboardTime(_HardEntry);
                }
                lvEasy.ItemsSource = _HardEntry;
            }
        }
    }
}