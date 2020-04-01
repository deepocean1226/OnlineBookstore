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
    public class CateDetailsModel : PageModel
    {
        private readonly IUserService userService;
        private readonly IBookService _bookService;
        public ILoginedService _loginedService;
        private readonly ILogger<CateDetailsModel> _logger;
        private readonly OnlineBookstoreDBContext Context;

        public CateDetailsPageViewModel ViewModel { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        public CateDetailsModel(ILogger<CateDetailsModel> logger, OnlineBookstoreDBContext _context, IUserService userService, ILoginedService loginedService, IBookService bookService)
        {
            ViewModel = new CateDetailsPageViewModel();
            Context = _context;
            this.userService = userService;
            _bookService = bookService;
            _loginedService = loginedService;
            _logger = logger;

            ViewModel.RecommendedBooks = Context.Book.ToList();                     
        }

        public IActionResult OnGetAsync(string id)
        {
            ViewModel.Id = id;
            switch (id)
            {
                case "����": CategoryId = 1; break;
                case "С˵": CategoryId = 2; break;
                case "����": CategoryId = 3; break;
                case "�Ƽ�": CategoryId = 4; break;
                case "ͯ��": CategoryId = 5; break;
            }
            ViewModel.RecommendedBooks = Context.Book.ToList();
            var books = from x in ViewModel.RecommendedBooks
                        where x.CategoryId == CategoryId
                        select x;
            ViewModel.ResBooks=books.ToList();

            return Page();
        }
    }
}
