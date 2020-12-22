using Inlog.Frota.Model;

namespace Inlog.Frota.Presentation.Contract
{
    public class VeiculoUnitarioDataContract
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ProcessingTime { get; set; }
        public Veiculo Object { get; set; }
    }
}