using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDelivery.DesktopUI.Library.Models;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public interface IAdminEndpoint
    {
        Task<List<StaffModel>> GetAllStaffs();
        Task UpdateStaff(StaffModel staff, string password = null, string email = null);
        Task DeleteStaff(string staffId);
        Task AddStaff(NewStaffParamModel param);
    }
}