
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class MedicalTreatmentConfiguration : IEntityTypeConfiguration<MedicalTreatment>
{
    public void Configure(EntityTypeBuilder<MedicalTreatment> builder)
    {

        builder.ToTable("medicalTreatment");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(e => e.DateStartTreatment)
        .HasColumnName("dateStartTreatment")
        .HasColumnType("date")
        .IsRequired();
        
        builder.HasOne(u => u.Quote)
        .WithMany(u => u.MedicalTreatments)
        .HasForeignKey(u => u.QuoteIdFk);
        

    }
}
