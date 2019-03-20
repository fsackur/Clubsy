using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Web.Http;
using System.Web.Http.Description;
using Clubsy.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace Clubsy.Controllers
{
    public class ClubsController : BaseController
    {
        public ActionResult Index(string searchTerm = null)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == userId)
                               .Include(u => u.Memberships)
                               .FirstOrDefault();

            var clubs = db.Clubs.OrderBy(c => c.Name)
                                .Where(r => searchTerm == null || r.Name.Contains(searchTerm))
                                .ToList();

            var model = new List<ClubViewModel>();
            foreach (var c in clubs)
            {
                var membership = user != null ? user.Memberships.FirstOrDefault(m => m.Club == c) : null;
                model.Add(new ClubViewModel
                {
                    Name = c.Name,
                    Description = c.Description,
                    IsMember = membership != null,
                    IsAdmin = membership != null && membership.IsAdmin
                });
            }

            if (Request.IsAjaxRequest())
                return PartialView("_ClubList", model);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(string name, string description)
        {
            var club = new Club()
            {
                Name = name,
                Description = description
            };

            db.Clubs.Add(club);

            var admin = new ClubMembership()
            {
                Club = club,
                User = GetCurrentUser(),
                IsAdmin = true
            };

            db.Memberships.Add(admin);

            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    var message = e.InnerException.InnerException.Message;
                    ViewBag.DbValidationMessage = message;
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize]
        public ActionResult Join(string name, int? id)
        {
            var user = GetCurrentUser();
            Club club;
            if (id != null)
                club = db.Clubs.Find(id);
            else
                club = db.Clubs.Where(c => c.Name == name).Single();

            if (!user.IsMember(club))
            {
                db.Memberships.Add(new ClubMembership()
                {
                    Club = club,
                    User = user,
                    IsAdmin = false
                });

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}