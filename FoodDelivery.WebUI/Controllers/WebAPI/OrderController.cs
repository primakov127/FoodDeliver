﻿using FoodDelivery.Domain.Abstract;
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
    public class OrderController : ApiController
    {
        private IRepository<Order, int> repository = new EFOrderRepository();

        [Route("api/Order/GetAllNewOrders")]
        public List<Order> GetAllNewOrders()
        {
            return ((EFOrderRepository)repository).GetNewOrders();
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
            return repository.Add(order);
        }

        [HttpPut]
        public bool UpdateOrder(Order order)
        {
            
            return repository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            repository.Remove(id);
        }

    }
}