using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinkiOverflowProject.Models;
using Microsoft.AspNet.Identity;

namespace FinkiOverflowProject.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Post).Include(c => c.Student);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Text");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,VotesUp,VotesDown,StudentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.PostId = int.Parse(Request.QueryString["postId"]);
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details/"+comment.PostId, "Posts");
            }

            ViewBag.PostId = new SelectList(db.Posts, "Id", "Text", comment.PostId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName", comment.StudentId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Text", comment.PostId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName", comment.StudentId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,VotesUp,VotesDown,PostId,StudentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                //comment.PostId = int.Parse(Request.QueryString["postId"]);
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + comment.PostId, "Posts");
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Text", comment.PostId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName", comment.StudentId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewComments(int postId)
        {
            CommentsViewModel model = GetCommentsViewModelFor(postId);
            model.Comments = model.Comments.OrderByDescending(s => (s.VotesUp - s.VotesDown)).ThenByDescending(s => s.TimeAnswered).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteComments()
        {
            List<Comment> model = db.Comments.Include(c => c.Post).Include(c => c.Student).ToList().OrderBy(c => c.TimeAnswered).Reverse().ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewComments(CommentsViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(User.Identity.GetUserId()))
                {
                    ModelState.AddModelError("User", "User not logged in");
                    return View(GetCommentsViewModelFor(model.PostId));
                }
                Comment comment = new Comment()
                {
                    Text = model.NewCommentText,
                    PostId = model.PostId,
                    StudentId = model.CurrentStudentId,
                    TimeAnswered = DateTime.Now
                };
                StudentComment studentComment = new StudentComment()
                {
                     CommentId = comment.Id,
                     StudentId = comment.StudentId,
                     Voted = false
                };
                Post post = db.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == model.PostId);
                post.Comments.Add(comment);
                db.StudentComments.Add(studentComment);
                db.SaveChanges();
                return RedirectToAction("ViewComments", new { postId = model.PostId });
            }
            return View(GetCommentsViewModelFor(model.PostId));
        }

        private CommentsViewModel GetCommentsViewModelFor(int postId)
        {
            Post post = db.Posts
               .Include(p => p.Comments)
               .Include(p => p.Student)
               .Include(p => p.Subject)
               .FirstOrDefault(p => p.Id == postId);
            List<Comment> PostComments = db.Comments
                .Include(c => c.Student)
                .ToList()
                .Where(c => post.Comments.Contains(c))
                .OrderByDescending(c => c.TimeAnswered)
                .ToList();
            List<CommentViewModel> Comments = new List<CommentViewModel>();
            foreach (Comment comment in PostComments)
            {
                CommentViewModel c = new CommentViewModel()
                {
                    StudentId = comment.Student.Id,
                    StudentFirstName = comment.Student.FirstName,
                    StudentLastName = comment.Student.LastName,
                    ImageUrl = comment.Student.ImageURL,
                    TimeAnswered = comment.TimeAnswered,
                    CommentBody = comment.Text,
                    CommentId = comment.Id,
                    VotesUp = comment.VotesUp,
                    VotesDown = comment.VotesDown
                };
                Comments.Add(c);
            }
            string currentStudentId = User.Identity.GetUserId();
            Student student = db.Students.FirstOrDefault(s => s.UserId.Equals(currentStudentId));
            CommentsViewModel model = new CommentsViewModel
            {
                PostId = post.Id,
                PostTitle = post.Title,
                PostText = post.Text,
                PostVotes = post.Votes,
                PostAnswers = Comments.Count,
                StudentId = post.Student.Id,
                StudentFirstName = post.Student.FirstName,
                StudentLastName = post.Student.LastName,
                StudentImageUrl = post.Student.ImageURL,
                Comments = Comments,
                SubjectId = post.Subject.Id,
                SubjectName = post.Subject.Name,
                CurrentStudentId = student == null ? -1 : student.Id,
                isUserLoggedIn = string.IsNullOrEmpty(currentStudentId) ? false : true,
                TimeAsked = post.TimeAsked
        };
            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
