using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Clubsy.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Club Club { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public virtual IEnumerable<ApplicationUser> Attendees { get; set; }
    }

    public class EventSignup
    {
        public EventSignup(Event _event, ApplicationUser user)
        {
            this.EventId = _event.Id;
            this.UserId = user.Id;
            this.Event = _event;
            this.User = user;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public int EventId { get; }

        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public string UserId { get; }

        public virtual Event Event { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}