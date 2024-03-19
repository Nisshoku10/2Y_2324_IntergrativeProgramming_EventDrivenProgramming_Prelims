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
        private string path1 = "EasyLeaderboards.txt";
        private string path2 = "MediumLeaderboards.txt";
        private string path3 = "HardLeaderboards.txt";
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
            List<string[]> leaderboards;
            switch (_difficulty)
            {
                case 1:
                    leaderboards = _Leaderboards_Easy;
                    leaderboards.Add(new string[] { _name, _score.ToString(), _cummulTime.ToString() });

                    for (int i = 0; i < leaderboards.Count - 1; i++)
                    {
                        for (int j = 0; j < leaderboards.Count - 1 - i; j++)
                        {
                            int jScore = int.Parse(leaderboards[j][1]);
                            int jp1Score = int.Parse(leaderboards[j + 1][1]);

                            if (jScore < jp1Score)
                            {
                                string[] temp = leaderboards[j];
                                leaderboards[j] = leaderboards[j + 1];
                                leaderboards[j + 1] = temp;
                            }
                        }
                    }

                    leaderboards.Insert(0, new string[] { _name, _score.ToString(), _cummulTime.ToString() });

                    if (leaderboards.Count > 10)
                    {
                        leaderboards.RemoveAt(10);
                    }

                    WriteFile(path1, _Leaderboards_Easy);
                    break;
                case 2:
                    leaderboards = _Leaderboards_Medium;
                    leaderboards.Add(new string[] { _name, _score.ToString(), _cummulTime.ToString() });

                    for (int i = 0; i < leaderboards.Count - 1; i++)
                    {
                        for (int j = 0; j < leaderboards.Count - 1 - i; j++)
                        {
                            int jScore = int.Parse(leaderboards[j][1]);
                            int jp1Score = int.Parse(leaderboards[j + 1][1]);

                            if (jScore < jp1Score)
                            {
                                string[] temp = leaderboards[j];
                                leaderboards[j] = leaderboards[j + 1];
                                leaderboards[j + 1] = temp;
                            }
                        }
                    }

                    leaderboards.Insert(0, new string[] { _name, _score.ToString(), _cummulTime.ToString() });

                    if (leaderboards.Count > 10)
                    {
                        leaderboards.RemoveAt(10);
                    }
                    WriteFile(path2, _Leaderboards_Medium);
                    break;
                case 3: 
                    leaderboards = _Leaderboards_Hard;
                    leaderboards.Add(new string[] { _name, _score.ToString(), _cummulTime.ToString() });

                    for (int i = 0; i < leaderboards.Count - 1; i++)
                    {
                        for (int j = 0; j < leaderboards.Count - 1 - i; j++)
                        {
                            int jScore = int.Parse(leaderboards[j][1]);
                            int jp1Score = int.Parse(leaderboards[j + 1][1]);

                            if (jScore < jp1Score)
                            {
                                string[] temp = leaderboards[j];
                                leaderboards[j] = leaderboards[j + 1];
                                leaderboards[j + 1] = temp;
                            }
                        }
                    }

                    leaderboards.Insert(0, new string[] { _name, _score.ToString(), _cummulTime.ToString() });

                    if (leaderboards.Count > 10)
                    {
                        leaderboards.RemoveAt(10);
                    }
                    WriteFile(path3, _Leaderboards_Hard);
                    break;
                default:
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
    }
}