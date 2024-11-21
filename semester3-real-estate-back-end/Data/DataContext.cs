using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Data;

public class DataContext : IdentityDbContext<User, Role, string>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Category> Category { get; set; }
    public DbSet<Direction> Direction { get; set; }
    public DbSet<PropertyType> PropertyType { get; set; }
    public DbSet<Juridical> Juridical { get; set; }
    public DbSet<Property> Property { get; set; }
    public DbSet<PropertyImage> PropertyImage { get; set; }
    public DbSet<Province> Province { get; set; }
    public DbSet<District> District { get; set; }
    public DbSet<Ward> Ward { get; set; }


    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     new DbInitializer(modelBuilder).Seed();
    // }
}