using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class funcionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Fornecedor",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Cpf = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Cargo = table.Column<string>(maxLength: 45, nullable: false),
                    Data_nascimento = table.Column<DateTime>(nullable: false),
                    Endereco_Id_endereco = table.Column<int>(nullable: false),
                    Telefone_Id_telefone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Cpf);
                    table.ForeignKey(
                        name: "FK_Funcionario_Endereco_Endereco_Id_endereco",
                        column: x => x.Endereco_Id_endereco,
                        principalTable: "Endereco",
                        principalColumn: "Id_endereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionario_Telefone_Telefone_Id_telefone",
                        column: x => x.Telefone_Id_telefone,
                        principalTable: "Telefone",
                        principalColumn: "Id_telefone",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Endereco_Id_endereco",
                table: "Funcionario",
                column: "Endereco_Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Telefone_Id_telefone",
                table: "Funcionario",
                column: "Telefone_Id_telefone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Fornecedor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);
        }
    }
}
