using GR.Domains;
using GR.Domains.Enum;
using GR.MVC.Models;
using GR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GR.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _service;

        public HomeController(ILogger<HomeController> logger, IGameService service)
        {
            _logger = logger;
            _service = service;
        }
        //Main Page:
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllGamesAsync());
        }
        //Game filtering:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Genre? filter, Platform? filter2, string search)
        {

            IEnumerable<Game> games = await _service.GetAllGamesAsync();

            if (filter.HasValue)
            {
                games = games.Where(x => x.GenreList.Any(y => y.Genre == filter));
            }
            if (filter2.HasValue)
            {
                games = games.Where(x => x.PlatformList.Any(y => y.Platform == filter2));
            }
            if (!String.IsNullOrEmpty(search))
            {
                games = games.Where(x => x.Title.ToLower().Contains(search.ToLower()));
            }
            return View(games);
        }

        //Privacy Page:
        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy page says hello");
            return View();
        }

        //About Page:
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
