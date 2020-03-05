using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioPage.Data;
using System;
using System.Linq;

namespace PortfolioPage.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.project.Any())
                {
                    return;   // DB has been seeded
                }

                context.project.AddRange(
                    new project
                    {
                        title = "Project tracker application",
                        goalDescription = "The motivation behind this project is two-fold. First, it will help me set, track and meet my future portfolio project deadlines. Second, once a project is complete, it will serve as a repository of knowledge and experience. I often find myself in situations where I encountered a problem I'm facing in the past, but either don't remember the solution I used or worse, don't remember the problem I was working on.",
                        startDate = DateTime.Parse("2020-2-14"),
                        completionDeadline = DateTime.Parse("2020-3-13"),
                        currentProjectPhase = project.projectPhase.Execution,
                        currentProjectStatus = project.projectStatus.onTrack,
                        isPublic = true
                    },

                    new project
                    {
                        title = "Podcast alarm clock",
                        goalDescription = "I am curious to test how effective an alarm clock that plays a podcast to wake me up will be. My hypothesis is that I will naturally wake up if I hear an interesting conversation. It will also help me learn to work with a mobile operating system.",
                        startDate = DateTime.Parse("2020-3-16"),
                        completionDeadline = DateTime.Parse("2020-4-10"),
                        currentProjectPhase = project.projectPhase.Initiation,
                        currentProjectStatus = project.projectStatus.onTrack,
                        isPublic = true
                    });

                context.SaveChanges();
            }
        }
    }
}