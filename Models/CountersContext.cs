using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CountersContext: DbContext
    {
        public CountersContext(DbContextOptions<CountersContext> options) : base(options)
        {

        }

        public DbSet<Counters> Counters { get; set; }

        internal async Task<List<Counters>> GetCounters()
        {
            return await Counters.ToListAsync();
        }

        internal async Task SaveCounters(Counters counters)
        {
            Counters.Add(counters);
            await this.SaveChangesAsync();
        }

        internal bool CountersExists(int id)
        {
            return this.Counters.Any(e => e.Id == id);
        }

        internal Task<List<Report>> GetReport()
        {
            var report = new List<Report>();
            if (this.Counters.Any())
            {
                report.Add(GetReportValues(1));
                report.Add(GetReportValues(2));

            }
            return Task.FromResult(report);
        }

        private Report GetReportValues(int key)
        {
            var cntkey = this.Counters.Where(x => x.Key == key);
            return new Report() { 
                Key = key, 
                Count = cntkey.Count(), 
                CountMoreThenOne = cntkey.Where(x => x.Value > 1).Count()
            };
        }


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CountersContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CountersContext>>()))
            {
                if (context.Counters.Any())
                {
                    return;
                }

                context.Counters.AddRange(
                    new Counters
                    {
                        Key = 1,
                        Value = 1
                    },
                    new Counters
                    {
                        Key = 1,
                        Value = 2
                    },
                    new Counters
                    {
                        Key = 1,
                        Value = 3
                    },
                    new Counters
                    {
                        Key = 2,
                        Value = 1
                    },
                    new Counters
                    {
                        Key = 2,
                        Value = 1
                    },
                    new Counters
                    {
                        Key = 2,
                        Value = 3
                    },
                    new Counters
                    {
                        Key = 2,
                        Value = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }

}
