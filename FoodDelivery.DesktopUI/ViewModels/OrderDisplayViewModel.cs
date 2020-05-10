using Caliburn.Micro;
using FoodDelivery.DesktopUI.Library.Models;
using FoodDelivery.DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class OrderDisplayViewModel : Screen
    {
        private OrderDisplayModel order;

        public OrderDisplayViewModel(OrderModel order)
        {
            
        }

        public OrderDisplayModel Order
        {
            get => order;
            set => Set(ref order, value);
        }


    }
}
