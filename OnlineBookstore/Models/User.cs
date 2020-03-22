using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookstore.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "请输入密码！")]
        public string Pwd { get; set; }
        [Required(ErrorMessage = "请输入用户名！")]
        public string UserName { get; set; }
        public int? ShoppingCartNo { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
