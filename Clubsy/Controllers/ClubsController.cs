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
                db.Clubs.Add(club);

                var admin = new ClubMember()
                {
                    ClubId = club.Id,
                    UserId = User.Identity.GetUserId(),
                    IsAdmin = true
                };

                db.ClubMembers.Add(admin);
                
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
    }
}