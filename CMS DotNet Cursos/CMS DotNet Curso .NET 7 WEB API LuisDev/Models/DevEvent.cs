namespace AwesomeDevEvents.API.Models
{
    public class DevEvent
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<DevEventSpeaker> Speakers { get; set; }
        public bool IsDeleted { get; set; }

        public DevEvent()
        {
            this.Id = Guid.NewGuid();
            this.StartDate = DateTime.Now;
            this.EndDate = null;
            this.Speakers = new List<DevEventSpeaker>();
            this.IsDeleted = false;
        }

        public DevEvent(string title, string description) : this()
        {
            this.Title = title;
            this.Description = description;
        }

        public void Update(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }

        public void Delete()
        {
            this.EndDate = DateTime.Now;
            this.IsDeleted = true;
        }
    }
}
