using AutoMapper;
using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Persistence.Interfaces;
using AwesomeDevEvents.API.Repositories.Interfaces;
using AwesomeDevEvents.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Repositories
{
    public class DevEventRepository : IDevEventRepository, IDisposable
    {
        private readonly ILogger<DevEventRepository> _logger;
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private bool _disposed = false;

        public DevEventRepository(
            ILogger<DevEventRepository> logger,
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository");
        }

        public async Task<IEnumerable<DevEventOutput>> FindAllAsync()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindAll()");
            try
            {
                var devEvents = await _context
                  .DevEvents
                  .AsNoTracking()
                  //.Include(d => d.Speakers)
                  .Where(d => !d.IsDeleted)
                  .OrderBy(d => d.StartDate)
                  .ToListAsync();

                var results = _mapper.Map<List<DevEventOutput>>(devEvents);
                //var results = devEvents.Select(c => new DevEventOutput(c.Id, c.Title, c.Description, c.Speakers));
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindAll(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventOutput> FindByIdAsync(Guid id)
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

                var result = _mapper.Map<DevEventOutput>(devEvent);
                //var result = new DevEventOutput(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.Speakers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEvent> FindByIdSimpleAsync(Guid id)
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

        public async Task<bool> FindAnyAsync(Guid id)
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

        public async Task<DevEventOutput> CreateAsync(DevEventInput input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Create()");
            try
            {
                var devEvent = _mapper.Map<DevEvent>(input);
                //var devEvent = new DevEvent(input.title, input.description);

                await _context.DevEvents.AddAsync(devEvent);
                // await _context.SaveChangesAsync();

                var result = _mapper.Map<DevEventOutput>(devEvent);
                //var result = new DevEventOutput(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.Speakers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Create(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventOutput> UpdateAsync(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Update()");
            try
            {
                _context.DevEvents.Update(devEvent);
                // await _context.SaveChangesAsync();

                var result = _mapper.Map<DevEventOutput>(devEvent);
                //var result = new DevEventOutput(devEvent.Id, devEvent.Title, devEvent.Description, devEvent.Speakers);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Update(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Delete()");
            try
            {
                var devEvent = await this.FindByIdSimpleAsync(id);

                if (devEvent == null || devEvent?.Id == Guid.Empty)
                    return false;

                _context.DevEvents.Remove(devEvent);
                // await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Delete(Erro: {ex.Message})");
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!this.disposed)
            //{
            //    if (disposing)
            //    {
            //        context.Dispose();
            //    }
            //}
            if (!_disposed && disposing)
                _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public void Dispose()
        //{
        //    if (_context != null)
        //        _context.Dispose();
        //    GC.SuppressFinalize(this);
        //}
    }
}
