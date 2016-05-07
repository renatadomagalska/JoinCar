using System;
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
        public DateTime DateTime { get; set; }
        public int AvailableSeats { get; set; }
        public string Description { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}