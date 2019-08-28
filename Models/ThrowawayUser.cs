using Microsoft.AspNetCore.Identity;

namespace YourAnimeList.Models
{
   public class ThrowawayUser : IdentityUser
    {
        public int UserId {get;set;}
    }
}