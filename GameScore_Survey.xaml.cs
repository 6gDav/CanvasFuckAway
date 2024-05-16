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

namespace ChackPoint_tst_No1
{
    /// <summary>
    /// Interaction logic for GameScore_Survey.xaml
    /// </summary>
    public partial class GameScore_Survey : Window
    {
        Level5 lv5;
        public GameScore_Survey(Level5 lv5Instence)
        {
            InitializeComponent();
            lv5 = lv5Instence;
            sc_lb.Content = lv5.score;
            de_lb.Content = lv5.Dead;
            ti_lb.Content = lv5.Time;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void lobby_Click(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new();
            lobby.Show();   
            this.Close();   
        }
    }
}
