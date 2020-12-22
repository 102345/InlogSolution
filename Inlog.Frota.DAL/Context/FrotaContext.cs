using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Inlog.Frota.Model;

namespace Inlog.Frota.DAL.Context
{
     public class FrotaContext : DbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }


        public FrotaContext() :base("FrotaContext")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
