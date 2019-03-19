namespace Clubsy.Migrations
{
    using Clubsy.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Clubsy.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Clubsy.Models.ApplicationDbContext";
        }

        private DateTime GetNextNamedWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            var nextDay = start.AddDays(daysToAdd);
            return start.AddDays(daysToAdd);
        }

        protected override void Seed(Clubsy.Models.ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!db.Roles.Any(r => r.Name == "AppAdmin"))
            {
                roleManager.Create(new IdentityRole { Name = "AppAdmin" });
                roleManager.Create(new IdentityRole { Name = "AppUser" });
            }


            var fooUserName = "foo@foo.bar";
            var arryUserName = "arry@redkn.app";
            var chesterUserName = "chester@kg4.kxb";


            if (!userManager.Users.Any(u => u.Email == fooUserName))
            {
                userManager.Create(new ApplicationUser()
                {
                    FirstName = "foo",
                    LastName = "bar",
                    Email = "foo@foo.bar",
                    UserName = "foo@foo.bar"
                },
                "P@ssword1");
                //userManager.AddToRole(foouser.Id, "AppAdmin");
            }

            if (!userManager.Users.Any(u => u.Email == arryUserName))
            {
                userManager.Create(new ApplicationUser()
                {
                    FirstName = "Arry",
                    LastName = "Redknapp",
                    Email = "arry@redkn.app",
                    UserName = "arry@redkn.app"
                },
                "P@ssword1");
                //userManager.AddToRole(arry.Id, "AppUser");
            }

            if (!userManager.Users.Any(u => u.Email == chesterUserName))
            {
                userManager.Create(new ApplicationUser()
                {
                    FirstName = "Chester",
                    LastName = "Bishop",
                    Email = "chester@kg4.kxb",
                    UserName = "chester@kg4.kxb"
                },
                "P@ssword1");
                //userManager.AddToRole(chester.Id, "AppUser");
            }


            var norfolkenchants = new Club()
            {
                Name = "Norfolk Enchants",
                Description = "A bunch of no-hopers, desperate for a shot at the big time"
            };

            var chessbursters = new Club()
            {
                Name = "Chessbursters",
                Description = "A friendly bunch of shoe-gazers, relating better to symbolic carved figurines with highly constrained behaviour than to living organisms"
            };

            db.Clubs.Where(c => c.Name == norfolkenchants.Name || c.Name == chessbursters.Name).ForEachAsync(c => db.Clubs.Remove(c)).Wait();
            db.Clubs.AddOrUpdate(norfolkenchants);
            db.Clubs.AddOrUpdate(chessbursters);

            //var arryMembership = new ClubMember()
            //{
            //    Club = norfolkenchants,
            //    ApplicationUser = userManager.Users.First(u => u.UserName == arryUserName),
            //    IsAdmin = true
            //};
            //db.ClubMembers.AddOrUpdate(arryMembership);

            //var chesterMembership = new ClubMember()
            //{
            //    Club = chessbursters,
            //    ApplicationUser = userManager.Users.First(u => u.UserName == chesterUserName),
            //    IsAdmin = true
            //};
            //db.ClubMembers.AddOrUpdate(chesterMembership);



            var kickabout = new Event()
            {
                Club = norfolkenchants,
                Time = GetNextNamedWeekday(DateTime.Today.AddDays(1), DayOfWeek.Tuesday).AddHours(18),
                Name = "Training session",
                Description = "Sprints, keepy-uppies and shooting practice."
            };

            var league = new Event()
            {
                Club = norfolkenchants,
                Time = GetNextNamedWeekday(DateTime.Today.AddDays(1), DayOfWeek.Saturday).AddHours(13),
                Name = "High Barnet Chicken Cottage Cup",
                Description = "League match"
            };

            var knightOut = new Event()
            {
                Club = chessbursters,
                Time = GetNextNamedWeekday(DateTime.Today.AddDays(1), DayOfWeek.Thursday).AddHours(19),
                Name = "Knight Out!",
                Description = "Weekly meetup on Bishopsgate. Board provided."
            };

            db.Events.AddOrUpdate(kickabout);
            db.Events.AddOrUpdate(league);
            db.Events.AddOrUpdate(knightOut);

            

            // Uncomment this to attach a separate VS instance when migrating with Update-Databases
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
