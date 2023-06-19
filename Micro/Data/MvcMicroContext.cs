using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Micro.Models;

namespace Micro.Data
{
    public class MvcMicroContext : DbContext
    {
        public MvcMicroContext (DbContextOptions<MvcMicroContext> options)
            : base(options)
        {
        }

        public DbSet<Micro.Models.Profile> Profile { get; set; } = default!;
    }
}
