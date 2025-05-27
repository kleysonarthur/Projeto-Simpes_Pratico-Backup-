using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplesPratico.Models;

namespace SimplesPratico.Data.Map {
    public class ClienteMap : IEntityTypeConfiguration<ClienteModel> {
        public void Configure(EntityTypeBuilder<ClienteModel> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Funcionario);
        }
    }
}
