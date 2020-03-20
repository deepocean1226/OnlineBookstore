using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class Category
    {
        public Category()
        {
            Book = new HashSet<Book>();
        }

        public int CateId { get; set; }
        public string CateName { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
