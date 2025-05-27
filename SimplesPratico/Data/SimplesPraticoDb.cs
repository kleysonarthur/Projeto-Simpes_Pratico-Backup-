using Microsoft.EntityFrameworkCore;
using SimplesPratico.Data.Map;
using SimplesPratico.Models;

namespace SimplesPratico.Data {
    public class SimplesPraticoDb : DbContext {
        public SimplesPraticoDb(DbContextOptions<SimplesPraticoDb> options) : base(options) {

        }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ClienteMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
