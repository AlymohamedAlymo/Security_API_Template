﻿using Microsoft.EntityFrameworkCore;
using Security_API_Template.Data.Context;
using Security_API_Template.Data.Entites;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Security_API_Template.Data.SeedData
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/SeedData/UsersSeedData.json");

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUsers>>(userData, option);

            if (users == null) return;

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();

        }
    }
}
