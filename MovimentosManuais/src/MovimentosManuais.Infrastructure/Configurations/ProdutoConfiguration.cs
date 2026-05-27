using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;

namespace MovimentosManuais.Infrastructure.Configurations;

public sealed class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("PRODUTO");

        builder.HasKey(x => x.CodigoProduto);

        builder.Property(x => x.CodigoProduto)
            .HasColumnName("COD_PRODUTO")
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasColumnName("DES_PRODUTO")
            .HasMaxLength(30);

        builder.Property(x => x.Status)
            .HasColumnName("STA_STATUS")
            .HasMaxLength(1)
            .HasConversion(
                status => status.HasValue ? ((char)status.Value).ToString() : null,
                value => string.IsNullOrWhiteSpace(value)
                    ? null
                    : (StatusRegistro)value[0]);
    }
}
