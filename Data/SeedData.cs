using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioPage.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace PortfolioPage.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var creatingUserID = await EnsureUser(serviceProvider, testUserPw, "RoshanJM", "me@roshan.page");

                SeedDB(context, creatingUserID);
            }
        }
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName, string Email)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser {
                    UserName = UserName,
                    Email = Email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }
        public static void SeedDB(ApplicationDbContext context, string creatingUserID)
        {
            if (context.project.Any())
            {
                return;   // DB has been seeded
            }

            context.project.AddRange(
                new project
                {
                    ID = 1,
                    title = "Project tracker application",
                    creatingUserID = creatingUserID,
                    creationDate = DateTime.Now,
                    goalDescription = "The motivation behind this project is two-fold. First, it will help me set, track and meet my future portfolio project deadlines. Second, once a project is complete, it will serve as a repository of knowledge and experience. I often find myself in situations where I encountered a problem I'm facing in the past, but either don't remember the solution I used or worse, don't remember the problem I was working on.",
                    startDate = DateTime.Parse("2020-2-14"),
                    completionDeadline = DateTime.Parse("2020-3-13"),
                    currentProjectPhase = project.projectPhase.Execution,
                    currentProjectStatus = project.projectStatus.onTrack,
                    isPublic = true
                },

                new project
                {
                    ID = 2,
                    title = "Podcast alarm clock",
                    creatingUserID = creatingUserID,
                    creationDate = DateTime.Now,
                    goalDescription = "I am curious to test how effective an alarm clock that plays a podcast to wake me up will be. My hypothesis is that I will naturally wake up if I hear an interesting conversation. It will also help me learn to work with a mobile operating system.",
                    startDate = DateTime.Parse("2020-3-16"),
                    completionDeadline = DateTime.Parse("2020-4-10"),
                    currentProjectPhase = project.projectPhase.Initiation,
                    currentProjectStatus = project.projectStatus.onTrack,
                    isPublic = true
                });
            
            context.projectComponent.AddRange(
                new projectComponent{
                    ID = 1,
                    title = "Research project management strategies",
                    creatingUserID = creatingUserID,
                    creationDate = DateTime.Now,
                    componentDescription = "Create a list of requirements for the project management system",
                    startDate = DateTime.Parse("2020-3-16"),
                    completionDeadline = DateTime.Parse("2020-4-10"),
                    componentType = projectComponent.componentTypeEnum.Research,
                    componentStatus = projectComponent.componentStatusEnum.OnTrack, 
                    nodeDepth = 0,
                    projectID = 1
                },
                new projectComponent{
                    ID = 2,
                    title = "Research the Agile framework",
                    creatingUserID = creatingUserID,
                    creationDate = DateTime.Now,
                    componentDescription = "See if we can incorporate elements of Agile into the app's functionality",
                    startDate = DateTime.Parse("2020-3-16"),
                    completionDeadline = DateTime.Parse("2020-4-10"),
                    componentType = projectComponent.componentTypeEnum.Research,
                    componentStatus = projectComponent.componentStatusEnum.OnTrack, 
                    nodeDepth = 1,
                    projectComponentID = 1,
                    projectID = 1
                },
                new projectComponent{
                    ID = 3,
                    title = "Research SCRUM",
                    creatingUserID = creatingUserID,
                    creationDate = DateTime.Now,
                    componentDescription = "See if we can incorporate elements of this framework into the app's functionality",
                    startDate = DateTime.Parse("2020-3-16"),
                    completionDeadline = DateTime.Parse("2020-4-10"),
                    componentType = projectComponent.componentTypeEnum.Research,
                    componentStatus = projectComponent.componentStatusEnum.OnHold, 
                    nodeDepth = 1,
                    projectComponentID = 1,
                    projectID = 1
                }
                );

            context.SaveChanges();
        }
            
        
    }
}