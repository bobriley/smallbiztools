using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SBToolsService.Models;

public partial class SmallbizContext : DbContext
{
    public SmallbizContext()
    {
    }

    public SmallbizContext(DbContextOptions<SmallbizContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SmallBusiness> SmallBusinesses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SmallBizDB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SmallBusiness>(entity =>
        {
            entity.ToTable("SmallBusiness");

            entity.Property(e => e.HealthRatio).HasComputedColumnSql("([AskingPrice]*[SellableInventory])", false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PriceDelta).HasComputedColumnSql("([Rent]*[Utilities])", false);
            entity.Property(e => e.Sde).HasComputedColumnSql("([AskingPrice]*[SellableInventory])", false);
            entity.Property(e => e.Sdemultiple).HasColumnName("SDEMultiple");
            entity.Property(e => e.Sdevaluation)
                .HasComputedColumnSql("([SDEMultiple]*[MiscExpenses])", false)
                .HasColumnName("SDEValuation");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
