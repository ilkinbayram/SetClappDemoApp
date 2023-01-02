using Core.Entities.Concrete;
using Core.Resources.Enum;
using Core.Resources.ProjectResources.Exceptions;
using Core.Utilities.Generators;
using Core.Utilities.Security.Hashing;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SetClappDemoApp.MVC.Utilities.Extensions
{
    public static class ProgramConfigExtensions
    {
        public static WebApplication SeedData(this WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                SeedUsers(context, host.Configuration);
            }
            return host;
        }

        private static void SeedUsers(ApplicationDbContext context, IConfiguration config)
        {
            try
            {
                var serverName = config.GetConnectionString("SqlServerName");
                var databaseName = config.GetConnectionString("SqlDatabaseName");
                if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(databaseName))
                    throw new CustomDatabaseSqlException(1);

                if (!context.Users.Any())
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash("123", out passwordHash, out passwordSalt);

                    var chiefUser = new User
                    {
                        FirstName = "Thomas",
                        LastName = "Edison",
                        FatherName = "Samuel",
                        UserType = UserType.Manager,
                        ChiefId = 0,
                        ConfirmationStatus = true,
                        Username = "rhb1",
                        SecurityToken = StringGenerator.GenerateSecurityToken(),
                        SecurityCount = 111111,
                        Position = "1. Team Leader at IT Dep.",
                        Email = "tom1@example.com",
                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };

                    var chiefUser2 = new User
                    {
                        FirstName = "Harry",
                        LastName = "Potter",
                        FatherName = "J.K. Rowling",
                        UserType = UserType.Manager,
                        ChiefId = 0,
                        ConfirmationStatus = true,
                        Username = "rhb2",
                        SecurityToken = StringGenerator.GenerateSecurityToken(),
                        SecurityCount = 111111,
                        Position = "2. Team Leader at IT Dep.",
                        Email = "harry2@example.com",
                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };

                    var hR1 = new User
                    {
                        FirstName = "Caroline",
                        LastName = "Kennedy",
                        FatherName = "Bouvier",
                        UserType = UserType.HR,
                        Username = "hr1",
                        Position = "HR Manager",
                        Email = "carol1@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 0,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var hR2 = new User
                    {
                        FirstName = "Jessie",
                        LastName = "Pinkman",
                        FatherName = "Adam",
                        UserType = UserType.HR,
                        Username = "hr2",
                        Position = "HR Manager",
                        Email = "jessie@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 0,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var hR3 = new User
                    {
                        FirstName = "Tsubasa",
                        LastName = "Ozora",
                        FatherName = "Koudai",
                        UserType = UserType.HR,
                        Username = "hr3",
                        Position = "HR Manager",
                        Email = "tsubasa@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 0,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var wrk1 = new User
                    {
                        FirstName = "Mark",
                        LastName = "Zuckerberg",
                        FatherName = "Edward",
                        UserType = UserType.Worker,
                        Username = "user1",
                        Position = "Co-Founder at Facebook",
                        Email = "mrkzck@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 2,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var wrk2 = new User
                    {
                        FirstName = "Elon",
                        LastName = "Musk",
                        FatherName = "Errol",
                        UserType = UserType.Worker,
                        Username = "user2",
                        Position = "Co-Founder at Space-X",
                        Email = "elnmsk@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 2,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var wrk3 = new User
                    {
                        FirstName = "Nick",
                        LastName = "Chapsas",
                        FatherName = "Rudiger",
                        UserType = UserType.Worker,
                        Username = "user3",
                        Position = "Content Creator on Youtube",
                        Email = "nckchp@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 1,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var wrk4 = new User
                    {
                        FirstName = "Jeff",
                        LastName = "Bezos",
                        FatherName = "Miguel",
                        UserType = UserType.Worker,
                        Username = "user4",
                        Position = "Co-Founder at Amazon",
                        Email = "nckchp@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 1,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var wrk5 = new User
                    {
                        FirstName = "Lionel",
                        LastName = "Messi",
                        FatherName = "Jorge",
                        UserType = UserType.Worker,
                        Username = "user5",
                        Position = "The best football player ever",
                        Email = "best@example.com",

                        Created_at = DateTime.UtcNow,
                        Modified_at = DateTime.UtcNow,
                        Created_by = "System Seed",
                        Modified_by = "System Seed",
                        IsActive = true,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        SecurityCount = 111111,
                        ChiefId = 1,
                        ConfirmationStatus = true,
                        SecurityToken = StringGenerator.GenerateSecurityToken()
                    };

                    var userList = new List<User>
                {
                    chiefUser, chiefUser2, hR1, hR2, hR3, wrk1, wrk2, wrk3, wrk4, wrk5
                };

                    context.Users.AddRange(userList);
                    context.SaveChanges();
                };
            }
            catch (CustomDatabaseSqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                context.Database.Migrate();
                SeedUsers(context, config);
            }
        }
    }
}
