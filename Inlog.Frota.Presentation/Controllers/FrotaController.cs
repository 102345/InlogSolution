 using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Inlog.Frota.Model;
using Inlog.Frota.Presentation.Contract;
using Inlog.Frota.Presentation.ViewModels;
using Newtonsoft.Json;

namespace Inlog.Frota.Presentation.Controllers
{
    public class FrotaController : Controller
    {
        private readonly string ApiBaseUrl = ConfigurationManager.AppSettings["urlBaseApi"];
        // GET: Livraria
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult IncluirVeiculo(VeiculoViewModel veiculoViewModel)
        {

            if (ModelState.IsValid)
            {
              
                var model = Mapper.Map<VeiculoViewModel, Veiculo>(veiculoViewModel);


                bool ret = this.CriarVeiculo(model);

                if (!ret)
                {
                    TempData["warning"] = "Problemas de gravação de Veiculo!";

                }
                else
                {
                    TempData["success"] = "Veiculo incluido com sucesso.";
                }

                ModelState.Clear();

                return RedirectToAction("ListaVeiculos", "Frota");

            }

           

            return PartialView("_NovoVeiculo", veiculoViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult AtualizarVeiculo(VeiculoViewModel veiculoViewModel)
        {

            //if (ModelState.IsValid)
            //{

                var model = Mapper.Map<VeiculoViewModel, Veiculo>(veiculoViewModel);


                bool ret = this.AtualizarVeiculo(model);

                if (!ret)
                {
                    TempData["warning"] = "Problemas de gravação de Veiculo!";

                }
                else
                {
                    TempData["success"] = "Veiculo atualizado com sucesso.";
                }

                return RedirectToAction("ListaVeiculos", "Frota");

            //}

            //return PartialView("_EditaVeiculo", veiculoViewModel);

        }


        [HttpPost]
        public ActionResult ExcluirVeiculo(VeiculoViewModel veiculoViewModel)
        {



            bool ret = this.DeleteVeiculo(veiculoViewModel.Id);

            if (!ret)
            {
                TempData["warning"] = "Problemas de exclusão de Veiculo!";

            }
            else
            {
                TempData["success"] = "Veiculo excluído com sucesso.";
            }

            return RedirectToAction("ListaVeiculos", "Frota");

        }

        [HttpPost]
        public ActionResult PesquisarVeiculos(VeiculoViewModel veiculoViewModel)
        {
            string pathRecurso = string.Format("ListaFiltradaVeiculos/{0}", veiculoViewModel.Chassi);

            return RedirectToAction(pathRecurso, "Frota");

        }


        [HttpGet]
        public ActionResult ListaFiltradaVeiculos(string id)
        {

            var modelVM = Mapper.Map<IEnumerable<Veiculo>, IEnumerable<VeiculoViewModel>>(Pesquisar(id));

            return View(modelVM);


        }




        [HttpGet]
        public ActionResult ListaVeiculos()
        {

            var modelVM = Mapper.Map<IEnumerable<Veiculo>, IEnumerable<VeiculoViewModel>>(Listar());

            return View(modelVM);


        }

        public PartialViewResult ExibirJanelaExcluiVeiculo(int id)
        {
            var modelVM = Mapper.Map<Veiculo, VeiculoViewModel>(BuscarPorId(id));

            return PartialView("_ExcluiVeiculo", modelVM);

        }


        public PartialViewResult ExibirJanelaEditaVeiculo(int id)
        {

            var modelVM = Mapper.Map<Veiculo, VeiculoViewModel>(BuscarPorId(id));

            return PartialView("_EditaVeiculo", modelVM);


        }


        public PartialViewResult ExibirJanelaPesquisaVeiculo()
        {

            var modelVM = new VeiculoViewModel();

            return PartialView("_PesquisaVeiculo", modelVM);


        }


        public PartialViewResult ExibirJanelaIncluiVeiculo()
        {

            var modelVM = new VeiculoViewModel();


            return PartialView("_NovoVeiculo", modelVM);


        }





        private bool DeleteVeiculo(int id)
        {

            string MetodoPath = string.Format("Veiculo/Delete/{0}", id); //caminho do método a ser chamado
            bool ret = true;
            Uri u = new Uri(ApiBaseUrl + MetodoPath);

            try
            {

                var t = Task.Run(() => Helpers.HttpHelper.DeleteURI(u));
                t.Wait();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ret = false;
                throw;
            }

            return ret;

        }

        private bool CriarVeiculo(Veiculo Veiculo)
        {

            string MetodoPath = "Veiculo/Post"; //caminho do método a ser chamado
            bool ret = true;
            Uri u = new Uri(ApiBaseUrl + MetodoPath);
            var payload = JsonConvert.SerializeObject(Veiculo);

            try
            {
                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => Helpers.HttpHelper.PostURI(u, c));
                t.Wait();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ret = false;
                throw;
            }

            return ret;

        }

        private bool AtualizarVeiculo(Veiculo Veiculo)
        {

            string MetodoPath = "Veiculo/Atualiza"; //caminho do método a ser chamado
            bool ret = true;
            Uri u = new Uri(ApiBaseUrl + MetodoPath);
            var payload = JsonConvert.SerializeObject(Veiculo);

            try
            {
                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => Helpers.HttpHelper.PutURI(u, c));
                t.Wait();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ret = false;
                throw;
            }

            return ret;


        }

        private Veiculo BuscarPorId(int id)
        {



            string MetodoPath = string.Format("Veiculo/BuscaPorId/{0}", id); //caminho do método a ser chamado

            var model = new Veiculo();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<VeiculoUnitarioDataContract>(streamReader.ReadToEnd());

                    if (retorno != null)
                    {
                        model.Id = retorno.Object.Id;
                        model.Chassi = retorno.Object.Chassi;
                        model.NroPassageiros = retorno.Object.NroPassageiros;
                        model.Tipo = retorno.Object.Tipo;
                        model.Cor = retorno.Object.Cor;


                    }

                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                throw e;
            }


            return model;

        }



        private IEnumerable<Veiculo> Listar()
        {



            string MetodoPath = "Veiculo/Lista"; //caminho do método a ser chamado

            var modelVM = new VeiculoViewModel();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<VeiculoDataContract>(streamReader.ReadToEnd());

                    if (retorno != null)
                        modelVM.ColecaoVeiculos = retorno.Object;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                throw e;
            }


            return modelVM.ColecaoVeiculos;

        }


        private IEnumerable<Veiculo> Pesquisar(string chassi)
        {


           // api/v1/Veiculo/PesquisarVeiculos
            string MetodoPath = string.Format("Veiculo/PesquisarVeiculos/{0}",chassi); //caminho do método a ser chamado

            var modelVM = new VeiculoViewModel();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiBaseUrl + MetodoPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var retorno = JsonConvert.DeserializeObject<VeiculoDataContract>(streamReader.ReadToEnd());

                    if (retorno != null)
                        modelVM.ColecaoVeiculos = retorno.Object;
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                throw e;
            }


            return modelVM.ColecaoVeiculos;

        }

    }
}