using Cache.Domain.Models;
using Cache.Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Cache.App.Api.Services;

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
        var result = await _repository.GetAllAsync(cancellationToken);

        return result;
    }

    public async Task<UserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _cacheService.GetCacheValueAsync<UserModel>($"user:{id}");

        if (result is null)
        {
            //result = await _repository.GetByIdAsync(x => x.Id == id, cancellationToken);
            //await _cacheService.SetCacheValueAsync<UserModel>($"user:{id}", result);
        }

        return result;
    }

    public async Task<UserModel> CreateAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        //await _repository.AddAsync(userModel);
        await _repository.SaveChangesAsync();

        return userModel;
    }

    public async Task<UserModel?> UpdateAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
       // _repository.Update(userModel, cancellationToken);
        await _repository.SaveChangesAsync();

        return userModel;
    }

    public async Task DeleteByIdAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        _repository.Delete(userModel);
        await _repository.SaveChangesAsync();
    }
}
