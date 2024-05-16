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
using System.Windows.Threading;

namespace ChackPoint_tst_No1
{
    /// <summary>
    /// Interaction logic for Level3.xaml
    /// </summary>
    public partial class Level3 : Window
    {
        private double birdSpeed = 3;
        private double birdMove = 20;
        private DispatcherTimer timer;
        private double bscLeft;
        private double bscTop;
        public int Time = 0;
        public int Dead = 0;
        Level2 Lv2;
        public int score;
        public Level3(Level2 lv2Instance)
        {
            InitializeComponent();
            Lv2 = lv2Instance;
            #region Score
            score = Lv2.score;
            score_lb.Content = score;
            #endregion
            #region Time
            Time = Lv2.Time;
            watch.Content = Time;
            #endregion
            #region Dead
            Dead = Lv2.Dead;
            dead_lb.Content = Dead;
            #endregion
            bscLeft = Canvas.GetLeft(bird);
            bscTop = Canvas.GetTop(bird);
            //timer 
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += DropBack;
            //color changer shit.
            this.Loaded += AddTimeRequiredStuffs;
        }
        private void Do_Move(KeyEventArgs e, Key key, double move_mesure)
        {
            double currentDir;
            if (e.Key == key)
            {
                if (key == Key.A || key == Key.D)
                {
                    currentDir = Canvas.GetLeft(bird);
                    Canvas.SetLeft(bird, currentDir - move_mesure);
                    timer.Start();
                    return;
                }
                currentDir = Canvas.GetTop(bird);
                Canvas.SetTop(bird, currentDir - move_mesure);
                Events_Fuck_Point();
                Events_Score_Point();
                Events_MoveChange_Point();
                timer.Start();
            }
        }
        #region Keyboard event handlers
        private void MoveBirdUp(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.W, birdMove);
        }

        private void MoveBirdDown(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.S, -birdMove);
        }

        private void MoveBirdLeft(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.A, birdMove);
        }
        private void MoveBirdRight(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.D, -birdMove);
        }
        #endregion
        int b = 0;
        private void Events_Chack_Point()
        {
            ChackPoint_GameHelper chackPoint_Game = new();
            bool isBirdInChackPoint = chackPoint_Game.IsBirdInChackPoint(chackPoint, bird);
            if (isBirdInChackPoint)
            {
                if (b>=1)
                {
                    return;
                }
                b++;
                timer.Stop();
                Level4 level4 = new Level4(this);
                level4.Show();
                this.Close();
            }
        }
        private void Events_Fuck_Point()
        {
            FuckPoint_GameHelper fuckPoint_GameHelper = new();
            bool isBirdInChackPoint = fuckPoint_GameHelper.IsBirdInChackPoint(fuckPoint, bird);
            bool isBirdInChackPoint2 = fuckPoint_GameHelper.IsBirdInChackPoint(fuckPoint2, bird);
            bool isBirdInChackPoint3 = fuckPoint_GameHelper.IsBirdInChackPoint(fuckPoint3, bird);
            if (isBirdInChackPoint || isBirdInChackPoint2 || isBirdInChackPoint3)
            {
                Dead++;
                dead_lb.Content = Dead;
                Canvas.SetLeft(bird, bscLeft);
                Canvas.SetTop(bird, bscTop);
                timer.Stop();
            }
        }
        int a = 0;
        private void Events_Score_Point()
        {
            ScorePoint_GameHelper scorePoint_GameHelper = new();
            bool isBirdInChackPoint = scorePoint_GameHelper.IsBirdInChackPoint(Score, bird);
            bool isBirdInChackPoint2 = scorePoint_GameHelper.IsBirdInChackPoint(Score2, bird);

            if (isBirdInChackPoint)
            {
                gameCanvas.Children.Remove(Score);
                score = 3;
            }
            if (isBirdInChackPoint2)
            {
                if (a == 1)
                {
                    return;
                }
                a++;
                gameCanvas.Children.Remove(Score2);
                score += 8;
            }
            score_lb.Content = score;
        }
        private void Events_MoveChange_Point()
        {
            MoveChange_GameHelper moveChange_GameHelper = new();
            bool isBirdInChackPoint2 = moveChange_GameHelper.IsBirdInChackPoint(movemode, bird);
            if (isBirdInChackPoint2)
            {
                birdMove = 10;
                birdSpeed = 0;
                gameCanvas.Children.Remove(movemode);
            }
        }
        private void DropBack(object sender, EventArgs e)
        {
            double currentTop = Canvas.GetTop(bird);
            double currentLeft = Canvas.GetLeft(bird);

            Canvas.SetTop(bird, currentTop + birdSpeed);//itten csökken a magassága

            if (currentTop <= 0)
            {
                Canvas.SetTop(bird, bird.ActualHeight - bird.Height);
            }
            else if (currentLeft + bird.Width >= gameCanvas.ActualWidth)
            {
                Canvas.SetLeft(bird, gameCanvas.ActualWidth - bird.Width);
            }
            else if (currentLeft <= 0)
            {
                Canvas.SetLeft(bird, 0);
            }
            Events_Chack_Point();
            Events_Fuck_Point();
            Events_Score_Point();
            Events_MoveChange_Point();
        }
        private async Task ChangeColor()
        {
            while (true)
            {
                chackpointtext.Foreground = Brushes.DarkBlue;
                await Task.Delay(1000);
                chackpointtext.Foreground = Brushes.Red;
                await Task.Delay(1000);
                chackpointtext.Foreground = Brushes.HotPink;
                await Task.Delay(1000);
            }
        }
        private void AddTimeRequiredStuffs(object sender, RoutedEventArgs e)
        {
            ChangeColor();
            Watch();
        }
        private async Task Watch()
        {
            while (true)
            {
                watch.Content = Time;
                Time++;
                await Task.Delay(1000);
            }
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
