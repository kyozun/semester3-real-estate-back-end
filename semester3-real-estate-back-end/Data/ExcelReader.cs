using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Data;

public class ExcelReader
{
    public async Task<List<Direction>> ImportDirectionFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Direction>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Direction
            {
                DirectionId = worksheet.Cells[row, 1].Text,
                Name = worksheet.Cells[row, 2].Text
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<Category>> ImportCategoryFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Category>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Category
            {
                CategoryId = worksheet.Cells[row, 1].Text,
                Name = worksheet.Cells[row, 2].Text
            };
            entities.Add(entity);
        }

        return entities;
    }


    public async Task<List<Province>> ImportProvinceFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Province>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Province
            {
                ProvinceId = int.Parse(worksheet.Cells[row, 1].Text),
                Name = worksheet.Cells[row, 2].Text
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<District>> ImportDistrictFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<District>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new District
            {
                DistrictId = int.Parse(worksheet.Cells[row, 1].Text),
                Name = worksheet.Cells[row, 2].Text,
                ProvinceId = int.Parse(worksheet.Cells[row, 3].Text)
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<Ward>> ImportWardFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Ward>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Ward
            {
                WardId = int.Parse(worksheet.Cells[row, 1].Text),
                Name = worksheet.Cells[row, 2].Text,
                DistrictId = int.Parse(worksheet.Cells[row, 3].Text)
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<Juridical>> ImportJuridicalFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Juridical>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Juridical
            {
                JuridicalId = worksheet.Cells[row, 1].Text,
                Name = worksheet.Cells[row, 2].Text,
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<PropertyType>> ImportPropertyTypeFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<PropertyType>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new PropertyType
            {
                PropertyTypeId = worksheet.Cells[row, 1].Text,
                Name = worksheet.Cells[row, 2].Text,
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<User>> ImportUserFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<User>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new User
            {
                Id = worksheet.Cells[row, 1].Text,
                UserName = worksheet.Cells[row, 2].Text,
                Email = worksheet.Cells[row, 3].Text,
                EmailConfirmed = bool.Parse(worksheet.Cells[row, 4].Text),
                PasswordHash = worksheet.Cells[row, 5].Text,
                SecurityStamp = worksheet.Cells[row, 6].Text,
                ConcurrencyStamp = worksheet.Cells[row, 7].Text,
                PhoneNumberConfirmed = bool.Parse(worksheet.Cells[row, 8].Text),
                TwoFactorEnabled = bool.Parse(worksheet.Cells[row, 9].Text),
                LockoutEnabled = bool.Parse(worksheet.Cells[row, 10].Text),
                AccessFailedCount = int.Parse(worksheet.Cells[row, 11].Text),
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<Role>> ImportRoleFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Role>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Role
            {
                Id = worksheet.Cells[row, 1].Text,
                Name = worksheet.Cells[row, 2].Text,
                Description = worksheet.Cells[row, 3].Text,
            };
            entities.Add(entity);
        }

        return entities;
    }
    
    public async Task<List<IdentityUserRole<string>>> ImportUserRoleFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<IdentityUserRole<string>>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new IdentityUserRole<string>
            {
                UserId = worksheet.Cells[row, 1].Text,
                RoleId = worksheet.Cells[row, 2].Text,
            };
            entities.Add(entity);
        }

        return entities;
    }

    public async Task<List<Property>> ImportPropertyFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<Property>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new Property
            {
                PropertyId = worksheet.Cells[row, 1].Text,
                Title = worksheet.Cells[row, 2].Text,
                Description = worksheet.Cells[row, 3].Text,
                Address = worksheet.Cells[row, 4].Text,
                Price = double.Parse(worksheet.Cells[row, 5].Text),
                Area = double.Parse(worksheet.Cells[row, 6].Text),
                Floor = int.Parse(worksheet.Cells[row, 7].Text),
                Bedroom = int.Parse(worksheet.Cells[row, 8].Text),
                Bathroom = int.Parse(worksheet.Cells[row, 9].Text),
                ViewCount = int.Parse(worksheet.Cells[row, 10].Text),
                CoverImage = worksheet.Cells[row, 11].Text,
                DirectionId = worksheet.Cells[row, 12].Text,
                CategoryId = worksheet.Cells[row, 13].Text,
                PropertyTypeId = worksheet.Cells[row, 14].Text,
                JuridicalId = worksheet.Cells[row, 15].Text,
                WardId = int.Parse(worksheet.Cells[row, 16].Text),
                UserId = worksheet.Cells[row, 17].Text,
            };
            entities.Add(entity);
        }

        return entities;
    }
    
    public async Task<List<PropertyImage>> ImportPropertyImageFromExcel(string filePath, string workSheetName)
    {
        var entities = new List<PropertyImage>();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[workSheetName];
        int rows = worksheet.Dimension.Rows;

        for (int row = 2; row <= rows; row++) // Skip header
        {
            var entity = new PropertyImage
            {
                PropertyImageId = worksheet.Cells[row, 1].Text,
                ImageUrl = worksheet.Cells[row, 2].Text,
                PropertyId = worksheet.Cells[row, 3].Text,
                Description = worksheet.Cells[row, 4].Text,
               
            };
            entities.Add(entity);
        }

        return entities;
    }
}