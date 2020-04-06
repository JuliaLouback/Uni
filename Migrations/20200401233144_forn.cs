using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class forn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id_venda = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_venda = table.Column<DateTime>(nullable: false),
                    Valor_frete = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Valor_total = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Funcionario_Cpf = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id_venda);
                    table.ForeignKey(
                        name: "FK_Venda_Fornecedor_Funcionario_Cpf",
                        column: x => x.Funcionario_Cpf,
                        principalTable: "Fornecedor",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venda_Funcionario_Cpf",
                table: "Venda",
                column: "Funcionario_Cpf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venda");
        }
    }
}
