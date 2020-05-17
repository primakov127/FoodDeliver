using Caliburn.Micro;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class OrderManagerViewModel : Screen
    {
        private IAdminEndpoint adminEndpoint;
        private BindingList<OrderModel> orders;
        private List<OrderModel> storedList;
        private BindingList<StaffModel> staffs;
        private DateTime? selectedDate;

        public OrderManagerViewModel(IAdminEndpoint adminEndpoint)
        {
            this.adminEndpoint = adminEndpoint;
        }

        public BindingList<StaffModel> Staffs
        {
            get => staffs;
            set => Set(ref staffs, value);
        }

        public BindingList<OrderModel> Orders
        {
            get => orders;
            set => Set(ref orders, value);
        }

        public DateTime? SelectedDate
        {
            get => selectedDate;
            set
            {
                Set(ref selectedDate, value);
                NotifyOfPropertyChange(() => CanSearch);
            }
        }

        private async Task LoadOrders()
        {
            storedList = await adminEndpoint.GetOrders();
            Orders = new BindingList<OrderModel>(storedList);
        }

        private async Task LoadStaffs()
        {
            var staffList = await adminEndpoint.GetAllStaffs();
            Staffs = new BindingList<StaffModel>(staffList);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            LoadOrders();
            LoadStaffs();
        }

        public async void Update()
        {
            await LoadOrders();
            await LoadStaffs();
        }

        public bool CanSearch => SelectedDate != null;

        public void Search()
        {
            var findedOrders = storedList.Where(order => order.Date.Date == SelectedDate).ToList();
            Orders = new BindingList<OrderModel>(findedOrders);
        }

    }
}
