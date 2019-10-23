using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
