
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("user");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
        .IsRequired();

        builder.Property(u => u.Name)
        .HasColumnName("name")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
        
        builder.Property(u => u.UserName)
        .HasColumnName("username")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(u => u.Email)
        .HasColumnName("email")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(u => u.Password)
        .HasColumnName("password")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(u => u.Gender)
        .WithMany(u => u.Users)
        .HasForeignKey(u => u.GenderIdfk);

        builder
       .HasMany(p => p.Roles)
       .WithMany(r => r.Users)
       .UsingEntity<UserRole>(

           j => j
           .HasOne(pt => pt.Rol)
           .WithMany(t => t.UsersRoles)
           .HasForeignKey(ut => ut.RolIdFk),


           j => j
           .HasOne(et => et.User)
           .WithMany(et => et.UsersRoles)
           .HasForeignKey(el => el.UserIdFk),

           j =>
           {
               j.ToTable("userRol");
               j.HasKey(t => new { t.UserIdFk, t.RolIdFk });

           });


        builder.HasMany(p => p.RefreshTokens)
        .WithOne(p => p.User)
        .HasForeignKey(p => p.UserId);




    }
}
