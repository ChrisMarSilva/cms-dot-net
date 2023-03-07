using AutoMapper;
using AwesomeDevEvents.Domain.Dtos;
using AwesomeDevEvents.Domain.Models;
using AwesomeDevEvents.Infrastructure.Persistence.Interfaces;
using AwesomeDevEvents.Infrastructure.Repositories.Interfaces;
using AwesomeDevEvents.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AwesomeDevEvents.Service
{
    public class DevEventService : IDevEventService
    {

        private readonly ILogger<DevEventService> _logger;
        private IDevEventRepository _eventRepo;
        private readonly IMemoryCache _cache;
        // private IDistributedCache _cache;
        private IMapper _mapper;
        private IUnitofWork _uow; // _unitOfWork
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public DevEventService(
            ILogger<DevEventService> logger, 
            IDevEventRepository eventRepository,
            IMemoryCache cache,
            // IDistributedCache cache
            IMapper mapper,
            IUnitofWork uow
            )
        {
            _logger = logger;
            _eventRepo = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository)); // DevEventRepository
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            //_memoryCache = memoryCache;
            _mapper = mapper;
            _uow = uow;

            _logger.LogInformation("AwesomeDevEvents.API.DevEventSerrvice");
        }

        public async Task<IEnumerable<DevEventOutputDto>> GetAllAsync()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventService.GetAll()");
            try
            {
                //  var devEvents = await _eventRepo.FindAllAsync();               

                _logger.LogInformation("Trying to fetch the list of DevEvent from cache.");
                
                if (_cache.TryGetValue(CacheKeys.DevEvents, out IEnumerable<DevEvent> devEvents))
                {
                    _logger.Log(LogLevel.Information, "DevEvent list found in cache.");
                }
                else 
                {
                    try
                    {
                        await _semaphore.WaitAsync();

                        _logger.LogInformation("Trying to fetch the list of DevEvent from cache - semaphore.WaitAsync().");

                        if (_cache.TryGetValue(CacheKeys.DevEvents, out devEvents))
                        {
                            _logger.Log(LogLevel.Information, "DevEvent list found in cache - semaphore.WaitAsync().");
                        }
                        else
                        {
                            _logger.Log(LogLevel.Information, "DevEvent list not found in cache. Fetching from database.");

                            devEvents = await _eventRepo.FindAllAsync();

                            var cacheEntryOptions = new MemoryCacheEntryOptions {
                                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                                SlidingExpiration = TimeSpan.FromSeconds(60),
                                Priority = CacheItemPriority.Normal,
                                Size = 1024,
                            };

                            _cache.Set(CacheKeys.DevEvents, devEvents, cacheEntryOptions);
                            // _cache.Remove(CacheKeys.DevEvents);
                        }
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }

                // var devEvents = _cacheProvider.GetCachedResponse().Result;

                var results = _mapper.Map<List<DevEventOutputDto>>(devEvents);
                //var results = devEvents.Select(c => new DevEventOutputDto(c.Id, c.Title, c.Description, c.Speakers));
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventService.FindAll(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventOutputDto> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventService.GetById()");
            try
            {
                var devEvent = await _eventRepo.FindByIdAsync(id);

                var result = _mapper.Map<DevEventOutputDto>(devEvent);
                //var result = new DevEventOutputDto(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.Speakers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventService.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventOutputDto> InsertAsync(DevEventInputDto input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventService.Post()");
            try
            {
                var devEvent = _mapper.Map<DevEvent>(input);
                //var devEvent = new DevEvent(input.title, input.description);

                if (!devEvent.IsValid)
                    throw new Exception("");  // devEvent.Notifications.ConvertToProblemDetails();

                var devEventNew = await _eventRepo.CreateAsync(devEvent);
                //if (!devEvent.IsValid)
                //    return BadRequest(devEvent.Notifications.ConvertToProblemDetails());

                var resultCommit = await _uow.CommitAsync();
                //if (!resultCommit)
                //    return BadRequest();

                var result = _mapper.Map<DevEventOutputDto>(devEventNew);
                //var result = new DevEventOutputDto(devEventNew.Id, devEventNew.Title, devEventNew.Description, devEventNew.Speakers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventService.Create(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventOutputDto> UpdateAsync(Guid id, DevEventInputDto input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventService.Update()");
            try
            {
                var devEvent = await _eventRepo.FindByIdAsync(id);
                if (devEvent == null || devEvent?.Id == Guid.Empty)
                    return null;

                devEvent.Update(title: input.title, description: input.description);

                if (!devEvent.IsValid)
                    return null;  // return BadRequest(devEvent.Notifications.ConvertToProblemDetails());

                var devEventNew = _eventRepo.Update(devEvent);
                if (devEventNew == null || devEventNew?.Id == Guid.Empty)
                    return null;

                var resultCommit = await _uow.CommitAsync();
                if (!resultCommit)
                    return null;

                var result = _mapper.Map<DevEventOutputDto>(devEventNew);
                //var result = new DevEventOutput(devEventNew.Id, devEventNew.Title, devEventNew.Description, devEventNew.Speakers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventService.Update(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventService.Delete()");
            try
            {
                var devEvent = await _eventRepo.FindByIdAsync(id);
                if (devEvent == null || devEvent?.Id == Guid.Empty)
                    return false;

                var status = _eventRepo.Delete(devEvent);
                if (!status)
                    return false;

                var resultCommit = await _uow.CommitAsync();
                if (!resultCommit)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventService.Delete(Erro: {ex.Message})");
                return false;
            }
        }
    }
}
