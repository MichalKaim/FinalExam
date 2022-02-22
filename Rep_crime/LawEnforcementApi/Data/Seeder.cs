﻿using LawEnforcementApi.Model;
using LawEnforcementApi.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Data
{
    public class Seeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
            {
                if (context.LawEnforcements.Any())
                {
                    return;
                }

                context.LawEnforcements.Add(
                    new LawEnforcement
                    {
                        Id = 1,
                        Rank = Rank.first,
                        Events = null
                    });

                context.LawEnforcements.Add(
                    new LawEnforcement
                    {
                        Id = 2,
                        Rank = Rank.second,
                        Events = null
                    });
                context.LawEnforcements.Add(
                    new LawEnforcement
                    {
                        Id = 3,
                        Rank = Rank.third,
                        Events = null
                    });

                context.SaveChanges();
            }
        }
    }
}
