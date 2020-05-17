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
        private bool isAdmin = false;

        public MenuViewModel(IEventAggregator events, ILoggedInUserModel user)
        {   
            this.events = events;
            this.user = user;

            if (user.Position == "admin")
            {
                IsAdmin = true;
            }
        }

        public bool IsAdmin
        {
            get => isAdmin;
            set => Set(ref isAdmin, value);
        }

        public ILoggedInUserModel User
        {
            get => user;
            set => Set(ref user, value);
        }

        public void UserManager()
        {
            events.PublishOnUIThread(new AdminInnerViewChangeEvent("users"));
        }

        public void Orders()
        {
            events.PublishOnUIThread(new AdminInnerViewChangeEvent("orders"));
        }

        public void LogOut()
        {
            events.PublishOnUIThread(new LogOutEvent());
        }
    }
}
