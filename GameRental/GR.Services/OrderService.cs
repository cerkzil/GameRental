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
            return orders.OrderBy(x => x.OrderDate.Date)
            .ThenBy(x => x.OrderDate.TimeOfDay);
        }
        public async Task<IEnumerable<Order>> GetUserOrdersAsync(AppUser user)
        {
            var orders = await _context.Orders.ToListAsync();
            return orders.Where(x => x.UserId == Guid.Parse(user.Id))
                         .OrderBy(x => x.OrderDate.Date)
                         .ThenBy(x => x.OrderDate.TimeOfDay);
        }
        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }
        public async Task CreateOrderAsync(string gameTitle, string userName, Guid gameId, Guid userId)
        {
            _context.Add(new Order()
            {
                GameTitle = gameTitle,
                UserName = userName,
                OrderId = Guid.NewGuid(),
                GameId = gameId,
                UserId = userId,
                OrderStatus = OrderStatus.OnGoing,
                OrderDate = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrderStatusByIdAsync(Guid id, OrderStatus newStatus)
        {
            var order = await GetOrderByIdAsync(id);
            order.OrderStatus = newStatus;
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOrderByIdAsync(Guid id)
        {
            _context.Remove(await GetOrderByIdAsync(id));
            await _context.SaveChangesAsync();
        }
    }
}
