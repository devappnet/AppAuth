using App.Domain;
using App.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace App.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration Model Builder
    /// </summary>
    public static class ConfigurationModelBuilder
    {
        /// <summary>
        /// Apply model builder base entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public static void ApplyBaseEntityConfiguration<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : BaseEntity
        {
            entity.HasKey(e => e.Pk_ID);
            entity.Property(e => e.Pk_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasMaxLength(5000);
            entity.Property(e => e.Version).IsRowVersion();
            entity.Property(e => e.Active);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.LastUpdateBy).HasMaxLength(50);
            entity.Property(e => e.ApprovedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.LastUpdateDate);
            entity.Property(e => e.ApprovedDate);
        }
        /// <summary>
        /// apply model builder aspnetuser
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        public static ModelBuilder ApplyAspNetUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.ApplyBaseEntityConfiguration<AspNetUser>();

                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(150);
                entity.Property(e => e.EmailConfirmed);
                entity.Property(e => e.PasswordHash).HasMaxLength(500);
                entity.Property(e => e.SecurityStamp).HasMaxLength(500);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.PhoneConfirmed);
                entity.Property(e => e.TwoFactorEnabled);
                entity.Property(e => e.LockoutEnd);
                entity.Property(e => e.LockoutEnabled);
                entity.Property(e => e.AccessFailedCount);
                entity.HasMany(e => e.AspNetUserRoles)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);
            });
            return modelBuilder;
        }
        /// <summary>
        /// apply model builder aspnetroles
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        public static ModelBuilder ApplyAspNetRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.ApplyBaseEntityConfiguration<AspNetRole>();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NormalizedRole).HasMaxLength(100);
                entity.HasMany(e => e.AspNetUserRoles)
                      .WithOne(e => e.Role)
                      .HasForeignKey(e => e.RoleId);
            });
            return modelBuilder;
        }
        /// <summary>
        /// apply model builder aspnetuserroles
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        public static ModelBuilder ApplyAspNetUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.ApplyBaseEntityConfiguration<AspNetUserRole>();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.RoleId).IsRequired();
                entity.HasOne(e => e.User)
                      .WithMany(e => e.AspNetUserRoles)
                      .HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Role)
                      .WithMany(e => e.AspNetUserRoles)
                      .HasForeignKey(e => e.RoleId);
            });
            return modelBuilder;
        }

        public static ModelBuilder ApplyAspNetUserRolePermission(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUserRolePermission>(entity =>
            {
                entity.ApplyBaseEntityConfiguration<AspNetUserRolePermission>();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.RoleId).IsRequired();
                entity.Property(e => e.Permission).HasMaxLength(100).IsRequired();
                entity.HasOne(e => e.AspNetUser)
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Role)
                      .WithMany()
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            return modelBuilder;
        }
    }
}
