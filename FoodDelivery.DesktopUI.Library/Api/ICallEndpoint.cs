using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDelivery.DesktopUI.Library.Models;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public interface ICallEndpoint
    {
        Task<List<OrderModel>> GetAllNewOrders();
        Task<List<StaffModel>> GetAllCook();
        Task<List<ProductModel>> GetAllProduct();
        Task UpdateOrder(OrderModel order);
        ILoggedInUserModel User { get; }
    }
}