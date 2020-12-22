using System;
using Inlog.Frota.Model;
using Inlog.Frota.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Inlog.Frota.Service.Interface;
using Inlog.Frota.DAL.Interface.Repositories;
using System.Collections.Generic;

namespace Inlog.Frota.Test
{
    [TestClass]
    public class TesteVeiculo
    {
        private Mock<IVeiculoRepository> _veiculoRepository;
        private IVeiculoService _veiculoService;

        [TestInitialize]
        public void Initialize()
        {
            _veiculoRepository = new Mock<IVeiculoRepository>();

            _veiculoService = new VeiculoService(_veiculoRepository.Object);


        }

        [TestMethod]
        public void ListarVeiculosSucesso()
        {
            var veiculo = new Veiculo()
            {
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            var veiculo1 = new Veiculo()
            {
                Chassi = "AAAAA",
                Cor = "Preto",
                NroPassageiros = 42,
                Tipo = 0
            };

            List<Veiculo> veiculos = new List<Veiculo>();

            veiculos.Add(veiculo);
            veiculos.Add(veiculo1);


            _veiculoRepository.Setup(x => x.GetAll()).Returns(veiculos);

            var veiculos1 = _veiculoService.ListarVeiculos();

            Assert.IsNotNull(veiculos1);

        }



        [TestMethod]
        public void ListarVeiculosFalha()
        {
           

            List<Veiculo> veiculos = new List<Veiculo>();


            _veiculoRepository.Setup(x => x.GetAll()).Throws(new Exception());

            var veiculos1 = _veiculoService.ListarVeiculos();

            Assert.IsNull(veiculos1);

        }

        [TestMethod]
        public void IncluirVeiculoSucessoNovo()
        {
            var veiculo = new Veiculo()
            {
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            _veiculoRepository.Setup(x => x.Add(It.IsAny<Veiculo>())).Returns(true);

            bool ret = _veiculoService.InserirVeiculo(veiculo);


            Assert.IsTrue(ret);

            //var veiculoService = new VeiculoService();

            //bool ret = false;

            //veiculoService.InserirVeiculo(veiculo);

            //ret = true;
            //Assert.AreEqual(true, ret);

        }


        [TestMethod]
        public void IncluirVeiculoFalhaNovo()
        {
            var veiculo = new Veiculo()
            {
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            _veiculoRepository.Setup(x => x.Add(It.IsAny<Veiculo>())).Throws(new Exception());

            bool ret = _veiculoService.InserirVeiculo(veiculo);


            Assert.IsFalse(ret);

            //var veiculoService = new VeiculoService();

            //bool ret = false;

            //veiculoService.InserirVeiculo(veiculo);

            //ret = true;
            //Assert.AreEqual(true, ret);

        }




        [TestMethod]
        public void BuscarVeiculoQueExiste()
        {

            var veiculo = new VeiculoService().BuscarVeiculo("AQWE567");

            Assert.AreNotEqual(null, veiculo);


        }

        [TestMethod]
        public void PesquisarVeiculoExistente()
        {
            bool ret = false;

            var veiculos = new VeiculoService().PesquisarVeiculos("BC1");

            if (veiculos.Count > 0) ret = true;

            Assert.AreEqual(true, ret);
        }

        [TestMethod]
        public void PesquisarVeiculoNãoExistente()
        {
            var veiculos = new VeiculoService().PesquisarVeiculos("aaa");

            int conta = veiculos.Count;

            Assert.AreEqual(0, conta);
        }


        [TestMethod]
        public void BuscarVeiculoQueNaoExiste()
        {

            var veiculo = new VeiculoService().BuscarVeiculo("xxxxx");

            Assert.AreEqual(0, veiculo.Id);


        }


        [TestMethod]
        public void ExcluirSucessoVeiculo()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            _veiculoRepository.Setup(x => x.Remove(It.IsAny<Veiculo>())).Returns(true);

            bool ret = _veiculoService.ExcluirVeiculo(veiculo.Id);


            Assert.IsTrue(ret);


            //var veiculo = new VeiculoService().BuscarVeiculo("AQWE567");

            //bool ret = false;
            //new VeiculoService().ExcluirVeiculo(veiculo.Id);

            //ret = true;

            //Assert.AreEqual(true, ret);

        }


        [TestMethod]
        public void ExcluirFalhaVeiculo()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            _veiculoRepository.Setup(x => x.Remove(It.IsAny<Veiculo>())).Throws(new Exception());

            bool ret = _veiculoService.ExcluirVeiculo(veiculo.Id);


            Assert.IsFalse(ret);


            //var veiculo = new VeiculoService().BuscarVeiculo("AQWE567");

            //bool ret = false;
            //new VeiculoService().ExcluirVeiculo(veiculo.Id);

            //ret = true;

            //Assert.AreEqual(true, ret);

        }



        [TestMethod]
        public void AtualizarSucessoVeiculo()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            _veiculoRepository.Setup(x => x.Update(It.IsAny<Veiculo>())).Returns(true);

            bool ret = _veiculoService.AtualizarVeiculo(veiculo);


            Assert.IsTrue(ret);

        }


        [TestMethod]
        public void AtualizarFalhaVeiculo()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            _veiculoRepository.Setup(x => x.Update(It.IsAny<Veiculo>())).Throws(new Exception());

            bool ret = _veiculoService.AtualizarVeiculo(veiculo);

            Assert.IsFalse(ret);


          
        }


    }
}
