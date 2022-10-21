using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(object pk);

    }
}
