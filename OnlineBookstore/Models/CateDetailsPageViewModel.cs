using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class CateDetailsPageViewModel
    {
        public string Id { get; set; }
        public List<Book> RecommendedBooks { get; set; }
        public List<Book> ResBooks { get; set; }
    }
}
