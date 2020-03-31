using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id_endereco = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<long>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    Rua = table.Column<string>(maxLength: 60, nullable: true),
                    Bairro = table.Column<string>(maxLength: 45, nullable: true),
                    Cidade = table.Column<string>(maxLength: 45, nullable: true),
                    Estado = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id_endereco);
                });

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    Id_telefone = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefone = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.Id_telefone);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Cnpj = table.Column<long>(nullable: false),
                    NomeEmpresa = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(nullable: true),
                    InscricaoEstadual = table.Column<long>(nullable: false),
                    InscricaoMunicipal = table.Column<long>(nullable: false),
                    EnderecoId_endereco = table.Column<int>(nullable: true),
                    TelefonesId_telefone = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Cnpj);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Endereco_EnderecoId_endereco",
                        column: x => x.EnderecoId_endereco,
                        principalTable: "Endereco",
                        principalColumn: "Id_endereco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Telefones_TelefonesId_telefone",
                        column: x => x.TelefonesId_telefone,
                        principalTable: "Telefones",
                        principalColumn: "Id_telefone",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EnderecoId_endereco",
                table: "Fornecedor",
                column: "EnderecoId_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_TelefonesId_telefone",
                table: "Fornecedor",
                column: "TelefonesId_telefone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Telefones");
        }
    }
}
