using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreeId = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    TrueBranchId = table.Column<int>(type: "int", nullable: true),
                    FalseBranchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecisionNodes_DecisionNodes_FalseBranchId",
                        column: x => x.FalseBranchId,
                        principalTable: "DecisionNodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DecisionNodes_DecisionNodes_TrueBranchId",
                        column: x => x.TrueBranchId,
                        principalTable: "DecisionNodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecisionNodes_FalseBranchId",
                table: "DecisionNodes",
                column: "FalseBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionNodes_TrueBranchId",
                table: "DecisionNodes",
                column: "TrueBranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecisionNodes");
        }
    }
}
