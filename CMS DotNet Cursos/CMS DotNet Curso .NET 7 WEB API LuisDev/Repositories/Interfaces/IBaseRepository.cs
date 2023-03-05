using AwesomeDevEvents.API.Models.Entities;
using System.Threading.Tasks;

namespace AwesomeDevEvents.API.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        bool SaveChanges();
        bool SaveChangesAsync();
    }
}
