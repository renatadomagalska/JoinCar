using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoinCar.Database.Entities
{
    [Table("Trips")]
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        [DisplayName("Date and time")]
        public DateTime DateTime { get; set; }

        [DisplayName("Available seats")]
        public int AvailableSeats { get; set; }

        public string Description { get; set; }
        public string UserId { get; set; }

        public string JsonDirections { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; } 
    }
}