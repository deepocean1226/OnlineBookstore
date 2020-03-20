using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class Purchase
    {
        public int BookId { get; set; }
        public int OrderNo { get; set; }
        public int DetailNo { get; set; }
        public int PurQuan { get; set; }
        public decimal PurPrice { get; set; }
        public int PurStatus { get; set; }

        public virtual Book Book { get; set; }
        public virtual Order OrderNoNavigation { get; set; }
    }
}
