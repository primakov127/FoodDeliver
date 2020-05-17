using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class AdminViewModel : Conductor<object>, IHandle<AdminInnerViewChangeEvent>
    {
        private SimpleContainer container;
        private IEventAggregator events;

        public AdminViewModel(SimpleContainer container, MenuViewModel menu, IEventAggregator events)
        {
            this.container = container;
            this.events = events;
            this.MenuViewModel = menu;

            this.events.Subscribe(this);

            ActivateItem(container.GetInstance<UserManagerViewModel>());
            //ActivateItem(container.GetInstance<OrderManagerViewModel>());

        }

        public MenuViewModel MenuViewModel { get; set; }

        public void Handle(AdminInnerViewChangeEvent message)
        {
            var view = message.Screen;
            switch (view)
            {
                case "users":
                    ActivateItem(container.GetInstance<UserManagerViewModel>());
                    break;
                case "orders":
                    ActivateItem(container.GetInstance<OrderManagerViewModel>());
                    break;
                default:
                    break;
            }
        }
    }
}
