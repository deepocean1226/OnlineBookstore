using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBookstore.Models;
using OnlineBookstore.Services;


namespace OnlineBookstore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly OnlineBookstoreDBContext Context;
        private readonly IUserNowService _userNowService;
        private readonly IUserService userService;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public User User_Now { get; set; }

        public IndexPageViewModel ViewModel { get; set; }

        public IndexModel(ILogger<IndexModel> logger,OnlineBookstoreDBContext _context,IUserNowService userNowService,IUserService userService)
        {
            ViewModel = new IndexPageViewModel();
            Context = _context;
            this._userNowService = userNowService;
            this.userService = userService;
            _logger = logger;
            //User user = _userNowService.Get_Now();
            User user = _userNowService.Get_Now();

            if (user != null)
            {
                User_Now = user;
            }
            else
            {
                User_Now = null;
            }
            //以下代码用来测试数据库
            
            //var db = _context.Book;
            //db.Add(new Book()
            //{

            //    BookName = "测试22",
            //    CategoryId = 1,
            //    Author = "sss",
            //    PublishDate = "uhhdkjd",
            //    Publisher = "cdc",
            //    UnitPrice = 22,
            //    ImagePath = null,
            //    ContentDescript = "cdcdcd"
            //});
            //_context.SaveChanges();
            //Console.WriteLine("数据已经写入");
            
        }

        public IActionResult OnGetAsync()
        {
            ViewModel.RecommendedBooks = Context.Book.ToList();
            ViewModel.LimitedSaleBooks = Context.Book.ToList();
            ViewModel.Categories = Context.Category.Select(x => x.CateName).ToList();

            return Page();
        }
    }
}
