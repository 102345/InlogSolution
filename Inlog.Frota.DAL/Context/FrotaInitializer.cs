using Inlog.Frota.Model;
using System.Collections.Generic;

namespace Inlog.Frota.DAL.Context
{
    public class FrotaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FrotaContext>
    {
        protected override void Seed(FrotaContext context)
        {
            var veiculos = new List<Veiculo>
            {
                new Veiculo { Chassi="ABC1234",Tipo = 0 , NroPassageiros = 2, Cor = "Verde"},
                new Veiculo { Chassi = "DEFRT7890", Tipo = 1, NroPassageiros = 42, Cor = "Branco" }

            };

            veiculos.ForEach(v => context.Veiculos.Add(v));
            context.SaveChanges();
        }

    }
}
