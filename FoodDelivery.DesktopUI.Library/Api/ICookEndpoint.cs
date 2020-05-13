using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDelivery.DesktopUI.Library.Models;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public interface ICookEndpoint
    {
        Task<List<OrderModel>> GetOrders();
        Task<List<ProductModel>> GetAllProduct();
        Task UpdateOrder(OrderModel order);
    }
}