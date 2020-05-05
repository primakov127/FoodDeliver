using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Abstract
{
    public interface IRepository<T, I>
    {
        IEnumerable<T> GetAll();
        T Get(I id);
        T Add(T entity);
        void Remove(I id);
        bool Update(T entity);
    }
}
