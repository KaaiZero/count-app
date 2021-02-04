using Count.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Seeder
{
    public interface IRoleSeeder
    {
        Task SeedAsync(CountDbContext dbContext, IServiceProvider serviceProvider);
    }
}
