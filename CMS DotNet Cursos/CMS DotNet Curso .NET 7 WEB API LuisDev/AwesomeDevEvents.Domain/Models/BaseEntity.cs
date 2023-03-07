using Flunt.Notifications;
using System;

namespace AwesomeDevEvents.Domain.Models
{
    public class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; private set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public bool IsActive { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public BaseEntity(Guid id)
        {
            this.Id = id;
        }

    }
}
