using AwesomeDevEvents.Domain.Models;
using AwesomeDevEvents.Infrastructure.Persistence;
using AwesomeDevEvents.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeDevEvents.Infrastructure.Repositories
{
    public class DevEventRepository : IDevEventRepository, IDisposable
    {
        private readonly ILogger<DevEventRepository> _logger;
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public DevEventRepository(ILogger<DevEventRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository");
        }

        public async Task<IEnumerable<DevEvent>> FindAllAsync()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindAll()");

            var devEvents = await _context
                .DevEvents
                .AsNoTracking()
                //.Include(d => d.Speakers)
                .Where(d => !d.IsDeleted)
                .OrderBy(d => d.StartDate)
                .ToListAsync();

            return devEvents;
        }

        public async Task<DevEvent> FindByIdAsync(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindById()");

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

            return devEvent;
        }

        public async Task<bool> FindAnyAsync(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.FindByIdSimple()");

            var isExistDevEvent = await _context
                    .DevEvents
                    .AnyAsync(d => d.Id == id);

            return isExistDevEvent;
        }

        public async Task<DevEvent> CreateAsync(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Create()");

            await _context.DevEvents.AddAsync(devEvent);

            return devEvent;
        }

        public DevEvent Update(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Update()");

            _context.DevEvents.Update(devEvent);

            return devEvent;
        }

        public bool Delete(DevEvent devEvent)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Delete()");

            _context.DevEvents.Remove(devEvent);

            return true;
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
