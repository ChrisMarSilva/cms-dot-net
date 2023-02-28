using AwesomeDevEvents.API.DTOs;
using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Repositories
{
    public class DevEventSpeakerRepository : IDevEventSpeakerRepository
    {
        private readonly ILogger<DevEventSpeakerRepository> _logger;
        private readonly ApplicationDbContext _context;

        public DevEventSpeakerRepository(
            ILogger<DevEventSpeakerRepository> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            this._context = context;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository");
        }

        public async Task<IEnumerable<DevEventSpeakerDTO>> FindAll()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.FindAll()");
            try
            {
                var products = await _context.DevEventSpeakers.ToListAsync();

                //var results = _mapper.Map<List<DevEventSpeakerDTO>>(products);
                var results = products.Select(c => new DevEventSpeakerDTO(c.Id, c.Name, c.TalkTitle, c.TalkDescription, c.LinkedInProfile, c.DevEventId));
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.FindAll(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventSpeakerDTO> FindById(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.FindById()");
            try
            {
                var product = await _context.DevEventSpeakers.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new DevEventSpeaker();

                //var result = _mapper.Map<DevEventSpeakerDTO>(product);
                var result = new DevEventSpeakerDTO(product.Id, product.Name, product.TalkTitle, product.TalkDescription, product.LinkedInProfile, product.DevEventId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventSpeakerDTO> Create(DevEventSpeakerDTO input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.Create()");
            try
            {
                //var product = _mapper.Map<DevEventSpeaker>(vo);
                var product = new DevEventSpeaker();
                product.Id = input.id;
                product.Name = input.name;
                product.TalkTitle = input.talkTitle;
                product.TalkDescription = input.talkDescription;
                product.LinkedInProfile = input.linkedInProfile;
                product.DevEventId = input.devEventId;

                await _context.DevEventSpeakers.AddAsync(product);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventSpeakerDTO>(product);
                var result = new DevEventSpeakerDTO(product.Id, product.Name, product.TalkTitle, product.TalkDescription, product.LinkedInProfile, product.DevEventId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.Create(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventSpeakerDTO> Update(DevEventSpeakerDTO input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.Update()");
            try
            {
                //var product = _mapper.Map<Product>(input);
                var product = new DevEventSpeaker();
                product.Id = input.id;
                product.Name = input.name;
                product.TalkTitle = input.talkTitle;
                product.TalkDescription = input.talkDescription;
                product.LinkedInProfile = input.linkedInProfile;
                product.DevEventId = input.devEventId;

                _context.DevEventSpeakers.Update(product);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventSpeakerDTO>(product);
                var result = new DevEventSpeakerDTO(product.Id, product.Name, product.TalkTitle, product.TalkDescription, product.LinkedInProfile, product.DevEventId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.Update(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.Delete()");
            try
            {
                var product = await _context
                    .DevEventSpeakers
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync()
                    ?? new DevEventSpeaker();

                if (product.Id == Guid.Empty)
                    return false;

                _context.DevEventSpeakers.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.Delete(Erro: {ex.Message})");
                return false;
            }
        }
    }
}
