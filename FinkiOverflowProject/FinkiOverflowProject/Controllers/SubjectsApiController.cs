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
    public class SubjectsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SubjectsApi
        public IQueryable<Subject> GetSubjects()
        {
            return db.Subjects;
        }

        // GET: api/SubjectsApi/5
        [ResponseType(typeof(Subject))]
        public IHttpActionResult GetSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            SubjectsDto s = new SubjectsDto()
            {
                Id = subject.Id,
                Name = subject.Name,
                Year = subject.Year,
                Posts = db.Posts.Select(p => new PostDto() {
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
                    }).Where(c => c.PostId == p.Id).ToList()
                }).Where(p => p.SubjectId == subject.Id).ToList()
            };
            return Ok(s);
        }

        // PUT: api/SubjectsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubject(int id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.Id)
            {
                return BadRequest();
            }

            db.Entry(subject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/SubjectsApi
        [ResponseType(typeof(Subject))]
        public IHttpActionResult PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subject);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subject.Id }, subject);
        }

        // DELETE: api/SubjectsApi/5
        [ResponseType(typeof(Subject))]
        public IHttpActionResult DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }

            db.Subjects.Remove(subject);
            db.SaveChanges();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectExists(int id)
        {
            return db.Subjects.Count(e => e.Id == id) > 0;
        }
    }
}