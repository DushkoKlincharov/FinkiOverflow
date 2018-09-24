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
using System.Web.Mvc;
using System.Web.Security;

namespace FinkiOverflowProject.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Posts
        public ActionResult Index()
        {
            
            List<PostViewModel> model = GetPostViewModels(db.Posts
                .Include(s => s.Student)
                .Include(s => s.Comments)
                .Include(s => s.Subject)
                .Where(s => s.IsApproved)
                .ToList());
            return View(model);
        }

        public ActionResult Guest()
        {
            // Sign out user
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                string currentStudentId = User.Identity.GetUserId();
                Student student = db.Students.FirstOrDefault(s => s.UserId.Equals(currentStudentId));
                post.StudentId = student.Id;
                post.Student = student;
                post.TimeAsked = DateTime.Now;
                StudentPost sp = new StudentPost()
                {
                     Post = post,
                     Student = student,
                     Voted = false
                };
                db.Posts.Add(post);
                db.StudentPosts.Add(sp);
                db.SaveChanges();
                return RedirectToAction("ViewPostsForStudent","Students");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", post.SubjectId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName", post.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", post.SubjectId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,IsApproved,SubjectId,StudentId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "UserName", post.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", post.SubjectId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewPostsForSubject(int id)
        {
            List<Post> posts = db.Subjects.Include(s => s.Posts).FirstOrDefault(s => s.Id == id).Posts.ToList();
            
            List<PostViewModel> model = GetPostViewModels(db.Posts
                .Include(s => s.Student)
                .Include(s => s.Comments)
                .Include(s => s.Subject)
                .ToList()
                .Where(s => posts.Contains(s))
                .ToList());

            return View("Index",model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ApprovePosts()
        {
            List<PostViewModel> model = GetPostViewModels(db.Posts
                .Include(s => s.Student)
                .Include(s => s.Comments)
                .Include(s => s.Subject)
                .Where(s => !s.IsApproved)
                .ToList());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePosts()
        {
            List<PostViewModel> model = GetPostViewModels(db.Posts
                 .Include(s => s.Student)
                 .Include(s => s.Comments)
                 .Include(s => s.Subject)
                 .Where(s => s.IsApproved)
                 .ToList());
            return View(model);
        }

        private List<PostViewModel> GetPostViewModels(List<Post> posts)
        {
            List<PostViewModel> model = posts.ConvertAll(post => new PostViewModel()
            {
                PostId = post.Id,
                Answers = post.Comments.Count,
                Votes = post.Votes,
                NumberOfVotes = post.NumberOfVotes,
                PostBody = post.Text,
                PostTitle = post.Title,
                TimeAsked = post.TimeAsked,
                StudentName = post.Student.FirstName,
                StudentImageUrl = post.Student.ImageURL,
                StudentId = post.Student.Id,
                SubjectId = post.SubjectId,
                SubjectName = post.Subject.Name
            })
            .OrderByDescending(p => p.TimeAsked)
            .ToList();
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
