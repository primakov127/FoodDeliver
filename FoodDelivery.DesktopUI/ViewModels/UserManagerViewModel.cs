using AutoMapper;
using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Helpers;
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
    public class UserManagerViewModel : Screen
    {
        private IAdminEndpoint adminEndpoint;
        private IMapper mapper;
        private BindingList<StaffDisplayModel> staffs;
        private StaffDisplayModel selectedStaff;
        private UpdateStaffInfoModel staffNewInfo;
        private IEventAggregator events;

        public UserManagerViewModel(IAdminEndpoint adminEndpoint, IMapper mapper, IEventAggregator events)
        {
            this.adminEndpoint = adminEndpoint;
            this.mapper = mapper;
            this.events = events;

            var ex = adminEndpoint.GetAllStaffs();
            StaffNewInfo = new UpdateStaffInfoModel();
            AddNewUserView = new AddNewUserViewModel(adminEndpoint, events, mapper, Roles);
        }

        public string[] Roles { get; } = { "admin", "call", "cook" };

        public AddNewUserViewModel AddNewUserView { get; set; }

        public BindingList<StaffDisplayModel> Staffs
        {
            get => staffs;
            set => Set(ref staffs, value);
        }

        public StaffDisplayModel SelectedStaff
        {
            get => selectedStaff;
            set => Set(ref selectedStaff, value);
        }

        public UpdateStaffInfoModel StaffNewInfo
        {
            get => staffNewInfo;
            set => Set(ref staffNewInfo, value);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadStaffs();
        }

        private async Task LoadStaffs()
        {
            var staffList = await adminEndpoint.GetAllStaffs();
            var mappedStaffList = mapper.Map<List<StaffDisplayModel>>(staffList);
            Staffs = new BindingList<StaffDisplayModel>(mappedStaffList);

        }

        public async void Update()
        {
            if (selectedStaff != null && StaffNewInfo.IsValid 
                && selectedStaff.IsValid)
            {
                var mappedStaff = mapper.Map<StaffModel>(selectedStaff);
                await adminEndpoint.UpdateStaff(mappedStaff, StaffNewInfo.NewPassword, StaffNewInfo.NewEmail);
            }
        }

        public async void Delete()
        {
            if (selectedStaff != null)
            {
                await adminEndpoint.DeleteStaff(selectedStaff.UserId);
                await LoadStaffs();
            }
        }

        public void Add()
        {
            events.PublishOnUIThread(new ShowAddMenuEvent());
        }

        public async void RefreshStaff()
        {
            await LoadStaffs();
        }
    }
}
