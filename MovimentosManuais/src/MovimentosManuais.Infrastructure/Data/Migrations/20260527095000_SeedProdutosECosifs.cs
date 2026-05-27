using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovimentosManuais.Infrastructure.Data.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260527095000_SeedProdutosECosifs")]
public partial class SeedProdutosECosifs : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            MERGE dbo.PRODUTO AS Target
            USING (VALUES
                (N'0001', N'Conta Corrente', N'A'),
                (N'0002', N'Conta Poupanca', N'A'),
                (N'0003', N'Credito Pessoal', N'A'),
                (N'0004', N'Financiamento', N'A'),
                (N'0005', N'Seguro Residencial', N'I')
            ) AS Source (COD_PRODUTO, DES_PRODUTO, STA_STATUS)
                ON Target.COD_PRODUTO = Source.COD_PRODUTO
            WHEN MATCHED THEN
                UPDATE SET
                    DES_PRODUTO = Source.DES_PRODUTO,
                    STA_STATUS = Source.STA_STATUS
            WHEN NOT MATCHED THEN
                INSERT (COD_PRODUTO, DES_PRODUTO, STA_STATUS)
                VALUES (Source.COD_PRODUTO, Source.DES_PRODUTO, Source.STA_STATUS);
            """);

        migrationBuilder.Sql(
            """
            MERGE dbo.PRODUTO_COSIF AS Target
            USING (VALUES
                (N'0001', N'11111111111', N'CC0001', N'A'),
                (N'0001', N'11111111112', N'CC0002', N'A'),
                (N'0002', N'22222222221', N'CP0001', N'A'),
                (N'0002', N'22222222222', N'CP0002', N'A'),
                (N'0003', N'33333333331', N'CR0001', N'A'),
                (N'0003', N'33333333332', N'CR0002', N'A'),
                (N'0004', N'44444444441', N'FN0001', N'A'),
                (N'0005', N'55555555551', N'SG0001', N'I')
            ) AS Source (COD_PRODUTO, COD_COSIF, COD_CLASSIFICACAO, STA_STATUS)
                ON Target.COD_PRODUTO = Source.COD_PRODUTO
                AND Target.COD_COSIF = Source.COD_COSIF
            WHEN MATCHED THEN
                UPDATE SET
                    COD_CLASSIFICACAO = Source.COD_CLASSIFICACAO,
                    STA_STATUS = Source.STA_STATUS
            WHEN NOT MATCHED THEN
                INSERT (COD_PRODUTO, COD_COSIF, COD_CLASSIFICACAO, STA_STATUS)
                VALUES (Source.COD_PRODUTO, Source.COD_COSIF, Source.COD_CLASSIFICACAO, Source.STA_STATUS);
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            DELETE FROM dbo.PRODUTO_COSIF
            WHERE (COD_PRODUTO = N'0001' AND COD_COSIF IN (N'11111111111', N'11111111112'))
               OR (COD_PRODUTO = N'0002' AND COD_COSIF IN (N'22222222221', N'22222222222'))
               OR (COD_PRODUTO = N'0003' AND COD_COSIF IN (N'33333333331', N'33333333332'))
               OR (COD_PRODUTO = N'0004' AND COD_COSIF = N'44444444441')
               OR (COD_PRODUTO = N'0005' AND COD_COSIF = N'55555555551');
            """);

        migrationBuilder.Sql(
            """
            DELETE FROM dbo.PRODUTO
            WHERE COD_PRODUTO IN (N'0001', N'0002', N'0003', N'0004', N'0005');
            """);
    }
}
