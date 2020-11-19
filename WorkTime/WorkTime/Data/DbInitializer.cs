using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkTime.Models;

namespace WorkTime.Data
{
    public class DbInitializer
    {
        public static void Initialize(WorkTimeContext context)
        {
            context.Database.EnsureCreated();

            //Look for any employees
            if (context.WorkHours.Any())
            {
                return; // DB Has been seed
            }

            var workHours = new WorkHourModel[]
            {
                new WorkHourModel{
                    UserId="606e998a-7d8f-494f-a46d-f141ce1e134c", 
                    Billable=true, 
                    StartTime=DateTime.UtcNow.AddDays(-1),
                    EndTime=DateTime.UtcNow.AddDays(-1).AddHours(3),
                    WorkedTime=3,
                    CreatedAt=DateTime.UtcNow.AddDays(-1),
                    UpdatedAt=DateTime.UtcNow.AddDays(-1).AddHours(3),
                    UpdatedBy="606e998a-7d8f-494f-a46d-f141ce1e134c",
                    ProjectId="1234567",
                    Note="This is data seed test note"

                },
                new WorkHourModel{
                    UserId="606e998a-7d8f-494f-a46d-f141ce1e134c",
                    Billable=true,
                    StartTime=DateTime.UtcNow.AddDays(-2),
                    EndTime=DateTime.UtcNow.AddDays(-2).AddHours(4),
                    WorkedTime=4,
                    CreatedAt=DateTime.UtcNow.AddDays(-2),
                    UpdatedAt=DateTime.UtcNow.AddDays(-2).AddHours(4),
                    UpdatedBy="606e998a-7d8f-494f-a46d-f141ce1e134c",
                    ProjectId="7654321",
                    Note="This is data seed test note 2"

                },
                new WorkHourModel{
                    UserId="0f513b0f-36eb-4b6c-92dd-e19ec06cdf4c",
                    Billable=true,
                    StartTime=DateTime.UtcNow.AddDays(-1),
                    EndTime=DateTime.UtcNow.AddDays(-1).AddHours(10),
                    WorkedTime=10,
                    CreatedAt=DateTime.UtcNow.AddDays(-1),
                    UpdatedAt=DateTime.UtcNow.AddDays(-1).AddHours(10),
                    UpdatedBy="0f513b0f-36eb-4b6c-92dd-e19ec06cdf4c",
                    ProjectId="123467",
                    Note="This is data seed test note"

                },
                new WorkHourModel{
                    UserId="0f513b0f-36eb-4b6c-92dd-e19ec06cdf4c",
                    Billable=true,
                    StartTime=DateTime.UtcNow.AddDays(-2),
                    EndTime=DateTime.UtcNow.AddDays(-2).AddHours(12),
                    WorkedTime=12,
                    CreatedAt=DateTime.UtcNow.AddDays(-2),
                    UpdatedAt=DateTime.UtcNow.AddDays(-2).AddHours(12),
                    UpdatedBy="0f513b0f-36eb-4b6c-92dd-e19ec06cdf4c",
                    ProjectId="123467",
                    Note="This is data seed test note"

                }
            };
            foreach (WorkHourModel w in workHours)
            {
                context.WorkHours.Add(w);
            }
            context.SaveChanges();
        }
    }
}
