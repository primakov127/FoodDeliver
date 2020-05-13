using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public class CookEndpoint : ICookEndpoint
    {
        private IAPIHelper apiHelper;
        private ILoggedInUserModel user;

        public CookEndpoint(IAPIHelper apiHelper, ILoggedInUserModel user)
        {
            this.apiHelper = apiHelper;
            this.user = user;
        }

        public async Task<List<OrderModel>> GetOrders()
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("api/Order/GetOneCookOrders"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<OrderModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<ProductModel>> GetAllProduct()
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("/api/product/GetAllProducts"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateOrder(OrderModel order)
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.PutAsJsonAsync("api/order/Update", order))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
