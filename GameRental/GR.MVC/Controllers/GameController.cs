using GR.Domains;
using GR.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GR.Domains.Enum;
using Microsoft.AspNetCore.Authorization;
using GR.Services.Interfaces;

namespace GR.MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _service;

        public GameController(ILogger<GameController> logger, IGameService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllGamesAsync());
        }

        // GET: Game/Details/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var game = await _service.GetGameByIdAsync((Guid)id);

            if (game == null) { return RedirectToAction("Error"); }

            return View(game);
        }

        // GET: Game/Create/
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Platforms,Genres,ImgLink")] GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Genres> genreList = new List<Genres>();
                model.Genres.ForEach(x => genreList.Add(new Genres(x)));

                List<Platforms> platformList = new List<Platforms>();
                model.Platforms.ForEach(x => platformList.Add(new Platforms(x)));

                await _service.CreateGameAsync(model.Title, model.ImgLink, genreList, platformList);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error");
        }

        // GET: Game/Edit/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var game = await _service.GetGameByIdAsync((Guid)id);

            if (game == null) { return RedirectToAction("Error"); }

            List<Genre> genreList = new List<Genre>();
            game.GenreList.ForEach(x => genreList.Add(x.Genre));

            List<Platform> platformList = new List<Platform>();
            game.PlatformList.ForEach(x => platformList.Add(x.Platform));

            return View(new GameViewModel
            {
                Title = game.Title,
                ImgLink = game.ImgLink,
                Genres = genreList,
                Platforms = platformList,
                Id = game.Id
            });
        }

        // POST: Game/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Title,Platforms,Genres,ImgLink,Id")] GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Genres> genreList = new List<Genres>();
                model.Genres.ForEach(x => genreList.Add(new Genres(x)));

                List<Platforms> platformList = new List<Platforms>();
                model.Platforms.ForEach(x => platformList.Add(new Platforms(x)));

                await _service.UpdateGameByIdAsync(model.Id, model.Title, model.ImgLink, genreList, platformList);

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error");
        }

        // GET: Game/Delete/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) { return RedirectToAction("Error"); }

            var game = await _service.GetGameByIdAsync((Guid)id);

            if (game == null) { return RedirectToAction("Error"); }

            return View(game);
        }

        // POST: Game/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteGameByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
