using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public class UserService : IUserService
    {

        private readonly List<UserInfo> _userInfos = new List<UserInfo>();
        private readonly OnlineBookstoreDBContext _context;

        public UserService(OnlineBookstoreDBContext context)
        {
            this._context = context;
            _userInfos= _context.UserInfo.ToList();
        }

        public Task<List<UserInfo>> GetAll()
        {
            return Task.Run(function: () => _userInfos);
        }
    }
}
