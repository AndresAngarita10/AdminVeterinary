
namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    IUserRepository Users { get; }
    IBreedRepository Breeds { get; }
    IDescriptionMedicalTreatmentRepository DescriptionMedicalTreatments { get; }
    IDetailMovementRepository DetailMovements { get; }
    IGenderRepository Genders { get; }
    ILaboratoryRepository Laboratories { get; }
    IMedicalTreatmentRepository MedicalTreatments { get; }
    IMedicineMovementRepository MedicineMovements { get; }
    IMedicinePartnerRepository MedicinePartners { get; }
    IMedicineRepository Medicines { get; }
    IPartnerRepository Partners { get; }
    IPartnerTypeRepository PartnerTypes { get; }
    IPetRepository Pets { get; }
    IQuoteRepository Quotes { get; }
    ISpecialityRepository Specialities { get; }
    ISpecieRepository Species { get; }
    ITypeMovementRepository TypeMovements { get; }
    Task<int> SaveAsync();
}
