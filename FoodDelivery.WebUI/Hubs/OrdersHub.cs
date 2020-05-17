using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodDelivery.WebUI.Hubs
{
    [HubName("ordersHub")]
    public class OrdersHub : Hub
    {
        public static void NotifyOrdersUpdateToAllClient()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();

            context.Clients.All.UpdateOrders();
        }
    }
}