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


        public IndexModel(ILogger<IndexModel> logger,OnlineBookstoreDBContext _context,IUserNowService userNowService,IUserService userService)
        {
            ViewModel = new IndexPageViewModel();
            Context = _context;
            this._userNowService = userNowService;
            this.userService = userService;
            _bookService = bookService;
            _loginedService = loginedService;
            _logger = logger;
            

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
