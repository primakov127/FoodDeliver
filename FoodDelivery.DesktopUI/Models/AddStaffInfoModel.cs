using FoodDelivery.DesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Models
{
    public class AddStaffInfoModel : PropertyValidateModel
    {
        private string name;
        private string email;
        private string password;
        private string role;
        private string phone;

        [Required]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email is incorect!")]
        public string Email
        {
            get => email;
            set => Set(ref email, value);
        }

        [Required]
        [RegularExpression(@"\S{6,}", ErrorMessage = "Field must be atleast 6 characters!")]
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }

        [Required]
        public string Role
        {
            get => role;
            set => Set(ref role, value);
        }

        [Required]
        [RegularExpression(@"(375)(29|33|44)(\d{7})", ErrorMessage = "Phone number is incorrect!")]
        public string Phone
        {
            get => phone;
            set => Set(ref phone, value);
        }

    }
}
