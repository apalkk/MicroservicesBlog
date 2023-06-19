using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Micro.Data;
using System;
using System.Linq;

namespace Micro.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMicroContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMicroContext>>()))
        {
            // Look for any movies.
            if (context.Profile.Any())
            {
                return;   // DB has been seeded
            }
            context.Profile.AddRange(
                new Micro.Models.Profile{
                   Id = 1,
                   UserName = "User",
                   Email = "me@gmail.com",
                   Password = "1234",
                   Followers = new(),
                   JoinDate = DateTime.Now
                }
            );
            context.SaveChanges();
        }
    }
}