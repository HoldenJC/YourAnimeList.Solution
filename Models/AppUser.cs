using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourAnimeList.Models
{
    public class AppUser 
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        public string UserName {get;set;}
        [Required]
        public string Password {get;set;}
        [Required]
        public string Email {get;set;}
    }
}