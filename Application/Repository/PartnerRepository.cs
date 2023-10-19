
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
{
    protected readonly ApiContext _context;

    public PartnerRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Partner>> GetAllAsync()
    {
        return await _context.Partners
        .ToListAsync();
    }

    public override async Task<Partner> GetByIdAsync(int id)
    {
        return await _context.Partners
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<(int totalRegistros, IEnumerable<Partner> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Partners as IQueryable<Partner>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            /* .Include(p => p.Pets) */
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    public async Task<IEnumerable<object>> VeterinarioCirujanoVascular()
    {
        return await (
            from p in _context.Partners
            join e in _context.Specialities on p.SpecialtyIdFk equals e.Id
            where e.Name == "Cirujano Vascular"
            select new
            {
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                Address = p.Address
            }
        ).ToListAsync();


    }

    public async Task<(int totalRegistros, List<Partner> registros)> VeterinarioCirujanoVascular(int pageIndex, int pageSize, string search)
    {
        var query = from p in _context.Partners
                    join e in _context.Specialities on p.SpecialtyIdFk equals e.Id
                    where e.Name == "Cirujano Vascular"
                    select p;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Name);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    public async Task<IEnumerable<object>> PropietarioYMascota()
    {
        return await (
            from p in _context.Partners
            join pt in _context.PartnerTypes on p.PartnerTypeIdFk equals pt.Id
            join es in _context.Specialities on p.SpecialtyIdFk equals es.Id
            where pt.Name.Contains("Cliente")
            where es.Name.Contains("Cliente")
            select new
            {
                Name = p.Name,
                Pets = (
                    from pet in _context.Pets
                    join esp in _context.Species on pet.SpeciesIdFk equals esp.Id
                    where pet.UserOwnerId == p.Id
                    select new
                    {
                        Name = pet.Name,
                        Birth = pet.DateBirth,
                        Especies = esp.Name
                    }
                ).ToList()
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, Object registros)> PropietarioYMascota(int pageIndex, int pageSize, string search)
    {
        var query = from p in _context.Partners
                    join pt in _context.PartnerTypes on p.PartnerTypeIdFk equals pt.Id
                    join es in _context.Specialities on p.SpecialtyIdFk equals es.Id
                    where pt.Name.Contains("Cliente")
                    where es.Name.Contains("Cliente")
                    select new
                    {
                        Name = p.Name,
                        Pets = (
                            from pet in _context.Pets
                            join esp in _context.Species on pet.SpeciesIdFk equals esp.Id
                            where pet.UserOwnerId == p.Id
                            select new
                            {
                                Name = pet.Name,
                                Birth = pet.DateBirth,
                                Especies = esp.Name
                            }
                        ).ToList()
                    };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Name);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<object>> MascotasAtendidasPorVeterinario(string NameVeterinary)
    {
        return await (
            from p in _context.Partners
            join e in _context.Specialities on p.SpecialtyIdFk equals e.Id
            join type in _context.PartnerTypes on p.PartnerTypeIdFk equals type.Id
            where type.Name.Contains("Veterinario")
            where p.Name.Contains(NameVeterinary)
            where (
                from q in _context.Quotes
                join pet in _context.Pets on q.PetIdFk equals pet.Id
                where q.VeterinarianIdFk == p.Id
                select pet
            ).Any()
            select new
            {
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                Address = p.Address,
                pets = (
                    from q in _context.Quotes
                    join pet in _context.Pets on q.PetIdFk equals pet.Id
                    where q.VeterinarianIdFk == p.Id
                    select new
                    {
                        NamePet = pet.Name,
                        Birth = pet.DateBirth,
                        DayDate = q.Date,
                        HourDate = q.Hour

                    }
                ).ToList()
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> MascotasAtendidasPorVeterinario(int pageIndex, int pageSize, string search, string NameVeterinary)
    {
        var query = from p in _context.Partners
                    join e in _context.Specialities on p.SpecialtyIdFk equals e.Id
                    join type in _context.PartnerTypes on p.PartnerTypeIdFk equals type.Id
                    where type.Name.Contains("Veterinario")
                    where p.Name.Contains(NameVeterinary)
                    where (
                        from q in _context.Quotes
                        join pet in _context.Pets on q.PetIdFk equals pet.Id
                        where q.VeterinarianIdFk == p.Id
                        select pet
                    ).Any()
                    select new
                    {
                        Name = p.Name,
                        Email = p.Email,
                        Phone = p.Phone,
                        Address = p.Address,
                        pets = (
                            from q in _context.Quotes
                            join pet in _context.Pets on q.PetIdFk equals pet.Id
                            where q.VeterinarianIdFk == p.Id
                            select new
                            {
                                NamePet = pet.Name,
                                Birth = pet.DateBirth,
                                DayDate = q.Date,
                                HourDate = q.Hour

                            }
                        ).ToList()
                    };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Name);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }


    public async Task<IEnumerable<object>> ProvVendeXMedicamento(string NameMedicine)
    {
        return await (
            from p in _context.Partners
            join mp in _context.MedicinePartners on p.Id equals mp.PartnerIdFk
            join med in _context.Medicines on mp.MedicineIdFk equals med.Id
            join type in _context.PartnerTypes on p.PartnerTypeIdFk equals type.Id
            where type.Name.Contains("Proveedor")
            where med.Name.Equals(NameMedicine)
            where (
                from MedP in _context.MedicinePartners
                where MedP.PartnerIdFk == p.Id
                select MedP
            ).Any()
            select new
            {
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                Address = p.Address
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> ProvVendeXMedicamento(int pageIndex, int pageSize, string search, string NameMedicine)
    {
        var query = from p in _context.Partners
                    join mp in _context.MedicinePartners on p.Id equals mp.PartnerIdFk
                    join med in _context.Medicines on mp.MedicineIdFk equals med.Id
                    join type in _context.PartnerTypes on p.PartnerTypeIdFk equals type.Id
                    where type.Name.Contains("Proveedor")
                    where med.Name.Equals(NameMedicine)
                    where (
                        from MedP in _context.MedicinePartners
                        where MedP.PartnerIdFk == p.Id
                        select MedP
                    ).Any()
                    select new
                    {
                        Name = p.Name,
                        Email = p.Email,
                        Phone = p.Phone,
                        Address = p.Address
                    };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Name);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

