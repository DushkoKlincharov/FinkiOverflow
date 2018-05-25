using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class Subject
    {
        // Attributes
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 4)]
        public int Year { get; set; }

        // Foreign keys
        public ICollection<Post> Posts { get; set; }
    }
}