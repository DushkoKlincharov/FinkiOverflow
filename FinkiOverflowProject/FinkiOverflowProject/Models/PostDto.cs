using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SubjectId { get; set; }
        public string Subject { get; set; }
        public List<CommentDto> Comments { get; set; }
        public int? StudentId { get; set; }
        public string Student { get; set; }
    }
}