using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChackPoint_tst_No1
{
    /// <summary>
    /// Interaction logic for Level4.xaml
    /// </summary>
    public partial class Level4 : Window
    {
        private double birdSpeed = 3;
        private double birdMove = 20;
        private DispatcherTimer timer;
        private double bscLeft;
        private double bscTop;
        public int Time = 0;
        public int Dead = 0;
        Level3 lv3;
        public int score = 0;
        public Level4(Level3 lv3Instance)
        {
            InitializeComponent();
            lv3 = lv3Instance;
            #region score
            score = lv3.score;
            score_lb.Content = score;
            #endregion
            #region Time
            Time = lv3.Time;
            watch.Content = Time;
            #endregion
            #region
            Dead = lv3.Dead;    
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
                Events_Chack_Point();
                Events_Fuck_Point();
                Events_Score_Point();
                Events_MoveChange_Point();
                Events_EnemyMove_Point();
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
                Level5 level5 = new Level5(this);
                if (b>=1)
                {
                    return;
                }
                b++;
                timer.Stop();
                level5.Show();
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
            if (isBirdInChackPoint)
            {
                if (a == 1)
                {
                    return;
                }
                a++;
                score += 4;
                score_lb.Content = score;
                gameCanvas.Children.Remove(Score);
            }
        }
        private void Events_MoveChange_Point()
        {
            MoveChange_GameHelper moveChange_GameHelper = new();
            bool isBirdInChackPoint = moveChange_GameHelper.IsBirdInChackPoint(movemode, bird);
            bool isBirdInChackPoint2 = moveChange_GameHelper.IsBirdInChackPoint(movemode2, bird);
            if (isBirdInChackPoint)
            {
                birdMove = 10;
                birdSpeed = 0;
                gameCanvas.Children.Remove(movemode);
            }
            else if (isBirdInChackPoint2)
            {
                birdMove = 20;
                birdSpeed = 0;
                gameCanvas.Children.Remove(movemode2);
            }
        }
        private void Events_EnemyMove_Point()
        {

            FuckPoint_GameHelper fuckPoint_GameHelper = new();
            bool isBirdInChackPoint = fuckPoint_GameHelper.IsBirdInChackPoint(enemy, bird);
            if (isBirdInChackPoint)
            {
                Dead++;
                dead_lb.Content = Dead;
                Canvas.SetLeft(bird, bscLeft);
                Canvas.SetTop(bird, bscTop);
                timer.Stop();
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
            Events_EnemyMove_Point();
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
            EnemyMove();
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
        private void EnemyMove()
        {
            double bcsPosX = Canvas.GetTop(enemy);
            DoubleAnimation animationX = new DoubleAnimation();
            animationX.From = bcsPosX; // kezdő X pozíció
            animationX.To = 600; // cél X pozíció
            animationX.Duration = new Duration(TimeSpan.FromSeconds(10)); // animáció időtartama
            animationX.RepeatBehavior = RepeatBehavior.Forever; // ismétlés örökké

            enemy.BeginAnimation(Canvas.LeftProperty, animationX);

        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
