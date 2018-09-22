using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class StudentCommentDto
    {
        public int StudentId { get; set; }
        public int CommentId { get; set; }
        public bool Voted { get; set; }
    }
}