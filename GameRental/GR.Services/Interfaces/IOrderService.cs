using GR.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GR.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetUserOrdersAsync(AppUser user);
        Task<Game> GetGameByIdAsync(Guid id);
        Task DeleteGameByIdAsync(Guid id);
        Task CreateOrderAsync(string gameTitle, string userName, Guid gameId, Guid userId);
        Task UpdateGameByIdAsync(Guid id, string title, Uri imglink, List<Genres> genreList, List<Platforms> platformList);
    }
}
