using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using semester3_real_estate_back_end.Data;

public class DataSeeder
{
    private readonly DataContext _dbContext;

    public DataSeeder(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedFromExcel(string filePath)
    {
        await SeedDirection(filePath, "Direction");
        await SeedCategory(filePath, "Category");
        await SeedProvince(filePath, "Province");
        await SeedDistrict(filePath, "District");
        await SeedWard(filePath, "Ward");
        await SeedJuridical(filePath, "Juridical");
        await SeedPropertyType(filePath, "PropertyType");
        await SeedUser(filePath, "AspNetUsers");
        await SeedRole(filePath, "AspNetRoles");
        await SeedUserRole(filePath, "AspNetUserRoles");
        await SeedProperty(filePath, "Property");
    }

    private async Task SeedDirection(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportDirectionFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.Direction.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    private async Task SeedCategory(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportCategoryFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.Category.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedProvince(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportProvinceFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.Province.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedDistrict(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportDistrictFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.District.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedWard(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportWardFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.Ward.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedJuridical(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            // await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportJuridicalFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.Juridical.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedUser(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportUserFromExcel(filePath, "User");

            // Insert new data
            _dbContext.Users.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedRole(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportRoleFromExcel(filePath, "Role");

            // Insert new data
            _dbContext.Roles.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedUserRole(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportUserRoleFromExcel(filePath, "UserRole");

            // Insert new data
            _dbContext.UserRoles.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedPropertyType(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            // await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportPropertyTypeFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.PropertyType.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    private async Task SeedProperty(string filePath, string entityName)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Excel file not found: {filePath}");

        try
        {
            // Delete old table
            await _dbContext.Database.ExecuteSqlRawAsync($"DELETE  FROM  {entityName}");

            // Reset primary key 
            // await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM sqlite_sequence WHERE name = '{entityName}'");

            // Read data from Excel
            var reader = new ExcelReader();
            var entities = await reader.ImportPropertyFromExcel(filePath, entityName);

            // Insert new data
            _dbContext.Property.AddRange(entities);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Seed OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seed Error: {ex.Message}");
        }
    }
    
    
}