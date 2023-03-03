using AwesomeDevEvents.API.DTOs;
using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Repositories
{
    public class DevEventRepository : IDevEventRepository
    {
        private readonly ILogger<DevEventRepository> _logger;
        private readonly ApplicationDbContext _context;

        public DevEventRepository(
            ILogger<DevEventRepository> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            this._context = context;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository");
        }

        public async Task<IEnumerable<DevEventDTO>> FindAll()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindAll()");
            try
            {
                var devEvents = await _context
                  .DevEvents
                  .AsNoTracking()
                  //.Include(d => d.Speakers)
                  .Where(d => !d.IsDeleted)
                  .OrderByDescending(d => d.StartDate)
                  .ToListAsync();

                //var results = _mapper.Map<List<DevEventDTO>>(products);
                var results = devEvents
                    .Select(c => new DevEventDTO(c.Id, c.Title, c.Description, c.StartDate, c.EndDate, c.Speakers, c.IsDeleted));

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindAll(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventDTO> FindById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindById()");
            try
            {
                //var devEvent = await _context
                //    .DevEvents
                //    .Where(p => p.Id == id)
                //    .FirstOrDefaultAsync() ?? new DevEvent();

                //var devEvent = await _context
                //    .DevEvents
                //    .AsNoTracking()
                //    .Include(d => d.Speakers)
                //    .FirstOrDefaultAsync(d => d.Id == id);

                var devEvent = await _context
                    .DevEvents
                    .AsNoTracking()
                    .Include(d => d.Speakers)
                    .SingleOrDefaultAsync(d => d.Id == id) ?? new DevEvent();

                //If your result set returns 0 records:
                //SingleOrDefault returns the default value for the type(e.g. default for int is 0)
                //FirstOrDefault returns the default value for the type

                //If you result set returns 1 record:
                //SingleOrDefault returns that record
                //FirstOrDefault returns that record

                //If your result set returns many records:
                //SingleOrDefault throws an exception
                //FirstOrDefault returns the first record

                //var result = _mapper.Map<DevEventDTO>(product);
                var result = new DevEventDTO(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate, devEvent.Speakers, devEvent.IsDeleted);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEvent> FindByIdSimple(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindByIdSimple()");
            try
            {
                var devEvent = await _context
                    .DevEvents
                    .SingleOrDefaultAsync(d => d.Id == id) ?? new DevEvent();

                return devEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<bool> FindAny(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindByIdSimple()");
            try
            {
                var isExistDevEvent = await _context
                    .DevEvents
                    .AnyAsync(d => d.Id == id);

                return isExistDevEvent;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindById(Erro: {ex.Message})");
                return false;
            }
        }

        public async Task<DevEventDTO> Create(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Create()");
            try
            {
                //var devEvent = _mapper.Map<Product>(input);
                //var devEvent = new DevEvent();
                //devEvent.Id = input.id;
                //devEvent.Title = input.title;
                //devEvent.Description = input.description;
                //devEvent.StartDate = input.startDate;
                //devEvent.EndDate = input.endDate;
                //devEvent.Speakers = input.speakers;
                //devEvent.IsDeleted = input.isDeleted;

                await _context.DevEvents.AddAsync(devEvent);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventDTO>(product);
                var result = new DevEventDTO(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate, devEvent.Speakers, devEvent.IsDeleted);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Create(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventDTO> Update(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Update()");
            try
            {
                //var devEvent = _mapper.Map<Product>(input);
                //var devEvent = new DevEvent();
                //devEvent.Id = input.id;
                //devEvent.Title = input.title;
                //devEvent.Description = input.description;
                //devEvent.StartDate = input.startDate;
                //devEvent.EndDate = input.endDate;
                //devEvent.Speakers = input.speakers;
                //devEvent.IsDeleted = input.isDeleted;

                _context.DevEvents.Update(devEvent);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventDTO>(product);
                var result = new DevEventDTO(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate, devEvent.Speakers, devEvent.IsDeleted);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Update(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Delete()");
            try
            {
                var devEvent = await this.FindByIdSimple(id);

                if (devEvent == null || devEvent?.Id == Guid.Empty)
                    return false;

                _context.DevEvents.Remove(devEvent);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Delete(Erro: {ex.Message})");
                return false;
            }
        }
    }
}
