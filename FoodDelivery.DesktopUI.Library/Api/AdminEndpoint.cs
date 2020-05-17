using FoodDelivery.DesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public class AdminEndpoint : IAdminEndpoint
    {
        private IAPIHelper apiHelper;

        public AdminEndpoint(IAPIHelper apiHelper)
        {
            this.apiHelper = apiHelper;
        }

        public async Task<List<StaffModel>> GetAllStaffs()
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("/api/Admin/GetAllStaffs"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<StaffModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateStaff(StaffModel staff, string password, string email)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("idParam", staff.UserId),
                new KeyValuePair<string, string>("nameParam", staff.Name),
                new KeyValuePair<string, string>("emailParam", email),
                new KeyValuePair<string, string>("passwordParam", password),
                new KeyValuePair<string, string>("phoneParam", staff.Phone),
                new KeyValuePair<string, string>("positionParam", staff.Position)
            });


            var query = data.ReadAsStringAsync().Result;

            
            using (HttpResponseMessage response = await apiHelper.ApiClient.PostAsync("api/admin/edit?" + query, null))
            {
                if (response.IsSuccessStatusCode)
                {
                    
                }
                else
                {
                    var error = await response.Content.ReadAsAsync<ErrorModel>();
                    throw new Exception(error.Error_description);
                }
            }
        }

        public async Task DeleteStaff(string staffId)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("id", staffId)
            });

            var query = data.ReadAsStringAsync().Result;

            using (HttpResponseMessage response = await apiHelper.ApiClient.PostAsync("api/admin/delete?" + query, null))
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

        public async Task AddStaff(NewStaffParamModel param)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nameParam", param.Name),
                new KeyValuePair<string, string>("emailParam", param.Email),
                new KeyValuePair<string, string>("passwordParam", param.Password),
                new KeyValuePair<string, string>("roleParam", param.Role),
                new KeyValuePair<string, string>("phoneParam", param.Phone)
            });

            var query = data.ReadAsStringAsync().Result;

            using (HttpResponseMessage response = await apiHelper.ApiClient.PostAsync("api/admin/create?" + query, null))
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

        public async Task<List<OrderModel>> GetOrders()
        {
            using (HttpResponseMessage response = await apiHelper.ApiClient.GetAsync("/api/Order/GetAll"))
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
    }
}
