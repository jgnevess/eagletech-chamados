using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eagletechapi.Migrations
{
    /// <inheritdoc />
    public partial class CricaoEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Abertura",
                table: "Chamados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fechamento",
                table: "Chamados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Prioridade",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SolicitanteMatricula",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TecnicoMatricula",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCategoria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Funcao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeFuncao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Matricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCompleto = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FuncaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Matricula);
                    table.ForeignKey(
                        name: "FK_Usuario_Funcao_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_CategoriaId",
                table: "Chamados",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_SolicitanteMatricula",
                table: "Chamados",
                column: "SolicitanteMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_TecnicoMatricula",
                table: "Chamados",
                column: "TecnicoMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_FuncaoId",
                table: "Usuario",
                column: "FuncaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Categoria_CategoriaId",
                table: "Chamados",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuario_SolicitanteMatricula",
                table: "Chamados",
                column: "SolicitanteMatricula",
                principalTable: "Usuario",
                principalColumn: "Matricula",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuario_TecnicoMatricula",
                table: "Chamados",
                column: "TecnicoMatricula",
                principalTable: "Usuario",
                principalColumn: "Matricula",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Categoria_CategoriaId",
                table: "Chamados");

            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuario_SolicitanteMatricula",
                table: "Chamados");

            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuario_TecnicoMatricula",
                table: "Chamados");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Funcao");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_CategoriaId",
                table: "Chamados");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_SolicitanteMatricula",
                table: "Chamados");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_TecnicoMatricula",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "Abertura",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "Fechamento",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "SolicitanteMatricula",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "TecnicoMatricula",
                table: "Chamados");
        }
    }
}
