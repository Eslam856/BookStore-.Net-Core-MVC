using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required]
        //[MaxLength(20)]
        //[MinLength(5)]
        [StringLength(20,MinimumLength =5)]
        public String AuthorName { get; set; }

    }
}
