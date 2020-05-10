using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }

        public void ResetUserModel()
        {
            Token = "";
            UserId = "";
            Name = "";
            Phone = "";
            Position = "";
        }
    }
}
