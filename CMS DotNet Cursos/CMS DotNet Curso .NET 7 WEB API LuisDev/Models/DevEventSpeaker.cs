namespace AwesomeDevEvents.API.Models
{
    public class DevEventSpeaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinkedInProfile { get; set; }
        public Guid DevEventId { get; set; }

        public DevEventSpeaker()
        {
            this.Id = Guid.NewGuid();
        }

        public void Update(string name, string talkTitle, string talkDescription, string linkedInProfile)
        {
            this.Name = name;
            this.TalkTitle = talkTitle;
            this.TalkDescription = talkDescription;
            this.LinkedInProfile = linkedInProfile;
        }
    }
}
