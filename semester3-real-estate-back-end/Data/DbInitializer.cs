using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Data;

public class DbInitializer
{
    private readonly ModelBuilder _modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        // Property
        _modelBuilder.Entity<Property>().HasData(
            new Property
            {
                PropertyId = "a72879ca-bfaa-40ba-af21-c93c93723a85",
                Title = "Doraemon",
                Description =
                    "Doraemon is the story of a lovable loser named Nobi, and Doraemon the robot cat who comes from the future to help him",
                 Address = "Ha Noi",
                 Price = 5000000,
                 Area = 7.25m,
                 Floor = 4,
                 
            
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Manga
            {
                MangaId = "efe3cd7a-158a-4511-ac37-96bca4ea5ddb",
                Title = "Chainsaw Man",
                Description =
                    "Denji's a poor young man who'll do anything for money, even hunting down devils with his pet devil Pochita. He's a simple man with simple dreams, drowning under a mountain of debt. But his sad life gets turned upside down one day when he's betrayed by someone he trusts",
                Year = 2000,
                ViewCount = 0,
                MangaStatus = MangaStatus.Completed,
                IsLock = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }
        );

        // Role
        _modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = "ffe56d39-3939-4c5c-ade9-8f147dca9591",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Description = "Admin role"
            },
            new Role
            {
                Id = "28a11887-8723-4f56-a83f-f8d43529dfb9",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Description = "User role"
            }
        );

        // User Role
        _modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "70acc54a-44e0-4d31-8b07-52b5b82e9e55",
                RoleId = "ffe56d39-3939-4c5c-ade9-8f147dca9591"
            },
            new IdentityUserRole<string>
            {
                UserId = "4a9cd504-f018-4574-a525-98c07a998678",
                RoleId = "28a11887-8723-4f56-a83f-f8d43529dfb9"
            }
        );


        // User 
        _modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "70acc54a-44e0-4d31-8b07-52b5b82e9e55",
                UserName = "cuong",
                NormalizedUserName = "CUONG",
                Email = "cuong@gmail.com",
                NormalizedEmail = "CUONG@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEI/SWx93eTcgVMH5ll0cl2UAVQPvI9XX0kQPwNxgyGcz5ud88hHLZCxsxoQevxeeig==",
                SecurityStamp = "5VQFXGIF4X76SDIO4OLC3YTSQRVEZQDW",
                ConcurrencyStamp = "9934c5a0-6bfd-4fde-936d-c3ece61cf8d2",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            },
            new User
            {
                Id = "4a9cd504-f018-4574-a525-98c07a998678",
                UserName = "daica",
                NormalizedUserName = "DAICA",
                Email = "daica@gmail.com",
                NormalizedEmail = "DAICA@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAIAAYagAAAAEFfBxpYGAi+a0Iog6fqmUcgFkirdMOqUynFaOVSoy9DsmTor/h7YSfKuQW2HZzLHmg==",
                SecurityStamp = "RMVH3XFWGDEAFBVLMZBEHMGQF7LUOUVW",
                ConcurrencyStamp = "263312b7-be37-4d7f-bfd5-f8e935323be5",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        );
    }
}