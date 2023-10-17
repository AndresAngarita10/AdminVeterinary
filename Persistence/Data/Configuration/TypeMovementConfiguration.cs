
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class TypeMovementConfiguration : IEntityTypeConfiguration<TypeMovement>
{
    public void Configure(EntityTypeBuilder<TypeMovement> builder)
    {

        builder.ToTable("typeMovement");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Name)
        .HasColumnName("name")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();

    }
}
