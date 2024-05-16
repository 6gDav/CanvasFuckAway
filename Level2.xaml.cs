using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ChackPoint_tst_No1
{
    /// <summary>
    /// Interaction logic for Level2.xaml
    /// </summary>
    public partial class Level2 : Window
    {
        private double birdSpeed = 3;
        private DispatcherTimer timer;
        private double bscLeft;
        private double bscTop;
        public int Time = 0;
        public int Dead = 0;
        Level1 lv1 = new();
        public int score;

        public Level2(Level1 lv1Instance)
        {
            InitializeComponent();
            lv1 = lv1Instance;
            #region score
            score = lv1.score;
            score_lb.Content = score;
            #endregion
            #region Time
            Time = lv1.Time;
            watch.Content = Time;
            #endregion
            #region Dead
            Dead = lv1.Dead;
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
                timer.Start();
            }
        }
        #region Keyboard event handlers
        private void MoveBirdUp(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.W, 20);
        }

        private void MoveBirdDown(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.S, -20);
        }

        private void MoveBirdLeft(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.A, 20);
        }
        private void MoveBirdRight(object sender, KeyEventArgs e)
        {
            Do_Move(e, Key.D, -20);
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
                Level3 level3 = new(this);
                level3.Show();
                this.Close();
            }
        }
        private void Events_Fuck_Point()
        {
            FuckPoint_GameHelper fuckPoint_GameHelper = new();
            bool isBirdInChackPoint = fuckPoint_GameHelper.IsBirdInChackPoint(fuckPoint, bird);
            bool isBirdInChackPoint2 = fuckPoint_GameHelper.IsBirdInChackPoint(fuckPoint2, bird);
            if (isBirdInChackPoint || isBirdInChackPoint2)
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
                if (a == 1)
                {
                    return;
                }
                a++;
                score += 2;
                score_lb.Content = score;
                gameCanvas.Children.Remove(Score);
            }
            else if (isBirdInChackPoint2)
            {
                if (a == 2)
                {
                    return;
                }
                a++;
                score += 3;
                score_lb.Content = score;
                gameCanvas.Children.Remove(Score2);
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
