using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }
        public List<string> Skills { get; set; }
        public List<Comment> Comments { get; set; }
        public int NumberOfComments { get; set; }
        public List<Post> Posts { get; set; }
        public int NumberOfPosts { get; set; }
        public Dictionary<Subject,int> Subjects { get; set; }
        public int NumberOfSubjects { get; set; }
    }
}