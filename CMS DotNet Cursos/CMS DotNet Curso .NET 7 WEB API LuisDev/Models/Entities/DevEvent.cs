using Flunt.Validations;

namespace AwesomeDevEvents.API.Models.Entities
{
    public class DevEvent : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<DevEventSpeaker> Speakers { get; set; }
        public bool IsDeleted { get; set; }

        public DevEvent()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = null;
            this.Speakers = new List<DevEventSpeaker>();
            this.IsDeleted = false;
        }

        public DevEvent(string title, string description) : this()
        {
            this.Title = title;
            this.Description = description;

            this.Validate();
        }

        public void Update(string title, string description)
        {
            this.Title = title;
            this.Description = description;

            this.Validate();
        }

        public void Delete()
        {
            this.EndDate = DateTime.Now;
            this.IsDeleted = true;

            this.Validate();
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
