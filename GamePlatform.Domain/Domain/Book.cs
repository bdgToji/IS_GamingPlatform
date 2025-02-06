using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Domain.Domain
{
    public class Book: BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string BookImage { get; set; }

        [Required]
        public string AuthorFullName { get; set; }


        [Required]
        public string PublisherName { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
