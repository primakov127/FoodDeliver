using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Library.Api
{
    class CookEndpoint
    {
        private IAPIHelper apiHelper;
        private ILoggedInUserModel user;

        public CookEndpoint(IAPIHelper apiHelper, ILoggedInUserModel user)
        {
            this.apiHelper = apiHelper;
            this.user = user;
        }



    }
}
