using Inlog.Frota.Model;
using System.Collections.Generic;

namespace Inlog.Frota.Service.Interface
{
    public interface IVeiculoService
    {
        List<Veiculo> ListarVeiculos();
        List<Veiculo> PesquisarVeiculos(string filtroChassi);
        Veiculo BuscarVeiculo(string chassi);
        Veiculo BuscarVeiculo(int id);
        bool InserirVeiculo(Veiculo veiculo);
        bool AtualizarVeiculo(Veiculo veiculo);
        bool ExcluirVeiculo(int id);





    }
}
