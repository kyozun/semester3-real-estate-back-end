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
        // Category
        _modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = "ffe56d39-3939-4c5c-ade9-8f147dca9592",
                Name = "Category 1",
                UpdatedAt = DateTime.Now,
            },
            new Category
            {
                CategoryId = "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                Name = "Category 2",
                UpdatedAt = DateTime.Now,
            }
        );

        // Direction
        _modelBuilder.Entity<Direction>().HasData(
            new Direction
            {
                DirectionId = "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                Name = "North",
            },
            new Direction
            {
                DirectionId = "ffe56d39-3939-4c5c-ade9-8f147dca9594",
                Name = "South",
            }
        );

        // Juridical
        _modelBuilder.Entity<Juridical>().HasData(
            new Juridical
            {
                JuridicalId = "ffe56d39-3939-4c5c-ade9-8f147dca9594",
                Name = "Direction 1",
            }
        );

        // PropertyType
        _modelBuilder.Entity<PropertyType>().HasData(
            new PropertyType
            {
                PropertyTypeId = "ffe56d39-3939-4c5c-ade9-8f147dca9595",
                Name = "PropertyType 1",
            }
        );


        // Province
        _modelBuilder.Entity<Province>().HasData(
            new Province
            {
                ProvinceId = "ffe56d39-3939-4c5c-ade9-8f147dca9596",
                Name = "Province 1",
            },
            new Province
            {
                ProvinceId = "ffe56d39-3939-4c5c-ade9-8f147dca9597",
                Name = "Province 2",
            }
        );

        // District
        _modelBuilder.Entity<District>().HasData(
            new District
            {
                DistrictId = "ffe56d39-3939-4c5c-ade9-8f147dca9596",
                Name = "District 1",
                ProvinceId = "ffe56d39-3939-4c5c-ade9-8f147dca9596"
            },
            new District
            {
                DistrictId = "ffe56d39-3939-4c5c-ade9-8f147dca9597",
                Name = "District 2",
                ProvinceId = "ffe56d39-3939-4c5c-ade9-8f147dca9596"
            },
            new District
            {
                DistrictId = "ffe56d39-3939-4c5c-ade9-8f147dca9598",
                Name = "District 3",
                ProvinceId = "ffe56d39-3939-4c5c-ade9-8f147dca9597"
            }
        );


        // Ward
        _modelBuilder.Entity<Ward>().HasData(
            new Ward
            {
                WardId = "ffe56d39-3939-4c5c-ade9-8f147dca9596",
                Name = "Ward 1",
                DistrictId = "ffe56d39-3939-4c5c-ade9-8f147dca9596"
            },
            new Ward
            {
                WardId = "ffe56d39-3939-4c5c-ade9-8f147dca9597",
                Name = "Ward 2",
                DistrictId = "ffe56d39-3939-4c5c-ade9-8f147dca9597"
            }
        );

        // Property
        _modelBuilder.Entity<Property>().HasData(
            new Property
            {
                PropertyId = "ffe56d39-3939-4c5c-ade9-8f147dca9581",
                Description = "Description",
                Title = "Property title 1",
                Address = "Address 1",
                Price = 10.00,
                Area = 10.00,
                Floor = 1,
                Bathroom = 1,
                Bedroom = 1,

                CategoryId = "ffe56d39-3939-4c5c-ade9-8f147dca9592",
                DirectionId = "ffe56d39-3939-4c5c-ade9-8f147dca9593",
                JuridicalId = "ffe56d39-3939-4c5c-ade9-8f147dca9594",
                PropertyTypeId = "ffe56d39-3939-4c5c-ade9-8f147dca9595",
                WardId = "ffe56d39-3939-4c5c-ade9-8f147dca9596",
                UserId = "70acc54a-44e0-4d31-8b07-52b5b82e9e55",

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new Property
            {
                PropertyId = "ffe56d39-3939-4c5c-ade9-8f147dca9582",
                Description = "Description",
                Title = "Property title 2 ",
                Address = "Address 2",
                Price = 20.00,
                Area = 10.00,
                Floor = 1,
                Bathroom = 1,
                Bedroom = 1,

                CategoryId = "ffe56d39-3939-4c5c-ade9-8f147dca9592",
                DirectionId = "ffe56d39-3939-4c5c-ade9-8f147dca9594",
                JuridicalId = "ffe56d39-3939-4c5c-ade9-8f147dca9594",
                PropertyTypeId = "ffe56d39-3939-4c5c-ade9-8f147dca9595",
                WardId = "ffe56d39-3939-4c5c-ade9-8f147dca9597",
                UserId = "70acc54a-44e0-4d31-8b07-52b5b82e9e55",

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
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