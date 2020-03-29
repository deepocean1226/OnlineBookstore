using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OnlineBookstore.Models;
using OnlineBookstore.Services;

namespace OnlineBookstore.Pages.ShoppingCart
{
    public class ShoppingCartModel_Finished : PageModel
    {
        //服务
        private readonly ILogger<ShoppingCartModel_Finished> _logger;
        private readonly IPurchaseService _purchaseService;
        private readonly IOrderService _orderService;
        public IBookService _bookService;
        private readonly ILoginedService _loginedService;
        private readonly OnlineBookstoreDBContext _context;
        private readonly IUserService _userService;

        //局部变量
        public Order _order = new Order();
        public List<Purchase> _purchaseList = new List<Purchase>();
        public decimal sum = 0;

        public ShoppingCartModel_Finished(ILogger<ShoppingCartModel_Finished> logger, IPurchaseService purchaseService,
            IOrderService orderService, IBookService bookService, ILoginedService loginedService,
            OnlineBookstoreDBContext context, IUserService userService)
        {
            _logger = logger;
            _purchaseService = purchaseService;
            _orderService = orderService;
            _bookService = bookService;
            this._loginedService = loginedService;
            _context = context;
            _userService = userService;
        }

        public void OnGetAsync()
        {
            if (_loginedService.isLogined().Result)
            {
                _order = _orderService.GetById(_userService.GetAll().Result.Find(x => x.UserName == _loginedService.GetUserName().Result).UserId).Result.FirstOrDefault();

                if (_order != null)
                {
                    _purchaseList = _purchaseService.GetById(_order.OrderNo).Result;
                }
            }
            _order = _orderService.GetById(0).Result.FirstOrDefault();

            if (_order != null)
            {
                _purchaseList = _purchaseService.GetById(_order.OrderNo).Result;
            }

        }

        public async Task<IActionResult> OnPostDelAsync(int purchase)
        {
            Models.Book book = _context.Book.Find(_context.Purchase.Find(purchase).BookId);

            if (_context.Purchase.Find(purchase).PurStatus == 1)
            {
                _context.Purchase.Remove(_context.Purchase.Find(purchase));
                _context.SaveChanges();
                Console.WriteLine("已完成，不修改数据库"+book.Quantity);
            }
            else if(_context.Purchase.Find(purchase).PurStatus == 0)
            {
                Console.WriteLine("修改前" + book.Quantity);

                book.Quantity += _context.Purchase.Find(purchase).PurQuan;

                _context.Book.Update(book);
                _context.Purchase.Remove(_context.Purchase.Find(purchase));

                _context.SaveChanges();
                Console.WriteLine("修改后" + book.Quantity);
            }
           
            return RedirectToPage();
        }

    }
}

