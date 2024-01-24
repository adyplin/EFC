using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<CompanyEntity> Companies { get; set; }
    public virtual DbSet<CustomerAddressEntity> CustomerAddresses { get; set; }
    public virtual DbSet<CustomerContactEntity> CustomerContacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>().HasIndex(x => x.RoleName).IsUnique();

        modelBuilder.Entity<CustomerContactEntity>().HasIndex(x => x.Email).IsUnique();
    }
}
