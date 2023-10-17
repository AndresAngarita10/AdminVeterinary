
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public DbSet<Breed> Breeds { get; set; }
    public DbSet<DescriptionMedicalTreatment> DescriptionMedicalTreatments { get; set; }
    public DbSet<DetailMovement> DetailMovements { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<MedicalTreatment> MedicalTreatments { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<MedicineMovement> MedicineMovements { get; set; }
    public DbSet<MedicinePartner> MedicinePartners { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<PartnerType> PartnerTypes { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Specie> Species { get; set; }
    public DbSet<TypeMovement> TypeMovements { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UsersRoles { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
