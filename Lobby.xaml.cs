using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ChackPoint_tst_No1
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window
    {
        public Lobby()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Level1 level1 = new Level1();
            level1.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GameRule gameRule = new GameRule();
            gameRule.ShowDialog();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
        }

        private void git_link_Click(object sender, RoutedEventArgs e)
        {
            string gitRepoUrl = "https://github.com/6gDav/CanvasFuckAway_No1_Done-.git";

            OpenUrlInBrowser(gitRepoUrl);
        }
        private void OpenUrlInBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült megnyitni a linket: " + ex.Message);
            }
        }
    }
}
