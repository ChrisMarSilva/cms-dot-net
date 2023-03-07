using Flunt.Validations;

namespace AwesomeDevEvents.Domain.Models
{
    public class DevEvent : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<DevEventSpeaker> Speakers { get; set; }
        public bool IsDeleted { get; set; }

        public DevEvent() : base()
        {
            StartDate = DateTime.Now;
            EndDate = null;
            Speakers = new List<DevEventSpeaker>();
            IsDeleted = false;
        }

        public DevEvent(string title, string description) : this()
        {
            Title = title;
            Description = description;

            Validate();
        }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;

            Validate();
        }

        public void Delete()
        {
            EndDate = DateTime.Now;
            IsDeleted = true;

            Validate();
        }

        private void Validate()
        {
            var contract = new Contract<DevEvent>()
                .IsNotNullOrEmpty(Title, "Title")
                .IsNotNullOrEmpty(Description, "Description");

            //var contract = new Contract<Product>()
            //    .IsNotNullOrEmpty(Name, "Name")
            //    .IsGreaterOrEqualsThan(Name, 3, "Name")
            //    .IsNotNull(Category, "Category", "Category not found")
            //    .IsNotNullOrEmpty(Description, "Description")
            //    .IsGreaterOrEqualsThan(Description, 3, "Description")
            //.IsGreaterOrEqualsThan(Price, 1, "Price")
            //    .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            //    .IsNotNullOrEmpty(EditedBy, "EditedBy");

            this.AddNotifications(contract);
        }
    }
}
