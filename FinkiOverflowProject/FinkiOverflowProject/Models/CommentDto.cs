using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int VotesUp { get; set; } = 0;
        public int VotesDown { get; set; } = 0;
        public int PostId { get; set; }
        public int StudentId { get; set; }
        public string Student { get; set; }
    }
}