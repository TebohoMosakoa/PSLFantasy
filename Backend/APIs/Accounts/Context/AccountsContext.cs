using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Context
{
    public class AccountsContext : IdentityDbContext<IdentityUser>
    {
        public AccountsContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
