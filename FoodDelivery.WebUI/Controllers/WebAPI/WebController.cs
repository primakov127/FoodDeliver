using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Concrete;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodDelivery.WebUI.Controllers.WebAPI
{
    [Authorize]
    public class WebController : ApiController
    {
        private IRepository<Order, int> repository = new EFOrderRepository();

        [Authorize]
        public IEnumerable<Order> GetAllOrders()
        {
            return repository.GetAll();
        }

        public Order GetOrder(int id)
        {
            return repository.Get(id);
        }

        public Order PostReservation(Order order)
        {
            return repository.Add(order);
        }

        public bool PutOrder(Order order)
        {
            return repository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            repository.Remove(id);
        }

    }
}
