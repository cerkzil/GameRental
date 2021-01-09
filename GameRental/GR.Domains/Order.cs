using GR.Domains.Enum;
using System;
using System.Collections.Generic;

namespace GR.Domains
{
    public class Order
    {
        public string GameTitle { get; set; }
        public string UserName { get; set; }
        public Guid OrderId { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
