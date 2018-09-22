using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinkiOverflowProject.Models
{
    public class CommentsViewModel
    {
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public int PostId { get; set; }
        public int PostVotes { get; set; }
        public int PostAnswers { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int StudentId { get; set; }
        public int CurrentStudentId { get; set; }
        public bool isUserLoggedIn { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentImageUrl { get; set; }
        public DateTime TimeAsked { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        [Required]
        public string NewCommentText { get; set; }
    }
}