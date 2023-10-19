using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class ApiContextSeed
{
    public static async Task SeedAsync(ApiContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            /* var ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); */

            if (!context.TypeMovements.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerTypeMovement = new StreamReader("../Persistence/Data/Csvs/TypeMovement.csv"))
                {
                    using (var csv = new CsvReader(readerTypeMovement, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<TypeMovement>();
                        context.TypeMovements.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if (!context.Species.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerSpecies = new StreamReader("../Persistence/Data/Csvs/Specie.csv"))
                {
                    using (var csv = new CsvReader(readerSpecies, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Specie>();
                        context.Species.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if (!context.Specialities.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerSpeciality = new StreamReader("../Persistence/Data/Csvs/Speciality.csv"))
                {
                    using (var csv = new CsvReader(readerSpeciality, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Speciality>();
                        context.Specialities.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Roles.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerRol = new StreamReader("../Persistence/Data/Csvs/Rol.csv"))
                {
                    using (var csv = new CsvReader(readerRol, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Rol>();
                        context.Roles.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.PartnerTypes.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerPartnerType = new StreamReader("../Persistence/Data/Csvs/PartnerType.csv"))
                {
                    using (var csv = new CsvReader(readerPartnerType, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<PartnerType>();
                        context.PartnerTypes.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Laboratories.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerLaboratory = new StreamReader("../Persistence/Data/Csvs/Laboratory.csv"))
                {
                    using (var csv = new CsvReader(readerLaboratory, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Laboratory>();
                        context.Laboratories.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Genders.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerGender = new StreamReader("../Persistence/Data/Csvs/Gender.csv"))
                {
                    using (var csv = new CsvReader(readerGender, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Gender>();
                        context.Genders.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Breeds.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerBreed = new StreamReader("../Persistence/Data/Csvs/Breed.csv"))
                {
                    using (var csv = new CsvReader(readerBreed, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Breed>();
                        context.Breeds.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Partners.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerPartner = new StreamReader("../Persistence/Data/Csvs/Partner.csv"))
                {
                    using (var csv = new CsvReader(readerPartner, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<Partner>();

                        List<Partner> entidad = new List<Partner>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Partner
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Email = item.Email,
                                Phone = item.Phone,
                                Address = item.Address,
                                SpecialtyIdFk = item.SpecialtyIdFk,
                                GenderIdFk = item.GenderIdFk,
                                PartnerTypeIdFk = item.PartnerTypeIdFk,
                            });
                        }

                        context.Partners.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Pets.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerPet = new StreamReader("../Persistence/Data/Csvs/Pet.csv"))
                {
                    /* using (var csv = new CsvReader(readerPet, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerPet, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<Pet>();
                        List<Pet> entidad = new List<Pet>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Pet
                            {
                                Id = item.Id,
                                Name = item.Name,
                                DateBirth = item.DateBirth,
                                UserOwnerId = item.UserOwnerId,
                                SpeciesIdFk = item.SpeciesIdFk,
                                BreedIdFk = item.BreedIdFk,
                            });
                        }

                        context.Pets.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Medicines.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerMedicine = new StreamReader("../Persistence/Data/Csvs/Medicine.csv"))
                {
                    /* using (var csv = new CsvReader(readerMedicine, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerMedicine, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<Medicine>();

                        List<Medicine> entidad = new List<Medicine>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Medicine
                            {
                                Id = item.Id,
                                Name = item.Name,
                                QuantityAvalible = item.QuantityAvalible,
                                Price = item.Price,
                                LaboratoryIdFk = item.LaboratoryIdFk,
                            });
                        }

                        context.Medicines.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.MedicineMovements.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerMedicineMovement = new StreamReader("../Persistence/Data/Csvs/MedicineMovement.csv"))
                {
                    /* using (var csv = new CsvReader(readerMedicineMovement, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerMedicineMovement, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<MedicineMovement>();

                        List<MedicineMovement> entidad = new List<MedicineMovement>();
                        foreach (var item in list)
                        {
                            entidad.Add(new MedicineMovement
                            {
                                Id = item.Id,
                                Quantity = item.Quantity,
                                DateMovement = item.DateMovement,
                                TypeMovementFk = item.TypeMovementFk,
                                PartnerIdFk = item.PartnerIdFk,
                            });
                        }

                        context.MedicineMovements.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.DetailMovements.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerDetailMovement = new StreamReader("../Persistence/Data/Csvs/DetailMovement.csv"))
                {
                    /* using (var csv = new CsvReader(readerDetailMovement, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerDetailMovement, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<DetailMovement>();

                        List<DetailMovement> entidad = new List<DetailMovement>();
                        foreach (var item in list)
                        {
                            entidad.Add(new DetailMovement
                            {
                                Id = item.Id,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                MedicineIdFk = item.MedicineIdFk,
                                MedicineMovementIdFk = item.MedicineMovementIdFk,
                            });
                        }

                        context.DetailMovements.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Quotes.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerQuote = new StreamReader("../Persistence/Data/Csvs/Quote.csv"))
                {
                    /* using (var csv = new CsvReader(readerQuote, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerQuote, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<Quote>();

                        List<Quote> entidad = new List<Quote>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Quote
                            {
                                Id = item.Id,
                                Date = item.Date,
                                Hour = item.Hour,
                                Reason = item.Reason,
                                PetIdFk = item.PetIdFk,
                                VeterinarianIdFk = item.VeterinarianIdFk,
                            });
                        }

                        context.Quotes.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.MedicalTreatments.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerMedicalTreatment = new StreamReader("../Persistence/Data/Csvs/MedicalTreatment.csv"))
                {
                    /* using (var csv = new CsvReader(readerMedicalTreatment, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerMedicalTreatment, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<MedicalTreatment>();

                        List<MedicalTreatment> entidad = new List<MedicalTreatment>();
                        foreach (var item in list)
                        {
                            entidad.Add(new MedicalTreatment
                            {
                                Id = item.Id,
                                DateStartTreatment = item.DateStartTreatment,
                                QuoteIdFk = item.QuoteIdFk
                            });
                        }

                        context.MedicalTreatments.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.DescriptionMedicalTreatments.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerDescriptionMedicalTreatment = new StreamReader("../Persistence/Data/Csvs/DescriptionMedicalTreatment.csv"))
                {
                    /* using (var csv = new CsvReader(readerDescriptionMedicalTreatment, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerDescriptionMedicalTreatment, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<DescriptionMedicalTreatment>();

                        List<DescriptionMedicalTreatment> entidad = new List<DescriptionMedicalTreatment>();
                        foreach (var item in list)
                        {
                            entidad.Add(new DescriptionMedicalTreatment
                            {
                                Id = item.Id,
                                Dose = item.Dose,
                                AdministrationDate = item.AdministrationDate,
                                Observation = item.Observation,
                                MedicineIdFk = item.MedicineIdFk,
                                MedicalTreatmentIdFk = item.MedicalTreatmentIdFk
                            });
                        }

                        context.DescriptionMedicalTreatments.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.MedicinePartners.Any())
            {
                /* Console.WriteLine("ruta:"+ruta); */
                using (var readerMedicinePartner = new StreamReader("../Persistence/Data/Csvs/MedicinePartner.csv"))
                {
                    /* using (var csv = new CsvReader(readerMedicinePartner, CultureInfo.InvariantCulture)) */
                    using (var csv = new CsvReader(readerMedicinePartner, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        var list = csv.GetRecords<MedicinePartner>();

                        List<MedicinePartner> entidad = new List<MedicinePartner>();
                        foreach (var item in list)
                        {
                            entidad.Add(new MedicinePartner
                            {
                                Id = item.Id,
                                MedicineIdFk = item.MedicineIdFk,
                                PartnerIdFk = item.PartnerIdFk,
                            });
                        }

                        context.MedicinePartners.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApiContext>();
            logger.LogError(ex.Message);
        }
    }
}
