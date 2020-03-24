using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineBookstore.Services
{
    public class LoginedService:ILoginedService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Dictionary<string, string> LoginInfo = new Dictionary<string, string>();

        public LoginedService(IHttpContextAccessor _httpContextAccessor)
        {
            this.httpContextAccessor = _httpContextAccessor;
        }
        public Task AddLogin(string username)
        {
            string clientIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string clientPort = httpContextAccessor.HttpContext.Connection.RemotePort.ToString();

            if (LoginInfo.ContainsKey(clientIpAddress + clientPort))
            {
                LoginInfo.Remove(clientIpAddress + clientPort);
                LoginInfo.Add(clientIpAddress + clientPort, username);
            }
            else
            {
                LoginInfo.Add(clientIpAddress + clientPort, username);
            }

            return Task.CompletedTask;
        }

        public Task ReMoveLogin(string username)
        {
            string clientIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string clientPort = httpContextAccessor.HttpContext.Connection.RemotePort.ToString();

            LoginInfo.Remove(clientIpAddress + clientPort);

            return Task.CompletedTask;
        }
        public Task<bool> isLogined()
        {
            return Task.Run(function: () =>
            {
                string clientIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                string clientPort = httpContextAccessor.HttpContext.Connection.RemotePort.ToString();

                if (LoginInfo.ContainsKey(clientIpAddress + clientPort))
                {
                    return true;
                }

                return false;
            });
        }

        public Task<string> GetUserName()
        {
            return Task.Run(function: () =>
            {
                string clientIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                string clientPort = httpContextAccessor.HttpContext.Connection.RemotePort.ToString();

                if (LoginInfo.ContainsKey(clientIpAddress + clientPort))
                {
                    return LoginInfo[clientIpAddress + clientPort];
                }

                return null;

            });
        }
    }
}
