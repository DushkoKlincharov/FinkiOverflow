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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,FirstName,LastName,ImageURL")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,FirstName,LastName,ImageURL")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewStudent(int id)
        {
            StudentViewModel model = PopulateModelForStudent(id,true);
            return View(model);
        }

        public ActionResult ViewAllComments(int id)
        {
            StudentViewModel model = PopulateModelForStudent(id, false);
            return View(model);
        }

        public ActionResult ViewAllPosts(int id)
        {
            StudentViewModel model = PopulateModelForStudent(id, false);
            return View(model);
        }

        public ActionResult ViewAllStudents()
        {
            List<Student> students = GetStudentsWithApprovedPosts();
            List<StudentsViewModel> model = students.Select(s =>
           {
               List<Subject> tempSubjects = db.Posts.Where(p => p.IsApproved).Include(p => p.Subject).ToList().Where(p => s.Posts.Contains(p)).Select(p => p.Subject).ToList();
               Dictionary<Subject, int> subjects = getGroupedSubjects(s).Take(1).ToDictionary(sub => sub.Key, sub => sub.Value);
               Subject subject = subjects.Keys.FirstOrDefault();
               return new StudentsViewModel()
               {
                   StudentId = s.Id,
                   Username = s.UserName,
                   City = s.City,
                   ImageUrl = s.ImageURL,
                   NumberOfPosts = s.Posts.Count,
                   SubjectId = subject == null ? -1 : subject.Id,
                   SubjectName = subject == null ? null : subject.Name
               };
           }).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult ViewPostsForStudent()
        {
            string userId = User.Identity.GetUserId();
            int currentUserId = db.Students.FirstOrDefault(s => s.UserId.Equals(userId)).Id;
            List<PostViewModel> model = GetPostViewModels(db.Posts
                 .Include(s => s.Student)
                 .Include(s => s.Comments)
                 .Include(s => s.Subject)
                 .Where(s => s.IsApproved)
                 .ToList()
                 .Where(s => s.StudentId == currentUserId)
                 .ToList());
            return View(model);
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult ViewCommentsForStudent()
        {
            string userId = User.Identity.GetUserId();
            Student student = db.Students.Include(s => s.Comments).FirstOrDefault(s => s.UserId.Equals(userId));
            List<Comment> model = db.Comments
                .Include(c => c.Post)
                .Include(c => c.Student)
                .Where(c => c.StudentId == student.Id)
                .AsEnumerable()
                .OrderByDescending(c => c.TimeAnswered)
                .ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult UpdateProfileForStudent()
        {
            string userId = User.Identity.GetUserId();
            Student student = db.Students.FirstOrDefault(s => s.UserId.Equals(userId));
            UpdateStudentViewModel model = new UpdateStudentViewModel()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                City = student.City,
                Id = student.Id,
                Modules = db.Modules.Select(s => s.Name).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfileForStudent(UpdateStudentViewModel model)
        {
            if(ModelState.IsValid)
            {
                Student student = db.Students.First(s => s.Id.Equals(model.Id));
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.ImageURL = model.ImageURL;
                student.Description = model.Description;
                student.City = model.City;
                student.Skills = model.Skills;
                student.Module = db.Modules.FirstOrDefault(m => m.Name.Equals(model.selectedModule));
                db.SaveChanges();
                return RedirectToAction("ViewStudent", "Students", new { id = model.Id });
            }
            return View(model);
        }

        private List<PostViewModel> GetPostViewModels(List<Post> posts)
        {
            List<PostViewModel> model = posts.ConvertAll(post => new PostViewModel()
            {
                PostId = post.Id,
                Answers = post.Comments.Count,
                Votes = post.Votes,
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

        private StudentViewModel PopulateModelForStudent(int id, bool ordered)
        {
            Student student = GetStudentsWithApprovedPosts().FirstOrDefault(s => s.Id == id);
            Dictionary<Subject, int> subjects = getGroupedSubjects(student);
            Dictionary<Subject, int> topSubjects = subjects.Take(5).ToDictionary(s => s.Key, s => s.Value);
            List<string> Skills = student.Skills!=null? student.Skills.Split(',').ToList() : new List<string>();
            StudentViewModel model = new StudentViewModel()
            {
                Id = student.Id,
                UserName = student.UserName,
                FirstName = student.FirstName,
                LastName = student.LastName,
                ImageUrl = student.ImageURL,
                Subjects = topSubjects,
                Module = student.Module == null ? "":student.Module.Name,
                Description = student.Description,
                Skills = Skills,
                NumberOfComments = student.Comments.Count,
                NumberOfPosts = student.Posts.Count,
                NumberOfSubjects = subjects.Count
            };
            if (ordered)
            {
                model.Comments = student.Comments.OrderBy(c => c.VotesUp).Reverse().Take(10).ToList();
                model.Posts = student.Posts.OrderBy(p => p.Votes).Reverse().Take(10).ToList();
            }
            else
            {
                model.Comments = student.Comments.ToList();
                model.Posts = student.Posts.ToList();
            }
            return model;
        }

        private Dictionary<Subject, int> getGroupedSubjects(Student student)
        {
            List<Subject> tempSubjects = db.Posts.Where(p => p.IsApproved).Include(p => p.Subject).ToList().Where(p => student.Posts.Contains(p)).Select(p => p.Subject).ToList();
            Dictionary<Subject, int> subjects =
                tempSubjects.GroupBy(s => s, (key, result) => new { Subject = key, Count = result.Count() })
                .OrderBy(s => s.Count)
                .Reverse()
                .ToDictionary(s => s.Subject, s => s.Count);
            return subjects;
        }

        private List<Student> GetStudentsWithApprovedPosts()
        {
            return db.Students.Include(s => s.Posts).Include(s => s.Comments).Include(s => s.Module).ToList()
                .Select(s => new Student()
                {
                    City = s.City,
                    Description = s.Description,
                    Comments = s.Comments.Where(c => c.Post.IsApproved).ToList(),
                    FirstName = s.FirstName,
                    Id = s.Id,
                    ImageURL = s.ImageURL,
                    LastName = s.LastName,
                    Module = s.Module,
                    Skills = s.Skills,
                    UserName = s.UserName,
                    Posts = s.Posts.Where(p => p.IsApproved).ToList()
                }).ToList();
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
