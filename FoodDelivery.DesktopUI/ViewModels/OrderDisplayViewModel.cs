using AutoMapper;
using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Helpers;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using FoodDelivery.DesktopUI.Models;
using System;
using System.ComponentModel;
using System.Linq;
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
        private CartLineModel newCartLine;

        public OrderDisplayViewModel(OrderModel order, IMapper mapper, ICallEndpoint callEndpoint, IEventAggregator events)
        {
            this.mapper = mapper;
            this.order = mapper.Map<OrderDisplayModel>(order);
            this.callEndpoint = callEndpoint;
            this.events = events;
            this.newCartLine = new CartLineModel { Quantity = 1, OrderId = order.Id };

            IncreaseProductQuantityCommand = new Command(IncreaseProductQuantity, _ => true);
            ReduceProductQuantityCommand = new Command(ReduceProductQuantity, _ => true);
            RemoveProductFromCartCommand = new Command(RemoveProductFromCart, _ => true);
        }

        public Command IncreaseProductQuantityCommand { get; set; }
        public Command ReduceProductQuantityCommand { get; set; }
        public Command RemoveProductFromCartCommand { get; set; }

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

        public CartLineModel NewCartLine
        {
            get => newCartLine;
            set => Set(ref newCartLine, value);
        }

        public bool CanConfirm
        {
            get => !String.IsNullOrEmpty(Order.Cook_UserId);
        }

        public bool CanAddNewCartLineToOrder
        {
            get => NewCartLine.ProductId != 0 && 
                Order.CartLines.Where(line => line.ProductId == NewCartLine.ProductId).FirstOrDefault() == null;
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
            order.Status = "COOK";
            var confirmedOrder = mapper.Map<OrderModel>(order);
            await callEndpoint.UpdateOrder(confirmedOrder);
            events.PublishOnUIThread(new LogOnEvent());
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Order.PropertyChanged += OrderCookIdChanged;
            NewCartLine.PropertyChanged += NewCartLineChanged;
        }

        protected override void OnDeactivate(bool close)
        {
            Order.PropertyChanged -= OrderCookIdChanged;
            NewCartLine.PropertyChanged -= NewCartLineChanged;
            base.OnDeactivate(close);
        }

        protected void OrderCookIdChanged(object sender, EventArgs e)
        {
            NotifyOfPropertyChange(() => CanConfirm);
        }

        protected void NewCartLineChanged(object sender, EventArgs e)
        {
            NotifyOfPropertyChange(() => CanAddNewCartLineToOrder);
        }

        private void IncreaseProductQuantity(object param)
        {
            var cartLine = (param as CartLineModel);
            cartLine.Quantity += 1;
            RecountTotalCost();
        }

        private void ReduceProductQuantity(object param)
        {
            var cartLine = (param as CartLineModel);

            if (cartLine.Quantity > 1)
            {
                cartLine.Quantity -= 1;
                RecountTotalCost();
            }
        }

        private void RemoveProductFromCart(object param)
        {
            var cartLine = (param as CartLineModel);

            if (Order.CartLines.Count > 1)
            {
                Order.CartLines.Remove(cartLine);
                RecountTotalCost();
            }

        }

        private void RecountTotalCost()
        {
            decimal recountedTotalCost = 0;

            foreach (var cartLine in Order.CartLines)
            {
                decimal price = products.Where(product => product.Id == cartLine.ProductId).FirstOrDefault().Price;
                recountedTotalCost += (cartLine.Quantity * price);
            }

            Order.TotalCost = recountedTotalCost;
        }

        public void AddNewCartLineToOrder()
        {
            Order.CartLines.Add(newCartLine);
            RecountTotalCost();
        }
    }
}
