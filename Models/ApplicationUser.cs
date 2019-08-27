using Microsoft.AspNetCore.Identity;

namespace YourAnimeList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId {get;set;}
    }
}