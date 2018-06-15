using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class Post
    {
        // Attributes
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsApproved { get; set; } = false;
        public string Title { get; set; }
        public DateTime TimeAsked { get; set; }
        public int Votes { get; set; }

        // Foreign keys
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}