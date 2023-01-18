using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LemlemPharmacy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LemlemPharmacy.Data
{
    public class LemlemPharmacyContext : IdentityDbContext<ApplicationUser>
    {
        public LemlemPharmacyContext (DbContextOptions<LemlemPharmacyContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BinCard>()
                .Property(i => i.Invoice)
				.IsRequired(false);

			builder.Entity<BinCard>()
				.HasIndex(i => new { i.BatchNo,i.Id })
				.IsUnique();

			builder.Entity<SoldMedicine>()
                .Property(s => s.CustomerPhone)
                .IsRequired(false);

            builder.Entity<Medicine>()
                .HasIndex(i => i.BatchNo)
                .IsUnique();

            builder.Entity<Customer>()
                .HasIndex(i => i.PhoneNo)
                .IsUnique();

            builder.Entity<MedicineTrack>()
                .HasIndex(i => new {i.BatchNo, i.Invoice})
                .IsUnique();

			// every customer is not required to be registered on the soldMedicine table
			builder.Entity<SoldMedicine>()
                .Property(s => s.CustomerPhone)
                .IsRequired(false);

            builder.Entity<Medicine>()
                .HasCheckConstraint(
                "CK_Medicine_Category",
                "Category in ('Anti-Fungal', 'Anti-Allergy', 'Anti-Helmentic', 'Hormonal Drugs', 'ENT Drugs', 'NSAI', 'GIT', 'Anti-Respiratory', 'Narcotic and Anti-Psychotropic', 'Anti-Biotic', 'Vitamins and Minerals', 'CSV Drugs')");

            builder.Entity<Medicine>()
                .HasCheckConstraint(
                "CK_Medicine_Type",
                "Type in ('LongTerm', 'ShortTerm')");

            // Medicine-BinCard Relationship (One-to-Many)
            builder.Entity<BinCard>()
                .HasOne(s => s.Medicine)
                .WithMany(c => c.BinCardsBatchNos)
				.HasForeignKey(s => s.BatchNo)
                .HasPrincipalKey(c => c.BatchNo);

            // Medicine-BinCard Relationship (One-to-Many)
            builder.Entity<BinCard>()
                .HasOne(s => s.MedicineID)
                .WithMany(c => c.BinCardsMedicineIDs)
                .HasForeignKey(s => s.MedicineId)
                .HasPrincipalKey(c => c.Id);

            // Customer-SoldMedicine Relationship (One-to-Many)
            builder.Entity<SoldMedicine>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.SoldMedicines)
                .HasForeignKey(s => s.CustomerPhone)
                .HasPrincipalKey(c => c.PhoneNo)
                .OnDelete(DeleteBehavior.SetNull);

            // User-SoldMedicine Relationship (One-to-Many)
            builder.Entity<SoldMedicine>()
                .HasOne(s => s.ApplicationUser)
                .WithMany(c => c.SoldMedicines)
                .HasForeignKey(s => s.PharmacistId)
                .HasPrincipalKey(c => c.UserName);

            // Medicine-SoldMedicine Relationship (One-to-Many)
            builder.Entity<SoldMedicine>()
                .HasOne(s => s.Medicine)
                .WithMany(c => c.SoldMedicines)
                .HasForeignKey(s => s.MedicineId)
                .HasPrincipalKey(c => c.Id);

            // Medicine-CustomerNotification Relationship
            builder.Entity<CustomerNotification>()
                .HasOne(s => s.Medicine)
                .WithMany(c => c.CustomerNotifications)
                .HasForeignKey(s => s.BatchNo)
                .HasPrincipalKey(c => c.BatchNo);

			// Customer-CustomerNotification Relationship
			builder.Entity<CustomerNotification>()
				.HasOne(s => s.Customer)
				.WithMany(c => c.CustomerNotifications)
				.HasForeignKey(s => s.PhoneNo)
				.HasPrincipalKey(c => c.PhoneNo);
		}

        public DbSet<Medicine> Medicine { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<BinCard> BinCard { get; set; } = default!;
        public DbSet<SoldMedicine> SoldMedicine { get; set; } = default!;
        public DbSet<MedicineTrack> MedicineTrack { get; set; } = default!;
        public DbSet<CustomerNotification> CustomerNotification { get; set; } = default!;
    } 
}
