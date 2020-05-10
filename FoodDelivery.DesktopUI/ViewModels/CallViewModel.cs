using Caliburn.Micro;
using FoodDelivery.DesktopUI.Helpers;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class CallViewModel : Screen
    {
        private BindingList<OrderModel> orders;
        private ICallEndpoint callEndpoint;
        private DispatcherTimer timer;
        private IEventAggregator events;

        public CallViewModel(ICallEndpoint callEndpoint, IEventAggregator events, MenuViewModel menu)
        {
            this.callEndpoint = callEndpoint;
            this.MenuViewModel = menu;
            this.events = events;

            AcceptCommand = new Command(Accept, x => true);
            timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(5)};
            timer.Tick += TimeUpdateOrderList;
        }

        ~CallViewModel()
        {
            timer.Stop();
        }

        public Command AcceptCommand { get; set; }

        public MenuViewModel MenuViewModel { get; set; }

        public BindingList<OrderModel> Orders
        {
            get => orders;
            set
            {
                orders = value;
                NotifyOfPropertyChange(() => Orders);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadOrders();
            //timer.Start();
        }

        private async Task LoadOrders()
        {
            var orderList = await callEndpoint.GetAllNewOrders();
            Orders = new BindingList<OrderModel>(orderList);
        }

        private void Accept(object param)
        {
            var order = (param as OrderModel);
            // ПРОВЕРКА ПЕРЕД ОТПРАВКОЙ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
            order.Call_UserId = callEndpoint.User.UserId;
            order.Status = "CALL";
            callEndpoint.UpdateOrder(order);
            events.PublishOnUIThread(order);
        }

        private async void TimeUpdateOrderList(object sender, EventArgs e)
        {
            await LoadOrders();
        }

    }
}
