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
    public class ProductController : ApiController
    {
        private IProductRepository repository = new EFProductRepository();

        public List<Product> GetAllProducts()
        {
            return repository.Products.ToList();
        }
    }
}