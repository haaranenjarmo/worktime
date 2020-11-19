using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Http;

namespace WorkTime.Data
{
    public class WorkTimeContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;
        public WorkTimeContext(DbContextOptions<WorkTimeContext> options, IHttpContextAccessor httpContextAccessor) //string userId 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _userId = (_httpContextAccessor.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value);
        }

        public DbSet<WorkHourModel> WorkHours { get; set; }
        public DbSet<FlexTimeModel> FlexTimes { get; set; }
        public DbSet<BillingRateModel> BillingRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkHourModel>().ToTable("WorkHour")
                .HasQueryFilter(i => i.UserId == _userId || _userId == null);

            modelBuilder.Entity<FlexTimeModel>().ToTable("FlexTime")
                .HasQueryFilter(i => i.UserId == _userId || _userId == null);
            
            modelBuilder.Entity<BillingRateModel>().ToTable("BillingRate")
                .HasQueryFilter(i => i.UserId == _userId || _userId == null)
                .Property(p => p.BillingRate).HasColumnType("decimal(3,2)");
        }
    }
}
