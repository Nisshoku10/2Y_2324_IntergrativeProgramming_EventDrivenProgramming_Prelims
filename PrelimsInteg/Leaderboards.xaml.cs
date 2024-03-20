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

        #region Constructor Overloads   
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

            addNewEntry();

            switch (difficulty)
            {
                case 1:
                    lbDiff.Content = "Easy";
                    lvEasy.Visibility = Visibility.Visible;
                    lvMedium.Visibility = Visibility.Hidden;
                    lvHard.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    lbDiff.Content = "Medium";
                    lvEasy.Visibility = Visibility.Hidden;
                    lvMedium.Visibility = Visibility.Visible;
                    lvHard.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    lbDiff.Content = "Hard";
                    lvEasy.Visibility = Visibility.Hidden;
                    lvMedium.Visibility = Visibility.Hidden;
                    lvHard.Visibility = Visibility.Visible;
                    break;
            }
            
            cbSort.Items.Add("Score");
            cbSort.Items.Add("Time");
        }

        #endregion
        #region Read File
        private void ReadLeaderboards()
        {
            _EasyEntry = ReadFile(path1, _EasyEntry, 1);
            _MediumEntry = ReadFile(path2, _MediumEntry, 2);
            _HardEntry = ReadFile(path3, _HardEntry, 3);
        }

        private List<LeaderboardEntry> ReadFile(string path, List<LeaderboardEntry> leaderboards, int difficulty)
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

            leaderboards = SortLeaderboardScore(leaderboards);
            while (leaderboards.Count > 10) 
            {
                leaderboards.RemoveAt(leaderboards.Count - 1);
            }

            switch(difficulty)
            {
                case 1:
                    _EasyEntry = leaderboards;
                    lvEasy.ItemsSource = leaderboards;
                    break;
                case 2:
                    _MediumEntry = leaderboards;
                    lvMedium.ItemsSource = leaderboards;
                    break;
                case 3:
                    _HardEntry = leaderboards;
                    lvHard.ItemsSource = leaderboards;
                    break;
            }
            return leaderboards;
        }
        #endregion
        #region Navigations 
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            WriteLeaderboards();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (lbDiff.Content.ToString() == "Easy")
            {
                lbDiff.Content = "Hard";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Visible;
                if (cbSort.SelectedItem != null)
                {
                    if (cbSort.SelectedItem.ToString() == "Score")
                    {
                        _HardEntry = SortLeaderboardScore(_HardEntry);
                        lvHard.ItemsSource = _HardEntry;
                    }
                    else
                    {
                        _HardEntry = SortLeaderboardTime(_HardEntry);
                        lvHard.ItemsSource = _HardEntry;
                    }
                }
            }
            else if (lbDiff.Content.ToString() == "Hard")
            {
                lbDiff.Content = "Medium";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Visible;
                lvHard.Visibility = Visibility.Hidden;
                if(cbSort.SelectedItem != null)
                {
                    if (cbSort.SelectedItem.ToString() == "Score")
                    {
                        _MediumEntry = SortLeaderboardScore(_MediumEntry);
                        lvMedium.ItemsSource = _MediumEntry;
                    }
                    else
                    {
                        _MediumEntry = SortLeaderboardTime(_MediumEntry);
                        lvMedium.ItemsSource = _MediumEntry;
                    }
                }
            }
            else if (lbDiff.Content.ToString() == "Medium")
            {
                lbDiff.Content = "Easy";
                lvEasy.Visibility = Visibility.Visible;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Hidden;
                if(cbSort.SelectedItem != null)
                {
                    if (cbSort.SelectedItem.ToString() == "Score")
                    {
                        _EasyEntry = SortLeaderboardScore(_EasyEntry);
                        lvEasy.ItemsSource = _EasyEntry;
                    }
                    else
                    {
                        _EasyEntry = SortLeaderboardTime(_EasyEntry);
                        lvEasy.ItemsSource = _EasyEntry;
                    }
                }
            }
        }
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (lbDiff.Content.ToString() == "Easy")
            {
                lbDiff.Content = "Medium";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Visible;
                lvHard.Visibility = Visibility.Hidden;
                if (cbSort.SelectedItem != null)
                {
                    if (cbSort.SelectedItem != null)
                    {
                        if (cbSort.SelectedItem.ToString() == "Score")
                        {
                            _MediumEntry = SortLeaderboardScore(_MediumEntry);
                            lvMedium.ItemsSource = _MediumEntry;
                        }
                        else
                        {
                            _MediumEntry = SortLeaderboardTime(_MediumEntry);
                            lvMedium.ItemsSource = _MediumEntry;
                        }
                    }
                }
            }
            else if (lbDiff.Content.ToString() == "Medium")
            {
                lbDiff.Content = "Hard";
                lvEasy.Visibility = Visibility.Hidden;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Visible;
                if (cbSort.SelectedItem != null)
                {
                    if (cbSort.SelectedItem.ToString() == "Score")
                    {
                        _HardEntry = SortLeaderboardScore(_HardEntry);
                        lvHard.ItemsSource = _HardEntry;
                    }
                    else
                    {
                        _HardEntry = SortLeaderboardTime(_HardEntry);
                        lvHard.ItemsSource = _HardEntry;
                    }
                }
            }
            else if (lbDiff.Content.ToString() == "Hard")
            {
                lbDiff.Content = "Easy";
                lvEasy.Visibility = Visibility.Visible;
                lvMedium.Visibility = Visibility.Hidden;
                lvHard.Visibility = Visibility.Hidden;
                if(cbSort.SelectedItem != null)
                {
                    if (cbSort.SelectedItem.ToString() == "Score")
                    {
                        _EasyEntry = SortLeaderboardScore(_EasyEntry);
                        lvEasy.ItemsSource = _EasyEntry;
                    }
                    else
                    {
                        _EasyEntry = SortLeaderboardTime(_EasyEntry);
                        lvEasy.ItemsSource = _EasyEntry;
                    }
                }
            }
        } 
        #endregion
        #region Combo Box Sort By 
        private List<LeaderboardEntry> SortLeaderboardScore(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderByDescending(lb => lb.Score).ToList();
        }

        private List<LeaderboardEntry> SortLeaderboardTime(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderBy(lb => lb.Time).ToList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCbInst.Visibility = Visibility.Hidden;
            if (lbDiff.Content.ToString() == "Easy")
            {
                if (cbSort.SelectedItem.ToString() == "Score")
                {
                    _EasyEntry = SortLeaderboardScore(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                }
                else
                {
                    _EasyEntry = SortLeaderboardTime(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                }
            }
            else if (lbDiff.Content.ToString() == "Medium")
            {
                if (cbSort.SelectedItem.ToString() == "Score")
                {
                    _MediumEntry = SortLeaderboardScore(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                }
                else
                {
                    _MediumEntry = SortLeaderboardTime(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                }
            }
            else if (lbDiff.Content.ToString() == "Hard")
            {
                if (cbSort.SelectedItem.ToString() == "Score")
                {
                    _HardEntry = SortLeaderboardScore(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                }
                else
                {
                    _HardEntry = SortLeaderboardTime(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                }
            }
        }
        #endregion
        private void addNewEntry()
        {

            switch (_difficulty)
            {
                case 1:
                    _EasyEntry.Add(new LeaderboardEntry { Nickname = _name, Score = _score, Time = _finalTime });
                    _EasyEntry = SortLeaderboardScore(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                    while (_EasyEntry.Count > 10)
                    {
                        _EasyEntry.RemoveAt(_EasyEntry.Count - 1);
                    }
                    break;
                case 2:
                    _MediumEntry.Add(new LeaderboardEntry { Nickname = _name, Score = _score, Time = _finalTime });
                    _MediumEntry = SortLeaderboardScore(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                    while (_MediumEntry.Count > 10)
                    {
                        _MediumEntry.RemoveAt(_MediumEntry.Count - 1);
                    }
                    break;
                case 3:
                    _HardEntry.Add(new LeaderboardEntry { Nickname = _name, Score = _score, Time = _finalTime });
                    _HardEntry = SortLeaderboardScore(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                    while (_HardEntry.Count > 10)
                    {
                        _HardEntry.RemoveAt(_HardEntry.Count - 1);
                    }
                    break;
                default:
                    break;
            }
        }
        private void WriteLeaderboards()
        {
            using (StreamWriter sw = new StreamWriter(path1))
            {
                foreach (LeaderboardEntry lbe in _EasyEntry)
                {
                    sw.WriteLine($"{lbe.Nickname},{lbe.Score},{lbe.Time}");
                }
            }

            using (StreamWriter sw = new StreamWriter(path2))
            {
                foreach (LeaderboardEntry lbe in _MediumEntry)
                {
                    sw.WriteLine($"{lbe.Nickname},{lbe.Score},{lbe.Time}");
                }
            }

            using (StreamWriter sw = new StreamWriter(path3))
            {
                foreach (LeaderboardEntry lbe in _HardEntry)
                {
                    sw.WriteLine($"{lbe.Nickname},{lbe.Score},{lbe.Time}");
                }
            }
        }
    }
}