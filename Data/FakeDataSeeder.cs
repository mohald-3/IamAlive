using Bogus;
using IamAlive.Models;
using Microsoft.EntityFrameworkCore;

namespace IamAlive.Data
{
    public static class FakeDataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync())
                return; 

            var faker = new Faker("en");

            // Users
            var users = new List<User>();

            for (int i = 1; i <= 10; i++)
            {
                users.Add(new User
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Email = faker.Internet.Email(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"),
                    Phone = faker.Phone.PhoneNumber(),
                    EmergencyContactName = faker.Name.FullName(),
                    EmergencyContactPhone = faker.Phone.PhoneNumber(),
                    AccountCreationTime = DateTime.UtcNow,
                    AccountModificationTime = DateTime.UtcNow
                });
            }

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            // CheckIns 
            var checkIns = new List<CheckIn>();
            foreach (var user in users)
            {
                var userCheckIns = new Faker<CheckIn>()
                    .RuleFor(ci => ci.UserId, _ => user.UserId)
                    .RuleFor(ci => ci.Timestamp, _ => faker.Date.Recent(30))
                    .Generate(faker.Random.Int(3, 5));

                checkIns.AddRange(userCheckIns);
            }
            await context.CheckIns.AddRangeAsync(checkIns);

            // Friendships 
            var friendships = new List<Friendship>();
            var userIds = users.Select(u => u.UserId).ToList();
            var rng = new Random();

            for (int i = 0; i < 20; i++) // 20 random friendships
            {
                var userA = userIds[rng.Next(userIds.Count)];
                var userB = userIds[rng.Next(userIds.Count)];

                if (userA == userB || friendships.Any(f => f.UserId == userA && f.FriendId == userB))
                    continue;

                friendships.Add(new Friendship
                {
                    UserId = userA,
                    FriendId = userB
                });
            }
            await context.Friendships.AddRangeAsync(friendships);

            await context.SaveChangesAsync();
        }
    }
}
