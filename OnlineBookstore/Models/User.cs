using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Pwd { get; set; }
        public string UserName { get; set; }
        public int? ShoppingCartNo { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
