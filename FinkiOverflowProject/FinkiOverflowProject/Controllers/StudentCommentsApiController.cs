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
    public class StudentCommentsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StudentCommentsApi
        public IQueryable<StudentComment> GetStudentComments()
        {
            return db.StudentComments;
        }

        // GET: api/StudentCommentsApi/5
        [ResponseType(typeof(StudentComment))]
        public IHttpActionResult GetStudentComment(int id)
        {
            StudentComment studentComment = db.StudentComments.Find(id);
            if (studentComment == null)
            {
                return NotFound();
            }

            return Ok(studentComment);
        }

        // PUT: api/StudentCommentsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentComment(int id, StudentComment studentComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentComment.StudentId)
            {
                return BadRequest();
            }

            db.Entry(studentComment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCommentExists(id))
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

        // POST: api/StudentCommentsApi
        [ResponseType(typeof(StudentComment))]
        public IHttpActionResult PostStudentComment(StudentComment studentComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentComments.Add(studentComment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentCommentExists(studentComment.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentComment.StudentId }, studentComment);
        }

        // DELETE: api/StudentCommentsApi/5
        [ResponseType(typeof(StudentComment))]
        public IHttpActionResult DeleteStudentComment(int id)
        {
            StudentComment studentComment = db.StudentComments.Find(id);
            if (studentComment == null)
            {
                return NotFound();
            }

            db.StudentComments.Remove(studentComment);
            db.SaveChanges();

            return Ok(studentComment);
        }

        [HttpGet]
        [ResponseType(typeof(StudentCommentDto))]
        public IHttpActionResult GetVoted(int studentId, int id)
        {
            StudentComment studentComment = db.StudentComments.FirstOrDefault(p => p.StudentId == studentId && p.CommentId == id);
            if (studentComment == null)
            {
                return NotFound();
            }
            StudentCommentDto dto = new StudentCommentDto()
            {
                CommentId = studentComment.CommentId,
                StudentId = studentComment.StudentId,
                Voted = studentComment.Voted
            };
            return Ok(dto);
        }

        [HttpPut]
        [ResponseType(typeof(StudentPostDto))]
        public IHttpActionResult SetVoted(int studentId, int id)
        {
            StudentComment studentComment = db.StudentComments.FirstOrDefault(p => p.StudentId == studentId && p.CommentId == id);
            if (studentComment == null)
            {
                return NotFound();
            }
            studentComment.Voted = true;
            db.SaveChanges();
            return Ok(studentComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentCommentExists(int id)
        {
            return db.StudentComments.Count(e => e.StudentId == id) > 0;
        }
    }
}