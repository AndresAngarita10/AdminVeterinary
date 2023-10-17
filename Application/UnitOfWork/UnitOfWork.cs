
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiContext _context;
    private IRolRepository _roles;
    private IUserRepository _users;
    private IBreedRepository _breeds ;
    private IDescriptionMedicalTreatmentRepository _descriptionMedicalTreatments;
    private IDetailMovementRepository _detailMovements;
    private IGenderRepository _genders;
    private ILaboratoryRepository _laboratories ;
    private IMedicalTreatmentRepository _medicalTreatments;
    private IMedicineMovementRepository _medicineMovements ;
    private IMedicinePartnerRepository _medicinePartners;
    private IMedicineRepository _medicines ;
    private IPartnerRepository _partners;
    private IPartnerTypeRepository _partnerTypes ;
    private IPetRepository _pets;
    private IQuoteRepository _quotes;
    private ISpecialityRepository _specialities;
    private ISpecieRepository _species ;
    private ITypeMovementRepository _typeMovements;

    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }

    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    public IBreedRepository Breeds 
    {
        get
        {
            if (_breeds == null)
            {
                _breeds = new BreedRepository(_context);
            }
            return _breeds;
        }
    }

    public IDescriptionMedicalTreatmentRepository DescriptionMedicalTreatments 
    {
        get
        {
            if (_descriptionMedicalTreatments == null)
            {
                _descriptionMedicalTreatments = new DescriptionMedicalTreatmentRepository(_context);
            }
            return _descriptionMedicalTreatments;
        }
    }

    public IDetailMovementRepository DetailMovements 
    {
        get
        {
            if (_detailMovements == null)
            {
                _detailMovements = new DetailMovementRepository(_context);
            }
            return _detailMovements;
        }
    }

    public IGenderRepository Genders 
    {
        get
        {
            if (_genders == null)
            {
                _genders = new GenderRepository(_context);
            }
            return _genders;
        }
    }
    public ILaboratoryRepository Laboratories 
    {
        get
        {
            if (_laboratories == null)
            {
                _laboratories = new LaboratoryRepository(_context);
            }
            return _laboratories;
        }
    }

    public IMedicalTreatmentRepository MedicalTreatments 
    {
        get
        {
            if (_medicalTreatments == null)
            {
                _medicalTreatments = new MedicalTreatmentRepository(_context);
            }
            return _medicalTreatments;
        }
    }

    public IMedicineMovementRepository MedicineMovements 
    {
        get
        {
            if (_medicineMovements == null)
            {
                _medicineMovements = new MedicineMovementRepository(_context);
            }
            return _medicineMovements;
        }
    }

    public IMedicinePartnerRepository MedicinePartners 
    {
        get
        {
            if (_medicinePartners == null)
            {
                _medicinePartners = new MedicinePartnerRepository(_context);
            }
            return _medicinePartners;
        }
    }

    public IMedicineRepository Medicines 
    {
        get
        {
            if (_medicines == null)
            {
                _medicines = new MedicineRepository(_context);
            }
            return _medicines;
        }
    }

    public IPartnerRepository Partners 
    {
        get
        {
            if (_partners == null)
            {
                _partners = new PartnerRepository(_context);
            }
            return _partners;
        }
    }

    public IPartnerTypeRepository PartnerTypes 
    {
        get
        {
            if (_partnerTypes == null)
            {
                _partnerTypes = new PartnerTypeRepository(_context);
            }
            return _partnerTypes;
        }
    }

    public IPetRepository Pets 
    {
        get
        {
            if (_pets == null)
            {
                _pets = new PetRepository(_context);
            }
            return _pets;
        }
    }

    public IQuoteRepository Quotes 
    {
        get
        {
            if (_quotes == null)
            {
                _quotes = new QuoteRepository(_context);
            }
            return _quotes;
        }
    }

    public ISpecialityRepository Specialities 
    {
        get
        {
            if (_specialities == null)
            {
                _specialities = new SpecialityRepository(_context);
            }
            return _specialities;
        }
    }

    public ISpecieRepository Species 
    {
        get
        {
            if (_species == null)
            {
                _species = new SpecieRepository(_context);
            }
            return _species;
        }
    }

    public ITypeMovementRepository TypeMovements 
    {
        get
        {
            if (_typeMovements == null)
            {
                _typeMovements = new TypeMovementRepository(_context);
            }
            return _typeMovements;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
