
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PetRepository : GenericRepository<Pet>, IPetRepository
{
    protected readonly ApiContext _context;

    public PetRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _context.Pets
        .ToListAsync();
    }

    public override async Task<Pet> GetByIdAsync(int id)
    {
        return await _context.Pets
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            /* .Include(p => p) */
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<Object>> MascotasFelinos()
    {
        return await (
            from pet in _context.Pets
            join s in _context.Species on pet.SpeciesIdFk equals s.Id
            where s.Name.Contains("Felino")
            select new
            {
                Name = pet.Name,
                Birth = pet.DateBirth,
                Species = s.Name
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, Object registros)> MascotasFelinos(int pageIndex, int pageSize, string search)
    {
        var query = from pet in _context.Pets
                    join s in _context.Species on pet.SpeciesIdFk equals s.Id
                    where s.Name.Contains("Felino")
                    select new
                    {
                        Name = pet.Name,
                        Birth = pet.DateBirth,
                        Species = s.Name
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
    public async Task<IEnumerable<Object>> MascotasVacunadasPrimerTrim2023()
    {
        DateOnly fechaInicio = DateOnly.FromDateTime(new DateTime(2023, 1, 1));
        DateOnly fechaFin = DateOnly.FromDateTime(new DateTime(2023, 03, 31));
        return await (
            from pet in _context.Pets
            join q in _context.Quotes on pet.Id equals q.PetIdFk
            where q.Reason.Contains("Vacunacion")
            where q.Date >= fechaInicio && q.Date <= fechaFin
            select new
            {
                Name = pet.Name,
                Birth = pet.DateBirth,
                Reason = q.Reason,
                Date = q.Date
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> MascotasVacunadasPrimerTrim2023(int pageIndex, int pageSize, string search)
    {
        DateOnly fechaInicio = DateOnly.FromDateTime(new DateTime(2023, 1, 1));
        DateOnly fechaFin = DateOnly.FromDateTime(new DateTime(2023, 03, 31));
        var query =
            from pet in _context.Pets
            join q in _context.Quotes on pet.Id equals q.PetIdFk
            where q.Reason.Contains("Vacunacion")
            where q.Date >= fechaInicio && q.Date <= fechaFin
            select new
            {
                Name = pet.Name,
                Birth = pet.DateBirth,
                Reason = q.Reason,
                Date = q.Date
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
    public async Task<IEnumerable<object>> EspecieYMascota()
    {
        return await (
            from e in _context.Species
            select new
            {
                NameSpecies = e.Name,
                Pets = (
                    from pet in _context.Pets
                    where pet.SpeciesIdFk == e.Id
                    select new
                    {
                        Name = pet.Name,
                        Birth = pet.DateBirth,
                        Especies = e.Name
                    }
                ).ToList()
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> EspecieYMascota(int pageIndex, int pageSize, string search)
    {
        var query =
            from e in _context.Species
            select new
            {
                NameSpecies = e.Name,
                Pets = (
                    from pet in _context.Pets

                    where pet.SpeciesIdFk == e.Id
                    select new
                    {
                        Id = pet.Id,
                        Name = pet.Name,
                        Birth = pet.DateBirth,
                        Especies = e.Name
                    }).ToList()
            };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NameSpecies.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.NameSpecies);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }


    public async Task<IEnumerable<object>> GoldenRetrieverMasDueño()
    {
        return await (
            from e in _context.Pets
            join br in _context.Breeds on e.BreedIdFk equals br.Id
            where br.Name.Equals("Golden Retriever")
            select new
            {
                NamePet = e.Name,
                Owner = (
                    from p in _context.Partners
                    join type in _context.PartnerTypes on p.PartnerTypeIdFk equals type.Id
                    join sp in _context.Specialities on p.SpecialtyIdFk equals sp.Id
                    where type.Name.Equals("Cliente")
                    where sp.Name.Equals("Cliente")
                    where e.UserOwnerId.Equals(p.Id)
                    select new
                    {
                        NameOwner = p.Name,
                        PhoneOwner = p.Phone,
                        EmailOwner = p.Email,
                    }
                ).ToList()
            }
        ).ToListAsync();
    }


    public async Task<IEnumerable<object>> MascotasXRaza()
    {
        return await (
            from br in _context.Breeds
            where (
                from pe in _context.Pets
                where pe.BreedIdFk == br.Id
                select pe
            ).Any()
            select new
            {
                NameBreed = br.Name,
                Pets = (
                    from p in _context.Pets
                    where p.BreedIdFk.Equals(br.Id)
                    select new
                    {
                        NamePet = p.Name,
                        Birth = p.DateBirth
                    }
                ).ToList()
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> GoldenRetrieverMasDueño(int pageIndex, int pageSize, string search)
    {
        var query = from e in _context.Pets
                    join br in _context.Breeds on e.BreedIdFk equals br.Id
                    where br.Name.Equals("Golden Retriever")
                    select new
                    {
                        NamePet = e.Name,
                        Owner = (
                            from p in _context.Partners
                            join type in _context.PartnerTypes on p.PartnerTypeIdFk equals type.Id
                            join sp in _context.Specialities on p.SpecialtyIdFk equals sp.Id
                            where type.Name.Equals("Cliente")
                            where sp.Name.Equals("Cliente")
                            where e.UserOwnerId.Equals(p.Id)
                            select new
                            {
                                NameOwner = p.Name,
                                PhoneOwner = p.Phone,
                                EmailOwner = p.Email,
                            }
                        ).ToList()
                    };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NamePet.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.NamePet);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> MascotasXRaza(int pageIndex, int pageSize, string search)
    {
        var query = from br in _context.Breeds
                    where (
                        from pe in _context.Pets
                        where pe.BreedIdFk == br.Id
                        select pe
                    ).Any()
                    select new
                    {
                        NameBreed = br.Name,
                        Pets = (
                            from p in _context.Pets
                            where p.BreedIdFk.Equals(br.Id)
                            select new
                            {
                                NamePet = p.Name,
                                Birth = p.DateBirth
                            }
                        ).ToList()
                    };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NameBreed.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.NameBreed);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}
