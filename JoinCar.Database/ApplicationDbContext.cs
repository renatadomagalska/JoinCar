﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JoinCar.Database.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        } 

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Interest> Interets { get; set; }
        public IDbSet<Opinion> Opinions { get; set; }
        public IDbSet<Trip> Trips { get; set; }
    }
}