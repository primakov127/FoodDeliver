using FoodDelivery.Domain.Abstract;
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

        public List<Order> GetOrdersByCookId(string cookId)
        {
            return context.Orders.Where(order => order.Cook_UserId == cookId && order.Status == "COOK").ToList();
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

            if (storedOrder == null)
            {
                return false;
            }

            storedOrder.ClientName = order.ClientName;
            storedOrder.ClientAddress = order.ClientAddress;
            storedOrder.ClientPhone = order.ClientPhone;
            storedOrder.Comment = order.Comment;
            storedOrder.Status = order.Status;
            storedOrder.Date = order.Date;
            storedOrder.Call_UserId = order.Call_UserId;
            storedOrder.Cook_UserId = order.Cook_UserId;
            storedOrder.TotalCost = order.TotalCost;
            

            if (order.OrderedProducts != null)
            {
                // Update stored Order
                foreach (var orderedProduct in storedOrder.OrderedProducts.ToList())
                {
                    var matchingLine = order.OrderedProducts.Where(orderLine => orderLine.ProductId == orderedProduct.ProductId).FirstOrDefault();
                    if (matchingLine != null)
                    {
                        orderedProduct.Quantity = matchingLine.Quantity;
                    }
                    else
                    {
                        storedOrder.OrderedProducts.Remove(orderedProduct);
                    }
                }

                // Update stored Order with added products
                foreach (var product in order.OrderedProducts)
                {
                    var mathcingLine = storedOrder.OrderedProducts.Where(orderLine => orderLine.ProductId == product.ProductId).FirstOrDefault();
                    if (mathcingLine == null)
                    {
                        storedOrder.OrderedProducts.Add(product);
                    }
                }
            }

            context.SaveChanges();
            return true;
        }
    }
}
