using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class DetailsPageViewModel
    {
        [Required]
        public Book Book { get; set; }
        public ICollection<Book> Similar  { get; set; }

        [Range(1, 100)]
        [Required]
        public int OrderQuantity { get; set; }
    }
}
