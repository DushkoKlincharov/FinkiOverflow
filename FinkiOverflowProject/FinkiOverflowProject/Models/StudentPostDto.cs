using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class StudentPostDto
    {
        public int StudentId { get; set; }
        public int PostId { get; set; }
        public bool Voted { get; set; }
    }
}