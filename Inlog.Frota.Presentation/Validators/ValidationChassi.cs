using Inlog.Frota.Model;
using Inlog.Frota.Presentation.Contract;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Net;

namespace Inlog.Frota.Presentation.Validators
{   

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidationChassi : ValidationAttribute
    {
        private readonly string ApiBaseUrl = ConfigurationManager.AppSettings["urlBaseApi"];

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var veiculo = this.BuscarPorChassi(Convert.ToString(value));

                if (veiculo.Id > 0)
                {
                    return new ValidationResult("Existe veículo com este número de chassi.");
                }
            }
            return ValidationResult.Success;
        }


        private Veiculo BuscarPorChassi(string chassi)
        {



            string MetodoPath = string.Format("Veiculo/BuscaPorChassi/{0}", chassi); //caminho do método a ser chamado

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

    }
}