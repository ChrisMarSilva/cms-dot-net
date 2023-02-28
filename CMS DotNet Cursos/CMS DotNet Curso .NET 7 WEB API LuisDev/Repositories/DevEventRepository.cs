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
                var products = await _context.DevEvents.ToListAsync();

                //var results = _mapper.Map<List<DevEventDTO>>(products);
                var results = products.Select(c => new DevEventDTO(c.Id, c.Title, c.Description, c.StartDate, c.EndDate, c.Speakers, c.IsDeleted));
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
                var product = await _context.DevEvents.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new DevEvent();

                //var result = _mapper.Map<DevEventDTO>(product);
                var result = new DevEventDTO(product.Id, product.Title, product.Description, product.StartDate, product.EndDate, product.Speakers, product.IsDeleted);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventDTO> Create(DevEventDTO input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Create()");
            try
            {
                //var product = _mapper.Map<DevEvent>(vo);
                var product = new DevEvent();
                product.Id = input.id;
                product.Title = input.title;
                product.Description = input.description;
                product.StartDate = input.startDate;
                product.EndDate = input.endDate;
                product.Speakers = input.speakers;
                product.IsDeleted = input.isDeleted;

                await _context.DevEvents.AddAsync(product);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventDTO>(product);
                var result = new DevEventDTO(product.Id, product.Title, product.Description, product.StartDate, product.EndDate, product.Speakers, product.IsDeleted);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventRepository.Create(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventDTO> Update(DevEventDTO input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventRepository.Update()");
            try
            {
                //var product = _mapper.Map<Product>(input);
                var product = new DevEvent();
                product.Id = input.id;
                product.Title = input.title;
                product.Description = input.description;
                product.StartDate = input.startDate;
                product.EndDate = input.endDate;
                product.Speakers = input.speakers;
                product.IsDeleted = input.isDeleted;

                _context.DevEvents.Update(product);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventDTO>(product);
                var result = new DevEventDTO(product.Id, product.Title, product.Description, product.StartDate, product.EndDate, product.Speakers, product.IsDeleted);
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
                var product = await _context
                    .DevEvents
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync()
                    ?? new DevEvent();

                if (product.Id == Guid.Empty)
                    return false;

                _context.DevEvents.Remove(product);
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
