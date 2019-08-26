using Microsoft.AspNetCore.Mvc;
using YourAnimeList.Models;

namespace YourAnimeList.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index(string searchString)
    {
      var animes = Anime.GetAnimes("naruto");
      if(searchString != null && searchString != "")
      {
        animes = Anime.GetAnimes(searchString);
      }
      return View(animes);
    }

  }
}