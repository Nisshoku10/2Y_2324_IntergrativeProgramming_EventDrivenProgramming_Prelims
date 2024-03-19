using System;
using System.Windows;

namespace PrelimsInteg
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            DifficultySection df = new DifficultySection();
            df.Show();
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }

        private void Leaderboards_Click(object sender, RoutedEventArgs e)
        {
            Leaderboards lb = new Leaderboards();
            lb.Show();
            this.Close();
        }
    }
}
