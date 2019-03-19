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
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public virtual ICollection<ClubMember> Members { get; set; }
        //public virtual ICollection<ClubMember> Admins {
        //    get {
        //        return this.Members.Where(m => m.IsAdmin).ToList();
        //    }
        //}
    }

    public class ClubMember
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        //[ForeignKey("Club")]
        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public int ClubId { get; set; }

        [Required]
        [StringLength(128)]
        //[Column(TypeName = "NVARCHAR")]
        //[ForeignKey("ApplicationUser")]
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public string UserId { get; set; }

        public bool IsAdmin { get; set; }
    }
}