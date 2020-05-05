using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string userName;
        private string password;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }


        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (userName?.Length > 0 && password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void LogIn()
        {
            
        }
    }
}
