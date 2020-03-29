using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services
{
    public class OrderService:IOrderService
    {
        private readonly OnlineBookstoreDBContext _context;
        private readonly List<Order> _order = new List<Order>();
        public OrderService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _order = context.Order.ToList();
        }

        public Task<List<Order>> GetAll() => Task.Run(function: () => _order);

        public Task<List<Order>> GetById(int userId)
        {
            return Task.Run(function: () => { return _order.FindAll(x => x.UserId == userId); });
        }

        public Task<bool> Add(Order order)
        {
            return Task.Run(function: () =>
            {
                _order.Add(order);
                _context.Order.Add(order);
                _context.SaveChanges();
                return true;
            });
        }
    }
}
