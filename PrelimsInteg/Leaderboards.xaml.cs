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
        private List<string[]> _Leaderboards_Easy = new List<string[]>();
        private List<string[]> _Leaderboards_Medium = new List<string[]>();
        private List<string[]> _Leaderboards_Hard = new List<string[]>();

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
            //lvMedium.ItemsSource = _Leaderboards_Medium;
            //lvHard.ItemsSource = _Leaderboards_Hard;
        }
        public Leaderboards(string name, int score, int finalTime, int difficulty)
        {
            InitializeComponent();
            ReadLeaderboards();
            _name = name;
            _score = score;
            _finalTime = finalTime;
            _difficulty = difficulty;
        }

        #region Read File
        private void ReadLeaderboards()
        {
            ReadFile(path1, _Leaderboards_Easy, 1);
            ReadFile(path2, _Leaderboards_Medium, 2);
            ReadFile(path3, _Leaderboards_Hard, 3);

            //SortLeaderboards(_Leaderboards_Easy);
            //SortLeaderboards(_Leaderboards_Medium);
            //SortLeaderboards(_Leaderboards_Hard);
        }

        private void ReadFile(string path, List<string[]> leaderboards, int difficulty)
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
                        lvEasy.Items.Add(values);
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
        }
        #endregion


        private void DisplayLeaderboard(ListView listView, List<string[]> leaderboards)
        {
            //listView.Items.Clear();

            for (int i = 0; i < leaderboards.Count && i < 10; i++)
            {
                listView.Items.Add(leaderboards[i][0]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    WriteLeaderboards();
        //}

        //private void WriteLeaderboards()
        //{
        //   switch()
        //   {
        //        case 1:
        //            break;
        //        case 2:
        //            break;
        //        case 3:
        //            break;
        //   }
        //}

        //private void WriteFile(string path, List<string[]> leaderboards)
        //{
        //    using (StreamWriter sw = new StreamWriter(path))
        //    {
        //        for (int i = 0; i < leaderboards.Count; i++)
        //        {
        //            sw.WriteLine(string.Join(",", leaderboards[i]));
        //        }
        //    }
        //}

    }
}