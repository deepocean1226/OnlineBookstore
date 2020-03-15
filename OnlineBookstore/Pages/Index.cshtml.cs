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
            

        }

        public void OnGet()
        {

        }
    }
}
