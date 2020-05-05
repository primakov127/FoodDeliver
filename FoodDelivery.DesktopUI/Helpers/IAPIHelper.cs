using System.Threading.Tasks;
using FoodDelivery.DesktopUI.Models;

namespace FoodDelivery.DesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}