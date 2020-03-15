using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class PurchaseInfo
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int DetailNo { get; set; }
        public int PurQuan { get; set; }
        public decimal PurPrice { get; set; }

        public virtual BookInfo Book { get; set; }
        public virtual UserInfo User { get; set; }
    }
}
