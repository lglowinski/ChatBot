using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatBot.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthorEmailToQUestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorEmail",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorEmail",
                table: "Questions");
        }
    }
}
