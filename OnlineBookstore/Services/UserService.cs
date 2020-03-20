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

        public UserService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _users = _context.User.ToList();
        }

        public Task<List<User>> GetAll()
        {
            return Task.Run(function: () => _users);
        }

        public Task Add(User user)
        {
            user.UserId = _users.Max(x => x.UserId) + 1;
            _users.Add(user);
            var db = _context.User;
            db.Add(user);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
