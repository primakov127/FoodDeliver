using AutoMapper;
using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using FoodDelivery.DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class OrderDisplayViewModel : Screen
    {
        private OrderDisplayModel order;
        private IMapper mapper;
        private ICallEndpoint callEndpoint;
        private BindingList<StaffModel> cook;
        private BindingList<ProductModel> products;
        private IEventAggregator events;

        public OrderDisplayViewModel(OrderModel order, IMapper mapper, ICallEndpoint callEndpoint, IEventAggregator events)
        {
            this.mapper = mapper;
            this.order = mapper.Map<OrderDisplayModel>(order);
            this.callEndpoint = callEndpoint;
            this.events = events;

            
        }

        public OrderDisplayModel Order
        {
            get => order;
            set => Set(ref order, value);
        }

        public BindingList<StaffModel> Cook
        {
            get => cook;
            set => Set(ref cook, value);
        }

        public BindingList<ProductModel> Products
        {
            get => products;
            set => Set(ref products, value);
        }

        public bool CanConfirm
        {
            get => !String.IsNullOrEmpty(Order.Cook_UserId);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCook();
            await LoadProduct();
        }

        private async Task LoadCook()
        {
            var loadCook = await callEndpoint.GetAllCook();
            Cook = new BindingList<StaffModel>(loadCook);
        }

        private async Task LoadProduct()
        {
            var loadProducts = await callEndpoint.GetAllProduct();
            Products = new BindingList<ProductModel>(loadProducts);
        }

        public async void Confirm()
        {
            var confirmedOrder = mapper.Map<OrderModel>(order);
            await callEndpoint.UpdateOrder(confirmedOrder);
            events.PublishOnUIThread(new LogOnEvent());
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Order.PropertyChanged += OrderCookIdChanged;
        }

        protected override void OnDeactivate(bool close)
        {
            Order.PropertyChanged -= OrderCookIdChanged;
            base.OnDeactivate(close);
        }

        protected void OrderCookIdChanged(object sender, EventArgs e)
        {
            NotifyOfPropertyChange(() => CanConfirm);
        }
    }
}
