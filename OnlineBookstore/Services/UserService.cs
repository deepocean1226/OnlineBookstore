using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBookstore.Services
{
    public class UserService : IUserService
    {

        private readonly List<User> _users = new List<User>();
        private readonly OnlineBookstoreDBContext _context;

        public UserService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _users = _context.User.ToList();
        }

        public Task<List<User>> GetAll()
        {
            return Task.Run(function: () => _users);
        }

        public Task<bool> ExistUser(string userName)
        {
            return Task.Run(function: () =>
                {
                    return GetAll().Result.Exists(x => x.UserName == userName);
                }
            );
        }

        public Task<bool> Add(User user)
        {
            return Task.Run(function: () =>
            {
                if (ExistUser(user.UserName).Result)
                {
                    return false;
                }

                _users.Add(user);
                _context.User.Add(user);
                _context.SaveChanges();
                return true;

            });
        }
    }
}
