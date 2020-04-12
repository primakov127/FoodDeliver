using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
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
            var x = context.Orders.ToList();
            return context.Orders;
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
                storedOrder.Call = order.Call;
                storedOrder.Cook = order.Cook;
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
