using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Concrete;
using FoodDelivery.Domain.Entities;
using FoodDelivery.WebUI.Hubs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodDelivery.WebUI.Controllers.WebAPI
{
    [Authorize]
    public class OrderController : ApiController
    {
        private IRepository<Order, int> repository = new EFOrderRepository();

        [Route("api/Order/GetAllNewOrders")]
        public List<Order> GetAllNewOrders()
        {
            return ((EFOrderRepository)repository).GetNewOrders();
        }

        [Route("api/Order/GetOneCookOrders")]
        public List<Order> GetOneCookOrders()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            return ((EFOrderRepository)repository).GetOrdersByCookId(userId);
        }
        
        public IEnumerable<Order> GetAllOrders()
        {
            return repository.GetAll();
        }

        public Order GetOrder(int id)
        {
            return repository.Get(id);
        }

        public Order AddOrder(Order order)
        {
            var result = repository.Add(order);

            // Notify all Staff that orders table was updated
            OrdersHub.NotifyOrdersUpdateToAllClient();

            return result;
        }

        [HttpPut]
        public bool UpdateOrder(Order order)
        {
            var result = repository.Update(order);

            // Notify all Staff that orders table was updated
            OrdersHub.NotifyOrdersUpdateToAllClient();

            return result;
        }

        public void DeleteOrder(int id)
        {
            repository.Remove(id);

            // Notify all Staff that orders table was updated
            OrdersHub.NotifyOrdersUpdateToAllClient();
        }

    }
}
