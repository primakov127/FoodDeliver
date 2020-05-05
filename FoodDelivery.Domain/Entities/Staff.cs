using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Entities
{
    [Table("Staffs")]
    public class Staff
    {

        [Required, Key]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
    }
}
