using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class StudentsViewModel
    {
        public int StudentId { get; set; }
        public string ImageUrl { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public int NumberOfPosts { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}