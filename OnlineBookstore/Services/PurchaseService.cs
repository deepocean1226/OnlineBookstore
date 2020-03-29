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

        public Task<bool> Add(int order,Purchase purchase)
        {
            return Task.Run(function: () =>
             {
                 _purchases.Add(purchase);
                 _context.Purchase.Add(purchase);
                 _context.Order.Where(x => x.OrderNo == order).FirstOrDefault().OrderPrice += purchase.PurQuan * purchase.PurPrice;
                 _context.SaveChanges();
                 return true;
             });
        }

        public Task<bool> FindByBookid(int order,int bookid)
        {
            return Task.Run(function: () =>
            {
                List<Purchase> purchases = GetById(order).Result;
                var purchase = purchases.Find(x => x.BookId == bookid);
                if (purchase == null)
                    return false;
                else if (purchase.PurStatus == 1)
                    return false;
                else
                    return true;
            });

        }
        public Task AddBook(int order,int bookid,int num)
        {
            return Task.Run(function: () =>
            {
                var purchase = GetById(order).Result.Find(x => x.BookId == bookid);
                _context.Purchase.Where(x => x.OrderNo == order && x.BookId == bookid).FirstOrDefault().PurQuan += num;
                _context.Order.Where(x => x.OrderNo == order).FirstOrDefault().OrderPrice += num * (_context.Purchase.Where(x => x.OrderNo == order && x.BookId == bookid).FirstOrDefault().PurPrice);
                _context.SaveChanges();
                return Task.CompletedTask;
            });
        }
    }
}
