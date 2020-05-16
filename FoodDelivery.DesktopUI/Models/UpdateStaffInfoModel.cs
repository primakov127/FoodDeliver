using FoodDelivery.DesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Models
{
    public class UpdateStaffInfoModel : PropertyValidateModel
    {
        private string newPassword;
        private string newEmail;

        [RegularExpression(@"\S{6,}", ErrorMessage = "Field must be atleast 6 characters!")]
        public string NewPassword
        {
            get => newPassword;
            set => Set(ref newPassword, value);
        }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email is incorect!")]
        public string NewEmail
        {
            get => newEmail;
            set => Set(ref newEmail, value);
        }
    }
}
