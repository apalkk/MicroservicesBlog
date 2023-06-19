using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FollowAPI.Models;

    public class FollowContext : DbContext
    {
        public FollowContext (DbContextOptions<FollowContext> options)
            : base(options)
        {
        }

        public DbSet<FollowAPI.Models.Follow> Follow { get; set; } = default!;
    }
