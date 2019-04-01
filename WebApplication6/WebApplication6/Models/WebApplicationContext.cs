using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Models
{
    public class WebApplicationContext : DbContext
    {
        public WebApplicationContext(DbContextOptions<WebApplicationContext> options)
            : base(options)
        {
        }
        
        public DbSet<AuthenticationRequest> AuthenticationRequest { get; set; }
    }
}