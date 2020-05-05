using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel loginVM;

        public ShellViewModel(LoginViewModel loginVM)
        {
            this.loginVM = loginVM;
            ActivateItem(this.loginVM);
        }
    }
}
