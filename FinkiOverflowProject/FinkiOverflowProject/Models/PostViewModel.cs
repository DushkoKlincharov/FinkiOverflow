using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public int Votes { get; set; }
        public int NumberOfVotes { get; set; }
        public int Answers { get; set; }
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public DateTime TimeAsked { get; set; }
        public string StudentName { get; set; }
        public string StudentImageUrl { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}