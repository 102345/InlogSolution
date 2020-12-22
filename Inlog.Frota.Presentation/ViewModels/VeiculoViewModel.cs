using Inlog.Frota.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Inlog.Frota.Presentation.Validators;

namespace Inlog.Frota.Presentation.ViewModels
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "É necessário informar um chassi.")]
        [ValidationChassi(ErrorMessage = "Existe veículo com este número de chassi.")]
        public string Chassi { get; set; }
        public string Tipo { get; set; }
        public int NroPassageiros { get; set; }
        [Required(ErrorMessage = "É necessário informar uma cor.")]
        public string Cor { get; set; }
        public IEnumerable<Veiculo> ColecaoVeiculos { get; set; }

    }
}