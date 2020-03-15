using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public interface IUserService
    {
        Task<List<UserInfo>> GetAll();

    }
}
