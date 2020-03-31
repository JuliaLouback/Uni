using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Endereco_EnderecoId_endereco",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Telefone_TelefoneId_telefone",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_EnderecoId_endereco",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_TelefoneId_telefone",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EnderecoId_endereco",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "TelefoneId_telefone",
                table: "Fornecedor");

            migrationBuilder.AddColumn<int>(
                name: "Endereco_Id_endereco",
                table: "Fornecedor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Telefone_Id_telefone",
                table: "Fornecedor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_Endereco_Id_endereco",
                table: "Fornecedor",
                column: "Endereco_Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_Telefone_Id_telefone",
                table: "Fornecedor",
                column: "Telefone_Id_telefone");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Endereco_Endereco_Id_endereco",
                table: "Fornecedor",
                column: "Endereco_Id_endereco",
                principalTable: "Endereco",
                principalColumn: "Id_endereco",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Telefone_Telefone_Id_telefone",
                table: "Fornecedor",
                column: "Telefone_Id_telefone",
                principalTable: "Telefone",
                principalColumn: "Id_telefone",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Endereco_Endereco_Id_endereco",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Telefone_Telefone_Id_telefone",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_Endereco_Id_endereco",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_Telefone_Id_telefone",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Endereco_Id_endereco",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Telefone_Id_telefone",
                table: "Fornecedor");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId_endereco",
                table: "Fornecedor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TelefoneId_telefone",
                table: "Fornecedor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EnderecoId_endereco",
                table: "Fornecedor",
                column: "EnderecoId_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_TelefoneId_telefone",
                table: "Fornecedor",
                column: "TelefoneId_telefone");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Endereco_EnderecoId_endereco",
                table: "Fornecedor",
                column: "EnderecoId_endereco",
                principalTable: "Endereco",
                principalColumn: "Id_endereco",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Telefone_TelefoneId_telefone",
                table: "Fornecedor",
                column: "TelefoneId_telefone",
                principalTable: "Telefone",
                principalColumn: "Id_telefone",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
