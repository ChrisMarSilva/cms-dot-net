using System.Threading.Tasks;

namespace Core.CMS.Data.Contexts
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
