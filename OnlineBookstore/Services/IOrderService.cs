using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<List<Order>> GetById(int userId);

    }
}
