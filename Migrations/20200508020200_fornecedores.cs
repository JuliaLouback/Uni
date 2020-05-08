using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class fornecedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Inscricao_estadual",
                table: "Fornecedor",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Inscricao_estadual",
                table: "Fornecedor",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
