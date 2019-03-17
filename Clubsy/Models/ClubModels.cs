using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Clubsy.Models
{
    public class Club
    {
        public Club ()
        {
            this.Admins = new List<ApplicationUser>();
            //this.Admins.Add(UserManager<ApplicationUser, string>.FindById(HttpContext.Current.User.Identity.GetUserId());
            
        }
        //public Club (string name, string description)
        //{
        //    this.Name = name;
        //    this.Description = description;
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        //[Required]
        //[MinLength(1)]
        public virtual ICollection<ApplicationUser> Admins { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}