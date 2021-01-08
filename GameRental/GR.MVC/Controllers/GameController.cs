using GR.Domains;
using GR.EF;
using GR.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GR.Domains.Enum;
using Microsoft.AspNetCore.Authorization;

namespace GR.MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly Context _context;

        public GameController(ILogger<GameController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games
                .Include(x => x.GenreList)
                .Include(x => x.PlatformList)
                .ToListAsync());
        }

        // GET: Game/Details/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(x => x.GenreList)
                .Include(x => x.PlatformList)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (game == null)
            {
                return NotFound();
            }
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

                Game newGame = new Game()
                {
                    Title = model.Title,
                    ImgLink = model.ImgLink,
                    PlatformList = platformList,
                    GenreList = genreList
                };

                _context.Add(newGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Game/Edit/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(x => x.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var viewModel = new GameViewModel
            {
                Title = game.Title,
                ImgLink = game.ImgLink,
                Id = game.Id
            };

            return View(viewModel);
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

                var game = await _context.Games
                                    .Include(x => x.GenreList)
                                    .Include(x => x.PlatformList)
                                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                game.Title = model.Title;
                game.ImgLink = model.ImgLink;
                game.PlatformList = platformList;
                game.GenreList = genreList;

                _context.Update(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Game/Delete/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game = await _context.Games.Include(x => x.GenreList)
                                           .Include(x => x.PlatformList)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
