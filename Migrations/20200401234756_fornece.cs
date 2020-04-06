using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class fornece : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente_Venda",
                columns: table => new
                {
                    Id_clienteVenda = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente_Cpf = table.Column<long>(nullable: false),
                    Venda_Id_venda = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente_Venda", x => x.Id_clienteVenda);
                    table.ForeignKey(
                        name: "FK_Cliente_Venda_Cliente_Cliente_Cpf",
                        column: x => x.Cliente_Cpf,
                        principalTable: "Cliente",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cliente_Venda_Venda_Venda_Id_venda",
                        column: x => x.Venda_Id_venda,
                        principalTable: "Venda",
                        principalColumn: "Id_venda",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Venda_Cliente_Cpf",
                table: "Cliente_Venda",
                column: "Cliente_Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Venda_Venda_Id_venda",
                table: "Cliente_Venda",
                column: "Venda_Id_venda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente_Venda");
        }
    }
}
