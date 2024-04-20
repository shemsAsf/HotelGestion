using System;
using System.Linq;
using DBContext;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GestionHotel.Api.Data
{
    public static class DefaultUserInitializer
    {
        public static void Initialize(AuthenticationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // If there are already users in the database, don't add defaults
            if (context.Users.Any())
            {
                return;
            }

            // Add default users
            var defaultUsers = new[]
            {
                new User { UserId = 0, Username = "admin", Email = "admin@example.com", Password = "admin123", Role = Roles.Admin },
                new User { UserId = 1, Username = "client1", Email = "user1@example.com", Password = "client123", Role = Roles.Client},
                new User { UserId = 2, Username = "maid2", Email = "user2@example.com", Password = "user123", Role = Roles.Maid }
                // Add more default users as needed
            };

            // Add the default users to the database
            foreach (var user in defaultUsers)
            {
                context.Users.Add(user);
            }

            // Save changes to persist the default users in the database
            context.SaveChanges();
        }
    }
}