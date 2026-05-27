using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovimentosManuais.Infrastructure.Data.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260527084000_AddMovimentoManualListarProcedure")]
public partial class AddMovimentoManualListarProcedure : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            EXEC(N'
            CREATE OR ALTER PROCEDURE dbo.sp_MovimentoManual_Listar
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT
                    MM.DAT_MES               AS Mes,
                    MM.DAT_ANO               AS Ano,
                    MM.NUM_LANCAMENTO        AS NumeroLancamento,
                    MM.COD_PRODUTO           AS CodigoProduto,
                    P.DES_PRODUTO            AS DescricaoProduto,
                    MM.COD_COSIF             AS CodigoCosif,
                    PC.COD_CLASSIFICACAO     AS DescricaoCosif,
                    MM.VAL_VALOR             AS Valor,
                    MM.DES_DESCRICAO         AS Descricao,
                    MM.DAT_MOVIMENTO         AS DataMovimento,
                    MM.COD_USUARIO           AS CodigoUsuario
                FROM MOVIMENTO_MANUAL MM
                INNER JOIN PRODUTO P
                    ON P.COD_PRODUTO = MM.COD_PRODUTO
                INNER JOIN PRODUTO_COSIF PC
                    ON PC.COD_PRODUTO = MM.COD_PRODUTO
                    AND PC.COD_COSIF = MM.COD_COSIF
                ORDER BY
                    MM.DAT_MES,
                    MM.DAT_ANO,
                    MM.NUM_LANCAMENTO;
            END')
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            DROP PROCEDURE IF EXISTS dbo.sp_MovimentoManual_Listar
            """);
    }
}
