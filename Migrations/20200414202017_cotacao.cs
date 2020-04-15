using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class cotacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeRole",
                table: "CriarRole",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cotacao",
                columns: table => new
                {
                    Id_cotacao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_venda = table.Column<DateTime>(nullable: false),
                    Valor_total = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Funcionario_Cpf = table.Column<long>(nullable: false),
                    Cliente_Cpf = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotacao", x => x.Id_cotacao);
                    table.ForeignKey(
                        name: "FK_Cotacao_Cliente_Cliente_Cpf",
                        column: x => x.Cliente_Cpf,
                        principalTable: "Cliente",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cotacao_Funcionario_Funcionario_Cpf",
                        column: x => x.Funcionario_Cpf,
                        principalTable: "Funcionario",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotacao_Cliente_Cpf",
                table: "Cotacao",
                column: "Cliente_Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Cotacao_Funcionario_Cpf",
                table: "Cotacao",
                column: "Funcionario_Cpf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotacao");

            migrationBuilder.AlterColumn<string>(
                name: "NomeRole",
                table: "CriarRole",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}