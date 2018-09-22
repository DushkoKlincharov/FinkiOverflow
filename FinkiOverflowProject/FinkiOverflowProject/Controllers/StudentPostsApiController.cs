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
    public class StudentPostsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StudentPosts
        public IQueryable<StudentPost> GetStudentPosts()
        {
            return db.StudentPosts;
        }

        // GET: api/StudentPosts/5
        [ResponseType(typeof(StudentPost))]
        public IHttpActionResult GetStudentPost(int id)
        {
            StudentPost studentPost = db.StudentPosts.Find(id);
            if (studentPost == null)
            {
                return NotFound();
            }

            return Ok(studentPost);
        }

        // PUT: api/StudentPosts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentPost(int id, StudentPost studentPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentPost.StudentId)
            {
                return BadRequest();
            }

            db.Entry(studentPost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentPostExists(id))
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

        // POST: api/StudentPosts
        [ResponseType(typeof(StudentPost))]
        public IHttpActionResult PostStudentPost(StudentPost studentPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentPosts.Add(studentPost);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentPostExists(studentPost.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentPost.StudentId }, studentPost);
        }

        // DELETE: api/StudentPosts/5
        [ResponseType(typeof(StudentPost))]
        public IHttpActionResult DeleteStudentPost(int id)
        {
            StudentPost studentPost = db.StudentPosts.Find(id);
            if (studentPost == null)
            {
                return NotFound();
            }

            db.StudentPosts.Remove(studentPost);
            db.SaveChanges();

            return Ok(studentPost);
        }

        [HttpGet]
        [ResponseType(typeof(StudentPostDto))]
        public IHttpActionResult GetVoted(int studentId, int id)
        {
            StudentPost studentPost = db.StudentPosts.FirstOrDefault(p => p.StudentId == studentId && p.PostId == id);
            if (studentPost == null)
            {
                return NotFound();
            }
            StudentPostDto dto = new StudentPostDto()
            {
                PostId = studentPost.PostId,
                StudentId = studentPost.StudentId,
                Voted = studentPost.Voted
            };
            return Ok(dto);
        }

        [HttpPut]
        [ResponseType(typeof(StudentPostDto))]
        public IHttpActionResult SetVoted(int studentId, int id)
        {
            StudentPost studentPost = db.StudentPosts.FirstOrDefault(p => p.StudentId == studentId && p.PostId == id);
            if (studentPost == null)
            {
                return NotFound();
            }
            studentPost.Voted = true;
            db.SaveChanges();
            return Ok(studentPost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentPostExists(int id)
        {
            return db.StudentPosts.Count(e => e.StudentId == id) > 0;
        }
    }
}