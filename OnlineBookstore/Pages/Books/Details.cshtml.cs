using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Models;
using OnlineBookstore.Services;

namespace OnlineBookstore
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineBookstoreDBContext _context;
        private readonly ILoginedService _loginedService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;
        public Order order;
        public Purchase purchase;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        //private readonly UserManager<ExtendedIdentityUser> _userManager;
        //, UserManager<ExtendedIdentityUser> userManager

        public DetailsModel(OnlineBookstoreDBContext context,
            ILoginedService loginedService,
            IOrderService orderService,
            IUserService userService,
            IPurchaseService purchaseService)
        {
            //_userManager = userManager;
            _context = context;
            this._loginedService = loginedService;
            this._orderService = orderService;
            this._userService = userService;
            this._purchaseService = purchaseService;
            ViewModel = new DetailsPageViewModel();
        }

        [BindProperty]
        public DetailsPageViewModel ViewModel { get; set; }

        public string ReturnUrl { get; set; }

        public string AuthorString { get; set; }

        [BindProperty]
        public string Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewModel.Book =await _context
               .Book
               .FirstOrDefaultAsync(m => m.BookId == id);

            ViewModel.Similar = await _context
                .Book
                .Take(2).ToListAsync();

            if (ViewModel.Book == null)
                return NotFound();

            AuthorString = ViewModel.Book.Author;
            switch (ViewModel.Book.CategoryId)
            {
                case 1:Category = "教育";break;
                case 2: Category = "小说"; break;
                case 3: Category = "生活"; break;
                case 4: Category = "科技"; break;
                case 5: Category = "童书"; break;
            }
            return Page();
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!_loginedService.isLogined().Result)
            {
                return RedirectToPage("/SignOn/SignOn");
            }
            else {
                ViewModel.Book = await _context
              .Book
              .FirstOrDefaultAsync(m => m.BookId == id);

                ViewModel.Similar = await _context
                    .Book
                    .Take(2).ToListAsync();
                if (ModelState.IsValid)
                {
                    if (ViewModel.Book.Quantity < ViewModel.OrderQuantity)
                    {
                        return Content("<script >alert('剩余库存不足，请调整购物量');</script >", "text/html");
                    }
                    order = _orderService.GetById(_userService.GetAll().Result.Find(x => x.UserName == _loginedService.GetUserName().Result).UserId).Result.FirstOrDefault();
                    if (order == null)
                    {
                        order = new Order() { UserId = await _userService.GetId(_loginedService.GetUserName().Result) };
                        await _orderService.Add(order);   
                    }
                    if (_purchaseService.FindByBookid(order.OrderNo, id).Result)
                    {
                        await _purchaseService.AddBook(order.OrderNo,id,ViewModel.OrderQuantity);
                        return RedirectToPage("/ShoppingCart/ShoppingCart");
                    }
                    purchase = new Purchase()
                    {
                        BookId = id,
                        OrderNo = order.OrderNo,
                        PurQuan = ViewModel.OrderQuantity,
                        PurPrice = ViewModel.Book.UnitPrice,
                        PurStatus = 0
                    };
                    await _purchaseService.Add(order.OrderNo,purchase);
                    return RedirectToPage("/ShoppingCart/ShoppingCart");
                }
            }
            //Something went wrong
            return Page();
        }
    }
}
