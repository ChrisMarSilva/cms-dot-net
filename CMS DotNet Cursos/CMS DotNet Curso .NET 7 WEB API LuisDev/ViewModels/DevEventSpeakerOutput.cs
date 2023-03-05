namespace AwesomeDevEvents.API.ViewModels
{
    public record DevEventSpeakerOutput(
       Guid id,
       string name,
       string talkTitle,
       string talkDescription,
       string linkedInProfile
   );
}
