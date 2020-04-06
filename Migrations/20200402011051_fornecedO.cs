using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class fornecedO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Fornecedor_Funcionario_Cpf",
                table: "Venda");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Funcionario_Funcionario_Cpf",
                table: "Venda",
                column: "Funcionario_Cpf",
                principalTable: "Funcionario",
                principalColumn: "Cpf",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Funcionario_Funcionario_Cpf",
                table: "Venda");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Fornecedor_Funcionario_Cpf",
                table: "Venda",
                column: "Funcionario_Cpf",
                principalTable: "Fornecedor",
                principalColumn: "Cnpj",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
