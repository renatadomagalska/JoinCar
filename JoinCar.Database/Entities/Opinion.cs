﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JoinCar.Database.Entities
{
    [Table("Opinions")]
    public class Opinion
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
        public virtual ApplicationUser OpinionOwner { get; set; } //who created
        public virtual ApplicationUser OpinionRecipient { get; set; } //about who the opinion is
    }
}
