using Cache.Api.Models;
using Cache.Api.Repositories;

namespace Cache.Api.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserModel> CreateAsync(UserModel userModel, CancellationToken cancellationToken = default);
    Task<UserModel?> UpdateAsync(UserModel userModel, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(UserModel userModel, CancellationToken cancellationToken = default);
}

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _repository;
    private readonly ICacheService _cacheService;

    public UserService(ILogger<UserService> logger, IUserRepository repository, ICacheService cacheService)
    {
        _logger = logger;
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _cacheService.GetCacheValueAsync<IEnumerable<UserModel>>("user:list");

        if (result is null || result.Count() == 0)
        {
            result = await _repository.GetAllAsync(cancellationToken);
            await _cacheService.SetCacheValueAsync<IEnumerable<UserModel>>("user:list", result);
        }

        return result;
    }

    public async Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<UserModel> CreateAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(userModel);
        await _repository.SaveChangesAsync();

        return userModel;
    }

    public async Task<UserModel?> UpdateAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        _repository.Update(userModel, cancellationToken);
        await _repository.SaveChangesAsync();

        return userModel;
    }

    public async Task DeleteByIdAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        _repository.Delete(userModel);
        await _repository.SaveChangesAsync();
    }
}
