using APBD_Cw7.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;

namespace APBD_Cw7.Infrastructure;

public class DatabaseContext(DbContextOptions opt) : DbContext(opt)
{
    public DbSet<PCs> PCs { get; set; }
    public DbSet<Components> Components { get; set; }
    public DbSet<ComponentTypes> ComponentTypes { get; set; }
    public DbSet<ComponentsManufacturers> ComponentManufacturers { get; set; }
    public DbSet<PCComponents> PCComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PCs>(opt =>
        {
            opt.ToTable("PCs");

            opt.HasKey(x => x.Id);

            opt.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            opt.Property(x => x.Weight)
                .IsRequired();

            opt.Property(x => x.Warranty)
                .IsRequired();

            opt.Property(x => x.CreatedAt)
                .IsRequired();

            opt.Property(x => x.Stock)
                .IsRequired();
        });

        // Components
        modelBuilder.Entity<Components>(opt =>
        {
            opt.ToTable("Components");

            opt.HasKey(x => x.Code);

            opt.Property(x => x.Code)
                .HasColumnType("char(10)");

            opt.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(300);

            opt.Property(x => x.Description)
                .HasColumnType("nvarchar(max)");

            //FK -> Manufacturers
            opt.Property(x => x.ComponentManufacturerId)
                .IsRequired();

            //FK -> Types
            opt.Property(x => x.ComponentTypeId)
                .IsRequired();
        });

        // ComponentTypes
        modelBuilder.Entity<ComponentTypes>(opt =>
        {
            opt.ToTable("ComponentTypes");

            opt.HasKey(x => x.Id);

            opt.Property(x => x.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            opt.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();
        });
        
        // ComponentsManufacturers
        modelBuilder.Entity<ComponentsManufacturers>(opt =>
        {
            opt.ToTable("ComponentManufacturers");

            opt.HasKey(x => x.Id);

            opt.Property(x => x.Abbreviation)
                .HasMaxLength(30)
                .IsRequired();

            opt.Property(x => x.FullName)
                .HasMaxLength(300)
                .IsRequired();

            opt.Property(x => x.FoundationDate)
                .HasColumnType("date");
        });
        
        // PCComponents (join table)
        modelBuilder.Entity<PCComponents>(opt =>
        {
            opt.ToTable("PCComponents");

            // klucz
            opt.HasKey(x => new { x.PCId, x.ComponentCode });

            opt.Property(x => x.Amount)
                .IsRequired();

            //PC -> PCComponents
            opt.HasOne(x => x.PC)
                .WithMany(x => x.PCComponents)
                .HasForeignKey(x => x.PCId);

            // Components -> PCComponents
            opt.HasOne(x => x.Component)
                .WithMany(x => x.PCComponents)
                .HasForeignKey(x => x.ComponentCode);
        });

        //Components
        modelBuilder.Entity<Components>(opt =>
        {
            opt.HasOne(x => x.ComponentManufacturer)
                .WithMany(x => x.Components)
                .HasForeignKey(x => x.ComponentManufacturerId);

            opt.HasOne(x => x.ComponentType)
                .WithMany(x => x.Components)
                .HasForeignKey(x => x.ComponentTypeId);
        });
    }
}
