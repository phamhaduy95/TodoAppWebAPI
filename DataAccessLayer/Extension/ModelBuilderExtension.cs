using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Extension
{
    internal static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder model)
        {
            #region add Users

            var user1Id = Guid.Parse("c0e86673-bce3-4608-8c32-06763581e952");
            var passwordHasher = new PasswordHasher<UserEntity>();
            model.Entity<UserEntity>().HasData(new UserEntity()
            {
                Id = user1Id,
                UserName = "Guest",
                DisplayName = "Guest",
                NormalizedUserName = "Guest",
                PasswordHash = passwordHasher.HashPassword(null, "guest"),
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                SecurityStamp = DateTimeOffset.Now.ToString(),
                EmailConfirmed = true,
                JoinDate = DateTimeOffset.Now,
                FirstName = "A ",
                LastName = "Van Pham",
                PhoneNumber = "0785686004",
                Address = "Ho Chi Minh, Viet Nam",
                Organization = "PHD inc"
            });

            #endregion add Users

            #region add Categories

            var category1Id = Guid.Parse("c0e86673-bce3-4608-8c32-06763581e953");
            model.Entity<CategoryEntity>().HasData(new CategoryEntity()
            {
                Name = "Business",
                CategoryId = category1Id,
                Description = "",
                Color = "#4FA095",
                UserId = user1Id,
            });

            var Category2Id = Guid.Parse("174bf70b-86fd-49fa-af41-d7f0a1b9d7a9");
            model.Entity<CategoryEntity>().HasData(new CategoryEntity()
            {
                Name = "Family",
                CategoryId = Category2Id,
                Description = "Family",
                Color = "#FF9551",
                UserId = user1Id,
            });

            #endregion add Categories

            #region add Tasks

            var Task1Id = Guid.Parse("f2345130-92a0-4577-a79d-b4ace269ec41");
            model.Entity<TaskEntity>().HasData(new TaskEntity()
            {
                TaskId = Task1Id,
                Title = "the first task",
                Description = "the first example task  used for testing",
                StartTime = new DateTime(2022, 10, 15, 5, 20, 0),
                CreatedTime = DateTime.Now,
                EndTime = new DateTime(2022, 10, 15, 8, 20, 0),
                UserId = user1Id,
                CategoryId = category1Id,
            }); ;

            var Task2Id = Guid.Parse("54f16b02-a09e-42e8-8f47-d97fb54dfa94");
            model.Entity<TaskEntity>().HasData(new TaskEntity()
            {
                TaskId = Task2Id,
                Title = "second task",
                Description = "the second task is used for testing",
                CreatedTime = DateTime.Now,
                StartTime = new DateTime(2022, 10, 20, 0, 20, 0),
                EndTime = new DateTime(2022, 10, 29, 5, 20, 0),
                UserId = user1Id,
                CategoryId = category1Id,
            });

            #endregion add Tasks
        }
    }
}