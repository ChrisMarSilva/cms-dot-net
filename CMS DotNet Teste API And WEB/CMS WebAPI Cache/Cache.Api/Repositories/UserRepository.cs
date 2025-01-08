using Cache.Api.Database.Contexts;
using Cache.Api.Models;

namespace Cache.Api.Repositories;

public interface IUserRepository : IRepository<UserModel>
{

}

public class UserRepository : Repository<UserModel>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}
