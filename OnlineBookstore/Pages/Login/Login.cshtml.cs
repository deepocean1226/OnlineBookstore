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

        [BindProperty]
        public UserInfo UserInfo { get; set; }
        [BindProperty]
        public bool Name { get; set; }
        [BindProperty]
        public bool Password { get; set; }
        [BindProperty]
        public bool Login_b { get; set; }
        public LoginModel(IUserService userService)
        {
            this.userService = userService;
            this.Name = true;
            this.Password = true;
            this.Login_b = false;
        }
        public async Task OnPostAsync()
        {
            userInfos = await userService.GetAll();
            UserInfo user = userInfos.Find(x => x.UserName == UserInfo.UserName);
            if(user==null)
            {
                this.Name = false;
            }
            else
            {
                if (user.Pwd == UserInfo.Pwd)
                {
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