using Microsoft.AspNetCore.Identity;

namespace CMS_WebAPI_OAuth.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
    }

    public string DisplayName { get; set; }
    public bool IsSystem { get; set; }
}
