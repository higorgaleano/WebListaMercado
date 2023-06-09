using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebListaMercado.Models;

namespace WebListaMercado.Data.Map
{
    public class ComprasMap : IEntityTypeConfiguration<ComprasModel>
    {
        public void Configure(EntityTypeBuilder<ComprasModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
        }
    }
}
