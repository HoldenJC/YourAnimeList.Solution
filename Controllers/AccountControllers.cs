using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using YourAnimeList.Models;
using YourAnimeList.ViewModels;

namespace YourAnimeList.Controllers
{
    public class AccountController : Controller
    {
        private readonly YourAnimeListContext _db;
        private readonly UserManager<ThrowawayUser> _userManager;
        private readonly SignInManager<ThrowawayUser> _signInManager;


        public AccountController(UserManager<ThrowawayUser> userManager, SignInManager<ThrowawayUser> signInManager, YourAnimeListContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var user = new AppUser { UserName = model.UserName, Email = model.Email, Password = model.Password };
            _db.AppUsers.Add(user);
            _db.SaveChanges();
            GlobalVar.CurrentUser = user;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var thisUser = _db.AppUsers
                .FirstOrDefault(user => user.UserName == model.UserName);
            if (thisUser.Password == model.Password)
            {
                GlobalVar.CurrentUser = thisUser;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect("Failed");
            }
        }

        public ActionResult Details()
        {
            List<Anime> AnimeList = new List<Anime> ();
            foreach (var useranime in _db.UserAnimes)
            {
                if (useranime.UserId == GlobalVar.CurrentUser.UserId)
                {
                    AnimeList.Add(useranime);
                }
            }
            for(int i = 0; i < AnimeList.Count; i++)
            {
                for(int j = i+1; j < AnimeList.Count; j++)
                {
                    if(AnimeList[i].Mal_Id == AnimeList[j].Mal_Id)
                    {
                        AnimeList.RemoveAt(j);
                        j--;
                    }
                }
            }

            ViewBag.Treasure = AnimeList;
            return View();
        }
        public IActionResult LogOff()
        {
            GlobalVar.CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Failed()
        {
            return View();
        }
        public IActionResult Add(Anime anime)
        {
            var userAnime = new Anime { UserId = GlobalVar.CurrentUser.UserId, Mal_Id = anime.Mal_Id, Title = anime.Title, Synopsis = anime.Synopsis, Image_Url = anime.Image_Url, Score = anime.Score };
            _db.UserAnimes.Add(userAnime);
            _db.SaveChanges();
            return Redirect("Details");
        }
    }
}