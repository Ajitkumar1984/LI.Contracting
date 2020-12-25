using AutoMapper;
using LI.Contracting.DataContext;
using LI.Contracting.EntityDTO;

namespace LI.Contracting.WebApi
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            
            CreateMap<MGAEntity, MGADTO>().ReverseMap();
            CreateMap<CarrierEntity, CarrierDTO>().ReverseMap();
            CreateMap<AdvisorEntity, AdvisorDTO>().ReverseMap();
            CreateMap<ContractEntity, ContractDTO>().ReverseMap();

        }
    }
}
