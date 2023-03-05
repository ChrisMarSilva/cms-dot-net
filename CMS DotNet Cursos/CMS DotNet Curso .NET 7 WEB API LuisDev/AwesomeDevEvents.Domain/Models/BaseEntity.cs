using Flunt.Notifications;
using System;

namespace AwesomeDevEvents.Domain.Models
{
    public class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public bool IsActive { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

    }
}
