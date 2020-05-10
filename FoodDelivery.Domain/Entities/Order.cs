using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Введите адрес доставки")]
        public string ClientAddress { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        public string ClientPhone { get; set; }

        public decimal TotalCost { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Call_UserId { get; set; }
        public string Cook_UserId { get; set; }

        [ForeignKey("Call_UserId")]
        public virtual Staff Call { get; set; }

        [ForeignKey("Cook_UserId")]
        public virtual Staff Cook { get; set; }

        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
    }
}
