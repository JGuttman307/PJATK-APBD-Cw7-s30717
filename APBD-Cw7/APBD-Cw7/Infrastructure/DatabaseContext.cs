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
        
        //SEED DATA
        
        //PCs
        modelBuilder.Entity<PCs>().HasData(
            new PCs
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5f,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new PCs
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2f,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new PCs
            {
                Id = 3,
                Name = "Beast X Max",
                Weight = 10.0f,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 10, 10, 0, 0),
                Stock = 3
            }
        );
        
        //ComponentTypes
        modelBuilder.Entity<ComponentTypes>().HasData(
            new ComponentTypes { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentTypes { Id = 2, Abbreviation = "RAM", Name = "Memory" },
            new ComponentTypes { Id = 3, Abbreviation = "SSD", Name = "Storage" }
        );
        
        //ComponentsManufacturers
        modelBuilder.Entity<ComponentsManufacturers>().HasData(
            new ComponentsManufacturers
            {
                Id = 1,
                Abbreviation = "INT",
                FullName = "Intel Corporation",
                FoundationDate = new DateTime(1968, 7, 18)
            },
            new ComponentsManufacturers
            {
                Id = 2,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateTime(1969, 5, 1)
            },
            new ComponentsManufacturers
            {
                Id = 3,
                Abbreviation = "SAM",
                FullName = "Samsung Electronics",
                FoundationDate = new DateTime(1969, 1, 13)
            }
        );
        
        //Components (FK!!)
        modelBuilder.Entity<Components>().HasData(
            new Components
            {
                Code = "CPU1",
                Name = "Intel i7",
                Description = "High performance CPU",
                ComponentManufacturerId = 1,
                ComponentTypeId = 1
            },
            new Components
            {
                Code = "RAM1",
                Name = "Corsair 16GB",
                Description = "DDR4 Memory",
                ComponentManufacturerId = 2,
                ComponentTypeId = 2
            },
            new Components
            {
                Code = "SSD1",
                Name = "Samsung 1TB",
                Description = "Fast NVMe SSD",
                ComponentManufacturerId = 3,
                ComponentTypeId = 3
            }
        );
        
        //PCComponents (Join tabela)
        modelBuilder.Entity<PCComponents>().HasData(
            new PCComponents
            {
                PCId = 1,
                ComponentCode = "CPU1",
                Amount = 1
            },
            new PCComponents
            {
                PCId = 1,
                ComponentCode = "RAM1",
                Amount = 2
            },
            new PCComponents
            {
                PCId = 2,
                ComponentCode = "SSD1",
                Amount = 1
            }
        );
    }
}
