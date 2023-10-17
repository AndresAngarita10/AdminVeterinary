
using Domain;

namespace API.Dtos;

public class MedicinePartnerDto : BaseEntity
{
    public int MedicineIdFk {get;set;}
    //public Medicine Medicine {get;set;}
    public int PartnerIdFk {get;set;}
   // public Partner Partner {get;set;}
}

