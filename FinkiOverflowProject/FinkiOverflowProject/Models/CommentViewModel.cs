using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class CommentViewModel
    {
        public int StudentId { get; set; }
        public string CommentBody { get; set; }
        public int VotesUp { get; set; }
        public int VotesDown { get; set; }
        public int CommentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime TimeAnswered { get; set; }
    }
}