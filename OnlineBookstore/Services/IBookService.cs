using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();

        /*如果返回null表示未找到*/
        Task<Book> GetByBookName(string bookName);

        /*将卖出书籍将库存数量减bookCount*/
        /*如果数量不足或者书籍不存在则返回false表示无法出售*/
        Task<bool> SaleOut(string bookName,int bookCount);  
    }
}
