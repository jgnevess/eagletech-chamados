using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eagletechapi.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "firstLogin",
                table: "Usuarios",
                newName: "FirstLogin");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuarios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "FirstLogin",
                table: "Usuarios",
                newName: "firstLogin");
        }
    }
}
