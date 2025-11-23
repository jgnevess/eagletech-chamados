using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eagletechapi.Migrations
{
    /// <inheritdoc />
    public partial class AddChatbotId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatbotId",
                table: "Messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatbotId",
                table: "Messages",
                column: "ChatbotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chatbots_ChatbotId",
                table: "Messages",
                column: "ChatbotId",
                principalTable: "Chatbots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chatbots_ChatbotId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatbotId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatbotId",
                table: "Messages");
        }
    }
}
