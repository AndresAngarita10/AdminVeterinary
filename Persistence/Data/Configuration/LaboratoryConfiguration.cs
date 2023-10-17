
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class LaboratoryConfiguration : IEntityTypeConfiguration<Laboratory>
{
    public void Configure(EntityTypeBuilder<Laboratory> builder)
    {

        builder.ToTable("laboratory");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Name)
        .HasColumnName("name")
        .HasColumnType("varchar")
        .HasMaxLength(60)
        .IsRequired();
        
        builder.Property(p => p.Address)
        .HasColumnName("address")
        .HasColumnType("varchar")
        .HasMaxLength(150)
        .IsRequired();
        
        builder.Property(p => p.Phone)
        .HasColumnName("phone")
        .HasColumnType("varchar")
        .HasMaxLength(20)
        .IsRequired();

    }
}
