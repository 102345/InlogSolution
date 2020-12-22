using AutoMapper;
using Inlog.Frota.Model;
using Inlog.Frota.Presentation.ViewModels;

namespace Inlog.Frota.Presentation.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {

            CreateMap<VeiculoViewModel, Veiculo>()
                 .ForMember(d => d.Tipo, o => o.MapFrom(s => s.Tipo == "Caminhao" ? 0 : 1));
        }
    }
}