using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class historicoStatus1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricoStatus",
                columns: table => new
                {
                    Id_historicoStatus = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_inicio = table.Column<DateTime>(nullable: true),
                    Data_final = table.Column<DateTime>(nullable: true),
                    Funcionario_Cpf = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoStatus", x => x.Id_historicoStatus);
                    table.ForeignKey(
                        name: "FK_HistoricoStatus_Funcionario_Funcionario_Cpf",
                        column: x => x.Funcionario_Cpf,
                        principalTable: "Funcionario",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoStatus_Funcionario_Cpf",
                table: "HistoricoStatus",
                column: "Funcionario_Cpf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoStatus");
        }
    }
}
