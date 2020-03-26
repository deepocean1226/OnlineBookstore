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
        private readonly ILoginedService _loginedService;
        public List<User> users = new List<User>();

        
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public bool Name { get; set; }
        [BindProperty]
        public bool Password { get; set; }
        [BindProperty]
        public bool Login_b { get; set; }

        public LoginModel(IUserService userService,ILoginedService loginedService)
        {
            this.userService = userService;
            _loginedService = loginedService;
            this.Name = true;
            this.Password = true;
            this.Login_b = false;
        }

        
        public async Task OnPostAsync()
        {
            users = await userService.GetAll();
            User user = users.Find(x => x.UserName == User.UserName);
            if (user == null)
            {
                this.Name = false;
            }
            else
            {
                if (user.Pwd == User.Pwd)
                {
                    await _loginedService.AddLogin(user.UserName);
                    this.Login_b = true;
                }
                else
                {
                    this.Password = false;
                }
            }
        }
    }
}