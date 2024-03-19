using System.Collections.Generic;
using System.IO;
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

            //SortLeaderboards(_Leaderboards_Easy);
            //SortLeaderboards(_Leaderboards_Medium);
            //SortLeaderboards(_Leaderboards_Hard);
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
            leaderboards.Sort(sortScores);
        }

        private void SortLeaderboardTime(List<LeaderboardEntry> leaderboards)
        {
            leaderboards.Sort(sortTime);
        }

        private int sortTime(LeaderboardEntry ent1, LeaderboardEntry ent2)
        {
            return ent2.Time.CompareTo(ent1.Time);
        }
        private int sortScores(LeaderboardEntry ent1, LeaderboardEntry ent2)
        {
            return ent2.Score.CompareTo(ent1.Score);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            if (lbDiff.Content.ToString() == "Easy")
            {
                if (cbSort.SelectedItem.ToString() == "Score")
                {
                    SortLeaderboardScore(_EasyEntry);
                }
                else
                {
                    SortLeaderboardTime(_EasyEntry);
                }
            }
            else if (lbDiff.Content.ToString() == "Medium")
            {
                if (cbSort.SelectedItem.ToString() == "Score")
                {
                    SortLeaderboardScore(_MediumEntry);
                }
                else
                {
                    SortLeaderboardTime(_MediumEntry);
                }
            }
            else if (lbDiff.Content.ToString() == "Hard")
            {
                if (cbSort.SelectedItem.ToString() == "Score")
                {
                    SortLeaderboardScore(_HardEntry);
                }
                else
                {
                    SortLeaderboardTime(_HardEntry);
                }
            }
        }
    }
}