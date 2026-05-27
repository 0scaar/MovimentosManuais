using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentosManuais.Domain.Entities;
using MovimentosManuais.Domain.Enums;

namespace MovimentosManuais.Infrastructure.Configurations;

public sealed class ProdutoCosifConfiguration : IEntityTypeConfiguration<ProdutoCosif>
{
    public void Configure(EntityTypeBuilder<ProdutoCosif> builder)
    {
        builder.ToTable("PRODUTO_COSIF");

        builder.HasKey(x => new
        {
            x.CodigoProduto,
            x.CodigoCosif
        });

        builder.Property(x => x.CodigoProduto)
            .HasColumnName("COD_PRODUTO")
            .HasMaxLength(4)
            .HasColumnType("char(4)")
            .IsRequired();

        builder.Property(x => x.CodigoCosif)
            .HasColumnName("COD_COSIF")
            .HasMaxLength(11)
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(x => x.CodigoClassificacao)
            .HasColumnName("COD_CLASSIFICACAO")
            .HasMaxLength(6)
            .HasColumnType("char(6)");

        builder.Property(x => x.Status)
            .HasColumnName("STA_STATUS")
            .HasMaxLength(1)
            .HasColumnType("char(1)")
            .HasConversion(
                status => status.HasValue ? ((char)status.Value).ToString() : null,
                value => string.IsNullOrWhiteSpace(value)
                    ? null
                    : (StatusRegistro)value[0]);

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.Cosifs)
            .HasForeignKey(x => x.CodigoProduto)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
