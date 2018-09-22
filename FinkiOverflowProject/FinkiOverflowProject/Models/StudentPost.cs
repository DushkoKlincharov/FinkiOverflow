using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class StudentPost
    {
        [Key, Column(Order = 0)]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Key, Column(Order = 1)]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public bool Voted { get; set; } = false;
    }
}