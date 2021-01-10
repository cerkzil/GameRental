using GR.Domains.Enum;
using System;

namespace GR.MVC.Models
{
    public class OrderViewModel
    {
        public string GameTitle { get; set; }
        public string UserName { get; set; }
        public Guid OrderId { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
