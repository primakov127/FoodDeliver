using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Library.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public decimal TotalCost { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Call_UserId { get; set; }
        public string Cook_UserId { get; set; }
    }
}
