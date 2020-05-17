using AutoMapper;
using Caliburn.Micro;
using FoodDelivery.DesktopUI.EventModels;
using FoodDelivery.DesktopUI.Helpers;
using FoodDelivery.DesktopUI.Library.Api;
using FoodDelivery.DesktopUI.Library.Models;
using FoodDelivery.DesktopUI.Models;
using FoodDelivery.DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FoodDelivery.DesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        private IMapper ConfigureAutomapper()
        {
            var config = new MapperConfiguration(cnfg =>
            {
                cnfg.CreateMap<OrderedProductsModel, CartLineModel>()
                .ReverseMap();

                cnfg.CreateMap<OrderModel, OrderDisplayModel>()
                .ForMember(dest => dest.CartLines, opt => opt.MapFrom(src => src.OrderedProducts))
                .ReverseMap();

                cnfg.CreateMap<NewStaffParamModel, AddStaffInfoModel>()
                .ReverseMap();

                cnfg.CreateMap<StaffModel, StaffDisplayModel>()
                .ReverseMap();
            });

            var mapper = config.CreateMapper();

            return mapper;
        }

        protected override void Configure()
        {
            container.Instance(ConfigureAutomapper());

            container.Instance(container)
                .PerRequest<ICallEndpoint, CallEndpoint>()
                .PerRequest<ICookEndpoint, CookEndpoint>()
                .PerRequest<IAdminEndpoint, AdminEndpoint>();

            container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IAPIHelper, APIHelper>()
                .Singleton<ISignalrService, SignalrService>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override async void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await container.GetInstance<IEventAggregator>().PublishOnUIThreadAsync(new ExceptionEvent(e.Exception.InnerException?.Message ?? e.Exception.Message));
        }
    }
}
