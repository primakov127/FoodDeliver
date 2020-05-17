using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public class SignalrService : IDisposable, ISignalrService
    {
        private IHubProxy hubProxy;
        private HubConnection connection;
        private string url = ConfigurationManager.AppSettings["api"];

        public event Action UpdateOrders;

        public SignalrService()
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("ordersHub");

            hubProxy.On("UpdateOrders", () => UpdateOrders?.Invoke());

            connection.Start();
        }

        public void Dispose()
        {
            connection.Stop();
        }
    }
}
