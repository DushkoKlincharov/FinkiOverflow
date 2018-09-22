using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class Student
    {
        // Attributes
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ImageURL { get; set; }
        public string City { get; set; }
        public Module Module { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }

        // Foreign keys
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Post> Posts { get; set; }
        public string UserId { get; set; }
    }
}