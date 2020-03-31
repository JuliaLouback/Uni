using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Cpf = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Endereco_Id_endereco = table.Column<int>(nullable: false),
                    Telefone_Id_telefone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Cpf);
                    table.ForeignKey(
                        name: "FK_Cliente_Endereco_Endereco_Id_endereco",
                        column: x => x.Endereco_Id_endereco,
                        principalTable: "Endereco",
                        principalColumn: "Id_endereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cliente_Telefone_Telefone_Id_telefone",
                        column: x => x.Telefone_Id_telefone,
                        principalTable: "Telefone",
                        principalColumn: "Id_telefone",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Endereco_Id_endereco",
                table: "Cliente",
                column: "Endereco_Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Telefone_Id_telefone",
                table: "Cliente",
                column: "Telefone_Id_telefone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
