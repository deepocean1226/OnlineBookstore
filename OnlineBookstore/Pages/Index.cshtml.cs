using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBookstore.Models;

namespace OnlineBookstore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly OnlineBookstoreDBContext Context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger,OnlineBookstoreDBContext _context)
        {
            Context = _context;
            _logger = logger;
            var db = _context.BookInfo;
            db.Add(new BookInfo()
            {
                BookName = "海上钢琴师",
                CategoryId = 1,
                Author = "sss",
                PublishDate = "uhhdkjd",
                Publisher = "cdc",
                UnitPrice = 22,
                ImagePath = null,
                ContentDescript = "cdcdcd"
            });
            Context.SaveChanges();

        }

        public void OnGet()
        {

        }
    }
}
