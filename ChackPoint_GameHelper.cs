using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChackPoint_tst_No1
{
    public class ChackPoint_GameHelper
    {
        public virtual bool IsBirdInChackPoint(Rectangle chackPoint, Ellipse bird)
        {
            Point birdTopLeft = new Point(Canvas.GetLeft(bird), Canvas.GetTop(bird));
            Point birdBottomRight = new Point(birdTopLeft.X + bird.Width, birdTopLeft.Y + bird.Height);

            Point chackPointTopLeft = new Point(Canvas.GetLeft(chackPoint), Canvas.GetTop(chackPoint));
            Point chackPointBottomRight = new Point(chackPointTopLeft.X + chackPoint.Width, chackPointTopLeft.Y + chackPoint.Height);

            return birdTopLeft.X >= chackPointTopLeft.X && birdTopLeft.Y >= chackPointTopLeft.Y &&
                   birdBottomRight.X <= chackPointBottomRight.X && birdBottomRight.Y <= chackPointBottomRight.Y;
        }
        protected static bool IsElementInChackPoint(UIElement element, UIElement targetElement)
        {
            if (element == targetElement)
            {
                return true;
            }
            if (element is Panel panel)
            {
                foreach (UIElement child in panel.Children)
                {
                    if (IsElementInChackPoint(child, targetElement))
                    {
                        return true;
                    }
                }
            }
            else
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(element);
                for (int i = 0; i < childrenCount; i++)
                {
                    UIElement child = VisualTreeHelper.GetChild(element, i) as UIElement;
                    if (IsElementInChackPoint(child, targetElement))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
