using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostManAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileItems_ProfileItems_ProfileId",
                table: "ProfileItems");

            migrationBuilder.DropIndex(
                name: "IX_ProfileItems_ProfileId",
                table: "ProfileItems");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "ProfileItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "ProfileItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileItems_ProfileId",
                table: "ProfileItems",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileItems_ProfileItems_ProfileId",
                table: "ProfileItems",
                column: "ProfileId",
                principalTable: "ProfileItems",
                principalColumn: "Id");
        }
    }
}
