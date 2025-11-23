using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eagletechapi.Migrations
{
    /// <inheritdoc />
    public partial class setTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Chatbots_ChatbotId",
                table: "Chamados");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chatbots");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_ChatbotId",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "ChatbotId",
                table: "Chamados");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                newName: "IX_Usuarios_Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");

            migrationBuilder.AddColumn<long>(
                name: "ChatbotId",
                table: "Chamados",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chatbots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroChamado = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatbots", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChatbotId = table.Column<long>(type: "bigint", nullable: false),
                    MessageText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chatbots_ChatbotId",
                        column: x => x.ChatbotId,
                        principalTable: "Chatbots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_ChatbotId",
                table: "Chamados",
                column: "ChatbotId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatbotId",
                table: "Messages",
                column: "ChatbotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Chatbots_ChatbotId",
                table: "Chamados",
                column: "ChatbotId",
                principalTable: "Chatbots",
                principalColumn: "Id");
        }
    }
}
