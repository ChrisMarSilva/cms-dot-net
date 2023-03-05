using Flunt.Validations;
using System;

namespace AwesomeDevEvents.Domain.Models
{
    public class DevEventSpeaker : BaseEntity
    {
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinkedInProfile { get; set; }
        public Guid DevEventId { get; set; }

        public DevEventSpeaker() { }

        public DevEventSpeaker(string name, string talkTitle, string talkDescription, string linkedInProfile) : this()
        {
            Name = name;
            TalkTitle = talkTitle;
            TalkDescription = talkDescription;
            LinkedInProfile = linkedInProfile;

            Validate();
        }

        public void Update(string name, string talkTitle, string talkDescription, string linkedInProfile)
        {
            Name = name;
            TalkTitle = talkTitle;
            TalkDescription = talkDescription;
            LinkedInProfile = linkedInProfile;

            Validate();
        }

        private void Validate()
        {
            var contract = new Contract<DevEventSpeaker>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsNotNullOrEmpty(TalkTitle, "TalkTitle")
                .IsNotNullOrEmpty(TalkDescription, "TalkDescription")
                .IsNotNullOrEmpty(LinkedInProfile, "LinkedInProfile");

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
