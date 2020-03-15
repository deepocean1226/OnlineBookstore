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
    public class LoginModel : PageModel
    {
        private readonly IUserService userService;
        public List<UserInfo> userInfos = new List<UserInfo>();

        public LoginModel(IUserService userService)
        {
            this.userService = userService;
        }

        public async void OnGet()
        {
            userInfos = await userService.GetAll();
        }
    }
}