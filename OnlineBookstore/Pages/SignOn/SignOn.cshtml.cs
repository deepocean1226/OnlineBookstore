using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public User _User { get; set; } 

        public SignOnModel(IUserService userService)
        {
            _User = new User();
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "用户名")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm password")]
            //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            //public string ConfirmPassword { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _User.Pwd = Input.Password;
            _User.UserName = Input.UserName;
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