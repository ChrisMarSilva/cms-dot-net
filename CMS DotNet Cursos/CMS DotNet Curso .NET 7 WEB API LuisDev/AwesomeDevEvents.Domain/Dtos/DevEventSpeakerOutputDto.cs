using System;

namespace AwesomeDevEvents.Domain.Dtos
{
    public record DevEventSpeakerOutputDto(
       Guid id,
       string name,
       string talkTitle,
       string talkDescription,
       string linkedInProfile
   );
}
