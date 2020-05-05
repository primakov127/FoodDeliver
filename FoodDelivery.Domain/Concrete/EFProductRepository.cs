using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void Update(Product product)
        {
            Product storedProduct = context.Products.Find(product.Id);

            if (storedProduct != null)
            {
                storedProduct.Name = product.Name;
                storedProduct.Weight = product.Weight;
                storedProduct.Price = product.Price;
                storedProduct.Category = product.Category;
                storedProduct.Description = product.Description;
                storedProduct.ImageMimeType = product.ImageMimeType;
                storedProduct.ImageData = product.ImageData;
            }

            context.SaveChanges();
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public Product Remove(int productId)
        {
            Product storedProduct = context.Products.Find(productId);

            if (storedProduct != null)
            {
                context.Products.Remove(storedProduct);
                context.SaveChanges();
            }

            return storedProduct;
        }
    }
}
