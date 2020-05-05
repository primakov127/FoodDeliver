using FoodDelivery.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using FoodDelivery.Domain.Entities;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace FoodDelivery.WebUI.Controllers.WebAPI
{
    [Authorize(Roles = "admin")]
    public class AdminController : ApiController
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        public IEnumerable<AppUser> GetAll()
        {
            return UserManager.Users;
        }

        [HttpPost]
        public async Task<string> Create(string nameParam, string emailParam, string passwordParam, string roleParam, string phoneParam)
        {
            AppUser user = new AppUser { UserName = nameParam, Email = emailParam };
            IdentityResult result = await UserManager.CreateAsync(user, passwordParam);
            
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleParam);
                EFStaffRepository staff = new EFStaffRepository();
                staff.Add(new Staff
                {
                    Name = nameParam,
                    Position = roleParam,
                    UserId = user.Id,
                    Phone = phoneParam
                });
                return "User has been created";
            }
            else
            {
                if (result.Errors.Count() == 0)
                    return "User has not been created. ERROR";
                return string.Join(", ", result.Errors.ToArray());
            }
        }

        [HttpPost]
        public async Task<string> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return "OK";
                }
                else
                {
                    return "NO";
                }
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No User with ID = {id}"),
                    ReasonPhrase = "User ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
        }

        [HttpPost]
        public async Task<bool> Edit(string idParam, string nameParam, string emailParam, string passwordParam)
        {
            AppUser user = await UserManager.FindByIdAsync(idParam);
            if (user != null)
            {
                if (!String.IsNullOrEmpty(nameParam))
                {
                    user.UserName = nameParam;
                }
                if (!String.IsNullOrEmpty(emailParam))
                {
                    user.Email = emailParam;
                }
                if (!String.IsNullOrEmpty(passwordParam))
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(passwordParam);
                }

                await UserManager.UpdateAsync(user);
                return true;
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No User with ID = {idParam}"),
                    ReasonPhrase = "User ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}
