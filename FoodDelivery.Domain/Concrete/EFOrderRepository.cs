﻿using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Concrete
{
    public class EFOrderRepository : IRepository<Order, int>
    {
        EFDbContext context = new EFDbContext();

        public Order Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order Get(int id)
        {
            return context.Orders.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            //var x = context.Orders.ToList();
            return context.Orders;
        }

        public List<Order> GetNewOrders()
        {
            return context.Orders.Where(order => order.Status == "NEW").ToList();
        }

        public void Remove(int id)
        {
            Order order = Get(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }

        public bool Update(Order order)
        {
            Order storedOrder = Get(order.Id);
            if (storedOrder != null)
            {
                storedOrder.ClientName = order.ClientName;
                storedOrder.ClientAddress = order.ClientAddress;
                storedOrder.ClientPhone = order.ClientPhone;
                storedOrder.Comment = order.Comment;
                storedOrder.Status = order.Status;
                storedOrder.Date = order.Date;
                storedOrder.Call_UserId = order.Call_UserId;
                storedOrder.Cook_UserId = order.Cook_UserId;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
