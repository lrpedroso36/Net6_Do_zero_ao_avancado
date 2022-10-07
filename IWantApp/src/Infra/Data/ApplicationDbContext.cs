using IWantApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder builder) 
    {
        builder.Entity<Product>()
               .Property(x => x.Name).IsRequired();

        builder.Entity<Product>()
               .Property(x => x.Description).HasMaxLength(255);

        builder.Entity<Category>()
               .Property(x => x.Name).IsRequired();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>().HaveMaxLength(100);
    }
}