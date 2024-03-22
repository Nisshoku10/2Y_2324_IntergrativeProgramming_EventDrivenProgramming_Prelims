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

            cbSort.Items.Add("Score (Asc)");
            cbSort.Items.Add("Score (Desc)");
            cbSort.Items.Add("Time (Asc)");
            cbSort.Items.Add("Time (Desc)");
            cbSort.Items.Add("Score & Time");
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
            
            cbSort.Items.Add("Score (Asc)");
            cbSort.Items.Add("Score (Desc)");
            cbSort.Items.Add("Time (Asc)");
            cbSort.Items.Add("Time (Desc)");
            cbSort.Items.Add("Score & Time");
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

            leaderboards = SortLeaderboardScoreAndTime(leaderboards);
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
                    EasySortView();
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
                    MediumSortView();
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
                    HardSortView();
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
                    EasySortView();
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
                    MediumSortView();
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
                    HardSortView();
                }
            }
        } 
        #endregion
        #region Combo Box Sort By 
        private List<LeaderboardEntry> SortLeaderboardScoreAsc(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderBy(lb => lb.Score).ToList();
        }
        private List<LeaderboardEntry> SortLeaderboardScoreDesc(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderByDescending(lb => lb.Score).ToList();
        }
        private List<LeaderboardEntry> SortLeaderboardTimeAsc(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderBy(lb => lb.Time).ToList();
        }
        private List<LeaderboardEntry> SortLeaderboardTimeDesc(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderByDescending(lb => lb.Time).ToList();
        }
        private List<LeaderboardEntry> SortLeaderboardScoreAndTime(List<LeaderboardEntry> templeaderboards)
        {
            return templeaderboards.OrderByDescending( lb => lb.Score).ThenBy(lb => lb.Time).ToList();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbCbInst.Visibility = Visibility.Hidden;
            if (lbDiff.Content.ToString() == "Easy")
            {
                EasySortView();
            }
            else if (lbDiff.Content.ToString() == "Medium")
            {
                MediumSortView();
            }
            else if (lbDiff.Content.ToString() == "Hard")
            {
                HardSortView();
            }
        }
        private void EasySortView()
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    _EasyEntry = SortLeaderboardScoreAsc(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                    break;
                case 1:
                    _EasyEntry = SortLeaderboardScoreDesc(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                    break;
                case 2:
                    _EasyEntry = SortLeaderboardTimeAsc(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                    break;
                case 3:
                    _EasyEntry = SortLeaderboardTimeDesc(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                    break;
                case 4:
                    _EasyEntry = SortLeaderboardScoreAndTime(_EasyEntry);
                    lvEasy.ItemsSource = _EasyEntry;
                    break;
            }
        }

        private void MediumSortView()
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    _MediumEntry = SortLeaderboardScoreAsc(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                    break;
                case 1:
                    _MediumEntry = SortLeaderboardScoreDesc(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                    break;
                case 2:
                    _MediumEntry = SortLeaderboardTimeAsc(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                    break;
                case 3:
                    _MediumEntry = SortLeaderboardTimeDesc(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                    break;
                case 4:
                    _MediumEntry = SortLeaderboardScoreAndTime(_MediumEntry);
                    lvMedium.ItemsSource = _MediumEntry;
                    break;
            }
        }

        private void HardSortView()
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    _HardEntry = SortLeaderboardScoreAsc(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                    break;
                case 1:
                    _HardEntry = SortLeaderboardScoreDesc(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                    break;
                case 2:
                    _HardEntry = SortLeaderboardTimeAsc(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                    break;
                case 3:
                    _HardEntry = SortLeaderboardTimeDesc(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                    break;
                case 4:
                    _HardEntry = SortLeaderboardScoreAndTime(_HardEntry);
                    lvHard.ItemsSource = _HardEntry;
                    break;
            }
        }
        #endregion
        private void addNewEntry()
        {

            switch (_difficulty)
            {
                case 1:
                    _EasyEntry.Add(new LeaderboardEntry { Nickname = _name, Score = _score, Time = _finalTime });
                    _EasyEntry = SortLeaderboardScoreAndTime(_EasyEntry);
                    while (_EasyEntry.Count > 10)
                    {
                        _EasyEntry.RemoveAt(_EasyEntry.Count - 1);
                    }
                    
                    lvEasy.ItemsSource = _EasyEntry;

                    break;
                case 2:
                    _MediumEntry.Add(new LeaderboardEntry { Nickname = _name, Score = _score, Time = _finalTime });
                    _MediumEntry = SortLeaderboardScoreAndTime(_MediumEntry);
                    while (_MediumEntry.Count > 10)
                    {
                        _MediumEntry.RemoveAt(_MediumEntry.Count - 1);
                    }
                    lvMedium.ItemsSource = _MediumEntry;
                    break;
                case 3:
                    _HardEntry.Add(new LeaderboardEntry { Nickname = _name, Score = _score, Time = _finalTime });
                    _HardEntry = SortLeaderboardScoreAndTime(_HardEntry);
                    while (_HardEntry.Count > 10)
                    {
                        _HardEntry.RemoveAt(_HardEntry.Count - 1);
                    }
                    lvHard.ItemsSource = _HardEntry;
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