
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {

        builder.ToTable("pet");
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
        .IsRequired();
        
        builder.Property(p => p.Name)
        .HasColumnName("name")
        .HasColumnType("varchar")
        .HasMaxLength(100)
        .IsRequired();
        
        builder.Property(e => e.DateBirth)
        .HasColumnName("datebirth")
        .HasColumnType("date")
        .IsRequired();
        
        builder.HasOne(u => u.Owner)
        .WithMany(u => u.Pets)
        .HasForeignKey(u => u.UserOwnerId);
        
        builder.HasOne(u => u.Specie)
        .WithMany(u => u.Pets)
        .HasForeignKey(u => u.SpeciesIdFk);
        
        builder.HasOne(u => u.Breed)
        .WithMany(u => u.Pets)
        .HasForeignKey(u => u.BreedIdFk);

    }
}