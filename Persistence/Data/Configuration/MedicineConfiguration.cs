
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable("medicine");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Name)
        .HasColumnName("name")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();
        
        builder.Property(e => e.QuantityAvalible)
        .HasColumnName("quantityAvalible")
        .HasColumnType("int")
        .HasMaxLength(3)
        .IsRequired();
        
        builder.Property(e => e.Price)
        .HasColumnName("price")
        .HasColumnType("double")
        .IsRequired();

        builder.HasOne(u => u.Laboratory)
        .WithMany(u => u.Medicines)
        .HasForeignKey(u => u.LaboratoryIdFk);
    }
}