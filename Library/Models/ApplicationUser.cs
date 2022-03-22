using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Library.Models
{
    public class ApplicationUser : IdentityUser
    {
    public ApplicationUser()
    {
        this.Checkouts = new HashSet<BookUser>();
    }
    public  ICollection<BookUser> Checkouts { get; }
    }
}