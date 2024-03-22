using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedHelpfulCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HelpfulCount",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_HelpfulCount",
                table: "Questions",
                column: "HelpfulCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_HelpfulCount",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "HelpfulCount",
                table: "Questions");
        }
    }
}
