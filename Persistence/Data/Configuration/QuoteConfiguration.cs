
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {

        builder.ToTable("quote");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        
        builder.Property(e => e.Date)
        .HasColumnName("date")
        .HasColumnType("date")
        .IsRequired();
        
        builder.Property(e => e.Hour)
        .HasColumnName("hour")
        .HasColumnType("time")
        .IsRequired();

        builder.Property(p => p.Reason)
        .HasColumnName("reason")
        .HasColumnType("varchar")
        .HasMaxLength(255)
        .IsRequired();
        
        builder.HasOne(u => u.Pet)
        .WithMany(u => u.Quotes)
        .HasForeignKey(u => u.PetIdFk);
        
        builder.HasOne(u => u.Veterinarian)
        .WithMany(u => u.Quotes)
        .HasForeignKey(u => u.VeterinarianIdFk);

    }
}