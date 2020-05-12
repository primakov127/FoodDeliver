using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Models
{
    public class CartLineModel : PropertyChangedBase
    {
        private int orderId;
        private int productId;
        private int quantity;

        public int OrderId
        {
            get => orderId;
            set => Set(ref orderId, value);
        }

        public int ProductId
        {
            get => productId;
            set => Set(ref productId, value);
        }

        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }
    }
}
