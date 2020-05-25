using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class historico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historico",
                columns: table => new
                {
                    Id_historico = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_inicio = table.Column<DateTime>(nullable: true),
                    Data_final = table.Column<DateTime>(nullable: true),
                    Produto_Id_produto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico", x => x.Id_historico);
                    table.ForeignKey(
                        name: "FK_Historico_Produto_Produto_Id_produto",
                        column: x => x.Produto_Id_produto,
                        principalTable: "Produto",
                        principalColumn: "Id_produto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historico_Produto_Id_produto",
                table: "Historico",
                column: "Produto_Id_produto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historico");
        }
    }
}
