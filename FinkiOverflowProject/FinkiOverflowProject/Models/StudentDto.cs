using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageURL { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}