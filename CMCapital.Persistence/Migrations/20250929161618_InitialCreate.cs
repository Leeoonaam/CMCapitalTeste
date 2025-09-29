using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMCapital.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SaldoDisponivel = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    UsuarioIdInsert = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUpdate = table.Column<int>(type: "int", nullable: true),
                    UsuarioIdDelete = table.Column<int>(type: "int", nullable: true),
                    DTH_Insert = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DTH_Update = table.Column<DateTime>(type: "datetime", nullable: true),
                    DTH_Delete = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TblCliente_pk", x => x.ClienteId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TblProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdInsert = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUpdate = table.Column<int>(type: "int", nullable: true),
                    UsuarioIdDelete = table.Column<int>(type: "int", nullable: true),
                    DTH_Insert = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DTH_Update = table.Column<DateTime>(type: "datetime", nullable: true),
                    DTH_Delete = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TblProduto_pk", x => x.ProdutoId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TblUsuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Senha = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    UsuarioIdInsert = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUpdate = table.Column<int>(type: "int", nullable: true),
                    UsuarioIdDelete = table.Column<int>(type: "int", nullable: true),
                    DTH_Insert = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DTH_Update = table.Column<DateTime>(type: "datetime", nullable: true),
                    DTH_Delete = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TblUsuario_pk", x => x.UsuarioId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TblVenda",
                columns: table => new
                {
                    VendaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdInsert = table.Column<int>(type: "int", nullable: false),
                    UsuarioIdUpdate = table.Column<int>(type: "int", nullable: true),
                    UsuarioIdDelete = table.Column<int>(type: "int", nullable: true),
                    DTH_Insert = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DTH_Update = table.Column<DateTime>(type: "datetime", nullable: true),
                    DTH_Delete = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TblVenda_pk", x => x.VendaId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "TblVenda_TblCliente_ClienteId_fk",
                        column: x => x.ClienteId,
                        principalTable: "TblCliente",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "TblVenda_TblProduto_ProdutoId_fk",
                        column: x => x.ProdutoId,
                        principalTable: "TblProduto",
                        principalColumn: "ProdutoId");
                });

            migrationBuilder.CreateIndex(
                name: "TblCliente_ClienteId_index",
                table: "TblCliente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "TblProduto_ProdutoId_index",
                table: "TblProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "TblUsuario_UsuarioId_index",
                table: "TblUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "TblVenda_ClienteId_index",
                table: "TblVenda",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "TblVenda_ProdutoId_index",
                table: "TblVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "TblVenda_VendaId_index",
                table: "TblVenda",
                column: "VendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblUsuario");

            migrationBuilder.DropTable(
                name: "TblVenda");

            migrationBuilder.DropTable(
                name: "TblCliente");

            migrationBuilder.DropTable(
                name: "TblProduto");
        }
    }
}
