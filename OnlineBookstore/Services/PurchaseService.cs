using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly OnlineBookstoreDBContext _context;
        private readonly List<Purchase> _purchases = new List<Purchase>();
        public PurchaseService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _purchases = context.Purchase.ToList();
        }

        public Task<List<Purchase>> GetAll() => Task.Run(function: () => _purchases);

        public Task<List<Purchase>> GetById(int orderId)
        {
            return Task.Run(function: () => { return _purchases.FindAll(x => x.OrderNo == orderId); });
        }
    }
}
