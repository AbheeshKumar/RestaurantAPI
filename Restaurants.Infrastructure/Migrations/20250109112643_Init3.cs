using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurents_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurents",
                table: "Restaurents");

            migrationBuilder.RenameTable(
                name: "Restaurents",
                newName: "Restuarants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restuarants",
                table: "Restuarants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restuarants_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restuarants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restuarants_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restuarants",
                table: "Restuarants");

            migrationBuilder.RenameTable(
                name: "Restuarants",
                newName: "Restaurents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurents",
                table: "Restaurents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurents_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
