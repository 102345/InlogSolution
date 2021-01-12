using Inlog.Frota.Model;
using Inlog.Frota.Service.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inlog.Frota.Api.Controllers
{
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        /// <summary>
        /// Lista os veiculos da frota
        /// </summary>
        /// <returns>Lista objetos veiculos</returns>
        [HttpGet]
        [Route("api/v1/Veiculo/Lista")]
        public HttpResponseMessage Lista()
        {
            try
            {
                var veiculos = _veiculoService.ListarVeiculos();

                JsonResult.Status = true;
                JsonResult.Object = veiculos;
                JsonResult.Message = "Listado com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }


        }

      

        /// <summary>
        /// Lista veiculo com criterio de pesquisa pelo chassi
        /// </summary>
        /// <param name="id">string - numero do chassi</param>
        /// <returns>Lista de objetos veiuclos</returns>
        [HttpGet]
        [Route("api/v1/Veiculo/PesquisarVeiculos/{id}")]
        public HttpResponseMessage PesquisarVeiculos(string id)
        {
            try
            {
                var veiculos = _veiculoService.PesquisarVeiculos(id);

                JsonResult.Status = true;
                JsonResult.Object = veiculos;
                JsonResult.Message = "Listado com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }


        }



        /// <summary>
        /// Busca veiculo pelo seu identificador
        /// </summary>
        /// <param name="id">int - identificador do veiculo</param>
        /// <returns>objeto - veiculo</returns>
        [HttpGet]
        [Route("api/v1/Veiculo/BuscaPorId/{id}")]
        public HttpResponseMessage BuscaPorId(int id)
        {
            try
            {
                var veiculo = _veiculoService.BuscarVeiculo(id);

                if (veiculo == null)
                {

                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return Request.CreateResponse(HttpStatusCode.NotFound, JsonResult);

                }

                JsonResult.Status = true;
                JsonResult.Object = veiculo;
                JsonResult.Message = "Encontrado com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }


        }




        /// <summary>
        /// Busca veiculo pelo chassi
        /// </summary>
        /// <param name="id">string - numero do chassi</param>
        /// <returns>- objeto veiculo</returns>
        [HttpGet]
        [Route("api/v1/Veiculo/BuscaPorChassi/{id}")]
        public HttpResponseMessage BuscaPorChassi(string id)
        {
            try
            {
                var veiculo = _veiculoService.BuscarVeiculo(id);

                if (veiculo == null)
                {

                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return Request.CreateResponse(HttpStatusCode.NotFound, JsonResult);

                }

                JsonResult.Status = true;
                JsonResult.Object = veiculo;
                JsonResult.Message = "Encontrado com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }


        }

        /// <summary>
        /// Exclui um veiculo especifico
        /// </summary>
        /// <param name="id"> int - id do veiculo</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/v1/Veiculo/Delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var veiculoRet = _veiculoService.BuscarVeiculo(id);

                if (veiculoRet == null)
                {

                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return Request.CreateResponse(HttpStatusCode.NotFound, JsonResult);

                }

                _veiculoService.ExcluirVeiculo(id);

                JsonResult.Status = true;
                JsonResult.Object = null;
                JsonResult.Message = "Excluido com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }

        }

        /// <summary>
        /// Atualiza um veiculo especifio
        /// </summary>
        /// <param name="veiculo"> objeto Veiculo</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/v1/Veiculo/Atualiza")]
        public HttpResponseMessage Atualiza([FromBody]Veiculo veiculo)
        {
            try
            {

                    var veiculoRet = _veiculoService.BuscarVeiculo(veiculo.Chassi);

                if (veiculoRet == null)
                {

                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return Request.CreateResponse(HttpStatusCode.NotFound, JsonResult);

                }

                veiculoRet.Id = veiculo.Id;
                veiculoRet.Chassi = veiculo.Chassi;
                veiculoRet.Cor = veiculo.Cor;
                veiculoRet.Tipo = veiculo.Tipo;
                veiculoRet.NroPassageiros = veiculo.NroPassageiros;


                _veiculoService.AtualizarVeiculo(veiculoRet);

                JsonResult.Status = true;
                JsonResult.Object = null;
                JsonResult.Message = "Atualizado com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }

        }


        /// <summary>
        /// Inclui um novo veiculo
        /// </summary>
        /// <param name="veiculo">objeto veiculo</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/Veiculo/Post")]
        public HttpResponseMessage Post([FromBody]Veiculo veiculo)
        {
            try
            {
               
                var veiculoRet = _veiculoService.BuscarVeiculo(veiculo.Chassi);

                if (veiculoRet != null)
                {

                    JsonResult.Status = false;
                    JsonResult.Message = "Existe veiculo cadastrado com este chassi.";
                    return Request.CreateResponse(HttpStatusCode.NotFound, JsonResult);

                }


                _veiculoService.InserirVeiculo(veiculo);

                JsonResult.Status = true;
                JsonResult.Object = null;
                JsonResult.Message = "Gravado com sucesso";
                return Request.CreateResponse(HttpStatusCode.OK, JsonResult);
            }
            catch (Exception ex)
            {

                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonResult);
            }

        }
    }

}
