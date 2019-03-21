namespace Clubsy.Models
{
    using Microsoft.AspNet.Identity;
    using PagedList;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;

    public class ClubViewModelPagedList : PagedList<Club>, IPagedList<ClubViewModel>
    {
        public ClubViewModelPagedList(IQueryable<Club> source, int pageNumber, int pageSize, ApplicationUser user) :
            base (source, pageNumber, pageSize)
        {
            Subset.AddRange(source.Skip((pageNumber -1) * pageSize)
                                .Take(pageSize)
                                .ToList()
                                .Select<Club, ClubViewModel>(c => {
                                    var membership = user != null ? user.Memberships.FirstOrDefault(m => m.Club == c) : null;
                                    return new ClubViewModel
                                    {
                                        Name = c.Name,
                                        Description = c.Description,
                                        IsMember = membership != null,
                                        IsAdmin = membership != null && membership.IsAdmin
                                    };
                                })
                                );
        }
        protected new List<ClubViewModel> Subset = new List<ClubViewModel>();
        public new ClubViewModel this[int index] { get { return Subset[index]; } }
        public new IEnumerator<ClubViewModel> GetEnumerator()
        {
            return Subset.GetEnumerator();
        }
    }

    public static class ClubViewModelHelper
    {
        public static IPagedList<ClubViewModel> ToClubViewModelPagedList(this IQueryable<Club> source, int pageNumber, int pageSize, ApplicationUser user)
        {
            return (IPagedList<ClubViewModel>) new ClubViewModelPagedList(source, pageNumber, pageSize, user);
        }
    }

    public class ClubViewModel
    {
        //public ClubViewModel (ApplicationDbContext db)
        //{
        //    ClubsWhereAdmin = new List<Club>();
        //    ClubsWhereMember = new List<Club>();
        //    ClubsWhereNotMember = new List<Club>();

        //    var userId = HttpContext.Current.User.Identity.GetUserId();
        //    var user = db.Users.Where(u => u.Id == userId)
        //                       .Include(u => u.Memberships)
        //                       .Single();

        //    db.Clubs.ForEachAsync(c =>
        //    {
        //        var membership = user.Memberships.FirstOrDefault(m => m.Club == c);
        //        if (membership == null)
        //            ClubsWhereNotMember.Add(c);
        //        else if (membership.IsAdmin)
        //            ClubsWhereAdmin.Add(c);
        //        else
        //            ClubsWhereMember.Add(c);

        //    });

        //}

        //public ICollection<Club> ClubsWhereAdmin { get; set; }
        //public ICollection<Club> ClubsWhereMember { get; set; }
        //public ICollection<Club> ClubsWhereNotMember { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMember { get; set; }
    }
}