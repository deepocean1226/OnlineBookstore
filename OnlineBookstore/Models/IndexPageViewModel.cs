using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class IndexPageViewModel
    {
        public List<Book> RecommendedBooks { get; set; }
        public List<Book> LimitedSaleBooks { get; set; }
        public List<string> Categories { get; set; }
    }
}
