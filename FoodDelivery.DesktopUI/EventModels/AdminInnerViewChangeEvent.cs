using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.EventModels
{
    public class AdminInnerViewChangeEvent
    {
        public AdminInnerViewChangeEvent(string screen)
        {
            Screen = screen;
        }

        public string Screen { get; set; }
    }
}
