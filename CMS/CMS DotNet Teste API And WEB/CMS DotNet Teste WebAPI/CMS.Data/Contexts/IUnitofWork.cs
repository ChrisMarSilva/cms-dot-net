using System.Threading.Tasks;

namespace CMS.Data.Contexts
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
