
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {

        builder.ToTable("partner");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Name)
        .HasColumnName("name")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();
        
        builder.Property(p => p.Email)
        .HasColumnName("email")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();
        
        builder.Property(p => p.Phone)
        .HasColumnName("phone")
        .HasColumnType("varchar")
        .HasMaxLength(30)
        .IsRequired();
        
        builder.Property(p => p.Address)
        .HasColumnName("address")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();
        
        builder.HasOne(u => u.Specialty)
        .WithMany(u => u.Partners)
        .HasForeignKey(u => u.SpecialtyIdFk);
        
        builder.HasOne(u => u.Gender)
        .WithMany(u => u.Partners)
        .HasForeignKey(u => u.GenderIdFk);
        
        builder.HasOne(u => u.PartnerType)
        .WithMany(u => u.Partners)
        .HasForeignKey(u => u.PartnerTypeIdFk);

    }
}