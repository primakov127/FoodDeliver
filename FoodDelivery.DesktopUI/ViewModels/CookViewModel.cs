using Caliburn.Micro;
using FoodDelivery.DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class CookViewModel : Screen
    {
        private BindingList<OrderDisplayModel> orders;

        public CookViewModel()
        {

        }

        public BindingList<OrderDisplayModel> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }
    }
}
