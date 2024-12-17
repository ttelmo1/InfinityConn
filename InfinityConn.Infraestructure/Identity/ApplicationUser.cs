using System;
using Microsoft.AspNetCore.Identity;

namespace InfinityConn.Infraestructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
