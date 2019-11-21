using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulsifiApp.Data
{
    public class AppDBInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            // creating default users
            List<Tuple<IdentityUser, string>> users = new List<Tuple<IdentityUser, string>>(){
                    new Tuple<IdentityUser, string>(
                        new IdentityUser()
                        {
                            UserName = "admin@pulsifi.com",
                            Email = "admin@pulsifi.com"
                        },
                        "Admin"),
                    new Tuple<IdentityUser, string>(
                        new IdentityUser()
                        {
                            UserName = "recruiter@pulsifi.com",
                            Email = "recruiter@pulsifi.com"
                        },
                        "Recruiter"),
                    new Tuple<IdentityUser, string>(
                        new IdentityUser()
                        {
                            UserName = "candidate@pulsifi.com",
                            Email = "candidate@pulsifi.com"
                        },
                        "Candidate"),
                };

            foreach (var user in users)
            {
                if (userManager.FindByEmailAsync(user.Item1.Email).Result == null)
                {
                    IdentityResult result = userManager.CreateAsync(user.Item1, "SomePassword1!").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user.Item1, user.Item2).Wait();
                    }
                }
            }
        }
    }
}
