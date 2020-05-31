using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class salario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salario",
                table: "Funcionario",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HistoricoSalario",
                columns: table => new
                {
                    Id_historicoSalario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_inicio = table.Column<DateTime>(nullable: true),
                    Data_final = table.Column<DateTime>(nullable: true),
                    Funcionario_Cpf = table.Column<string>(nullable: true),
                    Salario = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoSalario", x => x.Id_historicoSalario);
                    table.ForeignKey(
                        name: "FK_HistoricoSalario_Funcionario_Funcionario_Cpf",
                        column: x => x.Funcionario_Cpf,
                        principalTable: "Funcionario",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoSalario_Funcionario_Cpf",
                table: "HistoricoSalario",
                column: "Funcionario_Cpf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoSalario");

            migrationBuilder.DropColumn(
                name: "Salario",
                table: "Funcionario");
        }
    }
}
