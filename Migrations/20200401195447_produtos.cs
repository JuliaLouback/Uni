using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class produtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Fornecedor_Fornecedor_Cnpj",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_Fornecedor_Cnpj",
                table: "Produto");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produto",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produto",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "Fornecedor Cnpj",
                table: "Produto",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Fornecedor Cnpj",
                table: "Produto",
                column: "Fornecedor Cnpj");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Fornecedor_Fornecedor Cnpj",
                table: "Produto",
                column: "Fornecedor Cnpj",
                principalTable: "Fornecedor",
                principalColumn: "Cnpj",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Fornecedor_Fornecedor Cnpj",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_Fornecedor Cnpj",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Fornecedor Cnpj",
                table: "Produto");

            migrationBuilder.AlterColumn<long>(
                name: "Nome",
                table: "Produto",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<long>(
                name: "Descricao",
                table: "Produto",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Fornecedor_Cnpj",
                table: "Produto",
                column: "Fornecedor_Cnpj");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Fornecedor_Fornecedor_Cnpj",
                table: "Produto",
                column: "Fornecedor_Cnpj",
                principalTable: "Fornecedor",
                principalColumn: "Cnpj",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
