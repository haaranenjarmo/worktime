using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTime.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkTime.Data
{
    public class WorkTimeContext : DbContext
    {
        private readonly string _userId;

        public WorkTimeContext(DbContextOptions<WorkTimeContext> options, string userId) 
            : base(options)
        {
            _userId = userId;
        }

        public DbSet<WorkHourModel> WorkHours { get; set; }
        public DbSet<FlexTimeModel> FlexTimes { get; set; }
        public DbSet<BillingRateModel> BillingRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkHourModel>().ToTable("WorkHour")
                .HasQueryFilter(i => i.UserId == _userId || _userId == null);
            modelBuilder.Entity<FlexTimeModel>().ToTable("FlexTime");
            modelBuilder.Entity<BillingRateModel>().ToTable("BillingRate")
                .Property(p => p.BillingRate).HasColumnType("decimal(3,2)");
        }
    }
}
