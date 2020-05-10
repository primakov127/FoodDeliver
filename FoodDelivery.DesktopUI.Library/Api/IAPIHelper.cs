using System.Net.Http;
using System.Threading.Tasks;
using FoodDelivery.DesktopUI.Library.Models;

namespace FoodDelivery.DesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<AuthenticatedUser> Authenticate(string username, string password);

        Task<ILoggedInUserModel> GetLoggedInUserInfo(string token);

        void LogOutUser();
        
    }
}