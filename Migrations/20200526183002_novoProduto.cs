using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class novoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Valor_unitario",
                table: "VendaProduto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor_unitario",
                table: "VendaProduto");
        }
    }
}
