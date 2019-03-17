using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Clubsy.Models
{
    public class Club
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [MinLength(1)]
        public virtual IEnumerable<ApplicationUser> Admins { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }
    }
}