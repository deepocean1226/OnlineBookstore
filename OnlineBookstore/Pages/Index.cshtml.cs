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

        public IndexPageViewModel ViewModel { get; set; }

        public IndexModel(ILogger<IndexModel> logger,OnlineBookstoreDBContext _context,IUserService userService,ILoginedService loginedService,IBookService bookService)
        {
            ViewModel = new IndexPageViewModel();
            Context = _context;
            this.userService = userService;
            _bookService = bookService;
            _loginedService = loginedService;
            _logger = logger;

            ViewModel.RecommendedBooks = Context.Book.ToList();
            ViewModel.LimitedSaleBooks = Context.Book.ToList();
            ViewModel.Categories = Context.Category.Select(x => x.CateName).ToList();
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
