using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BloodDonationDb.Infrastructure.Persistence;

public class BloodDonationDbContext : DbContext
{
    public BloodDonationDbContext(DbContextOptions<BloodDonationDbContext> options) : base(options)
    { }

    public DbSet<Donor> Donors { get; set; }
    public DbSet<BloodStock> BloodStocks { get; set; }
    public DbSet<DonationDonor> DonationDonor { get; set; }
    public DbSet<DonationReceiver> DonationReceiver { get; set; }
    public DbSet<Receiver> Receivers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Entity>();
        modelBuilder.Ignore<Donation>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BloodDonationDbContext).Assembly);
    }
}