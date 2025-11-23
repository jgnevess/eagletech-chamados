using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eagletechapi.Migrations
{
    /// <inheritdoc />
    public partial class updateChamados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Chatbots_ChatbotId",
                table: "Chamados");

            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuarios_TecnicoMatricula",
                table: "Chamados");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoMatricula",
                table: "Chamados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "ChatbotId",
                table: "Chamados",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Chatbots_ChatbotId",
                table: "Chamados",
                column: "ChatbotId",
                principalTable: "Chatbots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuarios_TecnicoMatricula",
                table: "Chamados",
                column: "TecnicoMatricula",
                principalTable: "Usuarios",
                principalColumn: "Matricula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Chatbots_ChatbotId",
                table: "Chamados");

            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Usuarios_TecnicoMatricula",
                table: "Chamados");

            migrationBuilder.AlterColumn<int>(
                name: "TecnicoMatricula",
                table: "Chamados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ChatbotId",
                table: "Chamados",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Chatbots_ChatbotId",
                table: "Chamados",
                column: "ChatbotId",
                principalTable: "Chatbots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Usuarios_TecnicoMatricula",
                table: "Chamados",
                column: "TecnicoMatricula",
                principalTable: "Usuarios",
                principalColumn: "Matricula",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
