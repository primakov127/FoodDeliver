using Caliburn.Micro;
using FoodDelivery.DesktopUI.Models;
using FoodDelivery.DesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FoodDelivery.DesktopUI.Library.Models;
using FoodDelivery.DesktopUI.Helpers;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class CookViewModel : Screen
    {
        private BindingList<OrderDisplayModel> orders;
        private ICookEndpoint cookEndpoint;
        private IMapper mapper;
        private ISignalrService signalrService;
        private BindingList<ProductModel> products;

        public CookViewModel(ICookEndpoint cookEndpoint, MenuViewModel menu, IMapper mapper, ISignalrService signalrService)
        {
            this.cookEndpoint = cookEndpoint;
            this.mapper = mapper;
            this.signalrService = signalrService;
            this.MenuViewModel = menu;

            ReadyCommand = new Command(Ready, _ => true);
            
        }

        public Command ReadyCommand { get; set; }

        public MenuViewModel MenuViewModel { get; set; }

        public BindingList<OrderDisplayModel> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }

        public BindingList<ProductModel> Products
        {
            get => products;
            set => Set(ref products, value);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadOrders();
            await LoadProduct();

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
            var orderList = await cookEndpoint.GetOrders();
            var mappedOrderList = mapper.Map<List<OrderDisplayModel>>(orderList);
            Orders = new BindingList<OrderDisplayModel>(mappedOrderList);
        }

        private async Task LoadProduct()
        {
            var loadProducts = await cookEndpoint.GetAllProduct();
            Products = new BindingList<ProductModel>(loadProducts);
        }

        private async void Ready(object param)
        {
            var order = (param as OrderDisplayModel);
            order.Status = "READY";
            var readyOrded = mapper.Map<OrderModel>(order);
            await cookEndpoint.UpdateOrder(readyOrded);
        }
    }
}
