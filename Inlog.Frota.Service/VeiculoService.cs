using System.Collections.Generic;
using Inlog.Frota.DAL.Repositories;
using Inlog.Frota.Model;
using Inlog.Frota.Service.Interface;
using System.Linq;
using Inlog.Frota.DAL.Interface.Repositories;

namespace Inlog.Frota.Service
{
    public class VeiculoService : IVeiculoService
    {
        //private readonly VeiculoRepository _veiculoRepository;

        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoService()
        {
            _veiculoRepository = new VeiculoRepository();
        }

        public VeiculoService(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public bool AtualizarVeiculo(Veiculo veiculo)
        {
            try
            {
                return _veiculoRepository.Update(veiculo);
            }
            catch (System.Exception ex)
            {

                string msg = ex.Message;
                return false;
            }

           
        }

        public Veiculo BuscarVeiculo(string chassi)
        {
            var veiculo = new Veiculo();

            try
            {
                veiculo = _veiculoRepository.GetAll().Where(x => x.Chassi == chassi).SingleOrDefault();


            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
              
            }

            return veiculo;
        }

        public Veiculo BuscarVeiculo(int id)
        {
            try
            {
                return _veiculoRepository.GetById(id);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;

                return null;
            }


            
        }

        public bool ExcluirVeiculo(int id)
        {
            try
            {
                return _veiculoRepository.Remove(_veiculoRepository.GetById(id));
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


           
        }

        public bool InserirVeiculo(Veiculo veiculo)
        {
    
            try
            {
                return _veiculoRepository.Add(veiculo);

            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return false;

            }
        }   
           

        public List<Veiculo> ListarVeiculos()
        {
            try
            {
                return _veiculoRepository.GetAll().OrderBy(x => x.Chassi).ToList();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return null;
            }


           
        }

        public List<Veiculo> PesquisarVeiculos(string filtroChassi)
        {
            try
            {
                return _veiculoRepository.GetAll().Where(p => p.Chassi.Contains(filtroChassi))
                                      .OrderBy(x => x.Chassi).ToList();
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                return null;
            }

          
        }
    }
}
