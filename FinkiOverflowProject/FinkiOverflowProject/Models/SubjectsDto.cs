using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class SubjectsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}