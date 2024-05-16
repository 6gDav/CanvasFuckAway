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
    /// Interaction logic for GameRule.xaml
    /// </summary>
    public partial class GameRule : Window
    {
        public GameRule()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
