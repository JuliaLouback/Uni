using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class CotacaoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CotacaoProduto",
                columns: table => new
                {
                    Id_vendaProduto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Cotacao_Id_cotacao = table.Column<int>(nullable: false),
                    Produto_Id_produto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacaoProduto", x => x.Id_vendaProduto);
                    table.ForeignKey(
                        name: "FK_CotacaoProduto_Venda_Cotacao_Id_cotacao",
                        column: x => x.Cotacao_Id_cotacao,
                        principalTable: "Venda",
                        principalColumn: "Id_venda",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CotacaoProduto_Produto_Produto_Id_produto",
                        column: x => x.Produto_Id_produto,
                        principalTable: "Produto",
                        principalColumn: "Id_produto",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CotacaoProduto_Cotacao_Id_cotacao",
                table: "CotacaoProduto",
                column: "Cotacao_Id_cotacao");

            migrationBuilder.CreateIndex(
                name: "IX_CotacaoProduto_Produto_Id_produto",
                table: "CotacaoProduto",
                column: "Produto_Id_produto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotacaoProduto");
        }
    }
}
