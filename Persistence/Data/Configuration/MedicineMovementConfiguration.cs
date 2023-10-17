
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class MedicineMovementConfiguration : IEntityTypeConfiguration<MedicineMovement>
{
    public void Configure(EntityTypeBuilder<MedicineMovement> builder)
    {

        builder.ToTable("medicineMovement");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(e => e.Quantity)
        .HasColumnName("quantity")
        .HasColumnType("int")
        .HasMaxLength(3)
        .IsRequired();
        
        builder.Property(e => e.DateMovement)
        .HasColumnName("dateMovement")
        .HasColumnType("date")
        .IsRequired();

        builder.HasOne(u => u.TypeMovement)
        .WithMany(u => u.MedicineMovements)
        .HasForeignKey(u => u.TypeMovementFk);

    }
}