using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class Comment
    {
        // Attributes
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int VotesUp { get; set; } = 0;
        public int VotesDown { get; set; } = 0;

        // Foreign Keys
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}