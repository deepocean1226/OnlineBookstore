using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Models;

namespace OnlineBookstore.Pages.Book
{
    public class DetailsModel : PageModel
    {
        private readonly OnlineBookstoreDBContext _context;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        //private readonly UserManager<ExtendedIdentityUser> _userManager;
        //, UserManager<ExtendedIdentityUser> userManager

        public DetailsModel(OnlineBookstoreDBContext context)
        {
            //_userManager = userManager;
            _context = context;
            ViewModel = new DetailsPageViewModel();
            ReturnUrl = "/Account/Cart";
        }

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


            ViewModel.Book = await _context
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
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);

        //        if (user == null)
        //            return LocalRedirect("/Identity/Account/Register");

        //        await _context.Cart.AddAsync(new Cart()
        //        {
        //            User = user,
        //            CartBooks = new List<CartBook>() { new CartBook() { Book = ViewModel.Book, Quantity = ViewModel.OrderQuantity } }
        //        });
        //        await _context.SaveChangesAsync();

        //        //Show cart
        //        return LocalRedirect(ReturnUrl);
        //    }

        //    //Something went wrong
        //    return Page();
        //}
    }
}
