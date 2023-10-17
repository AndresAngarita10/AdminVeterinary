
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class DetailMovementConfiguration : IEntityTypeConfiguration<DetailMovement>
{
    public void Configure(EntityTypeBuilder<DetailMovement> builder)
    {

        builder.ToTable("detailMovement");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        
        builder.Property(e => e.Price)
        .HasColumnName("price")
        .HasColumnType("double")
        .IsRequired();

        builder.Property(e => e.Quantity)
        .HasColumnName("quantity")
        .HasColumnType("int")
        .HasMaxLength(3)
        .IsRequired();

        builder.HasOne(u => u.Medicine)
        .WithMany(u => u.DetailMovements)
        .HasForeignKey(u => u.MedicineIdFk);
        
        builder.HasOne(u => u.MedicineMovement)
        .WithMany(u => u.DetailMovements)
        .HasForeignKey(u => u.MedicineMovementIdFk);

    }
}
