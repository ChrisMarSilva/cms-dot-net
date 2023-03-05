using AwesomeDevEvents.API.Models;

namespace AwesomeDevEvents.API.ViewModels
{
    public record DevEventOutput(
        Guid id,
        string title,
        string description,
        IEnumerable<DevEventSpeaker> speakers
     );
}
