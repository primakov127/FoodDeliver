using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
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
        private ILoggedInUserModel user;

        public MenuViewModel(IEventAggregator events, ILoggedInUserModel user)
        {   
            this.events = events;
            this.user = user;
        }

        public ILoggedInUserModel User
        {
            get => user;
            set => Set(ref user, value);
        }

        public void LogOut()
        {
            events.PublishOnUIThread(new LogOutEvent());
        }
    }
}
