using AwesomeDevEvents.Domain.Models;
using System;
using System.Collections.Generic;

namespace AwesomeDevEvents.Domain.Dtos
{
    public record DevEventOutputDto(
        Guid id,
        string title,
        string description,
        IEnumerable<DevEventSpeaker> speakers
     );
}
