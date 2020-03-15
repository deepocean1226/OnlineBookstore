using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public class PurchaseService:IPurchaseService
    {
        private readonly OnlineBookstoreDBContext _context;
        private readonly List<PurchaseInfo> _purchaseInfos = new List<PurchaseInfo>();
        public PurchaseService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _purchaseInfos = context.PurchaseInfo.ToList();
        }

        public Task<List<PurchaseInfo>> GetAll()
        {
            return Task.Run(function: () => _purchaseInfos);
        }
    }
}
