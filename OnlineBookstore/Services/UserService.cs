using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public class UserService : IUserService
    {

        private readonly List<User> _users = new List<User>();
        private readonly OnlineBookstoreDBContext _context;
        /// <summary>
        /// 当前用户，但是多客户端依旧冲突
        /// </summary>
        public static User user_now=null;

        public UserService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _users = _context.User.ToList();
        }
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> GetAll()
        {
            return Task.Run(function: () => _users);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task Add(User user)
        {
            user.UserId = _users.Max(x => x.UserId) + 1;
            _users.Add(user);
            var db = _context.User;
            db.Add(user);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得当前用户，主页引用
        /// </summary>
        /// <returns></returns>
        public User Get_Now()
        {
            return user_now;
        }

        /// <summary>
        /// 设置当前用户，登录引用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task Set_Now(User user)
        {
            user_now = user;
            return Task.CompletedTask;
        }

    }
}
