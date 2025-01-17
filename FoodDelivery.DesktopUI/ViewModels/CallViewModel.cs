﻿using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Helpers;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.ComponentModel;
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
        private ISignalrService signalrService;

        public CallViewModel(ICallEndpoint callEndpoint, IEventAggregator events, MenuViewModel menu, ISignalrService signalrService)
        {
            this.callEndpoint = callEndpoint;
            this.MenuViewModel = menu;
            this.events = events;
            this.signalrService = signalrService;

            AcceptCommand = new Command(Accept, _ => true);
        }

        public Command AcceptCommand { get; set; }

        public MenuViewModel MenuViewModel { get; set; }

        public BindingList<OrderModel> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadOrders();

            // Subscribe on the Server OrdersUpdate event
            signalrService.UpdateOrders += async () => await LoadOrders();
        }

        protected override void OnDeactivate(bool close)
        {
            signalrService.UpdateOrders -= async () => await LoadOrders();
            base.OnDeactivate(close);
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
            events.PublishOnUIThread(new ProccesOrderEvent(order, callEndpoint));
        }

        private async void TimeUpdateOrderList(object sender, EventArgs e)
        {
            await LoadOrders();
        }

    }
}
