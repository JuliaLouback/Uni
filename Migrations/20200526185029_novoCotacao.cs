using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class novoCotacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Valor_unitario",
                table: "CotacaoProduto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor_unitario",
                table: "CotacaoProduto");
        }
    }
}
