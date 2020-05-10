using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.DesktopUI.Models
{
    public class OrderDisplayModel : PropertyChangedBase
    {
        private int id;
        private string clientName;
        private string clientAddress;
        private string clientPhone;
        private decimal totalCost;
        private string comment;
        private string status;
        private DateTime date;
        private string call_UserId;
        private string cook_UserId;

        public int Id
        {
            get => id;
            set => Set(ref id, value);
        }

        public string ClientName
        {
            get => clientName;
            set => Set(ref clientName, value);
        }

        public string ClientAddress
        {
            get => clientAddress;
            set => Set(ref clientAddress, value);
        }

        public string ClientPhone
        {
            get => clientPhone;
            set => Set(ref clientPhone, value);
        }

        public decimal TotalCost
        {
            get => totalCost;
            set => Set(ref totalCost, value);
        }

        public string Comment
        {
            get => comment;
            set => Set(ref comment, value);
        }

        public string Status
        {
            get => status;
            set => Set(ref status, value);
        }

        public DateTime Date
        {
            get => date;
            set => Set(ref date, value);
        }

        public string Call_UserId
        {
            get => call_UserId;
            set => Set(ref call_UserId, value);
        }

        public string Cook_UserId
        {
            get => cook_UserId;
            set => Set(ref cook_UserId, value);
        }
    }
}
