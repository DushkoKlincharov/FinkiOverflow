namespace FinkiOverflowProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using FinkiOverflowProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;


    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinkiOverflowProject.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // Create roles
            var userRole = new IdentityRole { Name = "User" };
            var adminRole = new IdentityRole { Name = "Admin" };
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(userRole);
            }
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(adminRole);
            }

            // Create users
            var userTrajko = new ApplicationUser { UserName = "trajko@hotmail.com", Email = "trajko@hotmail.com" };
            var userDavid = new ApplicationUser { UserName = "david@hotmail.com", Email = "david@hotmail.com" };
            var userDushko = new ApplicationUser { UserName = "dushko@hotmail.com", Email = "dushko@hotmail.com" };
            var userBranko = new ApplicationUser { UserName = "branko@hotmail.com", Email = "branko@hotmail.com" };
            var userPetko = new ApplicationUser { UserName = "petko@hotmail.com", Email = "petko@hotmail.com" };
            var userVlado = new ApplicationUser { UserName = "vlado@hotmail.com", Email = "vlado@hotmail.com" };
            var userMartin = new ApplicationUser { UserName = "martin@hotmail.com", Email = "martin@hotmail.com" };

            // Register users
            if(userManager.FindByEmail("trajko@hotmail.com") == null)
            {
                userManager.Create(userTrajko, "Trajko123!");
                userManager.AddToRole(userTrajko.Id, "Admin");
            }
            if (userManager.FindByEmail("david@hotmail.com") == null)
            {
                userManager.Create(userDavid, "David123!");
                userManager.AddToRole(userDavid.Id, "User");
            }
            if (userManager.FindByEmail("dushko@hotmail.com") == null)
            {
                userManager.Create(userDushko, "Dushko123!");
                userManager.AddToRole(userDushko.Id, "User");
            }
            if (userManager.FindByEmail("branko@hotmail.com") == null)
            {
                userManager.Create(userBranko, "Branko123!");
                userManager.AddToRole(userBranko.Id, "User");
            }
            if (userManager.FindByEmail("petko@hotmail.com") == null)
            {
                userManager.Create(userPetko, "Petko123!");
                userManager.AddToRole(userPetko.Id, "User");
            }
            if (userManager.FindByEmail("vlado@hotmail.com") == null)
            {
                userManager.Create(userVlado, "Vlado123!");
                userManager.AddToRole(userVlado.Id, "User");
            }
            if (userManager.FindByEmail("martin@hotmail.com") == null)
            {
                userManager.Create(userMartin, "Martin123!");
                userManager.AddToRole(userMartin.Id, "User");
            }

            //Create modules
            Module m1 = new Module() { Name = "КНИ" };
            Module m2 = new Module() { Name = "ПЕТ" };
            Module m3 = new Module() { Name = "АСИ" };

            // Create Students
            Student trajko = new Student()
            {
                Id = 1,
                FirstName = "Trajko",
                LastName = "Trajkoski",
                City = "Skopje",
                ImageURL = "https://www.gravatar.com/avatar/57771de8c908c0aeb3cb182c52e0aea8?s=328&d=identicon&r=PG&f=1",
                UserId = userManager.FindByEmail("trajko@hotmail.com").Id,
                UserName = "trajko@hotmail.com"
            };

            Student david = new Student()
            {
                Id = 2,
                FirstName = "David",
                LastName = "Ristov",
                City = "Radovis",
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                ImageURL = "https://www.gravatar.com/avatar/818090f57c4ff3a6acffb0031b8f0453?s=328&d=identicon&r=PG&f=1",
                UserId = userManager.FindByEmail("david@hotmail.com").Id,
                Module = m1,
                UserName = "david@hotmail.com"
            };

            Student dushko = new Student()
            {
                Id = 3,
                FirstName = "Dushko",
                LastName = "Klincharov",
                City = "Kavadarci",
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                ImageURL = "https://www.gravatar.com/avatar/51b9471856e69245b677ddc4bb1423f2?s=328&d=identicon&r=PG&f=1",
                UserId = userManager.FindByEmail("dushko@hotmail.com").Id,
                Module = m1,
                UserName = "dushko@hotmail.com"
            };

            Student branko = new Student()
            {
                Id = 4,
                FirstName = "Branko",
                LastName = "Furnadjiski",
                City = "Berovo",
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                ImageURL = "https://www.gravatar.com/avatar/1b31bc4725aa5b89c228cb8e64fe2c4a?s=328&d=identicon&r=PG&f=1",
                UserId = userManager.FindByEmail("branko@hotmail.com").Id,
                Module = m1,
                UserName = "branko@hotmail.com"
            };

            Student petko = new Student()
            {
                Id = 5,
                FirstName = "Petko",
                LastName = "Petkoski",
                City = "Tetovo",
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                ImageURL = "https://www.gravatar.com/avatar/c8efebfd2751d74903979780f4ed57bd?s=328&d=identicon&r=PG&f=1",
                UserId = userManager.FindByEmail("petko@hotmail.com").Id,
                Module = m2,
                UserName = "petko@hotmail.com"
            };

            Student vlado = new Student()
            {
                Id = 6,
                FirstName = "Vlado",
                LastName = "Vladevski",
                City = "Ohrid",
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                ImageURL = "https://www.gravatar.com/avatar/9fcb5111d5310af2745c203b23e4cb03?s=328&d=identicon&r=PG&f=1",
                UserId = userManager.FindByEmail("vlado@hotmail.com").Id,
                Module = m3,
                UserName = "vlado@hotmail.com"
            };

            Student martin = new Student()
            {
                Id = 7,
                FirstName = "Martin",
                LastName = "Georgievski",
                City = "Kocani",
                Description = "I'm a web developer and a Student. Eager to learn new things and creating good code.",
                ImageURL = "https://www.gravatar.com/avatar/2516d9e60e239fc45554d6d433432444?s=328&d=identicon&r=PG",
                UserId = userManager.FindByEmail("martin@hotmail.com").Id,
                Module = m3,
                UserName = "martin@hotmail.com"
            };

            // Create subjects
            Subject dm1 = new Subject() { Id = 1, Name = "Дискретна математика 1", Year = 1, Description = "Елементи од математичка логика: искази и логички операции, таблици на вистинитост и исказни формули, тавтологии и позначајни логички закони,нормални форми, исказни функции и квантификатори, правила на изведување на залучоци.Вовед во математички докази: техники на докажување. Множества, подмножества, операции со множества и нивни својства. Пресликувања(функции): дефиниција и видови пресликувања, композиција на пресликувања, инверзно пресликување.Низи и редови.Кардиналност и преброивост.Цели броеви и деливост. Индукција и рекурзија: математичка индукција и принцип на строга математичка индукција. Аритметичка и геометриска прогресија. Рекурентни дефиниции и структурна индукција: рекурентни функции, Фибоначиеви броеви, рекурентно дефинирани множества и структури, обопштена индукција.Релации: релации и нивни својства, претставување на релации со графови и матрици, затворање на релации.Релации: релации за еквивалентност и разбивања, релации за подредување, подредени множества, мрежи." };
            Subject vi = new Subject() { Id = 2, Name = "Вештачка интелигенција", Year = 2, Description = "Способност да се одреди кои проблеми се решаваат со примена на методите на интелигентните системи, да се одреди најсоодветниот начин за претставување на соодветниот проблем и проблемот оптимално да се реши.Што се интелигентните системи? Интелигентни агенти; Неинформирано пребарување; Информирано пребарување; Спротивставено пребарување; Напредно пребарување; Логички агенти; Предикатно сметање; Претставување на знаењето; Расудување во услови на несигурност; Агенти способни да учат; Машинско учење; Генетски алгоритми и невронски мрежи; Комуникација меѓу агентите." };
            Subject bp = new Subject() { Id = 3, Name = "Бази на податоци", Year = 3, Description = "Вовед, историски развој, oсновни концепти на системи на бази податоци, споредба на процесирање датотеки и бази податоци.Управувачки софтвери на база податоци(DBMS) и архитектури, податочна независност.Модел на реалниот свет, семантичко моделирање: модел на ентитети и релации(Е - R модел), проширен модел на ентитети и релации(ЕЕ - R модел), UML oбјектен модел(класен дијаграм).Релациски модел на бази на податоци, ограничувања на интегритет, логичка и физичка организација. Дизајн на релациски бази податоци, трансформација на ЕЕ - R модел во релациски модел. Формални прашални јазици: релациска алгебра и релациско сметање.Прашални јазици(SQL), ограничувања, тригери, складирани процедури, индекси, аналитички прашалници.Функционални, клучни, јоин и повеќевредносни зависности. Нормални форми: прва, втора, трета, Boyce - Codd - ова, четврта и пета нормална форма.Процес на нормализација.Tрансакциско управување и конторла на конкурентност. Развој на апликации на бази податоци, вметнување на прашални јазици во програмските јазици, поврзување со базата." };
            Subject rob = new Subject() { Id = 4, Name = "Роботика", Year = 4, Description = @"Дефиниција на поимот “робот” и роботика како дел од вештачката интелигенција. Објаснување на односот помеѓу индустриската роботика и “интелигентната роботика.Запознавање со сензорски и актуаторски делови од роботскиот систем.Интердисциплинарни аспекти на роботика(вовед во механика на роботи).Од сигналите од сензорите до високонивовски симболи.Роботите како отелотворени системи на вештачката интелигенција. Моделирање на однесување на роботски системи.Запознавање со концептите: состојба, место и навигација. Вовед во повеќе - роботски системи." };
            Subject it = new Subject() { Id = 5, Name = "Интернет технологии", Year = 3, Description = "Платформи за развој на серверски веб апликации. Развојни околини за веб апликации. Управување со состојба кај веб апликации. Без состојбени веб апликации. Чување на состојбата кај веб апликаци: сесија, view state, колачиња, query string, глобална сесија. Контроли за приказ на податоци. Пристап до податоци кај веб апликации. Веб сервиси. Креирање на веб севриси. Состојба кај веб серевисите. SOAP веб сервиси. REST сервиси. Креирање на апликации со користење на веб сервиси. Користење на јавни веб сервиси." };
            Subject mpip = new Subject() { Id = 6, Name = "Мобилни платформи и програмирање", Year = 3, Description = "Мобилни оперативни системи. Нативни апликации и мобилни веб апликации, концепциски разлики и пристап за развој.Концепти на развој на мобилни апликации, со осврт на разликите што ги носи мобилноста. Мобилни инфрастуктури, разлика помеѓу мобилно и безжично. Карактеристики на мобилни апликации (мултимодална интеракција, повеќе комуникациски канали, инфрастурни ограничувања). Кориснички интерфејси и интеракција кај мобилните апликации. Каратеристики на мобилен корисник (неможност од фокус, разлики кои потекнуваат од различни култури). Кориснички центрирани методи и алатки за дизајнирање на мобилни апликации. Развојни платформи и технологии." };


            // Create posts
            Post p1 = new Post()
            {
                Id = 1,
                Subject = bp,
                Student = branko,
                Text = "Also how do LEFT JOIN, RIGHT JOIN and FULL JOIN fit in?"
            ,
                Title = "What is the difference between INNER JOIN and OUTER JOIN ?",
                TimeAsked = DateTime.Now,
                IsApproved = true
            };
            Post p2 = new Post()
            {
                Id = 2,
                Subject = it,
                Student = dushko,
                Text = @"Every time a user posts something containing < or > in a page in my web application, I get this exception thrown.

                I don't want to go into the discussion about the smartness of throwing an exception or crashing an entire web application because somebody entered a character in a text box, but I am looking for an elegant way to handle this.

                Trapping the exception and showing

                An error has occurred please go back and re-type your entire form again, but this time please do not use <

                doesn't seem professional enough to me.

                Disabling post validation (validateRequest=false) will definitely avoid this error, but it will leave the page vulnerable to a number of attacks.

                Ideally: When a post back occurs containing HTML restricted characters, that posted value in the Form collection will be automatically HTML encoded. So the .Text property of my text - box will be something & lt; html & gt;

                Is there a way I can do this from a handler? "
                ,
                    Title = "A potentially dangerous Request.Form value was detected from the client",
                    TimeAsked = DateTime.Now,
                    IsApproved = true
            };
            Post p3 = new Post()
            {
                Id = 3,
                Subject = mpip,
                Student = david,
                Text = @"I have an EditText and a Button in my layout. After writing in the edit field and clicking on the Button, I want to hide the virtual keyboard. I assume that this is a simple piece of code, but where can I find an example of it?",
                Title = "Close/hide the Android Soft Keyboard",
                TimeAsked = DateTime.Now,
                IsApproved = true
            };
          

            // Create comments
            Comment c1 = new Comment() { Id = 1, Post = p1, Student = dushko, Text = @"Both inner and outer joins are used to combine rows from two or more tables into a single result.  This is done using a join condition.  The join condition specifies how columns from each table are matched to one another.  In most cases the aim is to find equal values between tables, and include those matches.

            The most common case for this is when you’re matching the foreign key of one table to the primary key of another, such as when using and ID to lookup a value.

            Though both inner and outer joins include rows from both tables when the match condition is successful, they differ in how they handle a false match condition.

            Inner joins don’t include non-matching rows; whereas, outer joins do include them.", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };
            Comment c2 = new Comment() { Id = 2, Post = p1, Student = petko, Text = @"In SQL, a join is used to compare and combine — literally join — and return specific rows of data from two or more tables in a database. An inner join finds and returns matching data from tables, while an outer join finds and returns matching data and some dissimilar data from tables.", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };
            Comment c3 = new Comment() { Id = 3, Post = p1, Student = david, Text = @"(INNER) JOIN retains only those rows for which there was a match in both tables
            LEFT (OUTER) JOIN will retain those rows for which there was a match in both tables, plus it will retain those rows that appeared only in the LEFT table
            RIGHT (OUTER) JOIN will retain those rows for which there was a match in both tables, plus it will retain those rows that appeared only in the RIGHT table
            FULL OUTER JOIN will retain those rows for which there was a match in both tables, plus it will retain those rows that appeared only in either the LEFT or the RIGHT table", VotesDown = 0, VotesUp = 2, TimeAnswered = DateTime.Now };
            Comment c4 = new Comment() { Id = 4, Post = p2, Student = david, Text = @"I think you are attacking it from the wrong angle by trying to encode all posted data.
            Note that a  <  could also come from other outside sources, like a database field, a configuration, a file, a feed and so on.
            Furthermore,< is not inherently dangerous.It's only dangerous in a specific context: when writing strings that haven't been encoded to HTML output(because of XSS).
            In other contexts different sub - strings are dangerous, for example, if you write an user - provided URL into a link, the sub - string javascript: may be dangerous.The single quote character on the other hand is dangerous when interpolating strings in SQL queries, but perfectly safe if it is a part of a name submitted from a form or read from a database field.
            The bottom line is: you can't filter random input for dangerous characters, because any character may be dangerous under the right circumstances. You should encode at the point where some specific characters may become dangerous because they cross into a different sub-language where they have special meaning. When you write a string to HTML, you should encode characters that have special meaning in HTML, using Server.HtmlEncode. If you pass a string to a dynamic SQL statement, you should encode different characters (or better, let the framework do it for you by using prepared statements or the like)..", VotesDown = 1, VotesUp = 6, TimeAnswered = DateTime.Now };
            Comment c5 = new Comment() { Id = 5, Post = p2, Student = petko, Text = @"In ASP.NET MVC (starting in version 3), you can add the AllowHtml attribute to a property on your model.
            It allows a request to include HTML markup during model binding by skipping request validation for the property.", VotesDown = 3, VotesUp = 2, TimeAnswered = DateTime.Now };
            Comment c6 = new Comment() { Id = 6, Post = p2, Student = vlado, Text = @"You can HTML encode text box content, but unfortunately that won't stop the exception from happening. In my experience there is no way around, and you have to disable page validation. By doing that you're saying: I'll be careful, I promise.", VotesDown = 1, VotesUp = 2, TimeAnswered = DateTime.Now };
            Comment c7 = new Comment() { Id = 7, Post = p3, Student = martin, Text = @"To help clarify this madness, I'd like to begin by apologizing on behalf of all Android users for Google's downright ridiculous treatment of the soft keyboard. The reason there are so many answers, each different, for the same simple question is because this API, like many others in Android, is horribly designed. I can think of no polite way to state it.
            I want to hide the keyboard. I expect to provide Android with the following statement: Keyboard.hide(). The end. Thank you very much. But Android has a problem. You must use the InputMethodManager to hide the keyboard. OK, fine, this is Android's API to the keyboard. BUT! You are required to have a Context in order to get access to the IMM. Now we have a problem. I may want to hide the keyboard from a static or utility class that has no use or need for any Context. or And FAR worse, the IMM requires that you specify what View (or even worse, what Window) you want to hide the keyboard FROM.
            This is what makes hiding the keyboard so challenging. Dear Google: When I'm looking up the recipe for a cake, there is no RecipeProvider on Earth that would refuse to provide me with the recipe unless I first answer WHO the cake will be eaten by AND where it will be eaten!!
            This sad story ends with the ugly truth: to hide the Android keyboard, you will be required to provide 2 forms of identification: a Context and either a View or a Window.", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };
            Comment c8 = new Comment() { Id = 8, Post = p3, Student = branko, Text = @"You can force Android to hide the virtual keyboard using the InputMethodManager, calling hideSoftInputFromWindow, passing in the token of the window containing your focused view.", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };
            Comment c9 = new Comment() { Id = 9, Post = p3, Student = dushko, Text = @"I'm using a custom keyboard to input an Hex number so I can't have the IMM keyboard show up...
            In v3.2.4_r1 setSoftInputShownOnFocus(boolean show) was added to control weather or not to display the keyboard when a TextView gets focus, but its still hidden so reflection must be used", VotesDown = 1, VotesUp = 3, TimeAnswered = DateTime.Now };

            List<Student> students = new List<Student>() { dushko, branko, david, trajko, petko, vlado, martin };
            List<Post> posts = new List<Post>() { p1, p2, p3 };
            List<Comment> comments = new List<Comment>() { c1, c2, c3, c4, c5, c6, c7, c8, c9 };
            List<StudentComment> studentsComments = new List<StudentComment>();
            List<StudentPost> studentPosts = new List<StudentPost>();

            //Create student posts
            foreach(Student student in students)
            {
                foreach(Post post in posts)
                {
                    studentPosts.Add(new StudentPost() { Student = student, Post = post });
                }
            }

            //Create student comments
            foreach(Student student in students)
            {
                foreach(Comment comment in comments)
                {
                    studentsComments.Add(new StudentComment() { Student = student, Comment = comment });
                }
            }

            context.Subjects.AddOrUpdate(s => s.Id, dm1, vi, bp, rob, it, mpip);
            context.Students.AddOrUpdate(s => s.Id, dushko, branko, david, trajko, petko, vlado, martin);
            context.Posts.AddOrUpdate(s => s.Id, p1, p2, p3);
            context.Comments.AddOrUpdate(s => s.Id, c1, c2, c3, c4, c5, c6, c7, c8, c9);
            foreach(StudentPost sp in studentPosts)
            {
                context.StudentPosts.AddOrUpdate(s => new { s.StudentId, s.PostId }, sp);
            }
            foreach (StudentComment sc in studentsComments)
            {
                context.StudentComments.AddOrUpdate(s => new { s.StudentId, s.CommentId }, sc);
            }
            context.Modules.AddOrUpdate(s => s.Id, m1, m2, m3);
        }
    }
}
