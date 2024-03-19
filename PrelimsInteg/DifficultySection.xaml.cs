using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrelimsInteg
{
    /// <summary>
    /// Interaction logic for DifficultySection.xaml
    /// </summary>
    public partial class DifficultySection : Window
    {
        private string _name;
        public DifficultySection()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(1, _name, 90);
            mainWindow.Show();
            this.Close();
        }

        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(2, _name, 60);
            mainWindow.Show();
            this.Close();
        }

        private void btnHard_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(3, _name, 30);
            mainWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = tbPlayerName.Text;
            if (tbPlayerName.Text.Length >= 1 && tbPlayerName.Text.Length <= 10)
            {
                _name = tbPlayerName.Text;
                btnEasy.IsEnabled = true;
                btnMedium.IsEnabled = true;
                btnHard.IsEnabled = true;
                lbWarning.Visibility = Visibility.Hidden;
                lbNoName.Visibility = Visibility.Hidden;
            }
            else
            {
                if(tbPlayerName.Text.Length > 10)
                {
                    lbWarning.Visibility = Visibility.Visible;
                    btnEasy.IsEnabled = false;
                    btnMedium.IsEnabled = false;
                    btnHard.IsEnabled = false;
                }
                else
                {
                    lbNoName.Visibility= Visibility.Visible;
                    btnEasy.IsEnabled = false;
                    btnMedium.IsEnabled = false;
                    btnHard.IsEnabled = false;
                }
            }
        }
    }
}
