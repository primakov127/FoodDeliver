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
    public class LoginViewModel : Screen
    {
        private string userName;
        private string password;
        private IAPIHelper apiHelper;
        private IEventAggregator events;
        private string errorMessage;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            this.apiHelper = apiHelper;
            this.events = events;
        }

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

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }


        public bool CanLogIn
        {
            get => userName?.Length > 0 && password?.Length > 0;
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await apiHelper.Authenticate(UserName, Password);

                // Capture more information about the User
                var user = await apiHelper.GetLoggedInUserInfo(result.Access_Token);

                events.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
