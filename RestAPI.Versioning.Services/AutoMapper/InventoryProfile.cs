using AutoMapper;
using RestAPI.Versioning.Interfaces.Model;
using DB = Common.DB.Model;

namespace RestAPI.Versioning.Services.AutoMapper
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<DB.Item , Item> ();
        }
    }
}
