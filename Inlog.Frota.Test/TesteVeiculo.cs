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
        public void ListarVeiculosNapExistemSucesso()
        {
          

            List<Veiculo> veiculos = new List<Veiculo>();



            _veiculoRepository.Setup(x => x.GetAll()).Returns(veiculos);

            var veiculos1 = _veiculoService.ListarVeiculos();

            Assert.AreEqual(0, veiculos1.Count);

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

        }


        [TestMethod]
        public void BuscarVeiculoPorChassiSucesso()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            List<Veiculo> veiculos = new List<Veiculo>();

            veiculos.Add(veiculo);


            _veiculoRepository.Setup(x => x.GetAll()).Returns(veiculos);

            var veiculo1 = _veiculoService.BuscarVeiculo("TTTTT");

            Assert.IsNotNull(veiculo1);


        }


        [TestMethod]
        public void BuscarVeiculoPorChassiFalha()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            List<Veiculo> veiculos = new List<Veiculo>();

            veiculos.Add(veiculo);


            _veiculoRepository.Setup(x => x.GetAll()).Throws(new Exception());

            var veiculo1 = _veiculoService.BuscarVeiculo("TTTTT");

            Assert.AreEqual(0, veiculo1.Id);


        }





        [TestMethod]
        public void BuscarVeiculoSucesso()
        {
           

            var veiculo = new Veiculo()
            {  
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };


            _veiculoRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(veiculo);

            var veiculo1 = _veiculoService.BuscarVeiculo(1);

            Assert.IsNotNull(veiculo1);


        }


        [TestMethod]
        public void BuscarVeiculoFalha()
        {


            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };


            _veiculoRepository.Setup(x => x.GetById(It.IsAny<int>())).Throws(new Exception());

            var veiculo1 = _veiculoService.BuscarVeiculo(1);

            Assert.IsNull(veiculo1);


        }

        [TestMethod]
        public void PesquisarVeiculoExisteSucesso()
        {

            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            List<Veiculo> veiculos = new List<Veiculo>();

            veiculos.Add(veiculo);


            _veiculoRepository.Setup(x => x.GetAll()).Returns(veiculos);

            var veiculosRet = _veiculoService.PesquisarVeiculos(veiculo.Chassi);

            Assert.AreEqual(1,veiculosRet.Count);
        }



        [TestMethod]
        public void PesquisarVeiculoNaoExisteSucesso()
        {

            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            List<Veiculo> veiculos = new List<Veiculo>();

            veiculos.Add(veiculo);


            _veiculoRepository.Setup(x => x.GetAll()).Returns(veiculos);

            var veiculosRet = _veiculoService.PesquisarVeiculos("AAAA");

            Assert.AreEqual(0, veiculosRet.Count);
        }

        [TestMethod]
        public void PesquisarVeiculoFalha()
        {
            var veiculo = new Veiculo()
            {
                Id = 1,
                Chassi = "TTTTT",
                Cor = "Bordo",
                NroPassageiros = 2,
                Tipo = 1
            };

            List<Veiculo> veiculos = new List<Veiculo>();

            veiculos.Add(veiculo);


            _veiculoRepository.Setup(x => x.GetAll()).Throws(new Exception());

            var veiculosRet = _veiculoService.PesquisarVeiculos(veiculo.Chassi);

            Assert.IsNull(veiculosRet);
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
