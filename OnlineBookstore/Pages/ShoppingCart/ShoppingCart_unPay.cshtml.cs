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
    public class ShoppingCartModel_unPay : PageModel
    {
        //����
        private readonly ILogger<ShoppingCartModel_unPay> _logger;
        private readonly IPurchaseService _purchaseService;
        private readonly IOrderService _orderService;
        public IBookService _bookService;
        private readonly ILoginedService _loginedService;
        private readonly OnlineBookstoreDBContext _context;
        private readonly IUserService _userService;

        //�ֲ�����
        public Order _order = new Order();
        public List<Purchase> _purchaseList = new List<Purchase>();
        public decimal sum = 0;

        public ShoppingCartModel_unPay(ILogger<ShoppingCartModel_unPay> logger, IPurchaseService purchaseService,
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

            //��ӵ�¼�����Ĳ���
            if (_loginedService.isLogined().Result)
            {
                _order = _orderService.GetById(_userService.GetAll().Result.Find(x => x.UserName == _loginedService.GetUserName().Result).UserId).Result.FirstOrDefault();

                if (_order != null)
                {
                    _purchaseList = _purchaseService.GetById(_order.OrderNo).Result;
                }
            }

            ////�޵�¼����
            //_order = _orderService.GetById(0).Result.FirstOrDefault();

            //if (_order != null)
            //{
            //    _purchaseList = _purchaseService.GetById(_order.OrderNo).Result;
            //}

        }

        public async Task<IActionResult> OnPostDelAsync(int purchase)
        {

            Models.Book book = _context.Book.Find(_context.Purchase.Find(purchase).BookId);
            Console.WriteLine("�޸�ǰ" + book.Quantity);

            book.Quantity += _context.Purchase.Find(purchase).PurQuan;

            _context.Book.Update(book);
            _context.Purchase.Remove(_context.Purchase.Find(purchase));

            _context.SaveChanges();
            Console.WriteLine("�޸ĺ�"+book.Quantity);
            return RedirectToPage();
        }

        public IActionResult OnPostCheck()
        {
            OnGetAsync();
            foreach (var a in _purchaseList)
            {
                var p=_context.Purchase.Find(a.DetailNo);
                p.PurStatus = 1;
                _context.Purchase.Update(p);
            }

            _context.SaveChanges();
            Console.WriteLine("�������");
            return RedirectToPage();
        }
    }
}
