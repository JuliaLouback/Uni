using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class ChangeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Telefones_TelefonesId_telefone",
                table: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_TelefonesId_telefone",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "TelefonesId_telefone",
                table: "Fornecedor");

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId_telefone",
                table: "Fornecedor",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id_telefone = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefones = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id_telefone);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_TelefoneId_telefone",
                table: "Fornecedor",
                column: "TelefoneId_telefone");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Telefone_TelefoneId_telefone",
                table: "Fornecedor",
                column: "TelefoneId_telefone",
                principalTable: "Telefone",
                principalColumn: "Id_telefone",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Telefone_TelefoneId_telefone",
                table: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_TelefoneId_telefone",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "TelefoneId_telefone",
                table: "Fornecedor");

            migrationBuilder.AddColumn<int>(
                name: "TelefonesId_telefone",
                table: "Fornecedor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    Id_telefone = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefone = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.Id_telefone);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_TelefonesId_telefone",
                table: "Fornecedor",
                column: "TelefonesId_telefone");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Telefones_TelefonesId_telefone",
                table: "Fornecedor",
                column: "TelefonesId_telefone",
                principalTable: "Telefones",
                principalColumn: "Id_telefone",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
