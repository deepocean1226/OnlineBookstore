using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class Book
    {
        public Book()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string PublishDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public string ContentDescript { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
