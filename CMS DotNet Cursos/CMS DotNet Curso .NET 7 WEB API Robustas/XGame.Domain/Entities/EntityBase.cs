using prmToolkit.NotificationPattern;

namespace XGame.Domain.Entities;

public abstract class EntityBase : Notifiable
{
    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
}
