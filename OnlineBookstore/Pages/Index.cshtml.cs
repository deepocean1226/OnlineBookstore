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
        private readonly IUserService userService;
        private readonly IBookService _bookService;
        public ILoginedService _loginedService;
        private readonly ILogger<IndexModel> _logger;
        private readonly OnlineBookstoreDBContext Context;

        //获取的书名
        [BindProperty]
        public string bookname { get; set; }

        //书目ID
        [BindProperty]
        public int id { get; set; }

        public IndexPageViewModel ViewModel { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            OnlineBookstoreDBContext _context,
            IUserService userService,
            ILoginedService loginedService,
            IBookService bookService)
        {
            ViewModel = new IndexPageViewModel();
            Context = _context;
            this.userService = userService;
            _bookService = bookService;
            _loginedService = loginedService;
            _logger = logger;

            //对ViewModel的初始化
            if(Context.Book.ToList().Count>=4)
                ViewModel.RecommendedBooks = Context.Book.ToList().Take(4).ToList();
            else
                ViewModel.RecommendedBooks = Context.Book.ToList();
            ViewModel.LimitedSaleBooks = Context.Book.ToList();
            ViewModel.Categories = Context.Category.Select(x => x.CateName).ToList();
            ViewModel.NewBooks = new List<Book>();
            ViewModel.NewBooks.Add(Context.Book.ToList().Last());
            if(Context.Book.ToList().Count()>=2)
                ViewModel.NewBooks.Add(Context.Book.ToList()[Context.Book.ToList().Count()-2]);
        }
        /// <summary>
        /// 页面调用后初始化
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetAsync()
        {
            //对ViewModel的初始化
            if (Context.Book.ToList().Count >= 4)
                ViewModel.RecommendedBooks = Context.Book.ToList().Take(4).ToList();
            else
                ViewModel.RecommendedBooks = Context.Book.ToList();
            ViewModel.LimitedSaleBooks = Context.Book.ToList();
            ViewModel.Categories = Context.Category.Select(x => x.CateName).ToList();
            ViewModel.NewBooks = new List<Book>();
            ViewModel.NewBooks.Add(Context.Book.ToList().Last());
            if (Context.Book.ToList().Count() >= 2)
                ViewModel.NewBooks.Add(Context.Book.ToList()[Context.Book.ToList().Count() - 2]);

            return Page();
        }

        /// <summary>
        /// 点击详情页响应
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (bookname == null)
            {
                return Page();
            }
            Models.Book book = _bookService.GetByBookName(bookname).Result;
            return RedirectToPage("/Books/Details", new { id=book.BookId });
            //, routeValues: new { book.BookId }
        }

        /// <summary>
        /// 退出登录响应
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetLogout(string username)
        {
            await _loginedService.ReMoveLogin(_loginedService.GetUserName().Result);
            return RedirectToPage("/Index");
        }
    }
}
