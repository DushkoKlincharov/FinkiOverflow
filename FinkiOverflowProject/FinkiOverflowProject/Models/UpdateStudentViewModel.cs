using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class UpdateStudentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public string City { get; set; }
        public List<string> Modules { get; set; }
        public string selectedModule { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }
    }
}