
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        
        CreateMap<Breed,BreedDto>().ReverseMap();
        CreateMap<DescriptionMedicalTreatment, DescriptionMedicalTreatmentDto>().ReverseMap();
        CreateMap<DetailMovement, DetailMovementDto>().ReverseMap();
        CreateMap<Gender,GenderDto>().ReverseMap();
        CreateMap<Laboratory, LaboratoryDto>().ReverseMap();
        CreateMap<MedicalTreatment, MedicalTreatmentDto>().ReverseMap();
        CreateMap<Medicine, MedicineDto>().ReverseMap();
        CreateMap<MedicineMovement, MedicineMovementDto>().ReverseMap();
        CreateMap<MedicinePartner, MedicinePartnerDto>().ReverseMap();
        CreateMap<Partner, PartnerDto>().ReverseMap();
        CreateMap<PartnerType, PartnerTypeDto>().ReverseMap();
        CreateMap<Pet, PetDto>().ReverseMap();
        CreateMap<Quote, QuoteDto>().ReverseMap();
        CreateMap<Speciality, SpecialityDto>().ReverseMap();
        CreateMap<Specie, SpecieDto>().ReverseMap();
        CreateMap<TypeMovement, TypeMovementDto>().ReverseMap();
        CreateMap<Rol, RolDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
