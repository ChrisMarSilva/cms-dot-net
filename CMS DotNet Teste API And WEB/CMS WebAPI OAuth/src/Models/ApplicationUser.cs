using Microsoft.AspNetCore.Identity;

namespace CMS_WebAPI_OAuth.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    private ApplicationUser() { }

    public ApplicationUser(string firstName = null, string lastName = null) : base()
    {
        Id = Guid.NewGuid(); ;
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}