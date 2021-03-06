﻿using System;
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
    public class PostsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PostsApi
        public List<PostDto> GetPosts()
        {
            return db.Posts.Select(x => new PostDto
            {
                Id = x.Id,
                StudentId = x.StudentId,
                Student = x.Student.UserName,
                SubjectId = x.SubjectId,
                Subject = x.Subject.Name,
                Text = x.Text,
                Comments = db.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    Student = c.Student.UserName,
                    StudentId = c.StudentId,
                    Text = c.Text,
                    VotesDown = c.VotesDown,
                    VotesUp = c.VotesUp
                }).Where(c => c.PostId == x.Id).ToList()
            }).OrderByDescending(p => p.Id).ToList();
        }

        // GET: api/PostsApi/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = db.Posts.Include(p => p.Student).Include(p => p.Subject).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            PostDto postDto = new PostDto()
            {
                Id = post.Id,
                Student = post.Student.UserName,
                StudentId = post.StudentId,
                Subject = post.Subject.Name,
                SubjectId = post.SubjectId,
                Text = post.Text,
                Comments = db.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    Student = c.Student.UserName,
                    StudentId = c.StudentId,
                    Text = c.Text,
                    VotesDown = c.VotesDown,
                    VotesUp = c.VotesUp
                }).Where(c => c.PostId == post.Id).ToList()
            };
            return Ok(postDto);
        }

        // PUT: api/PostsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/PostsApi
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, post);
        }

        // DELETE: api/PostsApi/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = db.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            List<Comment> comments = post.Comments.ToList();
            foreach(Comment comment in comments)
            {
                List<StudentComment> scs = db.StudentComments.Where(s => s.CommentId == comment.Id).ToList();
                foreach (StudentComment sc in scs)
                {
                    db.StudentComments.Remove(sc);
                }
            }
            List<StudentPost> sps = db.StudentPosts.Where(s => s.PostId == id).ToList();
            foreach(StudentPost sp in sps)
            {
                db.StudentPosts.Remove(sp);
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return Ok(post);
        }

        // PUT: api/PostsApi/approve/5
        [HttpPut]
        [ResponseType(typeof(Post))]
        public IHttpActionResult ApprovePost(int id)
        {
            Post post = db.Posts.Find(id);
            if(post == null)
            {
                return NotFound();
            }
            post.IsApproved = true;
            db.SaveChanges();
            return Ok(post);
        }

        [HttpPut]
        public IHttpActionResult UpvotePost(int id)
        {
            Post post = db.Posts.Find(id);
            if(post == null)
            {
                return NotFound();
            }
            ++post.Votes;
            ++post.NumberOfVotes;
            db.SaveChanges();
            return Ok(post);
        }

        [HttpPut]
        public IHttpActionResult DownvotePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            --post.Votes;
            ++post.NumberOfVotes;
            db.SaveChanges();
            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }
}