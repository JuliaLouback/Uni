using Microsoft.EntityFrameworkCore.Migrations;

namespace Uni.Migrations
{
    public partial class fornec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Produto_Id_produto",
                table: "Produto",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Venda_Produto",
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
                    table.PrimaryKey("PK_Venda_Produto", x => x.Id_vendaProduto);
                    table.ForeignKey(
                        name: "FK_Venda_Produto_Venda_Venda_Id_venda",
                        column: x => x.Venda_Id_venda,
                        principalTable: "Venda",
                        principalColumn: "Id_venda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Produto_Id_produto",
                table: "Produto",
                column: "Produto_Id_produto");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_Produto_Venda_Id_venda",
                table: "Venda_Produto",
                column: "Venda_Id_venda");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Venda_Produto_Produto_Id_produto",
                table: "Produto",
                column: "Produto_Id_produto",
                principalTable: "Venda_Produto",
                principalColumn: "Id_vendaProduto",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Venda_Produto_Produto_Id_produto",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Venda_Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_Produto_Id_produto",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Produto_Id_produto",
                table: "Produto");
        }
    }
}
