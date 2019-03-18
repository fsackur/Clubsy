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

namespace Clubsy.Controllers
{
    public class ClubsController : BaseController
    {
        public ActionResult Index()
        {
            var model = db.Clubs.ToList();

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
        public ActionResult Create(Club club)
        {
            if (ModelState.IsValid)
            {
                //var admin = new ClubMember()
                //{
                //    ClubId = club.Id,
                //    UserId = User.Identity.GetUserId(),
                //    IsAdmin = true
                //};
                //club.Members.Add(admin);
                //club.Admins.Add(admin);

                //db.Clubs.Add(club);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}