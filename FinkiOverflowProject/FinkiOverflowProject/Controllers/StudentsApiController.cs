using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FinkiOverflowProject.Models;

namespace FinkiOverflowProject.Controllers
{
    public class StudentsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StudentsApi
        public IQueryable<Student> GetStudents()
        {
            return db.Students;
        }

        // GET: api/StudentsApi/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            StudentDto s = new StudentDto()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                UserName = student.UserName,
                ImageURL = student.ImageURL,
                Posts = db.Posts.Select(p => new PostDto()
                {
                    Id = p.Id,
                    Student = p.Student.UserName,
                    StudentId = p.StudentId,
                    Subject = p.Subject.Name,
                    SubjectId = p.SubjectId,
                    Text = p.Text,
                    Comments = db.Comments.Select(c => new CommentDto
                    {
                        Id = c.Id,
                        PostId = c.PostId,
                        Student = c.Student.UserName,
                        StudentId = c.StudentId,
                        Text = c.Text,
                        VotesDown = c.VotesDown,
                        VotesUp = c.VotesUp
                    }).Where(c => c.Id == p.Id).ToList()
                }).Where(p => p.StudentId == student.Id).ToList()
            };
            return Ok(s);
        }

        // PUT: api/StudentsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentsApi
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
        }

        // DELETE: api/StudentsApi/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.Students.Count(e => e.Id == id) > 0;
        }
    }
}