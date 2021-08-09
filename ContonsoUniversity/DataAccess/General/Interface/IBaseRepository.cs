using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> QueryAll();       
        Task Save(T entity);
        Task Delete(T entity);
        Task<List<T>> GetAll();
        Task<T> GetAsync(Guid id);
    }
}
