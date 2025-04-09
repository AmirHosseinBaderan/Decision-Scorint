using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "DecisionNodes");

            migrationBuilder.AddColumn<string>(
                name: "Formula",
                table: "DecisionNodes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Formula",
                table: "DecisionNodes");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "DecisionNodes",
                type: "int",
                nullable: true);
        }
    }
}
