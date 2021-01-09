using GR.Domains;
using GR.EF;
using GR.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using GR.Domains.Enum;
using System.Threading.Tasks;
using System.Linq;

namespace GR.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;

        public OrderController(ILogger<OrderController> logger, UserManager<AppUser> userManager, Context context)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // GET: Order/Index/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager
               .GetUserAsync(HttpContext.User);

            var order = await _context.Orders.ToListAsync();

            return View(order.Where(x => x.UserId == Guid.Parse(user.Id)));
        }

        // GET: Order/IndexAll/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAll()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Order/Create/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Create(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
              .FirstOrDefaultAsync(x => x.Id == id);

            var user = await _userManager
                .GetUserAsync(HttpContext.User);

            return View(new OrderViewModel
            {
                OrderId = Guid.NewGuid(),
                GameId = game.Id,
                GameTitle = game.Title,
                UserId = Guid.Parse(user.Id),
                UserName = user.UserName
            }); 
        }

        // POST: Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameTitle,UserName,OrderId,GameId,UserId")] OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order()
                {
                    GameTitle = model.GameTitle,
                    UserName = model.UserName,
                    OrderId = model.OrderId,
                    GameId = model.GameId,
                    UserId = model.UserId,
                    OrderStatus = OrderStatus.OnGoing,
                    OrderDate = DateTime.Now
                };

                _context.Add(newOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Order/Update
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == id);

            return View(new OrderViewModel{
                OrderId = order.OrderId, 
                OrderStatus = order.OrderStatus});
        }

        // POST: Order/Update/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("OrderId, OrderStatus")] OrderViewModel model)
        {
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == model.OrderId);

            order.OrderStatus = model.OrderStatus;

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAll));
        }

        // GET: Order/Cancel/
        [Authorize(Roles = "Member, Admin")]
        public async Task<IActionResult> Cancel(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == id);

            return View(new OrderViewModel
            {
                OrderId = order.OrderId,
                GameTitle = order.GameTitle
            });
        }

        // POST: Order/Cancel/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel([Bind("OrderId")] OrderViewModel model)
        {
            var order = await _context.Orders
              .FirstOrDefaultAsync(x => x.OrderId == model.OrderId);

            order.OrderStatus = OrderStatus.Canceled;

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Delete/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.OrderId == id);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}