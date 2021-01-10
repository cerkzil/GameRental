using GR.Domains;
using GR.Domains.Enum;
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
        Task DeleteOrderByIdAsync(Guid id);
        Task<Order> GetOrderByIdAsync(Guid id);
        Task UpdateOrderStatusByIdAsync(Guid id, OrderStatus newStatus);
        Task CreateOrderAsync(string gameTitle, string userName, Guid gameId, Guid userId);
    }
}
