namespace AwesomeDevEvents.API.Models.Dtos
{
    public record DevEventSpeakerOutputDto(
       Guid id,
       string name,
       string talkTitle,
       string talkDescription,
       string linkedInProfile
   );
}
