using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinCar.Database.Entities
{
    [Table("Interests")]
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }   
        public virtual Trip Trip { get; set; }
    }
}
