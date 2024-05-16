using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace ChackPoint_tst_No1
{
    internal class FuckPoint_GameHelper : ChackPoint_GameHelper
    {
        public override bool IsBirdInChackPoint(Rectangle chackPoint, Ellipse bird)
        {
            return base.IsBirdInChackPoint(chackPoint, bird); 
        }

        protected static bool IsElementInChackPoint(UIElement element, UIElement targetElement)
        {
            return ChackPoint_GameHelper.IsElementInChackPoint(element, targetElement);
        }
    }
}
