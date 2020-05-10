using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<LogOutEvent>, IHandle<ProccesOrderEvent>
    {
        private IEventAggregator events;
        private ILoggedInUserModel user;
        private SimpleContainer container;
        private IAPIHelper apiHelper;

        public ShellViewModel(IEventAggregator events, SimpleContainer container, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            this.events = events;
            this.container = container;
            this.user = user;
            this.apiHelper = apiHelper;

            events.Subscribe(this);

            ActivateItem(container.GetInstance<LoginViewModel>());
            //ActivateItem(container.GetInstance<CallViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            switch (user.Position)
            {
                case "admin":
                    ActivateItem(container.GetInstance<AdminViewModel>());
                    break;
                case "call":
                    ActivateItem(container.GetInstance<CallViewModel>());
                    break;
                case "cook":
                    ActivateItem(container.GetInstance<CookViewModel>());
                    break;
                default:
                    break;
            }
        }

        public void Handle(LogOutEvent message)
        {
            user.ResetUserModel();
            apiHelper.LogOutUser();
            ActivateItem(container.GetInstance<LoginViewModel>());
        }

        public void Handle(ProccesOrderEvent message)
        {
            ActivateItem(new OrderDisplayViewModel(message.Order, container.GetInstance<IMapper>(), message.CallEndpoint, events));
        }
    }
}
