using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Concrete;
using FoodDelivery.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodDelivery.WebUI.Controllers.WebAPI
{
    [Authorize]
    public class StaffController : ApiController
    {
        private IRepository<Staff, string> repository = new EFStaffRepository();

        public Staff GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();

            return repository.Get(userId);
        }

        [Route("api/Staff/GetAllCook")]
        public List<Staff> GetAllCook()
        {
            return repository.GetAll().Where(staff => staff.Position == "cook").ToList();
        }

        [Route("api/Admin/GetAllStaffs")]
        [Authorize(Roles="admin")]
        public List<Staff> GetAll()
        {
            return repository.GetAll().ToList();
        }
    }
}
