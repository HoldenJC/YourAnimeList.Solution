using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.IO;

namespace YourAnimeList.Models
{
    public class YourAnimeListContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserAnime> UserAnimes {get;set;}
        public YourAnimeListContext(DbContextOptions options) :base(options) { }
    }
}

