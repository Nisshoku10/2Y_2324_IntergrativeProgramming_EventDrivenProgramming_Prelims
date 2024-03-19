using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PrelimsInteg
{
    /// <summary>
    /// Interaction logic for Leaderboards.xaml
    /// </summary>
    public partial class Leaderboards : Window
    {
        private List<string[]> _Leaderboards_Easy = new List<string[]>();
        private List<string[]> _Leaderboards_Medium = new List<string[]>();
        private List<string[]> _Leaderboards_Hard = new List<string[]>();
        private string path1 = "EasyLeaderboards.csv";
        private string path2 = "MediumLeaderboards.csv";
        private string path3 = "HardLeaderboards.csv";
        private int _difficulty;
        private string _name;
        private int _score;
        private int _cummulTime;

        
        public Leaderboards()
        {
            LeaderboardEntry lbEnt = new LeaderboardEntry();
            _difficulty = lbEnt.Difficulty;
            _name = lbEnt.NickName;
            _score = lbEnt.Score;
            _cummulTime = lbEnt.Time;
            InitializeComponent();
            ReadLeaderboards();
        }

        #region Read File
        private void ReadLeaderboards()
        {
            ReadFile(path1, _Leaderboards_Easy);
            ReadFile(path2, _Leaderboards_Medium);
            ReadFile(path3, _Leaderboards_Hard);
        }

        private void ReadFile(string path, List<string[]> leaderboards)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = string.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        leaderboards.Add(values);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine();
                }
            }
        } 
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WriteLeaderboards();
        }

        private void WriteLeaderboards()
        {
           switch(_difficulty)
           {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
           }
        }

        private void WriteFile(string path, List<string[]> leaderboards)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < leaderboards.Count; i++)
                {
                    sw.WriteLine(string.Join(",", leaderboards[i]));
                }
            }
        }

        private void Sort(List<int[]> Leaderboard)
        {
            for (int i = 0; i < Leaderboard.Count - 1; i++)
            {
                for (int j = 0; i < Leaderboard[i].Length - 1 - i; j++)
                {
                    if (i < j)
                    {
                        
                    }
                }
            }
        }
    }
}