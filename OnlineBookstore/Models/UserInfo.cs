using System;
using System.Collections.Generic;

namespace OnlineBookstore.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            PurchaseInfo = new HashSet<PurchaseInfo>();
        }

        public int UserId { get; set; }
        public string Pwd { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<PurchaseInfo> PurchaseInfo { get; set; }
    }
}
