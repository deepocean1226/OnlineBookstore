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

        public Task<List<Purchase>> GetAll()
        {
            return Task.Run(function: () => _purchases);
        }
    }
}
