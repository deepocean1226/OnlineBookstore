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
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger,IUserService userService,IBookService bookService)
        {
            
            this.userService = userService;
            _bookService = bookService;
            _logger = logger;
            

        }

        public void OnGet()
        {

        }
    }
}
