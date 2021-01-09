using GR.Domains;
using GR.Domains.Enum;
using GR.EF;
using GR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders.OrderByDescending(x => x.OrderDate.Date)
            .ThenBy(x => x.OrderDate.TimeOfDay);
        }
        public async Task<IEnumerable<Order>> GetUserOrdersAsync(AppUser user)
        {
            var orders = await _context.Orders.ToListAsync();
            return orders.Where(x => x.UserId == Guid.Parse(user.Id))
                         .OrderByDescending(x => x.OrderDate.Date)
                         .ThenBy(x => x.OrderDate.TimeOfDay);
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

        public async Task CreateOrderAsync(string gameTitle, string userName, Guid gameId, Guid userId)
        {
            Order newOrder = new Order()
            {
                GameTitle = gameTitle,
                UserName = userName,
                OrderId = Guid.NewGuid(),
                GameId = gameId,
                UserId = userId,
                OrderStatus = OrderStatus.OnGoing,
                OrderDate = DateTime.Now
            };

            _context.Add(newOrder);
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
