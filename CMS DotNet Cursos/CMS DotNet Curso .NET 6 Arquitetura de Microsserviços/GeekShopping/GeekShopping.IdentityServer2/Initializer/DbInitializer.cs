using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(
            MySQLContext context, 
            UserManager<ApplicationUser> user, 
            RoleManager<IdentityRole> role
            )
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            var adminExist = _role.FindByNameAsync(IdentityConfiguration.Admin).Result;
            if (adminExist != null)
                return;

            //---------------------------------------------------------
            //---------------------------------------------------------

            // ROLE

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            //---------------------------------------------------------
            //---------------------------------------------------------

            // ADMIN

            ApplicationUser userAdmin = new ApplicationUser()
            {
                UserName = "user-admin",
                Email = "user-admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "CMS",
                LastName = "Admin"
            };

            _user.CreateAsync(userAdmin, "Admin@123").GetAwaiter().GetResult();

            _user.AddToRoleAsync(userAdmin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(userAdmin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{userAdmin.FirstName} {userAdmin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, userAdmin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, userAdmin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            //---------------------------------------------------------
            //---------------------------------------------------------

            // CLIENT

            ApplicationUser userClient = new ApplicationUser()
            {
                UserName = "user-client",
                Email = "user-client@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "CMS",
                LastName = "Client"
            };

            _user.CreateAsync(userClient, "Client@123").GetAwaiter().GetResult();

            _user.AddToRoleAsync(userClient, IdentityConfiguration.Client).GetAwaiter().GetResult();
           
            var clientClaims = _user.AddClaimsAsync(userClient, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{userClient.FirstName} {userClient.LastName}"),
                new Claim(JwtClaimTypes.GivenName, userClient.FirstName),
                new Claim(JwtClaimTypes.FamilyName, userClient.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;

            //---------------------------------------------------------
            //---------------------------------------------------------
        }
    }
}
