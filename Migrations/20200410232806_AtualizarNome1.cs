using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class AtualizarNome1 : Migration
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
                    Rua = table.Column<string>(maxLength: 60, nullable: false),
                    Bairro = table.Column<string>(maxLength: 45, nullable: false),
                    Cidade = table.Column<string>(maxLength: 45, nullable: false),
                    Estado = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id_endereco);
                });

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Id_telefone = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefones = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Id_telefone);
                });

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

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Cnpj = table.Column<long>(nullable: false),
                    Nome_empresa = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    Inscricao_estadual = table.Column<long>(nullable: false),
                    Inscricao_municipal = table.Column<long>(nullable: false),
                    Endereco_Id_endereco = table.Column<int>(nullable: false),
                    Telefone_Id_telefone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Cnpj);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Endereco_Endereco_Id_endereco",
                        column: x => x.Endereco_Id_endereco,
                        principalTable: "Endereco",
                        principalColumn: "Id_endereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Telefone_Telefone_Id_telefone",
                        column: x => x.Telefone_Id_telefone,
                        principalTable: "Telefone",
                        principalColumn: "Id_telefone",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id_produto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    Valor_unitario = table.Column<string>(maxLength: 10, nullable: false),
                    Unidade_medida = table.Column<string>(maxLength: 60, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Estoque_minimo = table.Column<long>(nullable: false),
                    Estoque_maximo = table.Column<long>(nullable: false),
                    Fornecedor_Cnpj = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id_produto);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_Fornecedor_Cnpj",
                        column: x => x.Fornecedor_Cnpj,
                        principalTable: "Fornecedor",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id_venda = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_venda = table.Column<DateTime>(nullable: false),
                    Valor_total = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Funcionario_Cpf = table.Column<long>(nullable: false),
                    Cliente_Cpf = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id_venda);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_Cliente_Cpf",
                        column: x => x.Cliente_Cpf,
                        principalTable: "Cliente",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venda_Funcionario_Funcionario_Cpf",
                        column: x => x.Funcionario_Cpf,
                        principalTable: "Funcionario",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VendaProduto",
                columns: table => new
                {
                    Id_vendaProduto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Venda_Id_venda = table.Column<int>(nullable: false),
                    Produto_Id_produto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaProduto", x => x.Id_vendaProduto);
                    table.ForeignKey(
                        name: "FK_VendaProduto_Produto_Produto_Id_produto",
                        column: x => x.Produto_Id_produto,
                        principalTable: "Produto",
                        principalColumn: "Id_produto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendaProduto_Venda_Venda_Id_venda",
                        column: x => x.Venda_Id_venda,
                        principalTable: "Venda",
                        principalColumn: "Id_venda",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Endereco_Id_endereco",
                table: "Cliente",
                column: "Endereco_Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Telefone_Id_telefone",
                table: "Cliente",
                column: "Telefone_Id_telefone");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_Endereco_Id_endereco",
                table: "Fornecedor",
                column: "Endereco_Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_Telefone_Id_telefone",
                table: "Fornecedor",
                column: "Telefone_Id_telefone");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Endereco_Id_endereco",
                table: "Funcionario",
                column: "Endereco_Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Telefone_Id_telefone",
                table: "Funcionario",
                column: "Telefone_Id_telefone");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Fornecedor_Cnpj",
                table: "Produto",
                column: "Fornecedor_Cnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_Cliente_Cpf",
                table: "Venda",
                column: "Cliente_Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_Funcionario_Cpf",
                table: "Venda",
                column: "Funcionario_Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_Produto_Id_produto",
                table: "VendaProduto",
                column: "Produto_Id_produto");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_Venda_Id_venda",
                table: "VendaProduto",
                column: "Venda_Id_venda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaProduto");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Telefone");
        }
    }
}
