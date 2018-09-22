namespace FinkiOverflowProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FinkiOverflowProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinkiOverflowProject.Models.ApplicationDbContext context)
        {
            Subject dm1 = new Subject() { Id = 1, Name = "Дискретна математика 1", Year = 1, Description = "JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, weakly-typed language used for both client-" };
            Subject vi = new Subject() { Id = 2, Name = "Вештачка интелигенција", Year = 2, Description = "JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, weakly-typed language used for both client-" };
            Subject bp = new Subject() { Id = 3, Name = "Бази на податоци", Year = 3, Description = "JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, weakly-typed language used for both client-" };
            Subject rob = new Subject() { Id = 4, Name = "Роботика", Year = 4, Description = "JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, weakly-typed language used for both client-" };

            Module m1 = new Module() { Name = "КНИ" };
            Module m2 = new Module() { Name = "ПЕТ" };
            Module m3 = new Module() { Name = "АСИ" };

            Student dushko = new Student() { Id = 1, FirstName = "Душко", LastName = "Клинчаров", UserName = "duklin", ImageURL = "imgurl", City = "Кавадарци, МК"
            , Module = m1, Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
            Skills = "HTML/CSS,Javascript/Jquery,Ajax/Json,PHP,SQL"
            };
            Student branko = new Student() { Id = 2, FirstName = "Бранко", LastName = "Фурнаџиски", UserName = "brane.berovo", ImageURL = "imgurl", City = "Берово, МК"
            ,
                Module = m1,
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                Skills = "Machine learning,Image processing,Ajax/Json,PHP,SQL"
            };
            Student david = new Student() { Id = 3, FirstName = "Давид", LastName = "Ристов", UserName = "dacho", ImageURL = "imgurl", City = "Радовиш, МК"
            ,
                Module = m1,
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                Skills = "Spring/Angular,Django,Swift/Android,PHP,HTML/CSS"
            };

            Post p1 = new Post()
            {
                Id = 1,
                Subject = dm1,
                Student = dushko,
                Text = "Како се поврзани матриците со системите линеарни равенки?"
            ,
                Title = "Релацијата помеѓу матриците и линеарните равенки",
                TimeAsked = DateTime.Now
            };
            Post p2 = new Post()
            {
                Id = 2,
                Subject = vi,
                Student = branko,
                Text = "Дали А* е информирано пребарување?"
            ,
                Title = "Алгоритми за пребарување",
                TimeAsked = DateTime.Now.AddMinutes(5)
            };
            Post p3 = new Post()
            {
                Id = 3,
                Subject = bp,
                Student = david,
                Text = "Што означува SELECT * FROM наредбата?",
                Title = "Наредби кај SQL",
                TimeAsked = DateTime.Now.AddMinutes(2)
            };
            Post p4 = new Post()
            {
                Id = 4,
                Subject = rob,
                Student = branko,
                Text = "Што означува диференцијален погон кај роботи?",
                Title = "Моторни движења во роботика",
                TimeAsked = DateTime.Now.AddMinutes(3)
            };

            Comment c1 = new Comment() { Id = 1, Post = p1, Student = branko, Text = "Матрицата е скратен запис од коефициентите во линеарни равенки.", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };
            Comment c2 = new Comment() { Id = 2, Post = p2, Student = david, Text = "Информирано е", VotesDown = 0, VotesUp = 1, TimeAnswered = DateTime.Now };
            Comment c3 = new Comment() { Id = 3, Post = p3, Student = dushko, Text = "Селектирање на сите атрибути за сите записи во една табела", VotesDown = 2, VotesUp = 1, TimeAnswered = DateTime.Now };
            Comment c4 = new Comment() { Id = 4, Post = p4, Student = david, Text = "Ротацијата е овозможена со разлика во брзините на тркалата поставени на иста оска", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };

            StudentPost sp1 = new StudentPost() { Student = dushko, Post = p1};
            StudentPost sp2 = new StudentPost() { Student = branko, Post = p2 };
            StudentPost sp3 = new StudentPost() { Student = david, Post = p3 };
            StudentPost sp4 = new StudentPost() { Student = branko, Post = p4 };

            StudentComment sc1 = new StudentComment() { Student = branko, Comment = c1};
            StudentComment sc2 = new StudentComment() { Student = david, Comment = c2 };
            StudentComment sc3 = new StudentComment() { Student = dushko, Comment = c3 };
            StudentComment sc4 = new StudentComment() { Student = david, Comment = c4 };

            context.Subjects.AddOrUpdate(s => s.Id, dm1, vi, bp, rob);
            context.Students.AddOrUpdate(s => s.Id, dushko, branko, david);
            context.Posts.AddOrUpdate(s => s.Id, p1, p2, p3, p4);
            context.Comments.AddOrUpdate(s => s.Id, c1, c2, c3, c4);
            context.StudentPosts.AddOrUpdate(s => new { s.StudentId, s.PostId }, sp1, sp2, sp3, sp4);
            context.StudentComments.AddOrUpdate(s => new { s.StudentId, s.CommentId }, sc1, sc2, sc3, sc4);
            context.Modules.AddOrUpdate(s => s.Id, m1, m2, m3);
        }
    }
}
