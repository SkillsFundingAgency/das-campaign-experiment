using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.Experiments.Application.Domain.Models
{
    public class ExperimentsContext : DbContext
    {
        public ExperimentsContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<EventData> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventData>().ToTable("CitizenApplicationEvents");

        }
    }
}
