using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }

    public class ClubMember
    {
        [Key]
        public int Id { get; set; }

        public bool IsAdmin { get; set; }

        public virtual Club Club { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}