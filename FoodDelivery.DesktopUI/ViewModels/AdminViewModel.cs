using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class AdminViewModel : Conductor<object>
    {
        private SimpleContainer container;

        public AdminViewModel(SimpleContainer container, MenuViewModel menu)
        {
            this.container = container;
            this.MenuViewModel = menu;

            ActivateItem(container.GetInstance<UserManagerViewModel>());

        }

        public MenuViewModel MenuViewModel { get; set; }

    }
}
