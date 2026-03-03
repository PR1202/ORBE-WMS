using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORBE_WMS.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddItemEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItensEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmazemId = table.Column<int>(type: "int", nullable: false),
                    DepositanteId = table.Column<int>(type: "int", nullable: false),
                    CodigoProduto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    UnidadeMedida = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Localizacao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensEstoque_Armazens_ArmazemId",
                        column: x => x.ArmazemId,
                        principalTable: "Armazens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensEstoque_Depositantes_DepositanteId",
                        column: x => x.DepositanteId,
                        principalTable: "Depositantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensEstoque_ArmazemId",
                table: "ItensEstoque",
                column: "ArmazemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEstoque_CodigoProduto",
                table: "ItensEstoque",
                column: "CodigoProduto");

            migrationBuilder.CreateIndex(
                name: "IX_ItensEstoque_DepositanteId",
                table: "ItensEstoque",
                column: "DepositanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensEstoque");
        }
    }
}
