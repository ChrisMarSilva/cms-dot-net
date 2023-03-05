using AwesomeDevEvents.API.Models.Entities;

namespace AwesomeDevEvents.API.Models.Dtos
{
    public record DevEventOutputDto(
        Guid id,
        string title,
        string description,
        IEnumerable<DevEventSpeaker> speakers
     );
}
