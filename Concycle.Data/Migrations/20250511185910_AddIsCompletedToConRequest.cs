using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concycle.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCompletedToConRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ConRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ConRequests");
        }
    }
}
