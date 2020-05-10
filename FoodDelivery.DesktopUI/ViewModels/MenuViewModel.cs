using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class MenuViewModel : Screen
    {
        private IEventAggregator events;

        public MenuViewModel(IEventAggregator events)
        {   
            this.events = events;
        }

        public void LogOut()
        {
            events.PublishOnUIThread(new LogOutEvent());
        }
    }
}
