using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Clubsy.Models;

namespace Clubsy.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            protected set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        protected ApplicationUser GetCurrentUser()
        {
            return UserManager.FindById(User.Identity.GetUserId());
        }

        //protected bool UserIsAdmin(Club club)
        //{
        //    var user = GetCurrentUser();
        //    return club.Admins.First(m => m.ApplicationUser == user) != null;
        //}
    }
}