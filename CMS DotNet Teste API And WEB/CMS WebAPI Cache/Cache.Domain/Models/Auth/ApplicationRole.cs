using Microsoft.AspNetCore.Identity;

namespace Cache.Domain.Models.Auth;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
    }

    public string DisplayName { get; set; }
    public bool IsSystem { get; set; }
}