namespace FinkiOverflowProject.Migrations
{
    using FinkiOverflowProject.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinkiOverflowProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FinkiOverflowProject.Models.ApplicationDbContext context)
        {
            Subject dm1 = new Subject() { Id = 1, Name = "Дискретна математика 1", Year = 1 };
            Subject vi = new Subject() { Id = 2, Name = "Вештачка интелигенција", Year = 2 };
            Subject bp = new Subject() { Id = 3, Name = "Бази на податоци", Year = 3 };
            Subject rob = new Subject() { Id = 4, Name = "Роботика", Year = 4 };

            Student dushko = new Student() { Id = 1, FirstName = "Душко", LastName = "Клинчаров", UserName = "duklin", ImageURL = "imgurl" };
            Student branko = new Student() { Id = 2, FirstName = "Бранко", LastName = "Фурнаџиски", UserName = "brane.berovo", ImageURL = "imgurl" };
            Student david = new Student() { Id = 3, FirstName = "Давид", LastName = "Ристов", UserName = "dacho", ImageURL = "imgurl" };

            Post p1 = new Post() { Id = 1, Subject = dm1, Student = dushko, Text = "Како се поврзани матриците со системите линеарни равенки?" };
            Post p2 = new Post() { Id = 2, Subject = vi, Student = branko, Text = "Дали А* е информирано пребарување?" };
            Post p3 = new Post() { Id = 3, Subject = bp, Student = david, Text = "Што означува SELECT * FROM наредбата?" };
            Post p4 = new Post() { Id = 4, Subject = rob, Student = branko, Text = "Што означува диференцијален погон кај роботи?" };

            Comment c1 = new Comment() { Id = 1, Post = p1, Student = branko, Text = "Матрицата е скратен запис од коефициентите во линеарни равенки.", VotesDown = 1, VotesUp = 3 };
            Comment c2 = new Comment() { Id = 2, Post = p2, Student = david, Text = "Информирано е", VotesDown = 0, VotesUp = 1 };
            Comment c3 = new Comment() { Id = 3, Post = p3, Student = dushko, Text = "Селектирање на сите атрибути за сите записи во една табела", VotesDown = 2, VotesUp = 1 };
            Comment c4 = new Comment() { Id = 4, Post = p4, Student = david, Text = "Ротацијата е овозможена со разлика во брзините на тркалата поставени на иста оска", VotesDown = 1, VotesUp = 3 };


            context.Subjects.AddOrUpdate(s => s.Id, dm1, vi, bp, rob);
            context.Students.AddOrUpdate(s => s.Id, dushko, branko, david);
            context.Posts.AddOrUpdate(s => s.Id, p1, p2, p3, p4);
            context.Comments.AddOrUpdate(s => s.Id, c1, c2, c3, c4);
        }
    }
}
