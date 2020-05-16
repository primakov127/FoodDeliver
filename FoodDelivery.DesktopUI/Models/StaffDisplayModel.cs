using Caliburn.Micro;
using FoodDelivery.DesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Models
{
    public class StaffDisplayModel : PropertyValidateModel
    {
        private string name;
        private string phone;
        private string position;
        private string userId;

        [Required]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        [Required]
        [RegularExpression(@"(375)(29|33|44)(\d{7})", ErrorMessage = "Phone number is incorrect!")]
        public string Phone
        {
            get => phone;
            set => Set(ref phone, value);
        }

        public string Position
        {
            get => position;
            set => Set(ref position, value);
        }

        public string UserId
        {
            get => userId;
            set => Set(ref userId, value);
        }

    }
}
