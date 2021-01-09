using GR.Domains;
using GR.EF;
using GR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GR.Services
{
    public class OrderService : IOrderService
    {
        private readonly Context _context;
        public OrderService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games.Include(x => x.GenreList).Include(x => x.PlatformList).ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            return await _context.Games.Include(x => x.GenreList).Include(x => x.PlatformList).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteGameByIdAsync(Guid id)
        {
            _context.Games.Remove(await GetGameByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task CreateGameAsync(string title, Uri imglink, List<Genres> genreList, List<Platforms> platformList)
        {
            Game newGame = new Game()
            {
                Title = title,
                ImgLink = imglink,
                GenreList = genreList,
                PlatformList = platformList
            };

            _context.Add(newGame);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateGameByIdAsync(Guid id, string title, Uri imglink, List<Genres> genreList, List<Platforms> platformList)
        {
            var game = await GetGameByIdAsync(id);

            game.Title = title;
            game.ImgLink = imglink;
            game.PlatformList = platformList;
            game.GenreList = genreList;

            _context.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
