using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services
{
    public class BookInfoService:IBookInfoService
    {
        private readonly OnlineBookstoreDBContext _context;

        public BookInfoService(OnlineBookstoreDBContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<BookInfo>> GetAll()
        {
            return Task.Run(function: () => _context.BookInfo.AsEnumerable<BookInfo>());
        }

        public Task<BookInfo> GetByBookName(string bookName)
        {
            return Task.Run(function: () =>
                _context.BookInfo.FirstOrDefault(x => bookName != null && x.BookName == bookName));
        }

        public async Task<bool> SaleOut(string bookName, int bookCount)
        {
            BookInfo? aBook = await GetByBookName(bookName);

            //找不到书籍
            if (aBook == null) return false;

            //数量不足
            int i = aBook.Quantity - bookCount;
            if (i < 0)
            {
                return false;
            }

            //成功修改
            aBook.Quantity = aBook.Quantity - bookCount;
            _context.SaveChanges();
            return true;

        }
    }
}
