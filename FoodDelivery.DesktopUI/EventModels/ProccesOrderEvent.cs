using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.EventModels
{
    public class ProccesOrderEvent
    {
        public ProccesOrderEvent(OrderModel order, ICallEndpoint callEndpoint)
        {
            this.Order = order;
            this.CallEndpoint = callEndpoint;
        }

        public OrderModel Order { get; set; }
        public ICallEndpoint CallEndpoint { get; set; }
    }
}
