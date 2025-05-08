using Cache.Domain.Models;
using Cache.Domain.Repository;
using Cache.Infra.Data.Context;

namespace Cache.Infra.Data.Repository;


public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    public UserRepository(AppDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}
