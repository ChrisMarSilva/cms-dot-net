using AutoMapper;
using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.Persistence;
using AwesomeDevEvents.API.Repositories.Interfaces;
using AwesomeDevEvents.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Repositories
{
    public class DevEventSpeakerRepository : IDevEventSpeakerRepository
    {
        private readonly ILogger<DevEventSpeakerRepository> _logger;
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private bool _disposed = false;

        public DevEventSpeakerRepository(
            ILogger<DevEventSpeakerRepository> logger,
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            _logger = logger;
            this._context = context;
            _mapper = mapper;
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository");
        }

        public async Task<IEnumerable<DevEventSpeakerOutput>> FindAllAsync()
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.FindAll()");
            try
            {
                var speakers = await _context.DevEventSpeakers.ToListAsync();

                var results = _mapper.Map<List<DevEventSpeakerOutput>>(speakers);
                //var results = speakers.Select(c => new DevEventSpeakerOutput(c.Id, c.Name, c.TalkTitle, c.TalkDescription, c.LinkedInProfile, c.DevEventId));
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.FindAll(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventSpeakerOutput> FindByIdAsync(Guid id)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.FindById()");
            try
            {
                var speaker = await _context.DevEventSpeakers.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new DevEventSpeaker();

                var result = _mapper.Map<DevEventSpeakerOutput>(speaker);
                //var result = new DevEventSpeakerOutput(speaker.Id, speaker.Name, speaker.TalkTitle, speaker.TalkDescription, speaker.LinkedInProfile);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.FindById(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventSpeakerOutput> CreateAsync(DevEventSpeakerInput input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.Create()");
            try
            {
                var speaker = _mapper.Map<DevEventSpeaker>(input);
                //var speaker = new DevEventSpeaker(input.name, input.talkTitle, input.talkDescription, input.linkedInProfile, input.DevEventId);

                await _context.DevEventSpeakers.AddAsync(speaker);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventSpeakerOutput>(speaker);
                var result = new DevEventSpeakerOutput(speaker.Id, speaker.Name, speaker.TalkTitle, speaker.TalkDescription, speaker.LinkedInProfile);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.Create(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<DevEventSpeakerOutput> UpdateAsync(DevEventSpeakerInput input)
        {
            _logger.LogInformation("AwesomeDevEvents.API.DevEventSpeakerRepository.Update()");
            try
            {
                var speaker = _mapper.Map<DevEventSpeaker>(input);
                //var speaker = new DevEventSpeaker(input.name, input.talkTitle, input.talkDescription, input.linkedInProfile);

                _context.DevEventSpeakers.Update(speaker);
                await _context.SaveChangesAsync();

                //var result = _mapper.Map<DevEventSpeakerOutput>(speaker);
                var result = new DevEventSpeakerOutput(speaker.Id, speaker.Name, speaker.TalkTitle, speaker.TalkDescription, speaker.LinkedInProfile);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AwesomeDevEvents.API.DevEventSpeakerRepository.Update(Erro: {ex.Message})");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
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
