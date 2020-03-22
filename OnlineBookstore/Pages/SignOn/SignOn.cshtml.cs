using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstore.Models;
using OnlineBookstore.Services;

namespace OnlineBookstore
{
    public class SignOnModel : PageModel
    {
        private readonly IUserService _userService;
        
        public bool Ok { get; set; }

        public bool retype;

        [BindProperty]
        public User _User { get; set; }

        public SignOnModel(IUserService userService)
        {

            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!await _userService.Add(_User))
            { 
                retype = true;
                return Page();
            }
            else
            {
                retype = false;
                Ok = true;
            }
            return Page();
        }





    }
}