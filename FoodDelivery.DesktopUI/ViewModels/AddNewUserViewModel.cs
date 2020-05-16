using AutoMapper;
using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using FoodDelivery.DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.ViewModels
{
    public class AddNewUserViewModel : Screen, IHandle<ShowAddMenuEvent>
    {
        private AddStaffInfoModel newUser;
        private IAdminEndpoint adminEndpoint;
        private IEventAggregator events;
        private IMapper mapper;
        private bool isVisible;

        public AddNewUserViewModel(IAdminEndpoint adminEndpoint, IEventAggregator events, IMapper mapper, string[] roles)
        {
            this.adminEndpoint = adminEndpoint;
            this.events = events;
            this.mapper = mapper;
            this.Roles = roles;

            this.events.Subscribe(this);
            IsVisible = false;
        }

        public string[] Roles { get; }

        public bool IsVisible
        {
            get => isVisible;
            set => Set(ref isVisible, value);
        }

        public AddStaffInfoModel NewUser
        {
            get => newUser;
            set => Set(ref newUser, value);
        }

        public async void Add()
        {
            if (NewUser.IsValid)
            {
                var mappedStaff = mapper.Map<NewStaffParamModel>(NewUser);
                await adminEndpoint.AddStaff(mappedStaff);
                Close();
            }
        }

        public void Close()
        {
            NewUser = null;
            IsVisible = false;
        }

        public void Open()
        {
            IsVisible = true;
            NewUser = new AddStaffInfoModel();
        }

        public void Handle(ShowAddMenuEvent message)
        {
            Open();
        }
    }
}
