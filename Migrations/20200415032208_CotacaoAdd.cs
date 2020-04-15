using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class CotacaoAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CotacaoProduto_Venda_Cotacao_Id_cotacao",
                table: "CotacaoProduto");

          
            migrationBuilder.AddForeignKey(
                name: "FK_CotacaoProduto_Cotacao_Cotacao_Id_cotacao",
                table: "CotacaoProduto",
                column: "Cotacao_Id_cotacao",
                principalTable: "Cotacao",
                principalColumn: "Id_cotacao",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CotacaoProduto_Cotacao_Cotacao_Id_cotacao",
                table: "CotacaoProduto");

            

            migrationBuilder.AddForeignKey(
                name: "FK_CotacaoProduto_Venda_Cotacao_Id_cotacao",
                table: "CotacaoProduto",
                column: "Cotacao_Id_cotacao",
                principalTable: "Venda",
                principalColumn: "Id_venda",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
