using Flunt.Notifications;

namespace AwesomeDevEvents.API.Models.Entities
{
    public class Base : Notifiable<Notification>
    {
        public Guid Id { get; set; }

        public Base()
        {
            Id = Guid.NewGuid();
        }

    }
}
