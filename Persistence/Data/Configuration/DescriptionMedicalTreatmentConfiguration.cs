
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class DescriptionMedicalTreatmentConfiguration : IEntityTypeConfiguration<DescriptionMedicalTreatment>
{
    public void Configure(EntityTypeBuilder<DescriptionMedicalTreatment> builder)
    {

        builder.ToTable("descriptionMedicalTreatment");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Dose)
        .HasColumnName("dose")
        .HasColumnType("varchar")
        .HasMaxLength(200)
        .IsRequired();
        
        builder.Property(e => e.AdministrationDate)
        .HasColumnName("administrationDate")
        .HasColumnType("date")
        .IsRequired();
        
        builder.Property(p => p.Observation)
        .HasColumnName("observation")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
        
        builder.HasOne(u => u.Medicine)
        .WithMany(u => u.DescriptionMedicalTreatments)
        .HasForeignKey(u => u.MedicineIdFk);
        
        builder.HasOne(u => u.MedicalTreatment)
        .WithMany(u => u.DescriptionMedicalTreatments)
        .HasForeignKey(u => u.MedicalTreatmentIdFk);

    }
}
