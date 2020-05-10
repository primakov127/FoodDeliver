namespace FoodDelivery.DesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Name { get; set; }
        string Phone { get; set; }
        string Position { get; set; }
        string Token { get; set; }
        string UserId { get; set; }

        void ResetUserModel();
    }
}