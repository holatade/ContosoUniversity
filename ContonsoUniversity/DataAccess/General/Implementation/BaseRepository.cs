using DataAccess.General.Interface;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ISession _session;
        protected ITransaction _transaction;

        public BaseRepository(ISession session)
        {
            _session = session;
        }

        public IQueryable<T> QueryAll()
        {
            return _session.Query<T>();
        }

        public async Task<List<T>> GetAll()
        {
            return await _session.Query<T>().ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _session.GetAsync<T>(id);
        }

        public async Task Save(T entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(T entity)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
