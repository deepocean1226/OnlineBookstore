using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class Order
    {
        public Order()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int OrderNo { get; set; }
        public int UserId { get; set; }
        public decimal OrderPrice { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
