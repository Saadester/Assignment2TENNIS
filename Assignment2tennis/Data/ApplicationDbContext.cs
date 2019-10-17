using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment2tennis.Models;

namespace Assignment2tennis.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assignment2tennis.Models.Coach> Coach { get; set; }
        public DbSet<Assignment2tennis.Models.Schedule> Schedule { get; set; }
        public DbSet<Assignment2tennis.Models.ScheduleMembers> ScheduleMembers { get; set; }
    }
}
