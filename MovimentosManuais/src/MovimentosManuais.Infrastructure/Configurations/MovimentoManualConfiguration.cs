using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovimentosManuais.Domain.Entities;

namespace MovimentosManuais.Infrastructure.Configurations;

public sealed class MovimentoManualConfiguration : IEntityTypeConfiguration<MovimentoManual>
{
    public void Configure(EntityTypeBuilder<MovimentoManual> builder)
    {
        builder.ToTable("MOVIMENTO_MANUAL");

        builder.HasKey(x => new
        {
            x.Mes,
            x.Ano,
            x.NumeroLancamento,
            x.CodigoProduto,
            x.CodigoCosif
        });

        builder.Property(x => x.Mes)
            .HasColumnName("DAT_MES")
            .HasPrecision(2, 0)
            .IsRequired();

        builder.Property(x => x.Ano)
            .HasColumnName("DAT_ANO")
            .HasPrecision(4, 0)
            .IsRequired();

        builder.Property(x => x.NumeroLancamento)
            .HasColumnName("NUM_LANCAMENTO")
            .HasPrecision(18, 0)
            .IsRequired();

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

        builder.Property(x => x.Valor)
            .HasColumnName("VAL_VALOR")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasColumnName("DES_DESCRICAO")
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.DataMovimento)
            .HasColumnName("DAT_MOVIMENTO")
            .IsRequired();

        builder.Property(x => x.CodigoUsuario)
            .HasColumnName("COD_USUARIO")
            .HasMaxLength(15)
            .HasColumnType("varchar(15)")
            .IsRequired();

        builder.HasOne(x => x.ProdutoCosif)
            .WithMany(x => x.Movimentos)
            .HasForeignKey(x => new
            {
                x.CodigoProduto,
                x.CodigoCosif
            })
            .OnDelete(DeleteBehavior.Restrict);
    }
}
