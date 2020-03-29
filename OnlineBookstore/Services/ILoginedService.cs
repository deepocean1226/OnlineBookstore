using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public interface ILoginedService
    {
        Task AddLogin(string username);
        Task<bool> isLogined();
        Task<string> GetUserName();

        Task Exit();

    }
}
