using System;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public interface ISignalrService
    {
        event Action UpdateOrders;
    }
}