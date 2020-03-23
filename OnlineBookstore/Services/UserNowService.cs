using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    /// <summary>
    /// userNow为Singleton测试服务，多客户端报错
    /// </summary>
    public class UserNowService:IUserNowService
    {
        public User user_now = null;
        public User Get_Now()
        {
            return user_now;
        }

        public Task Set_Now(User user)
        {
            this.user_now = user;
            return Task.CompletedTask;
        }
    }
}
