
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class MedicinePartnerConfiguration : IEntityTypeConfiguration<MedicinePartner>
{
    public void Configure(EntityTypeBuilder<MedicinePartner> builder)
    {

        builder.ToTable("medicinePartner");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        
        builder.HasOne(u => u.Medicine)
        .WithMany(u => u.MedicinePartners)
        .HasForeignKey(u => u.MedicineIdFk);
        
        builder.HasOne(u => u.Partner)
        .WithMany(u => u.MedicinePartners)
        .HasForeignKey(u => u.PartnerIdFk);

    }
}