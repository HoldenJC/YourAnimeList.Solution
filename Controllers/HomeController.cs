using Microsoft.AspNetCore.Mvc;
using YourAnimeList.Models;

namespace YourAnimeList.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var allAnimes = Anime.GetAnimes("yuyu");
      return View(allAnimes);
    }
  }
}