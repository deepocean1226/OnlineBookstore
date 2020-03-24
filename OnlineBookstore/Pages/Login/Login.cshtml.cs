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
        private readonly IUserNowService userNowService;
        public List<User> users = new List<User>();

        /// <summary>
        /// 保存获取的用户信息
        /// </summary>
        [BindProperty]
        public User User { get; set; }

        /// <summary>
        /// 各标志位
        /// </summary>
        [BindProperty]
        public bool Name { get; set; }
        [BindProperty]
        public bool Password { get; set; }
        [BindProperty]
        public bool Login_b { get; set; }

        public LoginModel(IUserService userService,IUserNowService userNowService)
        {
            this.userService = userService;
            this.userNowService = userNowService;
            this.Name = true;
            this.Password = true;
            this.Login_b = false;
        }

        /// <summary>
        /// 页面的登录响应
        /// </summary>
        /// <returns></returns>
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
                    this.Login_b = true;
                    //await userNowService.Set_Now(user);
                    await userNowService.Set_Now(user);//当前用户
                }
                else
                {
                    this.Password = false;
                }
            }
        }
    }
}