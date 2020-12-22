using Inlog.Frota.Model;
using System.Collections.Generic;

namespace Inlog.Frota.Presentation.Contract
{
    public class VeiculoDataContract
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ProcessingTime { get; set; }
        public IEnumerable<Veiculo> Object { get; set; }
    }
}