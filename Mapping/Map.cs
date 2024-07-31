using AutoMapper;
using PrimeTech.DataAccess.Model;
using PrimeTech.ViewModels;

namespace PrimeTech.Mapping
{
    public class Map : Profile
    {
        public Map() {
            CreateMap<Company, VmCompany>().ReverseMap();
            CreateMap<Attributes, VmAttributes>().ReverseMap();
            CreateMap<AttributesValue, VmAttributesValue>().ReverseMap();
        }
    }
}
