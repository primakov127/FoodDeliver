using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Concrete
{
    public class EFStaffRepository : IRepository<Staff, string>
    {
        EFDbContext context = new EFDbContext();

        public Staff Add(Staff staff)
        {
            context.Staffs.Add(staff);
            context.SaveChanges();
            return staff;
        }

        public Staff Get(string id)
        {
            return context.Staffs.Where(x => x.UserId == id).FirstOrDefault();
        }

        public IEnumerable<Staff> GetAll()
        {
            return context.Staffs;
        }

        public void Remove(string id)
        {
            Staff staff = Get(id);
            if (staff != null)
            {
                context.Staffs.Remove(staff);
                context.SaveChanges();
            }
        }

        public bool Update(Staff entity)
        {
            Staff staff = Get(entity.UserId);
            if (staff != null)
            {
                staff.Name = entity.Name;
                staff.Phone = entity.Phone;
                staff.Position = entity.Position;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
