using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Concrete
{
    public class DBOrderProcessor : IOrderProcessor
    {

        public void ProcessOrder(Cart cart, Order order)
        {
            EFDbContext context = new EFDbContext();
            order.Status = "NEW";
            order.Date = DateTime.Now;
            context.Orders.Add(order);
            context.SaveChanges();
            foreach (var line in cart.Lines)
            {
                // Добавить метод добавления
                OrderedProducts product = new OrderedProducts()
                {
                    OrderId = order.Id,
                    ProductId = line.Product.Id,
                    Quantity = line.Quantity,
                    //Product = line.Product
                };
                //order.OrderedProducts.Add(product);
                context.OrderedProducts.Add(product);
            }
            context.SaveChanges();
        }
    }
}
