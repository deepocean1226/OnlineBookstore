using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class Category
    {
        public Category()
        {
            BookInfo = new HashSet<BookInfo>();
        }

        public int CateId { get; set; }
        public string CateName { get; set; }

        public virtual ICollection<BookInfo> BookInfo { get; set; }
    }
}
