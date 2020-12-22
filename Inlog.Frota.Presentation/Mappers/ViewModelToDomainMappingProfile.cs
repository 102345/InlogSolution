using AutoMapper;
using Inlog.Frota.Model;
using Inlog.Frota.Presentation.ViewModels;

namespace Inlog.Frota.Presentation.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {


        public ViewModelToDomainMappingProfile()
        {
            CreateMap<Veiculo, VeiculoViewModel>()
            .ForMember(d => d.Tipo, o => o.MapFrom(s => s.Tipo == 1 ? "Onibus" : "Caminhao"));
        }

    }
}