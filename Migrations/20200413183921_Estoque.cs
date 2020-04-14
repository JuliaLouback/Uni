using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class Estoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Estoque_atual",
                table: "Produto",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estoque_atual",
                table: "Produto");
        }
    }
}
